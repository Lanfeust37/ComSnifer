namespace ComSnifer.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rootTable = new System.Windows.Forms.TableLayoutPanel();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.cfgTable = new System.Windows.Forms.TableLayoutPanel();
            this.rowEndpoints = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDevice = new System.Windows.Forms.Label();
            this.cmbDevice = new System.Windows.Forms.ComboBox();
            this.btnRefreshPorts = new System.Windows.Forms.Button();
            this.lblApp = new System.Windows.Forms.Label();
            this.cmbApp = new System.Windows.Forms.ComboBox();
            this.rowParams = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBauds = new System.Windows.Forms.Label();
            this.cmbBaud = new System.Windows.Forms.ComboBox();
            this.lblBits = new System.Windows.Forms.Label();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.lblParite = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.lblStop = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.lblHandsh = new System.Windows.Forms.Label();
            this.cmbHandshake = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.split = new System.Windows.Forms.SplitContainer();
            this.gbDev = new System.Windows.Forms.GroupBox();
            this.rtbDev = new System.Windows.Forms.RichTextBox();
            this.cmsDev = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDevCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miDevSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miDevClear = new System.Windows.Forms.ToolStripMenuItem();
            this.gbApp = new System.Windows.Forms.GroupBox();
            this.rtbApp = new System.Windows.Forms.RichTextBox();
            this.cmsApp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAppCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miAppSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miAppClear = new System.Windows.Forms.ToolStripMenuItem();
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.chkHex = new System.Windows.Forms.CheckBox();
            this.chkAscii = new System.Windows.Forms.CheckBox();
            this.chkTimestamp = new System.Windows.Forms.CheckBox();
            this.chkPause = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.flpFiles = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnLogBrowse = new System.Windows.Forms.Button();
            this.lblTeeIn = new System.Windows.Forms.Label();
            this.txtTeeIn = new System.Windows.Forms.TextBox();
            this.btnTeeInBrowse = new System.Windows.Forms.Button();
            this.lblTeeOut = new System.Windows.Forms.Label();
            this.txtTeeOut = new System.Windows.Forms.TextBox();
            this.btnTeeOutBrowse = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblState = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRateDev = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRateApp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.gbConfig.SuspendLayout();
            this.cfgTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.gbDev.SuspendLayout();
            this.gbApp.SuspendLayout();
            this.cmsDev.SuspendLayout();
            this.cmsApp.SuspendLayout();
            this.SuspendLayout();
            //
            // rootTable
            //
            this.rootTable.ColumnCount = 1;
            this.rootTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootTable.Controls.Add(this.gbConfig, 0, 0);
            this.rootTable.Controls.Add(this.split, 0, 1);
            this.rootTable.Controls.Add(this.flpOptions, 0, 2);
            this.rootTable.Controls.Add(this.flpFiles, 0, 3);
            this.rootTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootTable.Location = new System.Drawing.Point(0, 0);
            this.rootTable.Name = "rootTable";
            this.rootTable.Padding = new System.Windows.Forms.Padding(8);
            this.rootTable.RowCount = 4;
            this.rootTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.rootTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.rootTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.rootTable.Size = new System.Drawing.Size(1024, 602);
            this.rootTable.TabIndex = 0;
            //
            // gbConfig
            //
            this.gbConfig.AutoSize = true;
            this.gbConfig.Controls.Add(this.cfgTable);
            this.gbConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbConfig.Location = new System.Drawing.Point(11, 11);
            this.gbConfig.Name = "gbConfig";
            this.gbConfig.Size = new System.Drawing.Size(1002, 78);
            this.gbConfig.TabIndex = 0;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "Connexion";
            //
            // cfgTable
            //
            this.cfgTable.AutoSize = true;
            this.cfgTable.ColumnCount = 1;
            this.cfgTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.cfgTable.Controls.Add(this.rowEndpoints, 0, 0);
            this.cfgTable.Controls.Add(this.rowParams, 0, 1);
            this.cfgTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgTable.Location = new System.Drawing.Point(3, 19);
            this.cfgTable.Name = "cfgTable";
            this.cfgTable.RowCount = 2;
            this.cfgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.cfgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.cfgTable.Size = new System.Drawing.Size(996, 56);
            this.cfgTable.TabIndex = 0;
            //
            // rowEndpoints
            //
            this.rowEndpoints.AutoSize = true;
            this.rowEndpoints.Controls.Add(this.lblDevice);
            this.rowEndpoints.Controls.Add(this.cmbDevice);
            this.rowEndpoints.Controls.Add(this.btnRefreshPorts);
            this.rowEndpoints.Controls.Add(this.lblApp);
            this.rowEndpoints.Controls.Add(this.cmbApp);
            this.rowEndpoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rowEndpoints.Location = new System.Drawing.Point(0, 0);
            this.rowEndpoints.Margin = new System.Windows.Forms.Padding(0);
            this.rowEndpoints.Name = "rowEndpoints";
            this.rowEndpoints.Size = new System.Drawing.Size(996, 27);
            this.rowEndpoints.TabIndex = 0;
            this.rowEndpoints.WrapContents = false;
            //
            // lblDevice
            //
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(3, 6);
            this.lblDevice.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(48, 15);
            this.lblDevice.TabIndex = 0;
            this.lblDevice.Text = "Device :";
            //
            // cmbDevice
            //
            this.cmbDevice.FormattingEnabled = true;
            this.cmbDevice.Location = new System.Drawing.Point(57, 3);
            this.cmbDevice.Name = "cmbDevice";
            this.cmbDevice.Size = new System.Drawing.Size(180, 23);
            this.cmbDevice.TabIndex = 1;
            this.toolTip.SetToolTip(this.cmbDevice, "COMx, tcp:hôte:port");
            //
            // btnRefreshPorts
            //
            this.btnRefreshPorts.Location = new System.Drawing.Point(240, 1);
            this.btnRefreshPorts.Margin = new System.Windows.Forms.Padding(3, 1, 12, 1);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(30, 25);
            this.btnRefreshPorts.TabIndex = 2;
            this.btnRefreshPorts.Text = "↻";
            this.toolTip.SetToolTip(this.btnRefreshPorts, "Rafraîchir la liste des ports série");
            this.btnRefreshPorts.UseVisualStyleBackColor = true;
            this.btnRefreshPorts.Click += new System.EventHandler(this.btnRefreshPorts_Click);
            //
            // lblApp
            //
            this.lblApp.AutoSize = true;
            this.lblApp.Location = new System.Drawing.Point(285, 6);
            this.lblApp.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblApp.Name = "lblApp";
            this.lblApp.Size = new System.Drawing.Size(35, 15);
            this.lblApp.TabIndex = 3;
            this.lblApp.Text = "App :";
            //
            // cmbApp
            //
            this.cmbApp.FormattingEnabled = true;
            this.cmbApp.Location = new System.Drawing.Point(326, 3);
            this.cmbApp.Name = "cmbApp";
            this.cmbApp.Size = new System.Drawing.Size(180, 23);
            this.cmbApp.TabIndex = 4;
            this.toolTip.SetToolTip(this.cmbApp, "COMx, tcp:hôte:port, listen:port");
            //
            // rowParams
            //
            this.rowParams.AutoSize = true;
            this.rowParams.Controls.Add(this.lblBauds);
            this.rowParams.Controls.Add(this.cmbBaud);
            this.rowParams.Controls.Add(this.lblBits);
            this.rowParams.Controls.Add(this.cmbDataBits);
            this.rowParams.Controls.Add(this.lblParite);
            this.rowParams.Controls.Add(this.cmbParity);
            this.rowParams.Controls.Add(this.lblStop);
            this.rowParams.Controls.Add(this.cmbStopBits);
            this.rowParams.Controls.Add(this.lblHandsh);
            this.rowParams.Controls.Add(this.cmbHandshake);
            this.rowParams.Controls.Add(this.btnStart);
            this.rowParams.Controls.Add(this.btnStop);
            this.rowParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rowParams.Location = new System.Drawing.Point(0, 27);
            this.rowParams.Margin = new System.Windows.Forms.Padding(0);
            this.rowParams.Name = "rowParams";
            this.rowParams.Size = new System.Drawing.Size(996, 29);
            this.rowParams.TabIndex = 1;
            this.rowParams.WrapContents = false;
            //
            // lblBauds
            //
            this.lblBauds.AutoSize = true;
            this.lblBauds.Location = new System.Drawing.Point(8, 6);
            this.lblBauds.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            this.lblBauds.Name = "lblBauds";
            this.lblBauds.Size = new System.Drawing.Size(42, 15);
            this.lblBauds.TabIndex = 0;
            this.lblBauds.Text = "Bauds :";
            //
            // cmbBaud
            //
            this.cmbBaud.FormattingEnabled = true;
            this.cmbBaud.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.cmbBaud.Location = new System.Drawing.Point(55, 3);
            this.cmbBaud.Name = "cmbBaud";
            this.cmbBaud.Size = new System.Drawing.Size(80, 23);
            this.cmbBaud.TabIndex = 1;
            this.cmbBaud.Text = "9600";
            //
            // lblBits
            //
            this.lblBits.AutoSize = true;
            this.lblBits.Location = new System.Drawing.Point(143, 6);
            this.lblBits.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            this.lblBits.Name = "lblBits";
            this.lblBits.Size = new System.Drawing.Size(30, 15);
            this.lblBits.TabIndex = 2;
            this.lblBits.Text = "Bits :";
            //
            // cmbDataBits
            //
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            5,
            6,
            7,
            8});
            this.cmbDataBits.Location = new System.Drawing.Point(178, 3);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(45, 23);
            this.cmbDataBits.TabIndex = 3;
            //
            // lblParite
            //
            this.lblParite.AutoSize = true;
            this.lblParite.Location = new System.Drawing.Point(231, 6);
            this.lblParite.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            this.lblParite.Name = "lblParite";
            this.lblParite.Size = new System.Drawing.Size(42, 15);
            this.lblParite.TabIndex = 4;
            this.lblParite.Text = "Parité :";
            //
            // cmbParity
            //
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd",
            "Mark",
            "Space"});
            this.cmbParity.Location = new System.Drawing.Point(278, 3);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(75, 23);
            this.cmbParity.TabIndex = 5;
            //
            // lblStop
            //
            this.lblStop.AutoSize = true;
            this.lblStop.Location = new System.Drawing.Point(361, 6);
            this.lblStop.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            this.lblStop.Name = "lblStop";
            this.lblStop.Size = new System.Drawing.Size(36, 15);
            this.lblStop.TabIndex = 6;
            this.lblStop.Text = "Stop :";
            //
            // cmbStopBits
            //
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cmbStopBits.Location = new System.Drawing.Point(402, 3);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(50, 23);
            this.cmbStopBits.TabIndex = 7;
            //
            // lblHandsh
            //
            this.lblHandsh.AutoSize = true;
            this.lblHandsh.Location = new System.Drawing.Point(460, 6);
            this.lblHandsh.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            this.lblHandsh.Name = "lblHandsh";
            this.lblHandsh.Size = new System.Drawing.Size(52, 15);
            this.lblHandsh.TabIndex = 8;
            this.lblHandsh.Text = "Handsh. :";
            //
            // cmbHandshake
            //
            this.cmbHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHandshake.FormattingEnabled = true;
            this.cmbHandshake.Items.AddRange(new object[] {
            "None",
            "XOnXOff",
            "RequestToSend",
            "RequestToSendXOnXOff"});
            this.cmbHandshake.Location = new System.Drawing.Point(517, 3);
            this.cmbHandshake.Name = "cmbHandshake";
            this.cmbHandshake.Size = new System.Drawing.Size(170, 23);
            this.cmbHandshake.TabIndex = 9;
            //
            // btnStart
            //
            this.btnStart.AutoSize = true;
            this.btnStart.Location = new System.Drawing.Point(702, 1);
            this.btnStart.Margin = new System.Windows.Forms.Padding(12, 1, 3, 1);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(82, 27);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "▶ Démarrer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //
            // btnStop
            //
            this.btnStop.AutoSize = true;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(790, 1);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(66, 27);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "■ Arrêter";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // split
            //
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(11, 95);
            this.split.Name = "split";
            //
            // split.Panel1
            //
            this.split.Panel1.Controls.Add(this.gbDev);
            this.split.Panel1MinSize = 120;
            //
            // split.Panel2
            //
            this.split.Panel2.Controls.Add(this.gbApp);
            this.split.Panel2MinSize = 120;
            this.split.Size = new System.Drawing.Size(1002, 455);
            this.split.SplitterDistance = 490;
            this.split.TabIndex = 1;
            //
            // gbDev
            //
            this.gbDev.Controls.Add(this.rtbDev);
            this.gbDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDev.Location = new System.Drawing.Point(0, 0);
            this.gbDev.Name = "gbDev";
            this.gbDev.Size = new System.Drawing.Size(490, 455);
            this.gbDev.TabIndex = 0;
            this.gbDev.TabStop = false;
            this.gbDev.Text = "Device → Host";
            //
            // rtbDev
            //
            this.rtbDev.BackColor = System.Drawing.SystemColors.Window;
            this.rtbDev.ContextMenuStrip = this.cmsDev;
            this.rtbDev.DetectUrls = false;
            this.rtbDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDev.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.rtbDev.Location = new System.Drawing.Point(3, 19);
            this.rtbDev.Name = "rtbDev";
            this.rtbDev.ReadOnly = true;
            this.rtbDev.Size = new System.Drawing.Size(484, 433);
            this.rtbDev.TabIndex = 0;
            this.rtbDev.Text = "";
            this.rtbDev.WordWrap = false;
            //
            // cmsDev
            //
            this.cmsDev.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDevCopy,
            this.miDevSelectAll,
            this.miDevClear});
            this.cmsDev.Name = "cmsDev";
            this.cmsDev.Size = new System.Drawing.Size(170, 70);
            //
            // miDevCopy
            //
            this.miDevCopy.Name = "miDevCopy";
            this.miDevCopy.Size = new System.Drawing.Size(169, 22);
            this.miDevCopy.Text = "Copier";
            this.miDevCopy.Click += new System.EventHandler(this.miDevCopy_Click);
            //
            // miDevSelectAll
            //
            this.miDevSelectAll.Name = "miDevSelectAll";
            this.miDevSelectAll.Size = new System.Drawing.Size(169, 22);
            this.miDevSelectAll.Text = "Tout sélectionner";
            this.miDevSelectAll.Click += new System.EventHandler(this.miDevSelectAll_Click);
            //
            // miDevClear
            //
            this.miDevClear.Name = "miDevClear";
            this.miDevClear.Size = new System.Drawing.Size(169, 22);
            this.miDevClear.Text = "Effacer";
            this.miDevClear.Click += new System.EventHandler(this.miDevClear_Click);
            //
            // gbApp
            //
            this.gbApp.Controls.Add(this.rtbApp);
            this.gbApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbApp.Location = new System.Drawing.Point(0, 0);
            this.gbApp.Name = "gbApp";
            this.gbApp.Size = new System.Drawing.Size(508, 455);
            this.gbApp.TabIndex = 0;
            this.gbApp.TabStop = false;
            this.gbApp.Text = "Host → Device";
            //
            // rtbApp
            //
            this.rtbApp.BackColor = System.Drawing.SystemColors.Window;
            this.rtbApp.ContextMenuStrip = this.cmsApp;
            this.rtbApp.DetectUrls = false;
            this.rtbApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbApp.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.rtbApp.Location = new System.Drawing.Point(3, 19);
            this.rtbApp.Name = "rtbApp";
            this.rtbApp.ReadOnly = true;
            this.rtbApp.Size = new System.Drawing.Size(502, 433);
            this.rtbApp.TabIndex = 0;
            this.rtbApp.Text = "";
            this.rtbApp.WordWrap = false;
            //
            // cmsApp
            //
            this.cmsApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAppCopy,
            this.miAppSelectAll,
            this.miAppClear});
            this.cmsApp.Name = "cmsApp";
            this.cmsApp.Size = new System.Drawing.Size(170, 70);
            //
            // miAppCopy
            //
            this.miAppCopy.Name = "miAppCopy";
            this.miAppCopy.Size = new System.Drawing.Size(169, 22);
            this.miAppCopy.Text = "Copier";
            this.miAppCopy.Click += new System.EventHandler(this.miAppCopy_Click);
            //
            // miAppSelectAll
            //
            this.miAppSelectAll.Name = "miAppSelectAll";
            this.miAppSelectAll.Size = new System.Drawing.Size(169, 22);
            this.miAppSelectAll.Text = "Tout sélectionner";
            this.miAppSelectAll.Click += new System.EventHandler(this.miAppSelectAll_Click);
            //
            // miAppClear
            //
            this.miAppClear.Name = "miAppClear";
            this.miAppClear.Size = new System.Drawing.Size(169, 22);
            this.miAppClear.Text = "Effacer";
            this.miAppClear.Click += new System.EventHandler(this.miAppClear_Click);
            //
            // flpOptions
            //
            this.flpOptions.AutoSize = true;
            this.flpOptions.Controls.Add(this.chkHex);
            this.flpOptions.Controls.Add(this.chkAscii);
            this.flpOptions.Controls.Add(this.chkTimestamp);
            this.flpOptions.Controls.Add(this.chkPause);
            this.flpOptions.Controls.Add(this.btnClear);
            this.flpOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOptions.Location = new System.Drawing.Point(8, 553);
            this.flpOptions.Margin = new System.Windows.Forms.Padding(0);
            this.flpOptions.Name = "flpOptions";
            this.flpOptions.Size = new System.Drawing.Size(1008, 27);
            this.flpOptions.TabIndex = 2;
            this.flpOptions.WrapContents = false;
            //
            // chkHex
            //
            this.chkHex.AutoSize = true;
            this.chkHex.Checked = true;
            this.chkHex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHex.Location = new System.Drawing.Point(3, 3);
            this.chkHex.Name = "chkHex";
            this.chkHex.Size = new System.Drawing.Size(92, 19);
            this.chkHex.TabIndex = 0;
            this.chkHex.Text = "Hexadécimal";
            this.chkHex.UseVisualStyleBackColor = true;
            //
            // chkAscii
            //
            this.chkAscii.AutoSize = true;
            this.chkAscii.Checked = true;
            this.chkAscii.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAscii.Location = new System.Drawing.Point(101, 3);
            this.chkAscii.Name = "chkAscii";
            this.chkAscii.Size = new System.Drawing.Size(54, 19);
            this.chkAscii.TabIndex = 1;
            this.chkAscii.Text = "ASCII";
            this.chkAscii.UseVisualStyleBackColor = true;
            //
            // chkTimestamp
            //
            this.chkTimestamp.AutoSize = true;
            this.chkTimestamp.Checked = true;
            this.chkTimestamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimestamp.Location = new System.Drawing.Point(161, 3);
            this.chkTimestamp.Name = "chkTimestamp";
            this.chkTimestamp.Size = new System.Drawing.Size(87, 19);
            this.chkTimestamp.TabIndex = 2;
            this.chkTimestamp.Text = "Horodatage";
            this.chkTimestamp.UseVisualStyleBackColor = true;
            //
            // chkPause
            //
            this.chkPause.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPause.AutoSize = true;
            this.chkPause.Location = new System.Drawing.Point(254, 0);
            this.chkPause.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.chkPause.Name = "chkPause";
            this.chkPause.Size = new System.Drawing.Size(57, 27);
            this.chkPause.TabIndex = 3;
            this.chkPause.Text = "⏸ Pause";
            this.chkPause.UseVisualStyleBackColor = true;
            //
            // btnClear
            //
            this.btnClear.AutoSize = true;
            this.btnClear.Location = new System.Drawing.Point(317, 0);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(57, 27);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Effacer";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // flpFiles
            //
            this.flpFiles.AutoSize = true;
            this.flpFiles.Controls.Add(this.lblLog);
            this.flpFiles.Controls.Add(this.txtLog);
            this.flpFiles.Controls.Add(this.btnLogBrowse);
            this.flpFiles.Controls.Add(this.lblTeeIn);
            this.flpFiles.Controls.Add(this.txtTeeIn);
            this.flpFiles.Controls.Add(this.btnTeeInBrowse);
            this.flpFiles.Controls.Add(this.lblTeeOut);
            this.flpFiles.Controls.Add(this.txtTeeOut);
            this.flpFiles.Controls.Add(this.btnTeeOutBrowse);
            this.flpFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFiles.Location = new System.Drawing.Point(8, 580);
            this.flpFiles.Margin = new System.Windows.Forms.Padding(0);
            this.flpFiles.Name = "flpFiles";
            this.flpFiles.Size = new System.Drawing.Size(1008, 29);
            this.flpFiles.TabIndex = 3;
            this.flpFiles.WrapContents = false;
            //
            // lblLog
            //
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(3, 6);
            this.lblLog.Margin = new System.Windows.Forms.Padding(3, 6, 2, 3);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(33, 15);
            this.lblLog.TabIndex = 0;
            this.lblLog.Text = "Log :";
            //
            // txtLog
            //
            this.txtLog.Location = new System.Drawing.Point(41, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(180, 23);
            this.txtLog.TabIndex = 1;
            //
            // btnLogBrowse
            //
            this.btnLogBrowse.Location = new System.Drawing.Point(226, 1);
            this.btnLogBrowse.Margin = new System.Windows.Forms.Padding(2, 1, 14, 1);
            this.btnLogBrowse.Name = "btnLogBrowse";
            this.btnLogBrowse.Size = new System.Drawing.Size(28, 25);
            this.btnLogBrowse.TabIndex = 2;
            this.btnLogBrowse.Text = "…";
            this.btnLogBrowse.UseVisualStyleBackColor = true;
            this.btnLogBrowse.Click += new System.EventHandler(this.btnLogBrowse_Click);
            //
            // lblTeeIn
            //
            this.lblTeeIn.AutoSize = true;
            this.lblTeeIn.Location = new System.Drawing.Point(271, 6);
            this.lblTeeIn.Margin = new System.Windows.Forms.Padding(3, 6, 2, 3);
            this.lblTeeIn.Name = "lblTeeIn";
            this.lblTeeIn.Size = new System.Drawing.Size(89, 15);
            this.lblTeeIn.TabIndex = 3;
            this.lblTeeIn.Text = "Tee dev→host :";
            //
            // txtTeeIn
            //
            this.txtTeeIn.Location = new System.Drawing.Point(365, 3);
            this.txtTeeIn.Name = "txtTeeIn";
            this.txtTeeIn.Size = new System.Drawing.Size(160, 23);
            this.txtTeeIn.TabIndex = 4;
            //
            // btnTeeInBrowse
            //
            this.btnTeeInBrowse.Location = new System.Drawing.Point(530, 1);
            this.btnTeeInBrowse.Margin = new System.Windows.Forms.Padding(2, 1, 14, 1);
            this.btnTeeInBrowse.Name = "btnTeeInBrowse";
            this.btnTeeInBrowse.Size = new System.Drawing.Size(28, 25);
            this.btnTeeInBrowse.TabIndex = 5;
            this.btnTeeInBrowse.Text = "…";
            this.btnTeeInBrowse.UseVisualStyleBackColor = true;
            this.btnTeeInBrowse.Click += new System.EventHandler(this.btnTeeInBrowse_Click);
            //
            // lblTeeOut
            //
            this.lblTeeOut.AutoSize = true;
            this.lblTeeOut.Location = new System.Drawing.Point(575, 6);
            this.lblTeeOut.Margin = new System.Windows.Forms.Padding(3, 6, 2, 3);
            this.lblTeeOut.Name = "lblTeeOut";
            this.lblTeeOut.Size = new System.Drawing.Size(89, 15);
            this.lblTeeOut.TabIndex = 6;
            this.lblTeeOut.Text = "Tee host→dev :";
            //
            // txtTeeOut
            //
            this.txtTeeOut.Location = new System.Drawing.Point(669, 3);
            this.txtTeeOut.Name = "txtTeeOut";
            this.txtTeeOut.Size = new System.Drawing.Size(160, 23);
            this.txtTeeOut.TabIndex = 7;
            //
            // btnTeeOutBrowse
            //
            this.btnTeeOutBrowse.Location = new System.Drawing.Point(834, 1);
            this.btnTeeOutBrowse.Margin = new System.Windows.Forms.Padding(2, 1, 3, 1);
            this.btnTeeOutBrowse.Name = "btnTeeOutBrowse";
            this.btnTeeOutBrowse.Size = new System.Drawing.Size(28, 25);
            this.btnTeeOutBrowse.TabIndex = 8;
            this.btnTeeOutBrowse.Text = "…";
            this.btnTeeOutBrowse.UseVisualStyleBackColor = true;
            this.btnTeeOutBrowse.Click += new System.EventHandler(this.btnTeeOutBrowse_Click);
            //
            // statusStrip
            //
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblState,
            this.lblMsg,
            this.lblRateDev,
            this.lblRateApp});
            this.statusStrip.Location = new System.Drawing.Point(0, 618);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1040, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            //
            // lblState
            //
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(53, 17);
            this.lblState.Text = "○ Arrêté";
            //
            // lblMsg
            //
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(824, 17);
            this.lblMsg.Spring = true;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblRateDev
            //
            this.lblRateDev.Name = "lblRateDev";
            this.lblRateDev.Size = new System.Drawing.Size(51, 17);
            this.lblRateDev.Text = "↓ 0 o/s";
            //
            // lblRateApp
            //
            this.lblRateApp.Name = "lblRateApp";
            this.lblRateApp.Size = new System.Drawing.Size(51, 17);
            this.lblRateApp.Text = "↑ 0 o/s";
            //
            // uiTimer
            //
            this.uiTimer.Interval = 250;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 640);
            this.Controls.Add(this.rootTable);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(800, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ComSnifer — Serial/TCP Line Sniffer";
            this.gbConfig.ResumeLayout(false);
            this.gbConfig.PerformLayout();
            this.cfgTable.ResumeLayout(false);
            this.cfgTable.PerformLayout();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.gbDev.ResumeLayout(false);
            this.gbApp.ResumeLayout(false);
            this.cmsDev.ResumeLayout(false);
            this.cmsApp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootTable;
        private System.Windows.Forms.GroupBox gbConfig;
        private System.Windows.Forms.TableLayoutPanel cfgTable;
        private System.Windows.Forms.FlowLayoutPanel rowEndpoints;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.ComboBox cmbDevice;
        private System.Windows.Forms.Button btnRefreshPorts;
        private System.Windows.Forms.Label lblApp;
        private System.Windows.Forms.ComboBox cmbApp;
        private System.Windows.Forms.FlowLayoutPanel rowParams;
        private System.Windows.Forms.Label lblBauds;
        private System.Windows.Forms.ComboBox cmbBaud;
        private System.Windows.Forms.Label lblBits;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.Label lblParite;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label lblStop;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.Label lblHandsh;
        private System.Windows.Forms.ComboBox cmbHandshake;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.GroupBox gbDev;
        private System.Windows.Forms.RichTextBox rtbDev;
        private System.Windows.Forms.ContextMenuStrip cmsDev;
        private System.Windows.Forms.ToolStripMenuItem miDevCopy;
        private System.Windows.Forms.ToolStripMenuItem miDevSelectAll;
        private System.Windows.Forms.ToolStripMenuItem miDevClear;
        private System.Windows.Forms.GroupBox gbApp;
        private System.Windows.Forms.RichTextBox rtbApp;
        private System.Windows.Forms.ContextMenuStrip cmsApp;
        private System.Windows.Forms.ToolStripMenuItem miAppCopy;
        private System.Windows.Forms.ToolStripMenuItem miAppSelectAll;
        private System.Windows.Forms.ToolStripMenuItem miAppClear;
        private System.Windows.Forms.FlowLayoutPanel flpOptions;
        private System.Windows.Forms.CheckBox chkHex;
        private System.Windows.Forms.CheckBox chkAscii;
        private System.Windows.Forms.CheckBox chkTimestamp;
        private System.Windows.Forms.CheckBox chkPause;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.FlowLayoutPanel flpFiles;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnLogBrowse;
        private System.Windows.Forms.Label lblTeeIn;
        private System.Windows.Forms.TextBox txtTeeIn;
        private System.Windows.Forms.Button btnTeeInBrowse;
        private System.Windows.Forms.Label lblTeeOut;
        private System.Windows.Forms.TextBox txtTeeOut;
        private System.Windows.Forms.Button btnTeeOutBrowse;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblState;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.ToolStripStatusLabel lblRateDev;
        private System.Windows.Forms.ToolStripStatusLabel lblRateApp;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer uiTimer;
    }
}
