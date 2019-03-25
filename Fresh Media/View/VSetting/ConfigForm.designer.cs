namespace FreshMedia.View.VSetting
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabLyricLabel = new System.Windows.Forms.Label();
            this.sleepGbox = new System.Windows.Forms.GroupBox();
            this.sleepLefttimeLabel = new System.Windows.Forms.Label();
            this.shutdownCheckBox = new System.Windows.Forms.CheckBox();
            this.sleepTimeButton = new System.Windows.Forms.Button();
            this.sleepTimeComboBox = new System.Windows.Forms.ComboBox();
            this.sleepCheckBox = new System.Windows.Forms.CheckBox();
            this.sleepLabel = new System.Windows.Forms.Label();
            this.hotkeyGbox = new System.Windows.Forms.GroupBox();
            this.desklrcGbox = new System.Windows.Forms.GroupBox();
            this.desklrcFontStyleComboBox = new System.Windows.Forms.ComboBox();
            this.desklrcFontStyleLabel = new System.Windows.Forms.Label();
            this.desklrcVisibleCheckBox = new System.Windows.Forms.CheckBox();
            this.desklrcColorComboBox = new System.Windows.Forms.ComboBox();
            this.desklrcPreviewLabel = new System.Windows.Forms.Label();
            this.desklrcCurrentPreviewLabel = new System.Windows.Forms.Label();
            this.desklrcPlayedComboBox = new System.Windows.Forms.ComboBox();
            this.desklrcFontSizeComboBox = new System.Windows.Forms.ComboBox();
            this.desklrcFontComboBox = new System.Windows.Forms.ComboBox();
            this.desklrcLabel = new System.Windows.Forms.Label();
            this.playGbox = new System.Windows.Forms.GroupBox();
            this.rfTimeComboBox = new System.Windows.Forms.ComboBox();
            this.cycleComboBox = new System.Windows.Forms.ComboBox();
            this.playLabel = new System.Windows.Forms.Label();
            this.tabPlayLabel = new System.Windows.Forms.Label();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.tabGeneralLabel = new System.Windows.Forms.Label();
            this.formlrcGbox = new System.Windows.Forms.GroupBox();
            this.formLyricFontStyleComboBox = new System.Windows.Forms.ComboBox();
            this.formlrcFontStyleLabel = new System.Windows.Forms.Label();
            this.formlrcCurrlineColorComboBox = new System.Windows.Forms.ComboBox();
            this.formlrcCurrentPreviewLabel = new System.Windows.Forms.Label();
            this.formlrcPreviewLabel = new System.Windows.Forms.Label();
            this.formlrcFontSizeComboBox = new System.Windows.Forms.ComboBox();
            this.formlrcColorComboBox = new System.Windows.Forms.ComboBox();
            this.formlrcFontComboBox = new System.Windows.Forms.ComboBox();
            this.formlrcPlayedColorComboBox = new System.Windows.Forms.ComboBox();
            this.formlrcLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.startGbox = new System.Windows.Forms.GroupBox();
            this.autoRunCheckBox = new System.Windows.Forms.CheckBox();
            this.blendableCheckBox = new System.Windows.Forms.CheckBox();
            this.exitStyleCheckBox = new System.Windows.Forms.CheckBox();
            this.autoPlayCheckBox = new System.Windows.Forms.CheckBox();
            this.themeComboBox = new System.Windows.Forms.ComboBox();
            this.startBoxCheckBox = new System.Windows.Forms.CheckBox();
            this.norLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.ContentPanel.SuspendLayout();
            this.sleepGbox.SuspendLayout();
            this.desklrcGbox.SuspendLayout();
            this.playGbox.SuspendLayout();
            this.formlrcGbox.SuspendLayout();
            this.startGbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.desklrcGbox);
            this.ContentPanel.Controls.Add(this.hotkeyGbox);
            this.ContentPanel.Controls.Add(this.tabLyricLabel);
            this.ContentPanel.Controls.Add(this.playGbox);
            this.ContentPanel.Controls.Add(this.sleepGbox);
            this.ContentPanel.Controls.Add(this.tabPlayLabel);
            this.ContentPanel.Controls.Add(this.pagePanel);
            this.ContentPanel.Controls.Add(this.tabGeneralLabel);
            this.ContentPanel.Controls.Add(this.formlrcGbox);
            this.ContentPanel.Controls.Add(this.okButton);
            this.ContentPanel.Controls.Add(this.startGbox);
            this.ContentPanel.Controls.Add(this.cancelButton);
            this.ContentPanel.Controls.Add(this.applyButton);
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(4, 32);
            this.ContentPanel.Size = new System.Drawing.Size(2160, 860);
            // 
            // tabLyricLabel
            // 
            this.tabLyricLabel.BackColor = System.Drawing.Color.Transparent;
            this.tabLyricLabel.Location = new System.Drawing.Point(432, 16);
            this.tabLyricLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tabLyricLabel.Name = "tabLyricLabel";
            this.tabLyricLabel.Size = new System.Drawing.Size(120, 24);
            this.tabLyricLabel.TabIndex = 2;
            this.tabLyricLabel.Text = "歌词设置";
            this.tabLyricLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tabLyricLabel.Click += new System.EventHandler(this.tabs_Click);
            this.tabLyricLabel.MouseEnter += new System.EventHandler(this.tabs_MouseEnter);
            this.tabLyricLabel.MouseLeave += new System.EventHandler(this.tabs_MouseLeave);
            // 
            // sleepGbox
            // 
            this.sleepGbox.BackColor = System.Drawing.Color.Transparent;
            this.sleepGbox.Controls.Add(this.sleepLefttimeLabel);
            this.sleepGbox.Controls.Add(this.shutdownCheckBox);
            this.sleepGbox.Controls.Add(this.sleepTimeButton);
            this.sleepGbox.Controls.Add(this.sleepTimeComboBox);
            this.sleepGbox.Controls.Add(this.sleepCheckBox);
            this.sleepGbox.Controls.Add(this.sleepLabel);
            this.sleepGbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sleepGbox.Location = new System.Drawing.Point(1397, 56);
            this.sleepGbox.Margin = new System.Windows.Forms.Padding(4);
            this.sleepGbox.Name = "sleepGbox";
            this.sleepGbox.Padding = new System.Windows.Forms.Padding(4);
            this.sleepGbox.Size = new System.Drawing.Size(747, 281);
            this.sleepGbox.TabIndex = 1;
            this.sleepGbox.TabStop = false;
            this.sleepGbox.Text = "睡眠模式";
            this.sleepGbox.Visible = false;
            // 
            // sleepLefttimeLabel
            // 
            this.sleepLefttimeLabel.AutoSize = true;
            this.sleepLefttimeLabel.ForeColor = System.Drawing.Color.Red;
            this.sleepLefttimeLabel.Location = new System.Drawing.Point(264, 40);
            this.sleepLefttimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sleepLefttimeLabel.Name = "sleepLefttimeLabel";
            this.sleepLefttimeLabel.Size = new System.Drawing.Size(0, 16);
            this.sleepLefttimeLabel.TabIndex = 8;
            // 
            // shutdownCheckBox
            // 
            this.shutdownCheckBox.AutoSize = true;
            this.shutdownCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shutdownCheckBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.shutdownCheckBox.Location = new System.Drawing.Point(168, 116);
            this.shutdownCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.shutdownCheckBox.Name = "shutdownCheckBox";
            this.shutdownCheckBox.Size = new System.Drawing.Size(77, 20);
            this.shutdownCheckBox.TabIndex = 7;
            this.shutdownCheckBox.Text = "不关机";
            this.shutdownCheckBox.UseVisualStyleBackColor = true;
            // 
            // sleepTimeButton
            // 
            this.sleepTimeButton.BackColor = System.Drawing.Color.Transparent;
            this.sleepTimeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sleepTimeButton.Location = new System.Drawing.Point(609, 70);
            this.sleepTimeButton.Margin = new System.Windows.Forms.Padding(4);
            this.sleepTimeButton.Name = "sleepTimeButton";
            this.sleepTimeButton.Size = new System.Drawing.Size(112, 33);
            this.sleepTimeButton.TabIndex = 5;
            this.sleepTimeButton.Text = "自定义";
            this.sleepTimeButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.sleepTimeButton.UseVisualStyleBackColor = false;
            this.sleepTimeButton.Click += new System.EventHandler(this.diyStopTimeButton_Click);
            // 
            // sleepTimeComboBox
            // 
            this.sleepTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sleepTimeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sleepTimeComboBox.ForeColor = System.Drawing.Color.OrangeRed;
            this.sleepTimeComboBox.FormattingEnabled = true;
            this.sleepTimeComboBox.ItemHeight = 16;
            this.sleepTimeComboBox.Location = new System.Drawing.Point(168, 74);
            this.sleepTimeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.sleepTimeComboBox.Name = "sleepTimeComboBox";
            this.sleepTimeComboBox.Size = new System.Drawing.Size(394, 24);
            this.sleepTimeComboBox.TabIndex = 4;
            // 
            // sleepCheckBox
            // 
            this.sleepCheckBox.AutoSize = true;
            this.sleepCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sleepCheckBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.sleepCheckBox.Location = new System.Drawing.Point(168, 38);
            this.sleepCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.sleepCheckBox.Name = "sleepCheckBox";
            this.sleepCheckBox.Size = new System.Drawing.Size(77, 20);
            this.sleepCheckBox.TabIndex = 2;
            this.sleepCheckBox.Text = "未激活";
            this.sleepCheckBox.UseVisualStyleBackColor = true;
            // 
            // sleepLabel
            // 
            this.sleepLabel.AutoSize = true;
            this.sleepLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.sleepLabel.Location = new System.Drawing.Point(8, 39);
            this.sleepLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sleepLabel.Name = "sleepLabel";
            this.sleepLabel.Size = new System.Drawing.Size(120, 80);
            this.sleepLabel.TabIndex = 0;
            this.sleepLabel.Text = "激活睡眠模式：\r\n\r\n停止时间：\r\n\r\n是否关机：";
            this.sleepLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hotkeyGbox
            // 
            this.hotkeyGbox.BackColor = System.Drawing.Color.Transparent;
            this.hotkeyGbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hotkeyGbox.Location = new System.Drawing.Point(1868, 349);
            this.hotkeyGbox.Margin = new System.Windows.Forms.Padding(4);
            this.hotkeyGbox.Name = "hotkeyGbox";
            this.hotkeyGbox.Padding = new System.Windows.Forms.Padding(4);
            this.hotkeyGbox.Size = new System.Drawing.Size(276, 313);
            this.hotkeyGbox.TabIndex = 16;
            this.hotkeyGbox.TabStop = false;
            this.hotkeyGbox.Text = "全局热键";
            this.hotkeyGbox.Visible = false;
            // 
            // desklrcGbox
            // 
            this.desklrcGbox.BackColor = System.Drawing.Color.Transparent;
            this.desklrcGbox.Controls.Add(this.desklrcFontStyleComboBox);
            this.desklrcGbox.Controls.Add(this.desklrcFontStyleLabel);
            this.desklrcGbox.Controls.Add(this.desklrcVisibleCheckBox);
            this.desklrcGbox.Controls.Add(this.desklrcColorComboBox);
            this.desklrcGbox.Controls.Add(this.desklrcPreviewLabel);
            this.desklrcGbox.Controls.Add(this.desklrcCurrentPreviewLabel);
            this.desklrcGbox.Controls.Add(this.desklrcPlayedComboBox);
            this.desklrcGbox.Controls.Add(this.desklrcFontSizeComboBox);
            this.desklrcGbox.Controls.Add(this.desklrcFontComboBox);
            this.desklrcGbox.Controls.Add(this.desklrcLabel);
            this.desklrcGbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.desklrcGbox.Location = new System.Drawing.Point(1026, 349);
            this.desklrcGbox.Margin = new System.Windows.Forms.Padding(4);
            this.desklrcGbox.Name = "desklrcGbox";
            this.desklrcGbox.Padding = new System.Windows.Forms.Padding(4);
            this.desklrcGbox.Size = new System.Drawing.Size(834, 313);
            this.desklrcGbox.TabIndex = 14;
            this.desklrcGbox.TabStop = false;
            this.desklrcGbox.Text = "桌面歌词";
            this.desklrcGbox.Visible = false;
            // 
            // desklrcFontStyleComboBox
            // 
            this.desklrcFontStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.desklrcFontStyleComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.desklrcFontStyleComboBox.FormattingEnabled = true;
            this.desklrcFontStyleComboBox.Location = new System.Drawing.Point(399, 74);
            this.desklrcFontStyleComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.desklrcFontStyleComboBox.Name = "desklrcFontStyleComboBox";
            this.desklrcFontStyleComboBox.Size = new System.Drawing.Size(160, 24);
            this.desklrcFontStyleComboBox.TabIndex = 18;
            // 
            // desklrcFontStyleLabel
            // 
            this.desklrcFontStyleLabel.AutoSize = true;
            this.desklrcFontStyleLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.desklrcFontStyleLabel.Location = new System.Drawing.Point(328, 80);
            this.desklrcFontStyleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.desklrcFontStyleLabel.Name = "desklrcFontStyleLabel";
            this.desklrcFontStyleLabel.Size = new System.Drawing.Size(56, 16);
            this.desklrcFontStyleLabel.TabIndex = 17;
            this.desklrcFontStyleLabel.Text = "样式：";
            // 
            // desklrcVisibleCheckBox
            // 
            this.desklrcVisibleCheckBox.AutoSize = true;
            this.desklrcVisibleCheckBox.Checked = true;
            this.desklrcVisibleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.desklrcVisibleCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.desklrcVisibleCheckBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.desklrcVisibleCheckBox.Location = new System.Drawing.Point(159, 232);
            this.desklrcVisibleCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.desklrcVisibleCheckBox.Name = "desklrcVisibleCheckBox";
            this.desklrcVisibleCheckBox.Size = new System.Drawing.Size(61, 20);
            this.desklrcVisibleCheckBox.TabIndex = 14;
            this.desklrcVisibleCheckBox.Text = "启用";
            this.desklrcVisibleCheckBox.UseVisualStyleBackColor = true;
            // 
            // desklrcColorComboBox
            // 
            this.desklrcColorComboBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.desklrcColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.desklrcColorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.desklrcColorComboBox.FormattingEnabled = true;
            this.desklrcColorComboBox.Location = new System.Drawing.Point(159, 190);
            this.desklrcColorComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.desklrcColorComboBox.Name = "desklrcColorComboBox";
            this.desklrcColorComboBox.Size = new System.Drawing.Size(400, 24);
            this.desklrcColorComboBox.TabIndex = 10;
            // 
            // desklrcPreviewLabel
            // 
            this.desklrcPreviewLabel.BackColor = System.Drawing.Color.Transparent;
            this.desklrcPreviewLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.desklrcPreviewLabel.Location = new System.Drawing.Point(579, 190);
            this.desklrcPreviewLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.desklrcPreviewLabel.Name = "desklrcPreviewLabel";
            this.desklrcPreviewLabel.Size = new System.Drawing.Size(228, 68);
            this.desklrcPreviewLabel.TabIndex = 13;
            this.desklrcPreviewLabel.Text = "非播放行";
            this.desklrcPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // desklrcCurrentPreviewLabel
            // 
            this.desklrcCurrentPreviewLabel.BackColor = System.Drawing.Color.Transparent;
            this.desklrcCurrentPreviewLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.desklrcCurrentPreviewLabel.Location = new System.Drawing.Point(579, 96);
            this.desklrcCurrentPreviewLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.desklrcCurrentPreviewLabel.Name = "desklrcCurrentPreviewLabel";
            this.desklrcCurrentPreviewLabel.Size = new System.Drawing.Size(228, 74);
            this.desklrcCurrentPreviewLabel.TabIndex = 12;
            this.desklrcCurrentPreviewLabel.Tag = "播放行";
            this.desklrcCurrentPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.desklrcCurrentPreviewLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.desklrcPreview);
            // 
            // desklrcPlayedComboBox
            // 
            this.desklrcPlayedComboBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.desklrcPlayedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.desklrcPlayedComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.desklrcPlayedComboBox.FormattingEnabled = true;
            this.desklrcPlayedComboBox.Location = new System.Drawing.Point(159, 152);
            this.desklrcPlayedComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.desklrcPlayedComboBox.Name = "desklrcPlayedComboBox";
            this.desklrcPlayedComboBox.Size = new System.Drawing.Size(400, 24);
            this.desklrcPlayedComboBox.TabIndex = 8;
            // 
            // desklrcFontSizeComboBox
            // 
            this.desklrcFontSizeComboBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.desklrcFontSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.desklrcFontSizeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.desklrcFontSizeComboBox.FormattingEnabled = true;
            this.desklrcFontSizeComboBox.Location = new System.Drawing.Point(159, 74);
            this.desklrcFontSizeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.desklrcFontSizeComboBox.Name = "desklrcFontSizeComboBox";
            this.desklrcFontSizeComboBox.Size = new System.Drawing.Size(160, 24);
            this.desklrcFontSizeComboBox.TabIndex = 10;
            // 
            // desklrcFontComboBox
            // 
            this.desklrcFontComboBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.desklrcFontComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.desklrcFontComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.desklrcFontComboBox.FormattingEnabled = true;
            this.desklrcFontComboBox.Location = new System.Drawing.Point(159, 34);
            this.desklrcFontComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.desklrcFontComboBox.Name = "desklrcFontComboBox";
            this.desklrcFontComboBox.Size = new System.Drawing.Size(400, 24);
            this.desklrcFontComboBox.TabIndex = 8;
            // 
            // desklrcLabel
            // 
            this.desklrcLabel.AutoSize = true;
            this.desklrcLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.desklrcLabel.Location = new System.Drawing.Point(44, 39);
            this.desklrcLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.desklrcLabel.Name = "desklrcLabel";
            this.desklrcLabel.Size = new System.Drawing.Size(96, 176);
            this.desklrcLabel.TabIndex = 7;
            this.desklrcLabel.Text = " 字    体：\r\n\r\n字体大小：\r\n\r\n\r\n\r\n已 播 放：\r\n\r\n未 播 放：\r\n\r\n桌面歌词：";
            this.desklrcLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // playGbox
            // 
            this.playGbox.BackColor = System.Drawing.Color.Transparent;
            this.playGbox.Controls.Add(this.rfTimeComboBox);
            this.playGbox.Controls.Add(this.cycleComboBox);
            this.playGbox.Controls.Add(this.playLabel);
            this.playGbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playGbox.Location = new System.Drawing.Point(800, 56);
            this.playGbox.Margin = new System.Windows.Forms.Padding(4);
            this.playGbox.Name = "playGbox";
            this.playGbox.Padding = new System.Windows.Forms.Padding(4);
            this.playGbox.Size = new System.Drawing.Size(589, 281);
            this.playGbox.TabIndex = 1;
            this.playGbox.TabStop = false;
            this.playGbox.Text = "播放设置";
            // 
            // rfTimeComboBox
            // 
            this.rfTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rfTimeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rfTimeComboBox.FormattingEnabled = true;
            this.rfTimeComboBox.Location = new System.Drawing.Point(159, 74);
            this.rfTimeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.rfTimeComboBox.Name = "rfTimeComboBox";
            this.rfTimeComboBox.Size = new System.Drawing.Size(400, 24);
            this.rfTimeComboBox.TabIndex = 6;
            // 
            // cycleComboBox
            // 
            this.cycleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cycleComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cycleComboBox.FormattingEnabled = true;
            this.cycleComboBox.Location = new System.Drawing.Point(159, 34);
            this.cycleComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.cycleComboBox.Name = "cycleComboBox";
            this.cycleComboBox.Size = new System.Drawing.Size(400, 24);
            this.cycleComboBox.TabIndex = 4;
            // 
            // playLabel
            // 
            this.playLabel.AutoSize = true;
            this.playLabel.Location = new System.Drawing.Point(44, 39);
            this.playLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playLabel.Name = "playLabel";
            this.playLabel.Size = new System.Drawing.Size(88, 48);
            this.playLabel.TabIndex = 0;
            this.playLabel.Text = "循环模式：\r\n\r\nRF(ms)：";
            this.playLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPlayLabel
            // 
            this.tabPlayLabel.BackColor = System.Drawing.Color.Transparent;
            this.tabPlayLabel.Location = new System.Drawing.Point(297, 16);
            this.tabPlayLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tabPlayLabel.Name = "tabPlayLabel";
            this.tabPlayLabel.Size = new System.Drawing.Size(120, 24);
            this.tabPlayLabel.TabIndex = 1;
            this.tabPlayLabel.Text = "播放设置";
            this.tabPlayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tabPlayLabel.Click += new System.EventHandler(this.tabs_Click);
            this.tabPlayLabel.MouseEnter += new System.EventHandler(this.tabs_MouseEnter);
            this.tabPlayLabel.MouseLeave += new System.EventHandler(this.tabs_MouseLeave);
            // 
            // pagePanel
            // 
            this.pagePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pagePanel.Location = new System.Drawing.Point(0, 0);
            this.pagePanel.Margin = new System.Windows.Forms.Padding(4);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(160, 846);
            this.pagePanel.TabIndex = 0;
            // 
            // tabGeneralLabel
            // 
            this.tabGeneralLabel.BackColor = System.Drawing.Color.Transparent;
            this.tabGeneralLabel.Location = new System.Drawing.Point(168, 16);
            this.tabGeneralLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tabGeneralLabel.Name = "tabGeneralLabel";
            this.tabGeneralLabel.Size = new System.Drawing.Size(120, 24);
            this.tabGeneralLabel.TabIndex = 0;
            this.tabGeneralLabel.Text = "常规设置";
            this.tabGeneralLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tabGeneralLabel.Click += new System.EventHandler(this.tabs_Click);
            this.tabGeneralLabel.MouseEnter += new System.EventHandler(this.tabs_MouseEnter);
            this.tabGeneralLabel.MouseLeave += new System.EventHandler(this.tabs_MouseLeave);
            // 
            // formlrcGbox
            // 
            this.formlrcGbox.BackColor = System.Drawing.Color.Transparent;
            this.formlrcGbox.Controls.Add(this.formLyricFontStyleComboBox);
            this.formlrcGbox.Controls.Add(this.formlrcFontStyleLabel);
            this.formlrcGbox.Controls.Add(this.formlrcCurrlineColorComboBox);
            this.formlrcGbox.Controls.Add(this.formlrcCurrentPreviewLabel);
            this.formlrcGbox.Controls.Add(this.formlrcPreviewLabel);
            this.formlrcGbox.Controls.Add(this.formlrcFontSizeComboBox);
            this.formlrcGbox.Controls.Add(this.formlrcColorComboBox);
            this.formlrcGbox.Controls.Add(this.formlrcFontComboBox);
            this.formlrcGbox.Controls.Add(this.formlrcPlayedColorComboBox);
            this.formlrcGbox.Controls.Add(this.formlrcLabel);
            this.formlrcGbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formlrcGbox.Location = new System.Drawing.Point(184, 349);
            this.formlrcGbox.Margin = new System.Windows.Forms.Padding(4);
            this.formlrcGbox.Name = "formlrcGbox";
            this.formlrcGbox.Padding = new System.Windows.Forms.Padding(4);
            this.formlrcGbox.Size = new System.Drawing.Size(834, 313);
            this.formlrcGbox.TabIndex = 0;
            this.formlrcGbox.TabStop = false;
            this.formlrcGbox.Text = "窗口歌词";
            // 
            // formLyricFontStyleComboBox
            // 
            this.formLyricFontStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formLyricFontStyleComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formLyricFontStyleComboBox.FormattingEnabled = true;
            this.formLyricFontStyleComboBox.Location = new System.Drawing.Point(399, 74);
            this.formLyricFontStyleComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.formLyricFontStyleComboBox.Name = "formLyricFontStyleComboBox";
            this.formLyricFontStyleComboBox.Size = new System.Drawing.Size(160, 24);
            this.formLyricFontStyleComboBox.TabIndex = 16;
            // 
            // formlrcFontStyleLabel
            // 
            this.formlrcFontStyleLabel.AutoSize = true;
            this.formlrcFontStyleLabel.BackColor = System.Drawing.Color.Transparent;
            this.formlrcFontStyleLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.formlrcFontStyleLabel.Location = new System.Drawing.Point(328, 80);
            this.formlrcFontStyleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.formlrcFontStyleLabel.Name = "formlrcFontStyleLabel";
            this.formlrcFontStyleLabel.Size = new System.Drawing.Size(56, 16);
            this.formlrcFontStyleLabel.TabIndex = 15;
            this.formlrcFontStyleLabel.Text = "样式：";
            // 
            // formlrcCurrlineColorComboBox
            // 
            this.formlrcCurrlineColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formlrcCurrlineColorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formlrcCurrlineColorComboBox.FormattingEnabled = true;
            this.formlrcCurrlineColorComboBox.Location = new System.Drawing.Point(159, 190);
            this.formlrcCurrlineColorComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.formlrcCurrlineColorComboBox.Name = "formlrcCurrlineColorComboBox";
            this.formlrcCurrlineColorComboBox.Size = new System.Drawing.Size(400, 24);
            this.formlrcCurrlineColorComboBox.TabIndex = 14;
            // 
            // formlrcCurrentPreviewLabel
            // 
            this.formlrcCurrentPreviewLabel.Location = new System.Drawing.Point(579, 96);
            this.formlrcCurrentPreviewLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.formlrcCurrentPreviewLabel.Name = "formlrcCurrentPreviewLabel";
            this.formlrcCurrentPreviewLabel.Size = new System.Drawing.Size(228, 76);
            this.formlrcCurrentPreviewLabel.TabIndex = 11;
            this.formlrcCurrentPreviewLabel.Tag = "播放行";
            this.formlrcCurrentPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.formlrcCurrentPreviewLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.formlrcStylePreview);
            // 
            // formlrcPreviewLabel
            // 
            this.formlrcPreviewLabel.Location = new System.Drawing.Point(579, 190);
            this.formlrcPreviewLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.formlrcPreviewLabel.Name = "formlrcPreviewLabel";
            this.formlrcPreviewLabel.Size = new System.Drawing.Size(228, 76);
            this.formlrcPreviewLabel.TabIndex = 10;
            this.formlrcPreviewLabel.Text = "非播放行";
            this.formlrcPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formlrcFontSizeComboBox
            // 
            this.formlrcFontSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formlrcFontSizeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formlrcFontSizeComboBox.FormattingEnabled = true;
            this.formlrcFontSizeComboBox.Location = new System.Drawing.Point(159, 74);
            this.formlrcFontSizeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.formlrcFontSizeComboBox.Name = "formlrcFontSizeComboBox";
            this.formlrcFontSizeComboBox.Size = new System.Drawing.Size(160, 24);
            this.formlrcFontSizeComboBox.TabIndex = 6;
            // 
            // formlrcColorComboBox
            // 
            this.formlrcColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formlrcColorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formlrcColorComboBox.FormattingEnabled = true;
            this.formlrcColorComboBox.Location = new System.Drawing.Point(159, 230);
            this.formlrcColorComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.formlrcColorComboBox.Name = "formlrcColorComboBox";
            this.formlrcColorComboBox.Size = new System.Drawing.Size(400, 24);
            this.formlrcColorComboBox.TabIndex = 10;
            // 
            // formlrcFontComboBox
            // 
            this.formlrcFontComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formlrcFontComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formlrcFontComboBox.FormattingEnabled = true;
            this.formlrcFontComboBox.Location = new System.Drawing.Point(159, 34);
            this.formlrcFontComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.formlrcFontComboBox.Name = "formlrcFontComboBox";
            this.formlrcFontComboBox.Size = new System.Drawing.Size(400, 24);
            this.formlrcFontComboBox.TabIndex = 4;
            // 
            // formlrcPlayedColorComboBox
            // 
            this.formlrcPlayedColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formlrcPlayedColorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formlrcPlayedColorComboBox.FormattingEnabled = true;
            this.formlrcPlayedColorComboBox.Location = new System.Drawing.Point(159, 152);
            this.formlrcPlayedColorComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.formlrcPlayedColorComboBox.Name = "formlrcPlayedColorComboBox";
            this.formlrcPlayedColorComboBox.Size = new System.Drawing.Size(400, 24);
            this.formlrcPlayedColorComboBox.TabIndex = 8;
            // 
            // formlrcLabel
            // 
            this.formlrcLabel.AutoSize = true;
            this.formlrcLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.formlrcLabel.Location = new System.Drawing.Point(44, 39);
            this.formlrcLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.formlrcLabel.Name = "formlrcLabel";
            this.formlrcLabel.Size = new System.Drawing.Size(88, 176);
            this.formlrcLabel.TabIndex = 0;
            this.formlrcLabel.Text = " 字体：\r\n\r\n字体大小：\r\n\r\n\r\n\r\n已 播 放：\r\n\r\n播 放 行：\r\n\r\n未 播 放：";
            this.formlrcLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.okButton.Location = new System.Drawing.Point(1823, 800);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 33);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "确定";
            this.okButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // startGbox
            // 
            this.startGbox.BackColor = System.Drawing.Color.Transparent;
            this.startGbox.Controls.Add(this.autoRunCheckBox);
            this.startGbox.Controls.Add(this.blendableCheckBox);
            this.startGbox.Controls.Add(this.exitStyleCheckBox);
            this.startGbox.Controls.Add(this.autoPlayCheckBox);
            this.startGbox.Controls.Add(this.themeComboBox);
            this.startGbox.Controls.Add(this.startBoxCheckBox);
            this.startGbox.Controls.Add(this.norLabel);
            this.startGbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startGbox.Location = new System.Drawing.Point(184, 56);
            this.startGbox.Margin = new System.Windows.Forms.Padding(4);
            this.startGbox.Name = "startGbox";
            this.startGbox.Padding = new System.Windows.Forms.Padding(4);
            this.startGbox.Size = new System.Drawing.Size(592, 281);
            this.startGbox.TabIndex = 0;
            this.startGbox.TabStop = false;
            this.startGbox.Text = "程序设置";
            // 
            // autoRunCheckBox
            // 
            this.autoRunCheckBox.AutoSize = true;
            this.autoRunCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.autoRunCheckBox.Checked = true;
            this.autoRunCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoRunCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autoRunCheckBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.autoRunCheckBox.Location = new System.Drawing.Point(168, 231);
            this.autoRunCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.autoRunCheckBox.Name = "autoRunCheckBox";
            this.autoRunCheckBox.Size = new System.Drawing.Size(61, 20);
            this.autoRunCheckBox.TabIndex = 8;
            this.autoRunCheckBox.Text = "启动";
            this.autoRunCheckBox.UseVisualStyleBackColor = false;
            // 
            // blendableCheckBox
            // 
            this.blendableCheckBox.AutoSize = true;
            this.blendableCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.blendableCheckBox.Checked = true;
            this.blendableCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.blendableCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blendableCheckBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.blendableCheckBox.Location = new System.Drawing.Point(168, 192);
            this.blendableCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.blendableCheckBox.Name = "blendableCheckBox";
            this.blendableCheckBox.Size = new System.Drawing.Size(61, 20);
            this.blendableCheckBox.TabIndex = 7;
            this.blendableCheckBox.Text = "启用";
            this.blendableCheckBox.UseVisualStyleBackColor = false;
            // 
            // exitStyleCheckBox
            // 
            this.exitStyleCheckBox.AutoSize = true;
            this.exitStyleCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.exitStyleCheckBox.Checked = true;
            this.exitStyleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.exitStyleCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitStyleCheckBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.exitStyleCheckBox.Location = new System.Drawing.Point(168, 153);
            this.exitStyleCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.exitStyleCheckBox.Name = "exitStyleCheckBox";
            this.exitStyleCheckBox.Size = new System.Drawing.Size(93, 20);
            this.exitStyleCheckBox.TabIndex = 6;
            this.exitStyleCheckBox.Text = "有提示框";
            this.exitStyleCheckBox.UseVisualStyleBackColor = false;
            // 
            // autoPlayCheckBox
            // 
            this.autoPlayCheckBox.AutoSize = true;
            this.autoPlayCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.autoPlayCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autoPlayCheckBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.autoPlayCheckBox.Location = new System.Drawing.Point(168, 116);
            this.autoPlayCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.autoPlayCheckBox.Name = "autoPlayCheckBox";
            this.autoPlayCheckBox.Size = new System.Drawing.Size(77, 20);
            this.autoPlayCheckBox.TabIndex = 5;
            this.autoPlayCheckBox.Text = "不播放";
            this.autoPlayCheckBox.UseVisualStyleBackColor = false;
            // 
            // themeComboBox
            // 
            this.themeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.themeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.themeComboBox.FormattingEnabled = true;
            this.themeComboBox.Location = new System.Drawing.Point(168, 74);
            this.themeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.themeComboBox.Name = "themeComboBox";
            this.themeComboBox.Size = new System.Drawing.Size(394, 24);
            this.themeComboBox.TabIndex = 3;
            // 
            // startBoxCheckBox
            // 
            this.startBoxCheckBox.AutoSize = true;
            this.startBoxCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.startBoxCheckBox.Checked = true;
            this.startBoxCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startBoxCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startBoxCheckBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.startBoxCheckBox.Location = new System.Drawing.Point(168, 38);
            this.startBoxCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.startBoxCheckBox.Name = "startBoxCheckBox";
            this.startBoxCheckBox.Size = new System.Drawing.Size(61, 20);
            this.startBoxCheckBox.TabIndex = 1;
            this.startBoxCheckBox.Text = "启用";
            this.startBoxCheckBox.UseVisualStyleBackColor = false;
            // 
            // norLabel
            // 
            this.norLabel.AutoSize = true;
            this.norLabel.BackColor = System.Drawing.Color.Transparent;
            this.norLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.norLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.norLabel.Location = new System.Drawing.Point(44, 39);
            this.norLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.norLabel.Name = "norLabel";
            this.norLabel.Size = new System.Drawing.Size(88, 176);
            this.norLabel.TabIndex = 0;
            this.norLabel.Text = "启动画面：\r\n\r\n主题：\r\n\r\n自动播放：\r\n\r\n退出模式：\r\n\r\n淡入淡出：\r\n\r\n开机启动：";
            this.norLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.cancelButton.Location = new System.Drawing.Point(2045, 800);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 33);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "取消";
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // applyButton
            // 
            this.applyButton.BackColor = System.Drawing.Color.Transparent;
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.applyButton.Location = new System.Drawing.Point(1934, 800);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(112, 33);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "应用";
            this.applyButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(2168, 896);
            this.Name = "ConfigForm";
            this.Opacity = 0.86D;
            this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.this_Load);
            this.Shown += new System.EventHandler(this.this_Shown);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.sleepGbox.ResumeLayout(false);
            this.sleepGbox.PerformLayout();
            this.desklrcGbox.ResumeLayout(false);
            this.desklrcGbox.PerformLayout();
            this.playGbox.ResumeLayout(false);
            this.playGbox.PerformLayout();
            this.formlrcGbox.ResumeLayout(false);
            this.formlrcGbox.PerformLayout();
            this.startGbox.ResumeLayout(false);
            this.startGbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pagePanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label tabPlayLabel;
        private System.Windows.Forms.Label tabGeneralLabel;
        private System.Windows.Forms.GroupBox playGbox;
        private System.Windows.Forms.ComboBox cycleComboBox;
        private System.Windows.Forms.Label playLabel;
        private System.Windows.Forms.GroupBox formlrcGbox;
        private System.Windows.Forms.ComboBox formlrcPlayedColorComboBox;
        private System.Windows.Forms.ComboBox formlrcFontSizeComboBox;
        private System.Windows.Forms.ComboBox formlrcFontComboBox;
        private System.Windows.Forms.Label formlrcLabel;
        private System.Windows.Forms.ComboBox formlrcColorComboBox;
        private System.Windows.Forms.Label formlrcPreviewLabel;
        private System.Windows.Forms.Label formlrcCurrentPreviewLabel;
        private System.Windows.Forms.GroupBox desklrcGbox;
        private System.Windows.Forms.ComboBox desklrcColorComboBox;
        private System.Windows.Forms.ComboBox desklrcPlayedComboBox;
        private System.Windows.Forms.ComboBox desklrcFontSizeComboBox;
        private System.Windows.Forms.ComboBox desklrcFontComboBox;
        private System.Windows.Forms.Label desklrcLabel;
        private System.Windows.Forms.CheckBox desklrcVisibleCheckBox;
        private System.Windows.Forms.Label desklrcPreviewLabel;
        private System.Windows.Forms.Label desklrcCurrentPreviewLabel;
        private System.Windows.Forms.ComboBox formlrcCurrlineColorComboBox;
        private System.Windows.Forms.ComboBox formLyricFontStyleComboBox;
        private System.Windows.Forms.Label formlrcFontStyleLabel;
        private System.Windows.Forms.ComboBox desklrcFontStyleComboBox;
        private System.Windows.Forms.Label desklrcFontStyleLabel;
        private System.Windows.Forms.ComboBox rfTimeComboBox;
        internal System.Windows.Forms.Label tabLyricLabel;
        private System.Windows.Forms.GroupBox startGbox;
        private System.Windows.Forms.CheckBox exitStyleCheckBox;
        private System.Windows.Forms.GroupBox sleepGbox;
        public System.Windows.Forms.Label sleepLefttimeLabel;
        private System.Windows.Forms.GroupBox hotkeyGbox;
        private System.Windows.Forms.CheckBox shutdownCheckBox;
        private System.Windows.Forms.Button sleepTimeButton;
        public System.Windows.Forms.ComboBox sleepTimeComboBox;
        private System.Windows.Forms.CheckBox sleepCheckBox;
        private System.Windows.Forms.Label sleepLabel;
        private System.Windows.Forms.CheckBox autoPlayCheckBox;
        private System.Windows.Forms.ComboBox themeComboBox;
        private System.Windows.Forms.CheckBox startBoxCheckBox;
        private System.Windows.Forms.Label norLabel;
        private System.Windows.Forms.CheckBox blendableCheckBox;
        private System.Windows.Forms.CheckBox autoRunCheckBox;
    }
}