namespace FreshMedia.View
{
    partial class DesktopLyricF
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
            this.components = new System.ComponentModel.Container();
            this.topLabel = new System.Windows.Forms.Label();
            this.cms_setting = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsi_lock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_fontSize = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsi_moreSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_lrcOpacity = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lockButton = new System.Windows.Forms.Button();
            this.fsDownButton = new System.Windows.Forms.Button();
            this.fsUpButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.playLastButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.mainformButton = new System.Windows.Forms.Button();
            this.ctlPanel = new System.Windows.Forms.Panel();
            this.cms_setting.SuspendLayout();
            this.ctlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topLabel
            // 
            this.topLabel.BackColor = System.Drawing.Color.Transparent;
            this.topLabel.ContextMenuStrip = this.cms_setting;
            this.topLabel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.topLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.topLabel.Location = new System.Drawing.Point(0, 39);
            this.topLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(907, 50);
            this.topLabel.TabIndex = 0;
            this.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cms_setting
            // 
            this.cms_setting.BackColor = System.Drawing.SystemColors.Control;
            this.cms_setting.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cms_setting.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cms_setting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsi_lock,
            this.tsmi_fontSize,
            this.tmsi_moreSet,
            this.tmsi_exit,
            this.tsmi_lrcOpacity});
            this.cms_setting.Name = "contextMenuStrip1";
            this.cms_setting.ShowImageMargin = false;
            this.cms_setting.Size = new System.Drawing.Size(118, 114);
            this.cms_setting.Opening += new System.ComponentModel.CancelEventHandler(this.cms_lrc_Opening);
            // 
            // tmsi_lock
            // 
            this.tmsi_lock.BackColor = System.Drawing.Color.Transparent;
            this.tmsi_lock.ForeColor = System.Drawing.Color.DarkGreen;
            this.tmsi_lock.Name = "tmsi_lock";
            this.tmsi_lock.Size = new System.Drawing.Size(117, 22);
            this.tmsi_lock.Text = "锁定歌词";
            this.tmsi_lock.Click += new System.EventHandler(this.tsmi_lock_Click);
            // 
            // tsmi_fontSize
            // 
            this.tsmi_fontSize.BackColor = System.Drawing.Color.Transparent;
            this.tsmi_fontSize.ForeColor = System.Drawing.Color.DarkGreen;
            this.tsmi_fontSize.Name = "tsmi_fontSize";
            this.tsmi_fontSize.Size = new System.Drawing.Size(117, 22);
            this.tsmi_fontSize.Text = "字体大小";
            // 
            // tmsi_moreSet
            // 
            this.tmsi_moreSet.BackColor = System.Drawing.Color.Transparent;
            this.tmsi_moreSet.ForeColor = System.Drawing.Color.DarkGreen;
            this.tmsi_moreSet.Name = "tmsi_moreSet";
            this.tmsi_moreSet.Size = new System.Drawing.Size(117, 22);
            this.tmsi_moreSet.Text = "更多设置";
            this.tmsi_moreSet.Click += new System.EventHandler(this.tsmi_moreSet_Click);
            // 
            // tmsi_exit
            // 
            this.tmsi_exit.BackColor = System.Drawing.Color.Transparent;
            this.tmsi_exit.ForeColor = System.Drawing.Color.DarkGreen;
            this.tmsi_exit.Name = "tmsi_exit";
            this.tmsi_exit.Size = new System.Drawing.Size(117, 22);
            this.tmsi_exit.Text = "退出桌面歌词";
            this.tmsi_exit.Click += new System.EventHandler(this.tsmi_exit_Click);
            // 
            // tsmi_lrcOpacity
            // 
            this.tsmi_lrcOpacity.ForeColor = System.Drawing.Color.DarkGreen;
            this.tsmi_lrcOpacity.Name = "tsmi_lrcOpacity";
            this.tsmi_lrcOpacity.Size = new System.Drawing.Size(117, 22);
            this.tsmi_lrcOpacity.Text = "歌词透明度";
            // 
            // bottomLabel
            // 
            this.bottomLabel.BackColor = System.Drawing.Color.Transparent;
            this.bottomLabel.ContextMenuStrip = this.cms_setting;
            this.bottomLabel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.bottomLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.bottomLabel.Location = new System.Drawing.Point(0, 93);
            this.bottomLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.bottomLabel.Name = "bottomLabel";
            this.bottomLabel.Size = new System.Drawing.Size(972, 64);
            this.bottomLabel.TabIndex = 1;
            this.bottomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 300;
            this.toolTip.BackColor = System.Drawing.Color.LightGreen;
            // 
            // lockButton
            // 
            this.lockButton.BackColor = System.Drawing.Color.Transparent;
            this.lockButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.lockButton.FlatAppearance.BorderSize = 0;
            this.lockButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lockButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lockButton.ForeColor = System.Drawing.Color.White;
            this.lockButton.Location = new System.Drawing.Point(126, 0);
            this.lockButton.Margin = new System.Windows.Forms.Padding(0);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(75, 23);
            this.lockButton.TabIndex = 0;
            this.lockButton.Text = "Lock";
            this.toolTip.SetToolTip(this.lockButton, "锁定或解锁歌词");
            this.lockButton.UseVisualStyleBackColor = false;
            this.lockButton.Click += new System.EventHandler(this.btns_Click);
            this.lockButton.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // fsDownButton
            // 
            this.fsDownButton.BackColor = System.Drawing.Color.Transparent;
            this.fsDownButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.fsDownButton.FlatAppearance.BorderSize = 0;
            this.fsDownButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.fsDownButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.fsDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fsDownButton.ForeColor = System.Drawing.Color.White;
            this.fsDownButton.Location = new System.Drawing.Point(218, 0);
            this.fsDownButton.Margin = new System.Windows.Forms.Padding(0);
            this.fsDownButton.Name = "fsDownButton";
            this.fsDownButton.Size = new System.Drawing.Size(75, 23);
            this.fsDownButton.TabIndex = 1;
            this.fsDownButton.Text = "A-";
            this.toolTip.SetToolTip(this.fsDownButton, "减小字体");
            this.fsDownButton.UseVisualStyleBackColor = false;
            this.fsDownButton.Click += new System.EventHandler(this.btns_Click);
            this.fsDownButton.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // fsUpButton
            // 
            this.fsUpButton.BackColor = System.Drawing.Color.Transparent;
            this.fsUpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.fsUpButton.FlatAppearance.BorderSize = 0;
            this.fsUpButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.fsUpButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.fsUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fsUpButton.ForeColor = System.Drawing.Color.White;
            this.fsUpButton.Location = new System.Drawing.Point(310, 0);
            this.fsUpButton.Margin = new System.Windows.Forms.Padding(0);
            this.fsUpButton.Name = "fsUpButton";
            this.fsUpButton.Size = new System.Drawing.Size(75, 23);
            this.fsUpButton.TabIndex = 2;
            this.fsUpButton.Text = "A+";
            this.toolTip.SetToolTip(this.fsUpButton, "增大字体");
            this.fsUpButton.UseVisualStyleBackColor = false;
            this.fsUpButton.Click += new System.EventHandler(this.btns_Click);
            this.fsUpButton.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(401, 0);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "X";
            this.toolTip.SetToolTip(this.closeButton, "退出桌面歌词");
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.btns_Click);
            this.closeButton.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // playLastButton
            // 
            this.playLastButton.BackColor = System.Drawing.Color.Transparent;
            this.playLastButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.playLastButton.FlatAppearance.BorderSize = 0;
            this.playLastButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.playLastButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.playLastButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playLastButton.ForeColor = System.Drawing.Color.White;
            this.playLastButton.Location = new System.Drawing.Point(492, 0);
            this.playLastButton.Margin = new System.Windows.Forms.Padding(0);
            this.playLastButton.Name = "playLastButton";
            this.playLastButton.Size = new System.Drawing.Size(75, 23);
            this.playLastButton.TabIndex = 4;
            this.playLastButton.Tag = "";
            this.playLastButton.Text = "|<";
            this.toolTip.SetToolTip(this.playLastButton, "上一首");
            this.playLastButton.UseVisualStyleBackColor = false;
            this.playLastButton.Click += new System.EventHandler(this.btns_Click);
            this.playLastButton.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // pauseButton
            // 
            this.pauseButton.BackColor = System.Drawing.Color.Transparent;
            this.pauseButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.pauseButton.FlatAppearance.BorderSize = 0;
            this.pauseButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pauseButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pauseButton.ForeColor = System.Drawing.Color.White;
            this.pauseButton.Location = new System.Drawing.Point(584, 0);
            this.pauseButton.Margin = new System.Windows.Forms.Padding(0);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 5;
            this.pauseButton.Tag = "";
            this.pauseButton.Text = ">";
            this.toolTip.SetToolTip(this.pauseButton, "播放暂停");
            this.pauseButton.UseVisualStyleBackColor = false;
            this.pauseButton.Click += new System.EventHandler(this.btns_Click);
            this.pauseButton.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.Color.Transparent;
            this.nextButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.nextButton.FlatAppearance.BorderSize = 0;
            this.nextButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.nextButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextButton.ForeColor = System.Drawing.Color.White;
            this.nextButton.Location = new System.Drawing.Point(676, 0);
            this.nextButton.Margin = new System.Windows.Forms.Padding(0);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 6;
            this.nextButton.Tag = "";
            this.nextButton.Text = ">|";
            this.toolTip.SetToolTip(this.nextButton, "下一首");
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.btns_Click);
            this.nextButton.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // mainformButton
            // 
            this.mainformButton.BackColor = System.Drawing.Color.Transparent;
            this.mainformButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.mainformButton.FlatAppearance.BorderSize = 0;
            this.mainformButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mainformButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mainformButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mainformButton.ForeColor = System.Drawing.Color.White;
            this.mainformButton.Location = new System.Drawing.Point(773, 0);
            this.mainformButton.Margin = new System.Windows.Forms.Padding(0);
            this.mainformButton.Name = "mainformButton";
            this.mainformButton.Size = new System.Drawing.Size(75, 23);
            this.mainformButton.TabIndex = 7;
            this.mainformButton.Text = "Open";
            this.toolTip.SetToolTip(this.mainformButton, "打开主界面");
            this.mainformButton.UseVisualStyleBackColor = false;
            this.mainformButton.Click += new System.EventHandler(this.btns_Click);
            this.mainformButton.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // ctlPanel
            // 
            this.ctlPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ctlPanel.Controls.Add(this.mainformButton);
            this.ctlPanel.Controls.Add(this.nextButton);
            this.ctlPanel.Controls.Add(this.pauseButton);
            this.ctlPanel.Controls.Add(this.playLastButton);
            this.ctlPanel.Controls.Add(this.closeButton);
            this.ctlPanel.Controls.Add(this.fsUpButton);
            this.ctlPanel.Controls.Add(this.fsDownButton);
            this.ctlPanel.Controls.Add(this.lockButton);
            this.ctlPanel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.ctlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlPanel.Location = new System.Drawing.Point(0, 0);
            this.ctlPanel.Name = "ctlPanel";
            this.ctlPanel.Size = new System.Drawing.Size(972, 23);
            this.ctlPanel.TabIndex = 2;
            this.ctlPanel.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            // 
            // DesktopLyricF
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(972, 157);
            this.Controls.Add(this.ctlPanel);
            this.Controls.Add(this.bottomLabel);
            this.Controls.Add(this.topLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DesktopLyricF";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Desktop Lyrics Service";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.this_Load);
            this.LocationChanged += new System.EventHandler(this.this_LocationChanged);
            this.MouseEnter += new System.EventHandler(this.bgf_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.bgf_MouseLeave);
            this.cms_setting.ResumeLayout(false);
            this.ctlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tmsi_exit;
        private System.Windows.Forms.ToolStripMenuItem tsmi_fontSize;
        private System.Windows.Forms.ToolStripMenuItem tmsi_moreSet;
        private System.Windows.Forms.ToolStripMenuItem tmsi_lock;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.Button fsDownButton;
        private System.Windows.Forms.Button fsUpButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button playLastButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button mainformButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Panel ctlPanel;
        private System.Windows.Forms.ContextMenuStrip cms_setting;
        private System.Windows.Forms.ToolStripMenuItem tsmi_lrcOpacity;
        private System.Windows.Forms.Label topLabel;
        private System.Windows.Forms.Label bottomLabel;
    }
}