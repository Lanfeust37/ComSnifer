using System.IO.Ports;
using System.Text;
using System.Threading.Channels;

namespace ComSnifer;

/// <summary>Un paquet intercepte a journaliser.</summary>
internal readonly record struct SniffEvent(DateTimeOffset Time, bool FromDevice, byte[] Data);

/// <summary>
/// Moteur de forwarding bidirectionnel entre le port cote application
/// (extremite d'une paire virtuelle com0com, ou second port physique)
/// et le port du peripherique reel.
///
/// Corrige par rapport a la version C :
///  - pas de fork/pipes : un Channel thread-safe alimente le logger ;
///  - les tee files recoivent les octets bruts AVANT tout formatage
///    (bug : -b ecrasait le buffer avec "Total bytes transmitted:") ;
///  - WriteAsync ecrit tout ou leve une exception (pas de write() partiel
///    silencieux sur fd non-bloquant) ;
///  - arret propre via CancellationToken au lieu de handlers de signaux ;
///  - SerialPort.Open() est exclusif : les lock files UUCP deviennent inutiles.
/// </summary>
public sealed class SnifferEngine : IAsyncDisposable
{
    private const int BufferSize = 4096;

    private readonly SnifferOptions _opts;
    private readonly List<FileStream> _inTees = new();
    private readonly List<FileStream> _outTees = new();
    private readonly Channel<SniffEvent> _events = Channel.CreateUnbounded<SniffEvent>();
    private readonly CancellationTokenSource _stop = new();
    private IEndpoint? _app;
    private IEndpoint? _dev;
    private StreamWriter? _logFile;

    public SnifferEngine(SnifferOptions opts) => _opts = opts;

    private bool UseColor => _logFile is null && !Console.IsOutputRedirected;

    public async Task<int> RunAsync(CancellationToken ct)
    {
        try { OpenOutputs(); }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erreur ouverture des fichiers : {ex.Message}");
            return 2;
        }

        try
        {
            _dev = EndpointFactory.Create(_opts.DevicePort!, _opts);
            _app = EndpointFactory.Create(_opts.AppPort!, _opts);
            await OpenEndpointAsync(_dev, ct);
            await OpenEndpointAsync(_app, ct);
        }
        catch (OperationCanceledException)
        {
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erreur ouverture endpoint : {ex.Message}");
            Console.Error.WriteLine("(un port serie ne peut etre ouvert que par un seul processus a la fois)");
            return 2;
        }

        Console.WriteLine($"Device : {_dev.Description}   <--->   App : {_app.Description}");
        Console.WriteLine($"{_opts.BaudRate} baud, {_opts.DataBits}{_opts.Parity.ToString()[0]}{FormatStopBits()} (ports serie) — Ctrl+C pour arreter.");

        using CancellationTokenSource linked = CancellationTokenSource.CreateLinkedTokenSource(ct, _stop.Token);
        CancellationToken token = linked.Token;

        Task hostToDev = ForwardAsync(_app.Stream, _app.Description, _dev.Stream, fromDevice: false, _outTees, token);
        Task devToHost = ForwardAsync(_dev.Stream, _dev.Description, _app.Stream, fromDevice: true, _inTees, token);
        Task logger = LoggerAsync();

        try { await Task.WhenAll(hostToDev, devToHost); }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Console.Error.WriteLine($"\nErreur : {ex.Message}"); }

        _events.Writer.TryComplete();
        try { await logger; } catch (OperationCanceledException) { }
        return 0;
    }

    private static async Task OpenEndpointAsync(IEndpoint ep, CancellationToken ct)
    {
        if (ep.WaitsForPeer)
            Console.WriteLine($"En attente d'une connexion sur {ep.Description} ...");
        await ep.OpenAsync(ct);
        Console.WriteLine($"Ouvert : {ep.Description}");
    }

    private string FormatStopBits() => _opts.StopBits switch
    {
        StopBits.One => "1",
        StopBits.OnePointFive => "1.5",
        StopBits.Two => "2",
        _ => "1",
    };

    private void OpenOutputs()
    {
        if (_opts.LogFile is not null)
        {
            _logFile = new StreamWriter(new FileStream(
                _opts.LogFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            { AutoFlush = true };
            Console.WriteLine($"Log vers '{_opts.LogFile}'.");
        }
        foreach (string path in _opts.InTeeFiles) _inTees.Add(OpenTee(path, "device"));
        foreach (string path in _opts.OutTeeFiles) _outTees.Add(OpenTee(path, "host"));
    }

    private static FileStream OpenTee(string path, string what)
    {
        var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read,
                                bufferSize: 1, useAsync: true);
        Console.WriteLine($"Raw data ({what}) -> '{path}'.");
        return fs;
    }

    /// <summary>Lit src, re-ecrit tout vers dst, copie brute vers les tee et le logger.</summary>
    private async Task ForwardAsync(Stream src, string srcName, Stream dst, bool fromDevice,
                                    List<FileStream> tees, CancellationToken ct)
    {
        var buffer = new byte[BufferSize];
        try
        {
            while (true)
            {
                int n = await src.ReadAsync(buffer, ct);
                if (n == 0) break;
                var chunk = buffer.AsMemory(0, n);

                await dst.WriteAsync(chunk, ct);
                foreach (FileStream tee in tees)
                {
                    await tee.WriteAsync(chunk, ct);
                    await tee.FlushAsync(ct);
                }
                _events.Writer.TryWrite(new SniffEvent(DateTimeOffset.Now, fromDevice, chunk.ToArray()));
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception) when (ct.IsCancellationRequested || _stop.IsCancellationRequested) { }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"\nErreur de lecture sur {srcName} : {ex.Message}");
            _stop.Cancel();
        }
    }

    /// <summary>Consommateur unique du canal : formate et ecrit vers console/fichier.</summary>
    private async Task LoggerAsync()
    {
        var sb = new StringBuilder(512);
        TextWriter output = _logFile ?? Console.Out;

        await foreach (SniffEvent ev in _events.Reader.ReadAllAsync())
        {
            sb.Clear();
            if (_opts.ShowTimestamp)
            {
                sb.Append('\n');
                Colorize(sb, _opts.TimeColor);
                sb.Append(ev.Time.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
            sb.Append('\n');
            Colorize(sb, _opts.Color);
            sb.Append(ev.FromDevice ? "Device --> " : "Host   --> ");
            sb.Append(DataFormatter.Format(ev.Data, _opts.Hex));
            if (_opts.ShowBytes)
            {
                sb.Append('\n');
                Colorize(sb, _opts.BytesColor);
                sb.Append("Total bytes transmitted: ").Append(ev.Data.Length);
            }
            if (UseColor) sb.Append(AnsiColors.Reset);
            await output.WriteAsync(sb.ToString());
        }
    }

    private void Colorize(StringBuilder sb, string colorName)
    {
        if (UseColor && AnsiColors.Get(colorName) is string code)
            sb.Append(code);
    }

    public async ValueTask DisposeAsync()
    {
        _stop.Cancel();
        _app?.Dispose();   // interrompt les ReadAsync pendants
        _dev?.Dispose();
        if (_logFile is not null) await _logFile.DisposeAsync();
        foreach (FileStream fs in _inTees) await fs.DisposeAsync();
        foreach (FileStream fs in _outTees) await fs.DisposeAsync();
        _stop.Dispose();
    }
}
