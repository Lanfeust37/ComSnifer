using System.IO.Ports;
using System.Text;
using ComSnifer;

namespace ComSnifer.Gui;

/// <summary>
/// Fenetre principale : configuration des endpoints et des parametres serie,
/// affichage du trafic en deux panneaux (Device→Host / Host→Device),
/// compteurs de debit, fichiers log/tee.
/// La partie visuelle est generee dans MainForm.Designer.cs
/// (compatible avec le Concepteur Windows Forms de Visual Studio).
/// </summary>
public partial class MainForm : Form
{
    private const int MaxTextChars = 500_000;

    private SnifferEngine? _engine;
    private CancellationTokenSource? _cts;
    private bool _running;

    private readonly object _pendingLock = new();
    private readonly StringBuilder _pendingDev = new();
    private readonly StringBuilder _pendingApp = new();
    private long _bytesDev, _bytesApp;
    private long _prevDev, _prevApp;
    private long _tickPrev;

    private readonly string _settingsPath = GuiSettings.DefaultPath;

    public MainForm()
    {
        InitializeComponent();
        LoadSettings();
        RefreshPorts();
    }

    // -------------------------------------------------------- evenements UI

    private async void btnStart_Click(object? sender, EventArgs e) => await StartAsync();

    private void btnStop_Click(object? sender, EventArgs e)
    {
        if (_cts is null) return;
        SetStatus("Arrêt en cours…", isError: false);
        _cts.Cancel();
    }

    private void btnRefreshPorts_Click(object? sender, EventArgs e) => RefreshPorts();

    private void btnClear_Click(object? sender, EventArgs e)
    {
        rtbDev.Clear();
        rtbApp.Clear();
    }

    private void uiTimer_Tick(object? sender, EventArgs e) => OnUiTick();

    private void btnLogBrowse_Click(object? sender, EventArgs e) => BrowseFile(txtLog);
    private void btnTeeInBrowse_Click(object? sender, EventArgs e) => BrowseFile(txtTeeIn);
    private void btnTeeOutBrowse_Click(object? sender, EventArgs e) => BrowseFile(txtTeeOut);

    private void miDevCopy_Click(object? sender, EventArgs e)
    {
        if (rtbDev.SelectionLength > 0) rtbDev.Copy();
    }

    private void miDevSelectAll_Click(object? sender, EventArgs e) => rtbDev.SelectAll();
    private void miDevClear_Click(object? sender, EventArgs e) => rtbDev.Clear();

    private void miAppCopy_Click(object? sender, EventArgs e)
    {
        if (rtbApp.SelectionLength > 0) rtbApp.Copy();
    }

    private void miAppSelectAll_Click(object? sender, EventArgs e) => rtbApp.SelectAll();
    private void miAppClear_Click(object? sender, EventArgs e) => rtbApp.Clear();

    private static void BrowseFile(TextBox tb)
    {
        using var dlg = new SaveFileDialog
        {
            Filter = "Tous les fichiers (*.*)|*.*",
            FileName = tb.Text,
        };
        if (dlg.ShowDialog() == DialogResult.OK) tb.Text = dlg.FileName;
    }

    private void RefreshPorts()
    {
        string[] ports = SerialPort.GetPortNames();
        Array.Sort(ports, StringComparer.OrdinalIgnoreCase);
        FillEndpoint(cmbDevice, ports, "tcp:127.0.0.1:4001");
        FillEndpoint(cmbApp, ports, "listen:5000");
    }

    private static void FillEndpoint(ComboBox cb, string[] ports, string extra)
    {
        string current = cb.Text;
        cb.Items.Clear();
        cb.Items.AddRange(ports.Cast<object>().ToArray());
        cb.Items.Add(extra);
        cb.Text = current;
    }

    // -------------------------------------------------------------- moteur

