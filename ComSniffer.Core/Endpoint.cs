using System.IO.Ports;
using System.Net;
using System.Net.Sockets;

namespace ComSniffer;

public enum EndpointKind { Serial, TcpConnect, TcpListen }

/// <summary>
/// Description d'une extremite de la connexion sniffee.
/// Syntaxes acceptees :
///   COM5 | \\.\COM15        -> port serie local
///   tcp:192.168.1.10:4001   -> connexion TCP sortante vers host:port
///   listen:5000             -> ecoute TCP sur toutes les interfaces
///   listen:127.0.0.1:5000   -> ecoute TCP sur une interface precise
/// </summary>
public readonly record struct EndpointSpec(EndpointKind Kind, string Host, int Port)
{
    public static EndpointSpec Parse(string spec)
    {
        if (string.IsNullOrWhiteSpace(spec))
            throw new OptionException("Nom d'endpoint vide.");

        if (spec.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase))
        {
            string rest = spec[4..];
            int sep = rest.LastIndexOf(':');
            if (sep <= 0 || sep == rest.Length - 1)
                throw new OptionException($"Endpoint TCP invalide : '{spec}' (attendu : tcp:hote:port)");
            return new EndpointSpec(EndpointKind.TcpConnect, rest[..sep], ParsePort(rest[(sep + 1)..], spec));
        }

        if (spec.StartsWith("listen:", StringComparison.OrdinalIgnoreCase))
        {
            string rest = spec[7..];
            int sep = rest.LastIndexOf(':');
            string host = "", portStr = rest;
            if (sep >= 0) { host = rest[..sep]; portStr = rest[(sep + 1)..]; }
            if (host.Length > 0 && !IPAddress.TryParse(host, out _))
                throw new OptionException($"Adresse d'ecoute invalide : '{host}' (IP attendue)");
            return new EndpointSpec(EndpointKind.TcpListen, host, ParsePort(portStr, spec));
        }

        return new EndpointSpec(EndpointKind.Serial, spec, 0);
    }

    private static int ParsePort(string s, string spec) =>
        int.TryParse(s, out int p) && p is > 0 and <= 65535
            ? p
            : throw new OptionException($"Port TCP invalide dans '{spec}'");

    public string Describe() => Kind switch
    {
        EndpointKind.Serial => Host,
        EndpointKind.TcpConnect => $"tcp://{Host}:{Port}",
        _ => $"listen:{(Host.Length > 0 ? Host : "*")}:{Port}",
    };
}

/// <summary>Une extremite ouvrable exposant un Stream.</summary>
internal interface IEndpoint : IDisposable
{
    string Description { get; }
    Stream Stream { get; }
    /// <summary>True si l'ouverture attend qu'un pair se connecte (TCP listen).</summary>
    bool WaitsForPeer { get; }
    Task OpenAsync(CancellationToken ct);
}

internal sealed partial class SerialEndpoint(EndpointSpec spec, SnifferOptions opts) : IEndpoint
{
    private SerialPort? _port;

    public string Description => spec.Describe();
    public bool WaitsForPeer => false;
    public Stream Stream => _port?.BaseStream
        ?? throw new InvalidOperationException("Port non ouvert.");

    public Task OpenAsync(CancellationToken ct)
    {
        string name = spec.Host;
        // la verification prealable n'a de sens que pour les noms COMx simples
        if (MyRegex().IsMatch(name)
            && !SerialPort.GetPortNames().Contains(name, StringComparer.OrdinalIgnoreCase))
        {
            string[] found = SerialPort.GetPortNames();
            throw new IOException($"le port '{name}' n'existe pas. Ports detectes : " +
                                  (found.Length > 0 ? string.Join(", ", found) : "aucun"));
        }
        _port = new SerialPort(name, opts.BaudRate, opts.Parity, opts.DataBits, opts.StopBits)
        {
            Handshake = opts.Handshake,
            ReadBufferSize = 8192,
            WriteBufferSize = 8192,
        };
        _port.Open();
        return Task.CompletedTask;
    }

    public void Dispose() => _port?.Dispose();
    [System.Text.RegularExpressions.GeneratedRegex(@"^COM\d+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase, "fr-FR")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}

internal sealed class TcpConnectEndpoint(EndpointSpec spec) : IEndpoint
{
    private TcpClient? _client;

    public string Description => spec.Describe();
    public bool WaitsForPeer => false;
    public Stream Stream => _client?.GetStream()
        ?? throw new InvalidOperationException("Client non connecte.");

    public async Task OpenAsync(CancellationToken ct)
    {
        _client = new TcpClient();
        await _client.ConnectAsync(spec.Host, spec.Port, ct);
    }

    public void Dispose() => _client?.Dispose();
}

internal sealed class TcpListenEndpoint(EndpointSpec spec) : IEndpoint
{
    private TcpListener? _listener;
    private TcpClient? _client;

    public string Description => spec.Describe();
    public bool WaitsForPeer => true;
    public Stream Stream => _client?.GetStream()
        ?? throw new InvalidOperationException("Aucun client connecte.");

    public async Task OpenAsync(CancellationToken ct)
    {
        IPAddress addr = spec.Host.Length > 0 ? IPAddress.Parse(spec.Host) : IPAddress.Any;
        _listener = new TcpListener(addr, spec.Port);
        _listener.Start();
        _client = await _listener.AcceptTcpClientAsync(ct);
        _listener.Stop();   // une seule connexion suffit
    }

    public void Dispose()
    {
        _client?.Dispose();
        _listener?.Stop();
    }
}

internal static class EndpointFactory
{
    public static IEndpoint Create(string spec, SnifferOptions opts) =>
        EndpointSpec.Parse(spec) switch
        {
            { Kind: EndpointKind.Serial } s => new SerialEndpoint(s, opts),
            { Kind: EndpointKind.TcpConnect } s => new TcpConnectEndpoint(s),
            { Kind: EndpointKind.TcpListen } s => new TcpListenEndpoint(s),
            var s => throw new OptionException($"Endpoint inconnu : {s}"),
        };
}
