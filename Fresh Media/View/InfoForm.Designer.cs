namespace FreshMedia.View
{
    partial class InfoForm
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
            this.lyricGbox = new System.Windows.Forms.GroupBox();
            this.openlyricButton = new System.Windows.Forms.Button();
            this.lyricLocButton = new System.Windows.Forms.Button();
            this.lyricTxtBox = new System.Windows.Forms.TextBox();
            this.lyricLabel = new System.Windows.Forms.Label();
            this.otherInfoGbox = new System.Windows.Forms.GroupBox();
            this.versonTxtBox = new System.Windows.Forms.TextBox();
            this.copyRightTxtBox = new System.Windows.Forms.TextBox();
            this.versonLabel = new System.Windows.Forms.Label();
            this.copyRightLabel = new System.Windows.Forms.Label();
            this.hashTxtBox = new System.Windows.Forms.TextBox();
            this.gainTxtBox = new System.Windows.Forms.TextBox();
            this.hashLabel = new System.Windows.Forms.Label();
            this.gainLabel = new System.Windows.Forms.Label();
            this.kbpsTxtBox = new System.Windows.Forms.TextBox();
            this.khzTxtBox = new System.Windows.Forms.TextBox();
            this.kbpsLabel = new System.Windows.Forms.Label();
            this.khzLabel = new System.Windows.Forms.Label();
            this.channelsTxtBox = new System.Windows.Forms.TextBox();
            this.tracksTxtBox = new System.Windows.Forms.TextBox();
            this.channelsLabel = new System.Windows.Forms.Label();
            this.tracksLabel = new System.Windows.Forms.Label();
            this.fileInfoGbox = new System.Windows.Forms.GroupBox();
            this.playTimesTxtBox = new System.Windows.Forms.TextBox();
            this.timeTxtBox = new System.Windows.Forms.TextBox();
            this.playTimesLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.albumTxtBox = new System.Windows.Forms.TextBox();
            this.singerTxtBox = new System.Windows.Forms.TextBox();
            this.albumLabel = new System.Windows.Forms.Label();
            this.singerLabel = new System.Windows.Forms.Label();
            this.creationTimeTxtBox = new System.Windows.Forms.TextBox();
            this.copyButton = new System.Windows.Forms.Button();
            this.locButton = new System.Windows.Forms.Button();
            this.lengthTxtBox = new System.Windows.Forms.TextBox();
            this.creationTimeLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.tBox_type = new System.Windows.Forms.TextBox();
            this.pathTxtBox = new System.Windows.Forms.TextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.tBox_name = new System.Windows.Forms.TextBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.ContentPanel.SuspendLayout();
            this.lyricGbox.SuspendLayout();
            this.otherInfoGbox.SuspendLayout();
            this.fileInfoGbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.lyricGbox);
            this.ContentPanel.Controls.Add(this.otherInfoGbox);
            this.ContentPanel.Controls.Add(this.fileInfoGbox);
            this.ContentPanel.Controls.Add(this.backButton);
            this.ContentPanel.Controls.Add(this.playButton);
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(2, 20);
            this.ContentPanel.Size = new System.Drawing.Size(651, 455);
            this.ContentPanel.SizeChanged += new System.EventHandler(this.this_SizeChanged);
            // 
            // lyricGbox
            // 
            this.lyricGbox.BackColor = System.Drawing.Color.Transparent;
            this.lyricGbox.Controls.Add(this.openlyricButton);
            this.lyricGbox.Controls.Add(this.lyricLocButton);
            this.lyricGbox.Controls.Add(this.lyricTxtBox);
            this.lyricGbox.Controls.Add(this.lyricLabel);
            this.lyricGbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lyricGbox.ForeColor = System.Drawing.Color.Teal;
            this.lyricGbox.Location = new System.Drawing.Point(10, 344);
            this.lyricGbox.Name = "lyricGbox";
            this.lyricGbox.Size = new System.Drawing.Size(622, 63);
            this.lyricGbox.TabIndex = 4;
            this.lyricGbox.TabStop = false;
            this.lyricGbox.Text = "歌词信息";
            // 
            // openlyricButton
            // 
            this.openlyricButton.BackColor = System.Drawing.Color.Transparent;
            this.openlyricButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openlyricButton.Font = new System.Drawing.Font("宋体", 9F);
            this.openlyricButton.Location = new System.Drawing.Point(474, 22);
            this.openlyricButton.Name = "openlyricButton";
            this.openlyricButton.Size = new System.Drawing.Size(75, 23);
            this.openlyricButton.TabIndex = 25;
            this.openlyricButton.Text = "打开";
            this.openlyricButton.UseVisualStyleBackColor = false;
            this.openlyricButton.Click += new System.EventHandler(this.btns_Click);
            // 
            // lyricLocButton
            // 
            this.lyricLocButton.BackColor = System.Drawing.Color.Transparent;
            this.lyricLocButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lyricLocButton.Font = new System.Drawing.Font("宋体", 9F);
            this.lyricLocButton.Location = new System.Drawing.Point(542, 22);
            this.lyricLocButton.Name = "lyricLocButton";
            this.lyricLocButton.Size = new System.Drawing.Size(75, 23);
            this.lyricLocButton.TabIndex = 24;
            this.lyricLocButton.Text = "浏览";
            this.lyricLocButton.UseVisualStyleBackColor = false;
            this.lyricLocButton.Click += new System.EventHandler(this.btns_Click);
            // 
            // lyricTxtBox
            // 
            this.lyricTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lyricTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lyricTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lyricTxtBox.Location = new System.Drawing.Point(88, 22);
            this.lyricTxtBox.Multiline = true;
            this.lyricTxtBox.Name = "lyricTxtBox";
            this.lyricTxtBox.ReadOnly = true;
            this.lyricTxtBox.Size = new System.Drawing.Size(387, 23);
            this.lyricTxtBox.TabIndex = 19;
            // 
            // lyricLabel
            // 
            this.lyricLabel.AutoSize = true;
            this.lyricLabel.BackColor = System.Drawing.Color.Transparent;
            this.lyricLabel.Location = new System.Drawing.Point(8, 28);
            this.lyricLabel.Name = "lyricLabel";
            this.lyricLabel.Size = new System.Drawing.Size(65, 12);
            this.lyricLabel.TabIndex = 18;
            this.lyricLabel.Text = "歌词路径：";
            // 
            // otherInfoGbox
            // 
            this.otherInfoGbox.BackColor = System.Drawing.Color.Transparent;
            this.otherInfoGbox.Controls.Add(this.versonTxtBox);
            this.otherInfoGbox.Controls.Add(this.copyRightTxtBox);
            this.otherInfoGbox.Controls.Add(this.versonLabel);
            this.otherInfoGbox.Controls.Add(this.copyRightLabel);
            this.otherInfoGbox.Controls.Add(this.hashTxtBox);
            this.otherInfoGbox.Controls.Add(this.gainTxtBox);
            this.otherInfoGbox.Controls.Add(this.hashLabel);
            this.otherInfoGbox.Controls.Add(this.gainLabel);
            this.otherInfoGbox.Controls.Add(this.kbpsTxtBox);
            this.otherInfoGbox.Controls.Add(this.khzTxtBox);
            this.otherInfoGbox.Controls.Add(this.kbpsLabel);
            this.otherInfoGbox.Controls.Add(this.khzLabel);
            this.otherInfoGbox.Controls.Add(this.channelsTxtBox);
            this.otherInfoGbox.Controls.Add(this.tracksTxtBox);
            this.otherInfoGbox.Controls.Add(this.channelsLabel);
            this.otherInfoGbox.Controls.Add(this.tracksLabel);
            this.otherInfoGbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.otherInfoGbox.ForeColor = System.Drawing.Color.Teal;
            this.otherInfoGbox.Location = new System.Drawing.Point(10, 186);
            this.otherInfoGbox.Name = "otherInfoGbox";
            this.otherInfoGbox.Size = new System.Drawing.Size(622, 147);
            this.otherInfoGbox.TabIndex = 0;
            this.otherInfoGbox.TabStop = false;
            this.otherInfoGbox.Text = "其他信息";
            // 
            // versonTxtBox
            // 
            this.versonTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.versonTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.versonTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.versonTxtBox.Location = new System.Drawing.Point(412, 110);
            this.versonTxtBox.Multiline = true;
            this.versonTxtBox.Name = "versonTxtBox";
            this.versonTxtBox.ReadOnly = true;
            this.versonTxtBox.Size = new System.Drawing.Size(200, 23);
            this.versonTxtBox.TabIndex = 29;
            // 
            // copyRightTxtBox
            // 
            this.copyRightTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.copyRightTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.copyRightTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.copyRightTxtBox.Location = new System.Drawing.Point(88, 110);
            this.copyRightTxtBox.Multiline = true;
            this.copyRightTxtBox.Name = "copyRightTxtBox";
            this.copyRightTxtBox.ReadOnly = true;
            this.copyRightTxtBox.Size = new System.Drawing.Size(200, 23);
            this.copyRightTxtBox.TabIndex = 31;
            // 
            // versonLabel
            // 
            this.versonLabel.AutoSize = true;
            this.versonLabel.BackColor = System.Drawing.Color.Transparent;
            this.versonLabel.Location = new System.Drawing.Point(357, 115);
            this.versonLabel.Name = "versonLabel";
            this.versonLabel.Size = new System.Drawing.Size(41, 12);
            this.versonLabel.TabIndex = 32;
            this.versonLabel.Text = "版本：";
            // 
            // copyRightLabel
            // 
            this.copyRightLabel.AutoSize = true;
            this.copyRightLabel.BackColor = System.Drawing.Color.Transparent;
            this.copyRightLabel.Location = new System.Drawing.Point(34, 114);
            this.copyRightLabel.Name = "copyRightLabel";
            this.copyRightLabel.Size = new System.Drawing.Size(41, 12);
            this.copyRightLabel.TabIndex = 30;
            this.copyRightLabel.Text = "版权：";
            // 
            // hashTxtBox
            // 
            this.hashTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hashTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.hashTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.hashTxtBox.Location = new System.Drawing.Point(412, 80);
            this.hashTxtBox.Multiline = true;
            this.hashTxtBox.Name = "hashTxtBox";
            this.hashTxtBox.ReadOnly = true;
            this.hashTxtBox.Size = new System.Drawing.Size(200, 23);
            this.hashTxtBox.TabIndex = 25;
            // 
            // gainTxtBox
            // 
            this.gainTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gainTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.gainTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.gainTxtBox.Location = new System.Drawing.Point(88, 80);
            this.gainTxtBox.Multiline = true;
            this.gainTxtBox.Name = "gainTxtBox";
            this.gainTxtBox.ReadOnly = true;
            this.gainTxtBox.Size = new System.Drawing.Size(200, 23);
            this.gainTxtBox.TabIndex = 27;
            // 
            // hashLabel
            // 
            this.hashLabel.AutoSize = true;
            this.hashLabel.BackColor = System.Drawing.Color.Transparent;
            this.hashLabel.Location = new System.Drawing.Point(344, 86);
            this.hashLabel.Name = "hashLabel";
            this.hashLabel.Size = new System.Drawing.Size(53, 12);
            this.hashLabel.TabIndex = 28;
            this.hashLabel.Text = "哈希值：";
            // 
            // gainLabel
            // 
            this.gainLabel.AutoSize = true;
            this.gainLabel.BackColor = System.Drawing.Color.Transparent;
            this.gainLabel.Location = new System.Drawing.Point(34, 83);
            this.gainLabel.Name = "gainLabel";
            this.gainLabel.Size = new System.Drawing.Size(41, 12);
            this.gainLabel.TabIndex = 26;
            this.gainLabel.Text = "增益：";
            // 
            // kbpsTxtBox
            // 
            this.kbpsTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kbpsTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.kbpsTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.kbpsTxtBox.Location = new System.Drawing.Point(412, 50);
            this.kbpsTxtBox.Multiline = true;
            this.kbpsTxtBox.Name = "kbpsTxtBox";
            this.kbpsTxtBox.ReadOnly = true;
            this.kbpsTxtBox.Size = new System.Drawing.Size(200, 23);
            this.kbpsTxtBox.TabIndex = 21;
            // 
            // khzTxtBox
            // 
            this.khzTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.khzTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.khzTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.khzTxtBox.Location = new System.Drawing.Point(88, 50);
            this.khzTxtBox.Multiline = true;
            this.khzTxtBox.Name = "khzTxtBox";
            this.khzTxtBox.ReadOnly = true;
            this.khzTxtBox.Size = new System.Drawing.Size(200, 23);
            this.khzTxtBox.TabIndex = 23;
            // 
            // kbpsLabel
            // 
            this.kbpsLabel.AutoSize = true;
            this.kbpsLabel.BackColor = System.Drawing.Color.Transparent;
            this.kbpsLabel.Location = new System.Drawing.Point(344, 54);
            this.kbpsLabel.Name = "kbpsLabel";
            this.kbpsLabel.Size = new System.Drawing.Size(53, 12);
            this.kbpsLabel.TabIndex = 24;
            this.kbpsLabel.Text = "比特率：";
            // 
            // khzLabel
            // 
            this.khzLabel.AutoSize = true;
            this.khzLabel.BackColor = System.Drawing.Color.Transparent;
            this.khzLabel.Location = new System.Drawing.Point(21, 52);
            this.khzLabel.Name = "khzLabel";
            this.khzLabel.Size = new System.Drawing.Size(53, 12);
            this.khzLabel.TabIndex = 22;
            this.khzLabel.Text = "采样率：";
            // 
            // channelsTxtBox
            // 
            this.channelsTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.channelsTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.channelsTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.channelsTxtBox.Location = new System.Drawing.Point(412, 20);
            this.channelsTxtBox.Multiline = true;
            this.channelsTxtBox.Name = "channelsTxtBox";
            this.channelsTxtBox.ReadOnly = true;
            this.channelsTxtBox.Size = new System.Drawing.Size(200, 23);
            this.channelsTxtBox.TabIndex = 17;
            // 
            // tracksTxtBox
            // 
            this.tracksTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tracksTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tracksTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tracksTxtBox.Location = new System.Drawing.Point(88, 20);
            this.tracksTxtBox.Multiline = true;
            this.tracksTxtBox.Name = "tracksTxtBox";
            this.tracksTxtBox.ReadOnly = true;
            this.tracksTxtBox.Size = new System.Drawing.Size(200, 23);
            this.tracksTxtBox.TabIndex = 19;
            // 
            // channelsLabel
            // 
            this.channelsLabel.AutoSize = true;
            this.channelsLabel.BackColor = System.Drawing.Color.Transparent;
            this.channelsLabel.Location = new System.Drawing.Point(357, 25);
            this.channelsLabel.Name = "channelsLabel";
            this.channelsLabel.Size = new System.Drawing.Size(41, 12);
            this.channelsLabel.TabIndex = 20;
            this.channelsLabel.Text = "声道：";
            // 
            // tracksLabel
            // 
            this.tracksLabel.AutoSize = true;
            this.tracksLabel.BackColor = System.Drawing.Color.Transparent;
            this.tracksLabel.Location = new System.Drawing.Point(34, 22);
            this.tracksLabel.Name = "tracksLabel";
            this.tracksLabel.Size = new System.Drawing.Size(41, 12);
            this.tracksLabel.TabIndex = 18;
            this.tracksLabel.Text = "音轨：";
            // 
            // fileInfoGbox
            // 
            this.fileInfoGbox.BackColor = System.Drawing.Color.Transparent;
            this.fileInfoGbox.Controls.Add(this.playTimesTxtBox);
            this.fileInfoGbox.Controls.Add(this.timeTxtBox);
            this.fileInfoGbox.Controls.Add(this.playTimesLabel);
            this.fileInfoGbox.Controls.Add(this.timeLabel);
            this.fileInfoGbox.Controls.Add(this.albumTxtBox);
            this.fileInfoGbox.Controls.Add(this.singerTxtBox);
            this.fileInfoGbox.Controls.Add(this.albumLabel);
            this.fileInfoGbox.Controls.Add(this.singerLabel);
            this.fileInfoGbox.Controls.Add(this.creationTimeTxtBox);
            this.fileInfoGbox.Controls.Add(this.copyButton);
            this.fileInfoGbox.Controls.Add(this.locButton);
            this.fileInfoGbox.Controls.Add(this.lengthTxtBox);
            this.fileInfoGbox.Controls.Add(this.creationTimeLabel);
            this.fileInfoGbox.Controls.Add(this.nameLabel);
            this.fileInfoGbox.Controls.Add(this.tBox_type);
            this.fileInfoGbox.Controls.Add(this.pathTxtBox);
            this.fileInfoGbox.Controls.Add(this.pathLabel);
            this.fileInfoGbox.Controls.Add(this.lengthLabel);
            this.fileInfoGbox.Controls.Add(this.tBox_name);
            this.fileInfoGbox.Controls.Add(this.typeLabel);
            this.fileInfoGbox.ForeColor = System.Drawing.Color.Teal;
            this.fileInfoGbox.Location = new System.Drawing.Point(10, 7);
            this.fileInfoGbox.Name = "fileInfoGbox";
            this.fileInfoGbox.Size = new System.Drawing.Size(622, 172);
            this.fileInfoGbox.TabIndex = 3;
            this.fileInfoGbox.TabStop = false;
            this.fileInfoGbox.Text = "文件信息";
            // 
            // playTimesTxtBox
            // 
            this.playTimesTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playTimesTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.playTimesTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.playTimesTxtBox.Location = new System.Drawing.Point(412, 141);
            this.playTimesTxtBox.Multiline = true;
            this.playTimesTxtBox.Name = "playTimesTxtBox";
            this.playTimesTxtBox.ReadOnly = true;
            this.playTimesTxtBox.Size = new System.Drawing.Size(200, 23);
            this.playTimesTxtBox.TabIndex = 13;
            // 
            // timeTxtBox
            // 
            this.timeTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.timeTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.timeTxtBox.Location = new System.Drawing.Point(88, 141);
            this.timeTxtBox.Multiline = true;
            this.timeTxtBox.Name = "timeTxtBox";
            this.timeTxtBox.ReadOnly = true;
            this.timeTxtBox.Size = new System.Drawing.Size(200, 23);
            this.timeTxtBox.TabIndex = 15;
            // 
            // playTimesLabel
            // 
            this.playTimesLabel.AutoSize = true;
            this.playTimesLabel.BackColor = System.Drawing.Color.Transparent;
            this.playTimesLabel.Location = new System.Drawing.Point(330, 144);
            this.playTimesLabel.Name = "playTimesLabel";
            this.playTimesLabel.Size = new System.Drawing.Size(65, 12);
            this.playTimesLabel.TabIndex = 16;
            this.playTimesLabel.Text = "播放次数：";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Location = new System.Drawing.Point(34, 142);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(41, 12);
            this.timeLabel.TabIndex = 14;
            this.timeLabel.Text = "时长：";
            // 
            // albumTxtBox
            // 
            this.albumTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.albumTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.albumTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.albumTxtBox.Location = new System.Drawing.Point(412, 110);
            this.albumTxtBox.Multiline = true;
            this.albumTxtBox.Name = "albumTxtBox";
            this.albumTxtBox.ReadOnly = true;
            this.albumTxtBox.Size = new System.Drawing.Size(200, 23);
            this.albumTxtBox.TabIndex = 9;
            // 
            // singerTxtBox
            // 
            this.singerTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.singerTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.singerTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.singerTxtBox.Location = new System.Drawing.Point(88, 110);
            this.singerTxtBox.Multiline = true;
            this.singerTxtBox.Name = "singerTxtBox";
            this.singerTxtBox.ReadOnly = true;
            this.singerTxtBox.Size = new System.Drawing.Size(200, 23);
            this.singerTxtBox.TabIndex = 11;
            // 
            // albumLabel
            // 
            this.albumLabel.AutoSize = true;
            this.albumLabel.BackColor = System.Drawing.Color.Transparent;
            this.albumLabel.Location = new System.Drawing.Point(357, 115);
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.Size = new System.Drawing.Size(41, 12);
            this.albumLabel.TabIndex = 12;
            this.albumLabel.Text = "专辑：";
            // 
            // singerLabel
            // 
            this.singerLabel.AutoSize = true;
            this.singerLabel.BackColor = System.Drawing.Color.Transparent;
            this.singerLabel.Location = new System.Drawing.Point(34, 114);
            this.singerLabel.Name = "singerLabel";
            this.singerLabel.Size = new System.Drawing.Size(41, 12);
            this.singerLabel.TabIndex = 10;
            this.singerLabel.Text = "歌手：";
            // 
            // creationTimeTxtBox
            // 
            this.creationTimeTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.creationTimeTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.creationTimeTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.creationTimeTxtBox.Location = new System.Drawing.Point(412, 80);
            this.creationTimeTxtBox.Multiline = true;
            this.creationTimeTxtBox.Name = "creationTimeTxtBox";
            this.creationTimeTxtBox.ReadOnly = true;
            this.creationTimeTxtBox.Size = new System.Drawing.Size(200, 23);
            this.creationTimeTxtBox.TabIndex = 3;
            // 
            // copyButton
            // 
            this.copyButton.BackColor = System.Drawing.Color.Transparent;
            this.copyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyButton.Font = new System.Drawing.Font("宋体", 9F);
            this.copyButton.Location = new System.Drawing.Point(542, 21);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(75, 23);
            this.copyButton.TabIndex = 8;
            this.copyButton.Text = "复制";
            this.copyButton.UseVisualStyleBackColor = false;
            this.copyButton.Click += new System.EventHandler(this.btns_Click);
            // 
            // locButton
            // 
            this.locButton.BackColor = System.Drawing.Color.Transparent;
            this.locButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.locButton.Font = new System.Drawing.Font("宋体", 9F);
            this.locButton.Location = new System.Drawing.Point(474, 21);
            this.locButton.Name = "locButton";
            this.locButton.Size = new System.Drawing.Size(75, 23);
            this.locButton.TabIndex = 7;
            this.locButton.Text = "浏览";
            this.locButton.UseVisualStyleBackColor = false;
            this.locButton.Click += new System.EventHandler(this.btns_Click);
            // 
            // lengthTxtBox
            // 
            this.lengthTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lengthTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lengthTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lengthTxtBox.Location = new System.Drawing.Point(88, 80);
            this.lengthTxtBox.Multiline = true;
            this.lengthTxtBox.Name = "lengthTxtBox";
            this.lengthTxtBox.ReadOnly = true;
            this.lengthTxtBox.Size = new System.Drawing.Size(200, 23);
            this.lengthTxtBox.TabIndex = 5;
            // 
            // creationTimeLabel
            // 
            this.creationTimeLabel.AutoSize = true;
            this.creationTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.creationTimeLabel.Location = new System.Drawing.Point(332, 86);
            this.creationTimeLabel.Name = "creationTimeLabel";
            this.creationTimeLabel.Size = new System.Drawing.Size(65, 12);
            this.creationTimeLabel.TabIndex = 6;
            this.creationTimeLabel.Text = "创建日期：";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.Color.Transparent;
            this.nameLabel.Location = new System.Drawing.Point(21, 56);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(53, 12);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "文件名：";
            // 
            // tBox_type
            // 
            this.tBox_type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tBox_type.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tBox_type.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tBox_type.Location = new System.Drawing.Point(412, 51);
            this.tBox_type.Multiline = true;
            this.tBox_type.Name = "tBox_type";
            this.tBox_type.ReadOnly = true;
            this.tBox_type.Size = new System.Drawing.Size(200, 23);
            this.tBox_type.TabIndex = 7;
            // 
            // pathTxtBox
            // 
            this.pathTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pathTxtBox.Font = new System.Drawing.Font("宋体", 10.5F);
            this.pathTxtBox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.pathTxtBox.Location = new System.Drawing.Point(88, 21);
            this.pathTxtBox.Multiline = true;
            this.pathTxtBox.Name = "pathTxtBox";
            this.pathTxtBox.ReadOnly = true;
            this.pathTxtBox.Size = new System.Drawing.Size(387, 23);
            this.pathTxtBox.TabIndex = 1;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.BackColor = System.Drawing.Color.Transparent;
            this.pathLabel.Location = new System.Drawing.Point(34, 26);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(41, 12);
            this.pathLabel.TabIndex = 0;
            this.pathLabel.Text = "路径：";
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.BackColor = System.Drawing.Color.Transparent;
            this.lengthLabel.Location = new System.Drawing.Point(8, 84);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(65, 12);
            this.lengthLabel.TabIndex = 4;
            this.lengthLabel.Text = "文件大小：";
            // 
            // tBox_name
            // 
            this.tBox_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tBox_name.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tBox_name.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tBox_name.Location = new System.Drawing.Point(88, 51);
            this.tBox_name.Multiline = true;
            this.tBox_name.Name = "tBox_name";
            this.tBox_name.ReadOnly = true;
            this.tBox_name.Size = new System.Drawing.Size(200, 23);
            this.tBox_name.TabIndex = 6;
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.BackColor = System.Drawing.Color.Transparent;
            this.typeLabel.Location = new System.Drawing.Point(332, 56);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(65, 12);
            this.typeLabel.TabIndex = 3;
            this.typeLabel.Text = "音乐类型：";
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.ForeColor = System.Drawing.Color.Teal;
            this.backButton.Location = new System.Drawing.Point(562, 417);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "返回";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.btns_Click);
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.Color.Transparent;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.ForeColor = System.Drawing.Color.Teal;
            this.playButton.Location = new System.Drawing.Point(494, 417);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 1;
            this.playButton.Text = "播放";
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Click += new System.EventHandler(this.btns_Click);
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(655, 478);
            this.Name = "InfoForm";
            this.Opacity = 0.88D;
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "信息";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.this_Load);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.lyricGbox.ResumeLayout(false);
            this.lyricGbox.PerformLayout();
            this.otherInfoGbox.ResumeLayout(false);
            this.otherInfoGbox.PerformLayout();
            this.fileInfoGbox.ResumeLayout(false);
            this.fileInfoGbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.GroupBox lyricGbox;
        private System.Windows.Forms.Button openlyricButton;
        private System.Windows.Forms.Button lyricLocButton;
        private System.Windows.Forms.TextBox lyricTxtBox;
        private System.Windows.Forms.Label lyricLabel;
        private System.Windows.Forms.GroupBox otherInfoGbox;
        private System.Windows.Forms.TextBox hashTxtBox;
        private System.Windows.Forms.TextBox gainTxtBox;
        private System.Windows.Forms.Label hashLabel;
        private System.Windows.Forms.Label gainLabel;
        private System.Windows.Forms.TextBox kbpsTxtBox;
        private System.Windows.Forms.TextBox khzTxtBox;
        private System.Windows.Forms.Label kbpsLabel;
        private System.Windows.Forms.Label khzLabel;
        private System.Windows.Forms.TextBox channelsTxtBox;
        private System.Windows.Forms.TextBox tracksTxtBox;
        private System.Windows.Forms.Label channelsLabel;
        private System.Windows.Forms.Label tracksLabel;
        private System.Windows.Forms.GroupBox fileInfoGbox;
        private System.Windows.Forms.TextBox playTimesTxtBox;
        private System.Windows.Forms.TextBox timeTxtBox;
        private System.Windows.Forms.Label playTimesLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TextBox albumTxtBox;
        private System.Windows.Forms.TextBox singerTxtBox;
        private System.Windows.Forms.Label albumLabel;
        private System.Windows.Forms.Label singerLabel;
        private System.Windows.Forms.TextBox creationTimeTxtBox;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button locButton;
        private System.Windows.Forms.TextBox lengthTxtBox;
        private System.Windows.Forms.Label creationTimeLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox tBox_type;
        private System.Windows.Forms.TextBox pathTxtBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.TextBox tBox_name;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.TextBox versonTxtBox;
        private System.Windows.Forms.TextBox copyRightTxtBox;
        private System.Windows.Forms.Label versonLabel;
        private System.Windows.Forms.Label copyRightLabel;
    }
}