    private async Task StartAsync()
    {
        SnifferOptions opts;
        try
        {
            opts = BuildOptions();
            opts.Validate();
        }
        catch (Exception ex)
        {
            SetStatus(ex.Message, isError: true);
            return;
        }

        _cts = new CancellationTokenSource();
        _engine = new SnifferEngine(opts) { UseConsole = false };
        _engine.PacketCaptured += OnPacket;
        _engine.StatusChanged += (_, m) => SetStatus(m, isError: false);
        _engine.ErrorOccurred += (_, m) => SetStatus(m, isError: true);

        SaveSettings();
        ResetStats();
        SetRunning(true);
        SetStatus($"{cmbDevice.Text} ↔ {cmbApp.Text} — démarrage…", isError: false);
        _tickPrev = Environment.TickCount64;
        uiTimer.Start();

        int rc = 0;
        try { rc = await _engine.RunAsync(_cts.Token); }
        catch (Exception ex)
        {
            rc = 2;
            SetStatus($"Erreur moteur : {ex.Message}", isError: true);
        }
        finally
        {
            await _engine.DisposeAsync();
            _engine = null;
            _cts.Dispose();
            _cts = null;
            uiTimer.Stop();
            if (!IsDisposed)
            {
                FlushPending();
                SetRunning(false);
                SetStatus(rc == 0 ? "Arrêté." : $"Terminé (code {rc}).", isError: rc != 0);
            }
        }
    }

    private SnifferOptions BuildOptions()
    {
        var opts = new SnifferOptions
        {
            DevicePort = cmbDevice.Text.Trim(),
            AppPort = cmbApp.Text.Trim(),
            BaudRate = int.TryParse(cmbBaud.Text.Trim(), out int b)
                ? b : throw new OptionException($"Baudrate invalide : '{cmbBaud.Text}'"),
            DataBits = (int)(cmbDataBits.SelectedItem ?? 8),
            Parity = Enum.TryParse<Parity>(cmbParity.Text, true, out Parity p)
                ? p : Parity.None,
            StopBits = cmbStopBits.Text switch
            {
                "1.5" => StopBits.OnePointFive,
                "2" => StopBits.Two,
                _ => StopBits.One,
            },
            Handshake = Enum.TryParse<Handshake>(cmbHandshake.Text, true, out Handshake h)
                ? h : Handshake.None,
            Hex = chkHex.Checked,
            ShowTimestamp = chkTimestamp.Checked,
            LogFile = string.IsNullOrWhiteSpace(txtLog.Text) ? null : txtLog.Text.Trim(),
        };
        if (!string.IsNullOrWhiteSpace(txtTeeIn.Text)) opts.InTeeFiles.Add(txtTeeIn.Text.Trim());
        if (!string.IsNullOrWhiteSpace(txtTeeOut.Text)) opts.OutTeeFiles.Add(txtTeeOut.Text.Trim());
        return opts;
    }

    private void OnPacket(object? sender, SniffEvent ev)
    {
        if (IsDisposed) return;
        if (ev.FromDevice) _bytesDev += ev.Data.Length;
        else _bytesApp += ev.Data.Length;
        if (chkPause.Checked) return;

        string line = DisplayFormatter.FormatLine(
            ev.Data, chkHex.Checked, chkAscii.Checked,
            chkTimestamp.Checked ? ev.Time : null);
        lock (_pendingLock)
            (ev.FromDevice ? _pendingDev : _pendingApp).Append(line);
    }

    private void OnUiTick()
    {
        FlushPending();
        long now = Environment.TickCount64;
        double dt = (now - _tickPrev) / 1000.0;
        if (dt > 0)
        {
            lblRateDev.Text = $"↓ {DisplayFormatter.FormatRate((_bytesDev - _prevDev) / dt)}";
            lblRateApp.Text = $"↑ {DisplayFormatter.FormatRate((_bytesApp - _prevApp) / dt)}";
            _prevDev = _bytesDev; _prevApp = _bytesApp; _tickPrev = now;
        }
        gbDev.Text = $"Device → Host   ({_bytesDev:N0} o)";
        gbApp.Text = $"Host → Device   ({_bytesApp:N0} o)";
    }

    private void FlushPending()
    {
        string dev, app;
        lock (_pendingLock)
        {
            dev = _pendingDev.ToString(); _pendingDev.Clear();
            app = _pendingApp.ToString(); _pendingApp.Clear();
        }
        if (dev.Length > 0) AppendCapped(rtbDev, dev);
        if (app.Length > 0) AppendCapped(rtbApp, app);
    }

