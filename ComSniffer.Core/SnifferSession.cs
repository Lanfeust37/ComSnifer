namespace ComSniffer;

/// <summary>
/// Orchestrateur de session de capture, partage entre ComSniffer.Gui et les
/// hotes (plugin ToolsBox) : encapsule SnifferEngine, le jeton d'annulation,
/// les compteurs d'octets, le dernier message d'erreur et les messages de
/// statut normalises (demarrage, arret en cours, arrete / erreur + code).
///
/// Threading : StartAsync est concu pour etre await depuis le thread UI
/// (les continuations reviennent sur le contexte capture, donc StatusChanged
/// et RunningChanged arrivent sur le thread UI). PacketCaptured est en revanche
/// emis depuis la tache de log du moteur : le consommateur reste responsable
/// du marshalling si besoin.
/// </summary>
public sealed class SnifferSession : IAsyncDisposable
{
    private SnifferEngine? _engine;
    private CancellationTokenSource? _cts;

    /// <summary>True tant que le moteur tourne.</summary>
    public bool IsRunning { get; private set; }

    /// <summary>Dernier message d'erreur (repris dans le message final).</summary>
    public string? LastError { get; private set; }

    /// <summary>Octets cumules device -&gt; host.</summary>
    public long TotalIn { get; private set; }

    /// <summary>Octets cumules host -&gt; device.</summary>
    public long TotalOut { get; private set; }

    /// <summary>Message de statut destine a l'utilisateur.</summary>
    public readonly record struct StatusMessage(string Text, bool IsError);

    /// <summary>Chaque paquet intercepte (tache de log du moteur).</summary>
    public event EventHandler<SniffEvent>? PacketCaptured;

    /// <summary>Messages de statut / d'erreur.</summary>
    public event EventHandler<StatusMessage>? StatusChanged;

    /// <summary>Transition de <see cref="IsRunning"/> (debut effectif / arret complet).</summary>
    public event EventHandler? RunningChanged;

    /// <summary>
    /// Valide les options puis lance le moteur ; rend la main a l'arret
    /// (<see cref="Stop"/>, deconnexion) ou sur erreur. No-op si deja lance.
    /// </summary>
    public async Task StartAsync(SnifferOptions opts)
    {
        if (IsRunning) return;
        try { opts.Validate(); }
        catch (Exception ex)
        {
            Report(ex.Message, isError: true);
            return;
        }

        _cts = new CancellationTokenSource();
        _engine = new SnifferEngine(opts) { UseConsole = false };
        _engine.PacketCaptured += OnEnginePacket;
        _engine.StatusChanged += (_, m) => Report(m, isError: false);
        _engine.ErrorOccurred += (_, m) => Report(m, isError: true);

        TotalIn = TotalOut = 0;
        LastError = null;
        IsRunning = true;
        RunningChanged?.Invoke(this, EventArgs.Empty);
        Report($"{opts.DevicePort} ↔ {opts.AppPort} — démarrage…", isError: false);

        int rc = 0;
        try { rc = await _engine.RunAsync(_cts.Token); }
        catch (Exception ex)
        {
            rc = 2;
            Report($"Erreur moteur : {ex.Message}", isError: true);
        }
        finally
        {
            await _engine.DisposeAsync();
            _engine = null;
            _cts.Dispose();
            _cts = null;
            IsRunning = false;
            RunningChanged?.Invoke(this, EventArgs.Empty);
            Report(rc == 0 ? "Arrêté."
                : LastError is string err ? $"{err} (code {rc})"
                : $"Terminé (code {rc}).", isError: rc != 0);
        }
    }

    /// <summary>Demande l'arret ; l'arret effectif est signale via RunningChanged.</summary>
    public void Stop()
    {
        if (_cts is null) return;
        Report("Arrêt en cours…", isError: false);
        _cts.Cancel();
    }

    private void OnEnginePacket(object? sender, SniffEvent ev)
    {
        if (ev.FromDevice) TotalIn += ev.Data.Length;
        else TotalOut += ev.Data.Length;
        PacketCaptured?.Invoke(this, ev);
    }

    private void Report(string text, bool isError)
    {
        if (isError) LastError = text;
        StatusChanged?.Invoke(this, new StatusMessage(text, isError));
    }

    public async ValueTask DisposeAsync()
    {
        _cts?.Cancel();
        if (_engine is not null) await _engine.DisposeAsync();
        _cts?.Dispose();
    }
}
