namespace ComSniffer.Gui
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
            components = new System.ComponentModel.Container();
            rootTable = new System.Windows.Forms.TableLayoutPanel();
            gbConfig = new System.Windows.Forms.GroupBox();
            cfgTable = new System.Windows.Forms.TableLayoutPanel();
            rowEndpoints = new System.Windows.Forms.FlowLayoutPanel();
            lblDevice = new System.Windows.Forms.Label();
            cmbDevice = new System.Windows.Forms.ComboBox();
            btnRefreshPorts = new System.Windows.Forms.Button();
            lblApp = new System.Windows.Forms.Label();
            cmbApp = new System.Windows.Forms.ComboBox();
            rowParams = new System.Windows.Forms.FlowLayoutPanel();
            lblBauds = new System.Windows.Forms.Label();
            cmbBaud = new System.Windows.Forms.ComboBox();
            lblBits = new System.Windows.Forms.Label();
            cmbDataBits = new System.Windows.Forms.ComboBox();
            lblParite = new System.Windows.Forms.Label();
            cmbParity = new System.Windows.Forms.ComboBox();
            lblStop = new System.Windows.Forms.Label();
            cmbStopBits = new System.Windows.Forms.ComboBox();
            lblHandsh = new System.Windows.Forms.Label();
            cmbHandshake = new System.Windows.Forms.ComboBox();
            btnStart = new System.Windows.Forms.Button();
            btnStop = new System.Windows.Forms.Button();
            split = new System.Windows.Forms.SplitContainer();
            gbDev = new System.Windows.Forms.GroupBox();
            rtbDev = new System.Windows.Forms.RichTextBox();
            cmsDev = new System.Windows.Forms.ContextMenuStrip(components);
            miDevCopy = new System.Windows.Forms.ToolStripMenuItem();
            miDevSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            miDevClear = new System.Windows.Forms.ToolStripMenuItem();
            gbApp = new System.Windows.Forms.GroupBox();
            rtbApp = new System.Windows.Forms.RichTextBox();
            cmsApp = new System.Windows.Forms.ContextMenuStrip(components);
            miAppCopy = new System.Windows.Forms.ToolStripMenuItem();
            miAppSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            miAppClear = new System.Windows.Forms.ToolStripMenuItem();
            flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            chkHex = new System.Windows.Forms.CheckBox();
            chkAscii = new System.Windows.Forms.CheckBox();
            chkTimestamp = new System.Windows.Forms.CheckBox();
            chkPause = new System.Windows.Forms.CheckBox();
            btnClear = new System.Windows.Forms.Button();
            statusStrip = new System.Windows.Forms.StatusStrip();
            lblState = new System.Windows.Forms.ToolStripStatusLabel();
            lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            lblRateDev = new System.Windows.Forms.ToolStripStatusLabel();
            lblRateApp = new System.Windows.Forms.ToolStripStatusLabel();
            toolTip = new System.Windows.Forms.ToolTip(components);
            uiTimer = new System.Windows.Forms.Timer(components);
            gbConfig.SuspendLayout();
            cfgTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(split)).BeginInit();
            split.Panel1.SuspendLayout();
            split.Panel2.SuspendLayout();
            gbDev.SuspendLayout();
            gbApp.SuspendLayout();
            cmsDev.SuspendLayout();
            cmsApp.SuspendLayout();
            SuspendLayout();
            //
            // rootTable
            //
            rootTable.ColumnCount = 1;
            rootTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootTable.Controls.Add(gbConfig, 0, 0);
            rootTable.Controls.Add(split, 0, 1);
            rootTable.Controls.Add(flpOptions, 0, 2);
            rootTable.Dock = System.Windows.Forms.DockStyle.Fill;
            rootTable.Location = new System.Drawing.Point(0, 0);
            rootTable.Name = "rootTable";
            rootTable.Padding = new System.Windows.Forms.Padding(8);
            rootTable.RowCount = 3;
            rootTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            rootTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rootTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            rootTable.Size = new System.Drawing.Size(1024, 602);
            rootTable.TabIndex = 0;
            //
            // gbConfig
            //
            gbConfig.AutoSize = true;
            gbConfig.Controls.Add(cfgTable);
            gbConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            gbConfig.Location = new System.Drawing.Point(11, 11);
            gbConfig.Name = "gbConfig";
            gbConfig.Size = new System.Drawing.Size(1002, 78);
            gbConfig.TabIndex = 0;
            gbConfig.TabStop = false;
            gbConfig.Text = "Connexion";
            //
            // cfgTable
            //
            cfgTable.AutoSize = true;
            cfgTable.ColumnCount = 1;
            cfgTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            cfgTable.Controls.Add(rowEndpoints, 0, 0);
            cfgTable.Controls.Add(rowParams, 0, 1);
            cfgTable.Dock = System.Windows.Forms.DockStyle.Fill;
            cfgTable.Location = new System.Drawing.Point(3, 19);
            cfgTable.Name = "cfgTable";
            cfgTable.RowCount = 2;
            cfgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            cfgTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            cfgTable.Size = new System.Drawing.Size(996, 56);
            cfgTable.TabIndex = 0;
            //
            // rowEndpoints
            //
            rowEndpoints.AutoSize = true;
            rowEndpoints.Controls.Add(lblDevice);
            rowEndpoints.Controls.Add(cmbDevice);
            rowEndpoints.Controls.Add(btnRefreshPorts);
            rowEndpoints.Controls.Add(lblApp);
            rowEndpoints.Controls.Add(cmbApp);
            rowEndpoints.Dock = System.Windows.Forms.DockStyle.Fill;
            rowEndpoints.Location = new System.Drawing.Point(0, 0);
            rowEndpoints.Margin = new System.Windows.Forms.Padding(0);
            rowEndpoints.Name = "rowEndpoints";
            rowEndpoints.Size = new System.Drawing.Size(996, 27);
            rowEndpoints.TabIndex = 0;
            rowEndpoints.WrapContents = false;
            //
            // lblDevice
            //
            lblDevice.AutoSize = true;
            lblDevice.Location = new System.Drawing.Point(3, 6);
            lblDevice.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            lblDevice.Name = "lblDevice";
            lblDevice.Size = new System.Drawing.Size(48, 15);
            lblDevice.TabIndex = 0;
            lblDevice.Text = "Device :";
            //
            // cmbDevice
            //
            cmbDevice.FormattingEnabled = true;
            cmbDevice.Location = new System.Drawing.Point(57, 3);
            cmbDevice.Name = "cmbDevice";
            cmbDevice.Size = new System.Drawing.Size(180, 23);
            cmbDevice.TabIndex = 1;
            toolTip.SetToolTip(cmbDevice, "COMx, tcp:hôte:port");
            //
            // btnRefreshPorts
            //
            btnRefreshPorts.Location = new System.Drawing.Point(240, 1);
            btnRefreshPorts.Margin = new System.Windows.Forms.Padding(3, 1, 12, 1);
            btnRefreshPorts.Name = "btnRefreshPorts";
            btnRefreshPorts.Size = new System.Drawing.Size(30, 25);
            btnRefreshPorts.TabIndex = 2;
            btnRefreshPorts.Text = "↻";
            toolTip.SetToolTip(btnRefreshPorts, "Rafraîchir la liste des ports série");
            btnRefreshPorts.UseVisualStyleBackColor = true;
            btnRefreshPorts.Click += new System.EventHandler(BtnRefreshPorts_Click);
            //
            // lblApp
            //
            lblApp.AutoSize = true;
            lblApp.Location = new System.Drawing.Point(285, 6);
            lblApp.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            lblApp.Name = "lblApp";
            lblApp.Size = new System.Drawing.Size(35, 15);
            lblApp.TabIndex = 3;
            lblApp.Text = "App :";
            //
            // cmbApp
            //
            cmbApp.FormattingEnabled = true;
            cmbApp.Location = new System.Drawing.Point(326, 3);
            cmbApp.Name = "cmbApp";
            cmbApp.Size = new System.Drawing.Size(180, 23);
            cmbApp.TabIndex = 4;
            toolTip.SetToolTip(cmbApp, "COMx, tcp:hôte:port, listen:port");
            //
            // rowParams
            //
            rowParams.AutoSize = true;
            rowParams.Controls.Add(lblBauds);
            rowParams.Controls.Add(cmbBaud);
            rowParams.Controls.Add(lblBits);
            rowParams.Controls.Add(cmbDataBits);
            rowParams.Controls.Add(lblParite);
            rowParams.Controls.Add(cmbParity);
            rowParams.Controls.Add(lblStop);
            rowParams.Controls.Add(cmbStopBits);
            rowParams.Controls.Add(lblHandsh);
            rowParams.Controls.Add(cmbHandshake);
            rowParams.Controls.Add(btnStart);
            rowParams.Controls.Add(btnStop);
            rowParams.Dock = System.Windows.Forms.DockStyle.Fill;
            rowParams.Location = new System.Drawing.Point(0, 27);
            rowParams.Margin = new System.Windows.Forms.Padding(0);
            rowParams.Name = "rowParams";
            rowParams.Size = new System.Drawing.Size(996, 29);
            rowParams.TabIndex = 1;
            rowParams.WrapContents = false;
            //
            // lblBauds
            //
            lblBauds.AutoSize = true;
            lblBauds.Location = new System.Drawing.Point(8, 6);
            lblBauds.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            lblBauds.Name = "lblBauds";
            lblBauds.Size = new System.Drawing.Size(42, 15);
            lblBauds.TabIndex = 0;
            lblBauds.Text = "Bauds :";
            //
            // cmbBaud
            //
            cmbBaud.FormattingEnabled = true;
            cmbBaud.Items.AddRange(new object[] {
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
            cmbBaud.Location = new System.Drawing.Point(55, 3);
            cmbBaud.Name = "cmbBaud";
            cmbBaud.Size = new System.Drawing.Size(80, 23);
            cmbBaud.TabIndex = 1;
            cmbBaud.Text = "9600";
            //
            // lblBits
            //
            lblBits.AutoSize = true;
            lblBits.Location = new System.Drawing.Point(143, 6);
            lblBits.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            lblBits.Name = "lblBits";
            lblBits.Size = new System.Drawing.Size(30, 15);
            lblBits.TabIndex = 2;
            lblBits.Text = "Bits :";
            //
            // cmbDataBits
            //
            cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbDataBits.FormattingEnabled = true;
            cmbDataBits.Items.AddRange(new object[] {
            5,
            6,
            7,
            8});
            cmbDataBits.Location = new System.Drawing.Point(178, 3);
            cmbDataBits.Name = "cmbDataBits";
            cmbDataBits.Size = new System.Drawing.Size(45, 23);
            cmbDataBits.TabIndex = 3;
            //
            // lblParite
            //
            lblParite.AutoSize = true;
            lblParite.Location = new System.Drawing.Point(231, 6);
            lblParite.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            lblParite.Name = "lblParite";
            lblParite.Size = new System.Drawing.Size(42, 15);
            lblParite.TabIndex = 4;
            lblParite.Text = "Parité :";
            //
            // cmbParity
            //
            cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbParity.FormattingEnabled = true;
            cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd",
            "Mark",
            "Space"});
            cmbParity.Location = new System.Drawing.Point(278, 3);
            cmbParity.Name = "cmbParity";
            cmbParity.Size = new System.Drawing.Size(75, 23);
            cmbParity.TabIndex = 5;
            //
            // lblStop
            //
            lblStop.AutoSize = true;
            lblStop.Location = new System.Drawing.Point(361, 6);
            lblStop.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            lblStop.Name = "lblStop";
            lblStop.Size = new System.Drawing.Size(36, 15);
            lblStop.TabIndex = 6;
            lblStop.Text = "Stop :";
            //
            // cmbStopBits
            //
            cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbStopBits.FormattingEnabled = true;
            cmbStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            cmbStopBits.Location = new System.Drawing.Point(402, 3);
            cmbStopBits.Name = "cmbStopBits";
            cmbStopBits.Size = new System.Drawing.Size(50, 23);
            cmbStopBits.TabIndex = 7;
            //
            // lblHandsh
            //
            lblHandsh.AutoSize = true;
            lblHandsh.Location = new System.Drawing.Point(460, 6);
            lblHandsh.Margin = new System.Windows.Forms.Padding(8, 6, 2, 3);
            lblHandsh.Name = "lblHandsh";
            lblHandsh.Size = new System.Drawing.Size(52, 15);
            lblHandsh.TabIndex = 8;
            lblHandsh.Text = "Handsh. :";
            //
            // cmbHandshake
            //
            cmbHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbHandshake.FormattingEnabled = true;
            cmbHandshake.Items.AddRange(new object[] {
            "None",
            "XOnXOff",
            "RequestToSend",
            "RequestToSendXOnXOff"});
            cmbHandshake.Location = new System.Drawing.Point(517, 3);
            cmbHandshake.Name = "cmbHandshake";
            cmbHandshake.Size = new System.Drawing.Size(170, 23);
            cmbHandshake.TabIndex = 9;
            //
            // btnStart
            //
            btnStart.AutoSize = true;
            btnStart.Location = new System.Drawing.Point(702, 1);
            btnStart.Margin = new System.Windows.Forms.Padding(12, 1, 3, 1);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(82, 27);
            btnStart.TabIndex = 10;
            btnStart.Text = "▶ Démarrer";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += new System.EventHandler(BtnStart_Click);
            //
            // btnStop
            //
            btnStop.AutoSize = true;
            btnStop.Enabled = false;
            btnStop.Location = new System.Drawing.Point(790, 1);
            btnStop.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            btnStop.Name = "btnStop";
            btnStop.Size = new System.Drawing.Size(66, 27);
            btnStop.TabIndex = 11;
            btnStop.Text = "■ Arrêter";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += new System.EventHandler(BtnStop_Click);
            //
            // split
            //
            split.Dock = System.Windows.Forms.DockStyle.Fill;
            split.Location = new System.Drawing.Point(11, 95);
            split.Name = "split";
            //
            // split.Panel1
            //
            split.Panel1.Controls.Add(gbDev);
            split.Panel1MinSize = 120;
            //
            // split.Panel2
            //
            split.Panel2.Controls.Add(gbApp);
            split.Panel2MinSize = 120;
            split.Size = new System.Drawing.Size(1002, 455);
            split.SplitterDistance = 490;
            split.TabIndex = 1;
            //
            // gbDev
            //
            gbDev.Controls.Add(rtbDev);
            gbDev.Dock = System.Windows.Forms.DockStyle.Fill;
            gbDev.Location = new System.Drawing.Point(0, 0);
            gbDev.Name = "gbDev";
            gbDev.Size = new System.Drawing.Size(490, 455);
            gbDev.TabIndex = 0;
            gbDev.TabStop = false;
            gbDev.Text = "Device → Host";
            //
            // rtbDev
            //
            rtbDev.BackColor = System.Drawing.SystemColors.Window;
            rtbDev.ContextMenuStrip = cmsDev;
            rtbDev.DetectUrls = false;
            rtbDev.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbDev.Font = new System.Drawing.Font("Consolas", 9.5F);
            rtbDev.Location = new System.Drawing.Point(3, 19);
            rtbDev.Name = "rtbDev";
            rtbDev.ReadOnly = true;
            rtbDev.Size = new System.Drawing.Size(484, 433);
            rtbDev.TabIndex = 0;
            rtbDev.Text = "";
            rtbDev.WordWrap = false;
            //
            // cmsDev
            //
            cmsDev.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            miDevCopy,
            miDevSelectAll,
            miDevClear});
            cmsDev.Name = "cmsDev";
            cmsDev.Size = new System.Drawing.Size(170, 70);
            //
            // miDevCopy
            //
            miDevCopy.Name = "miDevCopy";
            miDevCopy.Size = new System.Drawing.Size(169, 22);
            miDevCopy.Text = "Copier";
            miDevCopy.Click += new System.EventHandler(MiDevCopy_Click);
            //
            // miDevSelectAll
            //
            miDevSelectAll.Name = "miDevSelectAll";
            miDevSelectAll.Size = new System.Drawing.Size(169, 22);
            miDevSelectAll.Text = "Tout sélectionner";
            miDevSelectAll.Click += new System.EventHandler(MiDevSelectAll_Click);
            //
            // miDevClear
            //
            miDevClear.Name = "miDevClear";
            miDevClear.Size = new System.Drawing.Size(169, 22);
            miDevClear.Text = "Effacer";
            miDevClear.Click += new System.EventHandler(MiDevClear_Click);
            //
            // gbApp
            //
            gbApp.Controls.Add(rtbApp);
            gbApp.Dock = System.Windows.Forms.DockStyle.Fill;
            gbApp.Location = new System.Drawing.Point(0, 0);
            gbApp.Name = "gbApp";
            gbApp.Size = new System.Drawing.Size(508, 455);
            gbApp.TabIndex = 0;
            gbApp.TabStop = false;
            gbApp.Text = "Host → Device";
            //
            // rtbApp
            //
            rtbApp.BackColor = System.Drawing.SystemColors.Window;
            rtbApp.ContextMenuStrip = cmsApp;
            rtbApp.DetectUrls = false;
            rtbApp.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbApp.Font = new System.Drawing.Font("Consolas", 9.5F);
            rtbApp.Location = new System.Drawing.Point(3, 19);
            rtbApp.Name = "rtbApp";
            rtbApp.ReadOnly = true;
            rtbApp.Size = new System.Drawing.Size(502, 433);
            rtbApp.TabIndex = 0;
            rtbApp.Text = "";
            rtbApp.WordWrap = false;
            //
            // cmsApp
            //
            cmsApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            miAppCopy,
            miAppSelectAll,
            miAppClear});
            cmsApp.Name = "cmsApp";
            cmsApp.Size = new System.Drawing.Size(170, 70);
            //
            // miAppCopy
            //
            miAppCopy.Name = "miAppCopy";
            miAppCopy.Size = new System.Drawing.Size(169, 22);
            miAppCopy.Text = "Copier";
            miAppCopy.Click += new System.EventHandler(MiAppCopy_Click);
            //
            // miAppSelectAll
            //
            miAppSelectAll.Name = "miAppSelectAll";
            miAppSelectAll.Size = new System.Drawing.Size(169, 22);
            miAppSelectAll.Text = "Tout sélectionner";
            miAppSelectAll.Click += new System.EventHandler(MiAppSelectAll_Click);
            //
            // miAppClear
            //
            miAppClear.Name = "miAppClear";
            miAppClear.Size = new System.Drawing.Size(169, 22);
            miAppClear.Text = "Effacer";
            miAppClear.Click += new System.EventHandler(MiAppClear_Click);
            //
            // flpOptions
            //
            flpOptions.AutoSize = true;
            flpOptions.Controls.Add(chkHex);
            flpOptions.Controls.Add(chkAscii);
            flpOptions.Controls.Add(chkTimestamp);
            flpOptions.Controls.Add(chkPause);
            flpOptions.Controls.Add(btnClear);
            flpOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            flpOptions.Location = new System.Drawing.Point(8, 553);
            flpOptions.Margin = new System.Windows.Forms.Padding(0);
            flpOptions.Name = "flpOptions";
            flpOptions.Size = new System.Drawing.Size(1008, 27);
            flpOptions.TabIndex = 2;
            flpOptions.WrapContents = false;
            //
            // chkHex
            //
            chkHex.AutoSize = true;
            chkHex.Checked = true;
            chkHex.CheckState = System.Windows.Forms.CheckState.Checked;
            chkHex.Location = new System.Drawing.Point(3, 3);
            chkHex.Name = "chkHex";
            chkHex.Size = new System.Drawing.Size(92, 19);
            chkHex.TabIndex = 0;
            chkHex.Text = "Hexadécimal";
            chkHex.UseVisualStyleBackColor = true;
            //
            // chkAscii
            //
            chkAscii.AutoSize = true;
            chkAscii.Checked = true;
            chkAscii.CheckState = System.Windows.Forms.CheckState.Checked;
            chkAscii.Location = new System.Drawing.Point(101, 3);
            chkAscii.Name = "chkAscii";
            chkAscii.Size = new System.Drawing.Size(54, 19);
            chkAscii.TabIndex = 1;
            chkAscii.Text = "ASCII";
            chkAscii.UseVisualStyleBackColor = true;
            //
            // chkTimestamp
            //
            chkTimestamp.AutoSize = true;
            chkTimestamp.Checked = true;
            chkTimestamp.CheckState = System.Windows.Forms.CheckState.Checked;
            chkTimestamp.Location = new System.Drawing.Point(161, 3);
            chkTimestamp.Name = "chkTimestamp";
            chkTimestamp.Size = new System.Drawing.Size(87, 19);
            chkTimestamp.TabIndex = 2;
            chkTimestamp.Text = "Horodatage";
            chkTimestamp.UseVisualStyleBackColor = true;
            // 
            // chkPause
            // 
            chkPause.Appearance = System.Windows.Forms.Appearance.Button;
            chkPause.Location = new System.Drawing.Point(258, 0);
            chkPause.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            chkPause.Name = "chkPause";
            chkPause.Size = new System.Drawing.Size(65, 25);
            chkPause.TabIndex = 3;
            chkPause.Text = "⏸ Pause";
            chkPause.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.AutoSize = true;
            btnClear.Location = new System.Drawing.Point(329, 0);
            btnClear.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(65, 25);
            btnClear.TabIndex = 4;
            btnClear.Text = "Effacer";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += BtnClear_Click;
            //
            // statusStrip
            //
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            lblState,
            lblMsg,
            lblRateDev,
            lblRateApp});
            statusStrip.Location = new System.Drawing.Point(0, 618);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(1040, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip";
            //
            // lblState
            //
            lblState.Name = "lblState";
            lblState.Size = new System.Drawing.Size(53, 17);
            lblState.Text = "○ Arrêté";
            //
            // lblMsg
            //
            lblMsg.Name = "lblMsg";
            lblMsg.Size = new System.Drawing.Size(824, 17);
            lblMsg.Spring = true;
            lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblRateDev
            //
            lblRateDev.Name = "lblRateDev";
            lblRateDev.Size = new System.Drawing.Size(51, 17);
            lblRateDev.Text = "↓ 0 o/s";
            //
            // lblRateApp
            //
            lblRateApp.Name = "lblRateApp";
            lblRateApp.Size = new System.Drawing.Size(51, 17);
            lblRateApp.Text = "↑ 0 o/s";
            //
            // uiTimer
            //
            uiTimer.Interval = 250;
            uiTimer.Tick += new System.EventHandler(UiTimer_Tick);
            //
            // MainForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1040, 640);
            Controls.Add(rootTable);
            Controls.Add(statusStrip);
            MinimumSize = new System.Drawing.Size(800, 480);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ComSniffer — Serial/TCP Line Sniffer";
            gbConfig.ResumeLayout(false);
            gbConfig.PerformLayout();
            cfgTable.ResumeLayout(false);
            cfgTable.PerformLayout();
            split.Panel1.ResumeLayout(false);
            split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(split)).EndInit();
            gbDev.ResumeLayout(false);
            gbApp.ResumeLayout(false);
            cmsDev.ResumeLayout(false);
            cmsApp.ResumeLayout(false);
            ResumeLayout(false);

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
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblState;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.ToolStripStatusLabel lblRateDev;
        private System.Windows.Forms.ToolStripStatusLabel lblRateApp;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer uiTimer;
    }
}