    /// <summary>Ajoute du texte et tronque le debut si la zone devient trop grosse.</summary>
    private static void AppendCapped(RichTextBox rtb, string text)
    {
        rtb.AppendText(text);
        if (rtb.TextLength <= MaxTextChars) return;
        int cut = rtb.TextLength - MaxTextChars;
        int nl = rtb.Text.IndexOf('\n', cut);          // coupe sur une fin de ligne
        rtb.Select(0, nl >= 0 ? nl + 1 : cut);
        rtb.SelectedText = "";
        rtb.SelectionStart = rtb.TextLength;
        rtb.ScrollToCaret();
    }

    // ------------------------------------------------------------- divers

    private static bool IsDarkMode()
    {
#pragma warning disable SYSLIB5002  // IsDarkModeEnabled : API experimentale (.NET 9/10)
        return Application.IsDarkModeEnabled;
#pragma warning restore SYSLIB5002
    }

    private void SetRunning(bool running)
    {
        _running = running;
        rowEndpoints.Enabled = !running;
        foreach (Control c in rowParams.Controls)
            if (c is not Button)
                c.Enabled = !running;
        btnStart.Enabled = !running;
        btnStop.Enabled = running;
        flpFiles.Enabled = !running;
        lblState.Text = running ? "● Connecté" : "○ Arrêté";
        lblState.ForeColor = running
            ? (IsDarkMode() ? Color.MediumSeaGreen : Color.SeaGreen)
            : SystemColors.GrayText;
    }

    private void SetStatus(string message, bool isError)
    {
        if (InvokeRequired) { BeginInvoke(() => SetStatus(message, isError)); return; }
        lblMsg.Text = message;
        lblMsg.ForeColor = isError
            ? (IsDarkMode() ? Color.Salmon : Color.Firebrick)
            : SystemColors.ControlText;
        if (isError) lblState.ForeColor = IsDarkMode() ? Color.Salmon : Color.Firebrick;
    }

    private void ResetStats()
    {
        _bytesDev = _bytesApp = _prevDev = _prevApp = 0;
        lock (_pendingLock) { _pendingDev.Clear(); _pendingApp.Clear(); }
        chkPause.Checked = false;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _cts?.Cancel();               // arret en tache de fond ; le finally nettoie
        SaveSettings();
        base.OnFormClosing(e);
    }

    // ---------------------------------------------------------- settings

    private void LoadSettings()
    {
        GuiSettings s = GuiSettings.Load(_settingsPath);
        cmbDevice.Text = s.Device;
        cmbApp.Text = s.App;
        cmbBaud.Text = s.BaudRate.ToString();
        cmbDataBits.SelectedItem = s.DataBits;
        SelectByText(cmbParity, s.Parity);
        SelectByText(cmbStopBits, s.StopBits);
        SelectByText(cmbHandshake, s.Handshake);
        chkHex.Checked = s.Hex;
        chkAscii.Checked = s.Ascii;
        chkTimestamp.Checked = s.Timestamp;
        txtLog.Text = s.LogFile;
        txtTeeIn.Text = s.TeeIn;
        txtTeeOut.Text = s.TeeOut;
    }

    private static void SelectByText(ComboBox cb, string value)
    {
        int i = cb.Items.IndexOf(value);
        if (i >= 0) cb.SelectedIndex = i;
    }

    private void SaveSettings()
    {
        new GuiSettings
        {
            Device = cmbDevice.Text.Trim(),
            App = cmbApp.Text.Trim(),
            BaudRate = int.TryParse(cmbBaud.Text.Trim(), out int b) ? b : 9600,
            DataBits = (int)(cmbDataBits.SelectedItem ?? 8),
            Parity = cmbParity.Text,
            StopBits = cmbStopBits.Text,
            Handshake = cmbHandshake.Text,
            Hex = chkHex.Checked,
            Ascii = chkAscii.Checked,
            Timestamp = chkTimestamp.Checked,
            LogFile = txtLog.Text.Trim(),
            TeeIn = txtTeeIn.Text.Trim(),
            TeeOut = txtTeeOut.Text.Trim(),
        }.Save(_settingsPath);
    }
}
