using System.IO.Ports;
using System.Text;

namespace ComSniffer.Gui;

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

    private readonly SnifferSession _session = new();

    private readonly Lock _pendingLock = new();
    private readonly StringBuilder _pendingDev = new();
    private readonly StringBuilder _pendingApp = new();
    private long _prevDev, _prevApp;
    private long _tickPrev;

    private readonly string _settingsPath = GuiSettings.DefaultPath;
    private GuiSettings _settings = new();

    public MainForm()
    {
        InitializeComponent();
        _session.PacketCaptured += OnPacket;
        _session.StatusChanged += (_, m) => { if (!IsDisposed) SetStatus(m.Text, m.IsError); };
        _session.RunningChanged += OnSessionRunningChanged;
        LoadSettings();
        RefreshPorts();
    }

    // -------------------------------------------------------- evenements UI

    private async void BtnStart_Click(object? sender, EventArgs e)
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
        SaveSettings();
        ResetStats();
        await _session.StartAsync(opts);
    }

    private void BtnStop_Click(object? sender, EventArgs e) => _session.Stop();

    private void BtnRefreshPorts_Click(object? sender, EventArgs e) => RefreshPorts();

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        rtbDev.Clear();
        rtbApp.Clear();
    }

    private void UiTimer_Tick(object? sender, EventArgs e) => OnUiTick();

    private void MiDevCopy_Click(object? sender, EventArgs e)
    {
        if (rtbDev.SelectionLength > 0) rtbDev.Copy();
    }

    private void MiDevSelectAll_Click(object? sender, EventArgs e) => rtbDev.SelectAll();
    private void MiDevClear_Click(object? sender, EventArgs e) => rtbDev.Clear();

    private void MiAppCopy_Click(object? sender, EventArgs e)
    {
        if (rtbApp.SelectionLength > 0) rtbApp.Copy();
    }

    private void MiAppSelectAll_Click(object? sender, EventArgs e) => rtbApp.SelectAll();
    private void MiAppClear_Click(object? sender, EventArgs e) => rtbApp.Clear();

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
        cb.Items.AddRange([.. ports]);
        cb.Items.Add(extra);
        cb.Text = current;
    }

    // -------------------------------------------------------------- moteur

    private void OnSessionRunningChanged(object? sender, EventArgs e)
    {
        if (IsDisposed) return;
        bool running = _session.IsRunning;
        if (running)
        {
            _tickPrev = Environment.TickCount64;
            uiTimer.Start();
        }
        else
        {
            uiTimer.Stop();
            FlushPending();
        }
        SetRunning(running);
    }

    private SnifferOptions BuildOptions()
    {
        string stamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
        string logDir = Path.Combine(AppContext.BaseDirectory, "Logs");
        Directory.CreateDirectory(logDir);
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
            LogFile = Path.Combine(logDir, $"comsniffer-{stamp}.log"),
        };
        opts.InTeeFiles.Add(Path.Combine(logDir, $"tee-dev-to-host-{stamp}.bin"));
        opts.OutTeeFiles.Add(Path.Combine(logDir, $"tee-host-to-dev-{stamp}.bin"));
        return opts;
    }

    private void OnPacket(object? sender, SniffEvent ev)
    {
        if (IsDisposed) return;
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
            lblRateDev.Text = $"↓ {DisplayFormatter.FormatRate((_session.TotalIn - _prevDev) / dt)}";
            lblRateApp.Text = $"↑ {DisplayFormatter.FormatRate((_session.TotalOut - _prevApp) / dt)}";
            _prevDev = _session.TotalIn; _prevApp = _session.TotalOut; _tickPrev = now;
        }
        gbDev.Text = $"Device → Host   ({_session.TotalIn:N0} o)";
        gbApp.Text = $"Host → Device   ({_session.TotalOut:N0} o)";
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
        rowEndpoints.Enabled = !running;
        foreach (Control c in rowParams.Controls)
            if (c is not Button)
                c.Enabled = !running;
        btnStart.Enabled = !running;
        btnStop.Enabled = running;
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
        _prevDev = _prevApp = 0;
        lock (_pendingLock) { _pendingDev.Clear(); _pendingApp.Clear(); }
        chkPause.Checked = false;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _session.Stop();              // arret en tache de fond ; la session nettoie
        SaveSettings();
        base.OnFormClosing(e);
    }

    // ---------------------------------------------------------- settings

    private void LoadSettings()
    {
        _settings = GuiSettings.Load(_settingsPath);
        cmbDevice.Text = _settings.Device;
        cmbApp.Text = _settings.App;
        cmbBaud.Text = _settings.BaudRate.ToString();
        cmbDataBits.SelectedItem = _settings.DataBits;
        SelectByText(cmbParity, _settings.Parity);
        SelectByText(cmbStopBits, _settings.StopBits);
        SelectByText(cmbHandshake, _settings.Handshake);
        chkHex.Checked = _settings.Hex;
        chkAscii.Checked = _settings.Ascii;
        chkTimestamp.Checked = _settings.Timestamp;
    }

    private static void SelectByText(ComboBox cb, string value)
    {
        int i = cb.Items.IndexOf(value);
        if (i >= 0) cb.SelectedIndex = i;
    }

    private void SaveSettings()
    {
        _settings.Device = cmbDevice.Text.Trim();
        _settings.App = cmbApp.Text.Trim();
        _settings.BaudRate = int.TryParse(cmbBaud.Text.Trim(), out int b) ? b : 9600;
        _settings.DataBits = (int)(cmbDataBits.SelectedItem ?? 8);
        _settings.Parity = cmbParity.Text;
        _settings.StopBits = cmbStopBits.Text;
        _settings.Handshake = cmbHandshake.Text;
        _settings.Hex = chkHex.Checked;
        _settings.Ascii = chkAscii.Checked;
        _settings.Timestamp = chkTimestamp.Checked;
        _settings.Save(_settingsPath);
    }
}
