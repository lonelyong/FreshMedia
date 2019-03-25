using System.Drawing;
namespace FreshMedia.View
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.controlSplitContainer = new System.Windows.Forms.SplitContainer();
            this.aKbpsLabel = new System.Windows.Forms.Label();
            this.aLengthLabel = new System.Windows.Forms.Label();
            this.aIndexLabel = new System.Windows.Forms.Label();
            this.locbarLabel = new System.Windows.Forms.Label();
            this.aTimeLabel = new System.Windows.Forms.Label();
            this.aNameLabel = new System.Windows.Forms.Label();
            this.playLastButton = new System.Windows.Forms.Button();
            this.playPauseButton = new System.Windows.Forms.Button();
            this.playNextButton = new System.Windows.Forms.Button();
            this.volUpLabel = new System.Windows.Forms.Label();
            this.volDownLabel = new System.Windows.Forms.Label();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.infPanel = new System.Windows.Forms.Panel();
            this.aheadPicBox = new System.Windows.Forms.PictureBox();
            this.aInfLabel = new System.Windows.Forms.Label();
            this.listSplitContainer = new System.Windows.Forms.SplitContainer();
            this.lb_state = new System.Windows.Forms.Label();
            this.labelLyric = new System.Windows.Forms.Label();
            this.cms_main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_fLyric = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_showCurList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_opacity = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cmPicBox = new System.Windows.Forms.PictureBox();
            this.searchPicBox = new System.Windows.Forms.PictureBox();
            this.locPicBox = new System.Windows.Forms.PictureBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmi_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_history = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_search = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_option = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_theme = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_cm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_sleep = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_dLyric = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_moreSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_help = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_userHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_about = new System.Windows.Forms.ToolStripMenuItem();
            this.sleepLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlSplitContainer)).BeginInit();
            this.controlSplitContainer.Panel1.SuspendLayout();
            this.controlSplitContainer.Panel2.SuspendLayout();
            this.controlSplitContainer.SuspendLayout();
            this.infPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aheadPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSplitContainer)).BeginInit();
            this.listSplitContainer.Panel2.SuspendLayout();
            this.listSplitContainer.SuspendLayout();
            this.cms_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locPicBox)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.locPicBox);
            this.ContentPanel.Controls.Add(this.mainSplitContainer);
            this.ContentPanel.Controls.Add(this.menuStrip);
            this.ContentPanel.Controls.Add(this.cmPicBox);
            this.ContentPanel.Controls.Add(this.searchPicBox);
            this.ContentPanel.Controls.Add(this.sleepLabel);
            this.ContentPanel.Controls.Add(this.timeLabel);
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(3, 33);
            this.ContentPanel.Size = new System.Drawing.Size(1545, 925);
            this.ContentPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContentPanel_MouseUp);
            // 
            // controlSplitContainer
            // 
            this.controlSplitContainer.BackColor = System.Drawing.Color.Transparent;
            this.controlSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.controlSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.controlSplitContainer.Name = "controlSplitContainer";
            // 
            // controlSplitContainer.Panel1
            // 
            this.controlSplitContainer.Panel1.Controls.Add(this.aKbpsLabel);
            this.controlSplitContainer.Panel1.Controls.Add(this.aLengthLabel);
            this.controlSplitContainer.Panel1.Controls.Add(this.aIndexLabel);
            this.controlSplitContainer.Panel1.Controls.Add(this.locbarLabel);
            this.controlSplitContainer.Panel1.Controls.Add(this.aTimeLabel);
            this.controlSplitContainer.Panel1.Controls.Add(this.aNameLabel);
            this.controlSplitContainer.Panel1.Controls.Add(this.playLastButton);
            this.controlSplitContainer.Panel1.Controls.Add(this.playPauseButton);
            this.controlSplitContainer.Panel1.Controls.Add(this.playNextButton);
            this.controlSplitContainer.Panel1.Controls.Add(this.volUpLabel);
            this.controlSplitContainer.Panel1.Controls.Add(this.volDownLabel);
            this.controlSplitContainer.Panel1.Controls.Add(this.volumeLabel);
            this.controlSplitContainer.Panel1.SizeChanged += new System.EventHandler(this.controlSplitContainerContentPanel1_SizeChanged);
            this.controlSplitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.controlSplitContainerContentPanel1_Paint);
            this.controlSplitContainer.Panel1MinSize = 360;
            // 
            // controlSplitContainer.Panel2
            // 
            this.controlSplitContainer.Panel2.Controls.Add(this.infPanel);
            this.controlSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.controlSplitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.controlSplitContainerContentPanel2_Paint);
            this.controlSplitContainer.Panel2MinSize = 141;
            this.controlSplitContainer.Size = new System.Drawing.Size(1522, 160);
            this.controlSplitContainer.SplitterDistance = 500;
            this.controlSplitContainer.TabIndex = 75;
            this.controlSplitContainer.SizeChanged += new System.EventHandler(this.controlSplitContainer_SizeChanged);
            // 
            // aKbpsLabel
            // 
            this.aKbpsLabel.AutoSize = true;
            this.aKbpsLabel.BackColor = System.Drawing.Color.Transparent;
            this.aKbpsLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aKbpsLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.aKbpsLabel.Location = new System.Drawing.Point(177, 68);
            this.aKbpsLabel.Name = "aKbpsLabel";
            this.aKbpsLabel.Size = new System.Drawing.Size(88, 16);
            this.aKbpsLabel.TabIndex = 62;
            this.aKbpsLabel.Text = "    0 Kbps";
            this.aKbpsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // aLengthLabel
            // 
            this.aLengthLabel.AutoSize = true;
            this.aLengthLabel.BackColor = System.Drawing.Color.Transparent;
            this.aLengthLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.aLengthLabel.Location = new System.Drawing.Point(425, 68);
            this.aLengthLabel.Name = "aLengthLabel";
            this.aLengthLabel.Size = new System.Drawing.Size(72, 16);
            this.aLengthLabel.TabIndex = 61;
            this.aLengthLabel.Text = "   00:00";
            this.aLengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // aIndexLabel
            // 
            this.aIndexLabel.AutoSize = true;
            this.aIndexLabel.BackColor = System.Drawing.Color.Transparent;
            this.aIndexLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.aIndexLabel.Location = new System.Drawing.Point(299, 68);
            this.aIndexLabel.Name = "aIndexLabel";
            this.aIndexLabel.Size = new System.Drawing.Size(80, 16);
            this.aIndexLabel.TabIndex = 60;
            this.aIndexLabel.Text = "   0/0   ";
            this.aIndexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // locbarLabel
            // 
            this.locbarLabel.BackColor = System.Drawing.Color.Transparent;
            this.locbarLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.locbarLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.locbarLabel.Location = new System.Drawing.Point(12, 92);
            this.locbarLabel.Name = "locbarLabel";
            this.locbarLabel.Size = new System.Drawing.Size(300, 20);
            this.locbarLabel.TabIndex = 2;
            this.locbarLabel.Text = "   ";
            // 
            // aTimeLabel
            // 
            this.aTimeLabel.AutoSize = true;
            this.aTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.aTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.aTimeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.aTimeLabel.Location = new System.Drawing.Point(7, 1);
            this.aTimeLabel.Name = "aTimeLabel";
            this.aTimeLabel.Size = new System.Drawing.Size(81, 30);
            this.aTimeLabel.TabIndex = 59;
            this.aTimeLabel.Text = "00:00";
            this.aTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // aNameLabel
            // 
            this.aNameLabel.AutoEllipsis = true;
            this.aNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.aNameLabel.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.aNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.aNameLabel.Location = new System.Drawing.Point(354, 4);
            this.aNameLabel.Name = "aNameLabel";
            this.aNameLabel.Size = new System.Drawing.Size(143, 27);
            this.aNameLabel.TabIndex = 2;
            this.aNameLabel.Text = "请选择一首音乐";
            this.aNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // playLastButton
            // 
            this.playLastButton.AutoSize = true;
            this.playLastButton.BackColor = System.Drawing.Color.Transparent;
            this.playLastButton.FlatAppearance.BorderSize = 0;
            this.playLastButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playLastButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.playLastButton.Location = new System.Drawing.Point(15, 118);
            this.playLastButton.MinimumSize = new System.Drawing.Size(60, 28);
            this.playLastButton.Name = "playLastButton";
            this.playLastButton.Size = new System.Drawing.Size(60, 28);
            this.playLastButton.TabIndex = 53;
            this.playLastButton.TabStop = false;
            this.playLastButton.Text = "|<";
            this.playLastButton.UseVisualStyleBackColor = false;
            this.playLastButton.Click += new System.EventHandler(this.playCtlButtons_Click);
            this.playLastButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlButton_MouseDown);
            this.playLastButton.MouseEnter += new System.EventHandler(this.ctlButton_MouseEnter);
            this.playLastButton.MouseLeave += new System.EventHandler(this.ctlButton_MouseLeave);
            // 
            // playPauseButton
            // 
            this.playPauseButton.AutoSize = true;
            this.playPauseButton.BackColor = System.Drawing.Color.Transparent;
            this.playPauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.playPauseButton.FlatAppearance.BorderSize = 0;
            this.playPauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playPauseButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.playPauseButton.Location = new System.Drawing.Point(78, 118);
            this.playPauseButton.MinimumSize = new System.Drawing.Size(60, 28);
            this.playPauseButton.Name = "playPauseButton";
            this.playPauseButton.Size = new System.Drawing.Size(60, 28);
            this.playPauseButton.TabIndex = 51;
            this.playPauseButton.TabStop = false;
            this.playPauseButton.Text = ">";
            this.playPauseButton.UseVisualStyleBackColor = false;
            this.playPauseButton.Click += new System.EventHandler(this.playCtlButtons_Click);
            this.playPauseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlButton_MouseDown);
            this.playPauseButton.MouseEnter += new System.EventHandler(this.ctlButton_MouseEnter);
            this.playPauseButton.MouseLeave += new System.EventHandler(this.ctlButton_MouseLeave);
            // 
            // playNextButton
            // 
            this.playNextButton.AutoSize = true;
            this.playNextButton.BackColor = System.Drawing.Color.Transparent;
            this.playNextButton.FlatAppearance.BorderSize = 0;
            this.playNextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playNextButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.playNextButton.Location = new System.Drawing.Point(147, 118);
            this.playNextButton.MinimumSize = new System.Drawing.Size(60, 28);
            this.playNextButton.Name = "playNextButton";
            this.playNextButton.Size = new System.Drawing.Size(60, 28);
            this.playNextButton.TabIndex = 52;
            this.playNextButton.TabStop = false;
            this.playNextButton.Text = ">|";
            this.playNextButton.UseVisualStyleBackColor = false;
            this.playNextButton.Click += new System.EventHandler(this.playCtlButtons_Click);
            this.playNextButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlButton_MouseDown);
            this.playNextButton.MouseEnter += new System.EventHandler(this.ctlButton_MouseEnter);
            this.playNextButton.MouseLeave += new System.EventHandler(this.ctlButton_MouseLeave);
            // 
            // volUpLabel
            // 
            this.volUpLabel.AutoSize = true;
            this.volUpLabel.BackColor = System.Drawing.Color.Transparent;
            this.volUpLabel.Font = new System.Drawing.Font("SimSun", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.volUpLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.volUpLabel.Location = new System.Drawing.Point(273, 120);
            this.volUpLabel.Name = "volUpLabel";
            this.volUpLabel.Size = new System.Drawing.Size(27, 28);
            this.volUpLabel.TabIndex = 56;
            this.volUpLabel.Text = "+";
            this.volUpLabel.Click += new System.EventHandler(this.volCtlLabels_Click);
            this.volUpLabel.DoubleClick += new System.EventHandler(this.volCtlLabels_DoubleClick);
            this.volUpLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlButton_MouseDown);
            this.volUpLabel.MouseEnter += new System.EventHandler(this.ctlButton_MouseEnter);
            this.volUpLabel.MouseLeave += new System.EventHandler(this.ctlButton_MouseLeave);
            // 
            // volDownLabel
            // 
            this.volDownLabel.AutoSize = true;
            this.volDownLabel.BackColor = System.Drawing.Color.Transparent;
            this.volDownLabel.Font = new System.Drawing.Font("SimSun", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.volDownLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.volDownLabel.Location = new System.Drawing.Point(215, 120);
            this.volDownLabel.Name = "volDownLabel";
            this.volDownLabel.Size = new System.Drawing.Size(27, 28);
            this.volDownLabel.TabIndex = 57;
            this.volDownLabel.Tag = "";
            this.volDownLabel.Text = "-";
            this.volDownLabel.Click += new System.EventHandler(this.volCtlLabels_Click);
            this.volDownLabel.DoubleClick += new System.EventHandler(this.volCtlLabels_DoubleClick);
            this.volDownLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlButton_MouseDown);
            this.volDownLabel.MouseEnter += new System.EventHandler(this.ctlButton_MouseEnter);
            this.volDownLabel.MouseLeave += new System.EventHandler(this.ctlButton_MouseLeave);
            // 
            // volumeLabel
            // 
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.BackColor = System.Drawing.Color.Transparent;
            this.volumeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.volumeLabel.Location = new System.Drawing.Point(241, 124);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(24, 16);
            this.volumeLabel.TabIndex = 58;
            this.volumeLabel.Text = "50";
            this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.volumeLabel.Click += new System.EventHandler(this.valLabel_Click);
            this.volumeLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctlButton_MouseDown);
            this.volumeLabel.MouseEnter += new System.EventHandler(this.ctlButton_MouseEnter);
            this.volumeLabel.MouseLeave += new System.EventHandler(this.ctlButton_MouseLeave);
            // 
            // infPanel
            // 
            this.infPanel.Controls.Add(this.aheadPicBox);
            this.infPanel.Controls.Add(this.aInfLabel);
            this.infPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infPanel.Location = new System.Drawing.Point(1, 1);
            this.infPanel.Name = "infPanel";
            this.infPanel.Size = new System.Drawing.Size(1016, 158);
            this.infPanel.TabIndex = 76;
            // 
            // aheadPicBox
            // 
            this.aheadPicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.aheadPicBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.aheadPicBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.aheadPicBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("aheadPicBox.ErrorImage")));
            this.aheadPicBox.Image = global::FreshMedia.Properties.Resources.apple_green;
            this.aheadPicBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.aheadPicBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("aheadPicBox.InitialImage")));
            this.aheadPicBox.Location = new System.Drawing.Point(0, 0);
            this.aheadPicBox.MaximumSize = new System.Drawing.Size(105, 112);
            this.aheadPicBox.Name = "aheadPicBox";
            this.aheadPicBox.Size = new System.Drawing.Size(105, 112);
            this.aheadPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.aheadPicBox.TabIndex = 56;
            this.aheadPicBox.TabStop = false;
            this.aheadPicBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picBox_head_MouseDoubleClick);
            // 
            // aInfLabel
            // 
            this.aInfLabel.AutoSize = true;
            this.aInfLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.aInfLabel.Location = new System.Drawing.Point(121, 3);
            this.aInfLabel.Name = "aInfLabel";
            this.aInfLabel.Size = new System.Drawing.Size(56, 144);
            this.aInfLabel.TabIndex = 0;
            this.aInfLabel.Text = "标  题\r\n\r\n艺术家\r\n\r\n专  辑\r\n\r\n年  代\r\n\r\n描  述";
            this.aInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listSplitContainer
            // 
            this.listSplitContainer.BackColor = System.Drawing.Color.Transparent;
            this.listSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.listSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.listSplitContainer.Name = "listSplitContainer";
            // 
            // listSplitContainer.Panel1
            // 
            this.listSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(1);
            this.listSplitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1ContentPanel1_Paint);
            this.listSplitContainer.Panel1MinSize = 20;
            // 
            // listSplitContainer.Panel2
            // 
            this.listSplitContainer.Panel2.Controls.Add(this.lb_state);
            this.listSplitContainer.Panel2.Controls.Add(this.labelLyric);
            this.listSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.listSplitContainer.Panel2.SizeChanged += new System.EventHandler(this.splitContainer1ContentPanel2_SizeChanged);
            this.listSplitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1ContentPanel2_Paint);
            this.listSplitContainer.Panel2MinSize = 520;
            this.listSplitContainer.Size = new System.Drawing.Size(1522, 692);
            this.listSplitContainer.SplitterDistance = 200;
            this.listSplitContainer.TabIndex = 74;
            // 
            // lb_state
            // 
            this.lb_state.AutoSize = true;
            this.lb_state.BackColor = System.Drawing.Color.Transparent;
            this.lb_state.CausesValidation = false;
            this.lb_state.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb_state.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_state.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb_state.Location = new System.Drawing.Point(1, 675);
            this.lb_state.Name = "lb_state";
            this.lb_state.Size = new System.Drawing.Size(40, 16);
            this.lb_state.TabIndex = 56;
            this.lb_state.Text = "状态";
            this.lb_state.UseMnemonic = false;
            // 
            // labelLyric
            // 
            this.labelLyric.BackColor = System.Drawing.Color.Transparent;
            this.labelLyric.ContextMenuStrip = this.cms_main;
            this.labelLyric.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.labelLyric.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelLyric.Location = new System.Drawing.Point(19, 197);
            this.labelLyric.Name = "labelLyric";
            this.labelLyric.Size = new System.Drawing.Size(188, 87);
            this.labelLyric.TabIndex = 53;
            this.labelLyric.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cms_main
            // 
            this.cms_main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cms_main.ForeColor = System.Drawing.Color.Teal;
            this.cms_main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_fLyric,
            this.tsmi_showCurList,
            this.tsmi_opacity});
            this.cms_main.Name = "contextMenuStrip2";
            this.cms_main.Size = new System.Drawing.Size(225, 88);
            this.cms_main.Opening += new System.ComponentModel.CancelEventHandler(this.cms_main_Opening);
            // 
            // tsmi_fLyric
            // 
            this.tsmi_fLyric.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsmi_fLyric.Name = "tsmi_fLyric";
            this.tsmi_fLyric.Size = new System.Drawing.Size(224, 28);
            this.tsmi_fLyric.Text = "显示窗口歌词";
            this.tsmi_fLyric.Click += new System.EventHandler(this.tsmi_fLyric_Click);
            // 
            // tsmi_showCurList
            // 
            this.tsmi_showCurList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsmi_showCurList.Name = "tsmi_showCurList";
            this.tsmi_showCurList.Size = new System.Drawing.Size(224, 28);
            this.tsmi_showCurList.Text = "显示正在播放列表";
            this.tsmi_showCurList.Click += new System.EventHandler(this.tsmi_showPlaying_Click);
            // 
            // tsmi_opacity
            // 
            this.tsmi_opacity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsmi_opacity.Name = "tsmi_opacity";
            this.tsmi_opacity.Size = new System.Drawing.Size(224, 28);
            this.tsmi_opacity.Text = "透明度";
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.BackColor = System.Drawing.Color.Transparent;
            this.mainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainSplitContainer.IsSplitterFixed = true;
            this.mainSplitContainer.Location = new System.Drawing.Point(11, 24);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.controlSplitContainer);
            this.mainSplitContainer.Panel1MinSize = 140;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.listSplitContainer);
            this.mainSplitContainer.Panel2MinSize = 22;
            this.mainSplitContainer.Size = new System.Drawing.Size(1522, 856);
            this.mainSplitContainer.SplitterDistance = 160;
            this.mainSplitContainer.TabIndex = 63;
            // 
            // toolTip
            // 
            this.toolTip.OwnerDraw = true;
            this.toolTip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTip_Draw);
            // 
            // cmPicBox
            // 
            this.cmPicBox.BackColor = System.Drawing.Color.Transparent;
            this.cmPicBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmPicBox.BackgroundImage")));
            this.cmPicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmPicBox.ErrorImage = null;
            this.cmPicBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmPicBox.InitialImage = null;
            this.cmPicBox.Location = new System.Drawing.Point(219, 897);
            this.cmPicBox.Name = "cmPicBox";
            this.cmPicBox.Size = new System.Drawing.Size(21, 21);
            this.cmPicBox.TabIndex = 68;
            this.cmPicBox.TabStop = false;
            this.cmPicBox.Click += new System.EventHandler(this.picBoxs_Click);
            this.cmPicBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmPicBox_MouseDown);
            this.cmPicBox.MouseEnter += new System.EventHandler(this.picBoxs_MouseEnter);
            this.cmPicBox.MouseLeave += new System.EventHandler(this.picBoxs_MouseLeave);
            // 
            // searchPicBox
            // 
            this.searchPicBox.BackColor = System.Drawing.Color.Transparent;
            this.searchPicBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("searchPicBox.BackgroundImage")));
            this.searchPicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.searchPicBox.ErrorImage = null;
            this.searchPicBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.searchPicBox.InitialImage = null;
            this.searchPicBox.Location = new System.Drawing.Point(9, 897);
            this.searchPicBox.Name = "searchPicBox";
            this.searchPicBox.Size = new System.Drawing.Size(21, 21);
            this.searchPicBox.TabIndex = 64;
            this.searchPicBox.TabStop = false;
            this.searchPicBox.Click += new System.EventHandler(this.picBoxs_Click);
            this.searchPicBox.MouseEnter += new System.EventHandler(this.picBoxs_MouseEnter);
            this.searchPicBox.MouseLeave += new System.EventHandler(this.picBoxs_MouseLeave);
            // 
            // locPicBox
            // 
            this.locPicBox.BackColor = System.Drawing.Color.Transparent;
            this.locPicBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("locPicBox.BackgroundImage")));
            this.locPicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.locPicBox.ErrorImage = null;
            this.locPicBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.locPicBox.InitialImage = null;
            this.locPicBox.Location = new System.Drawing.Point(111, 897);
            this.locPicBox.Name = "locPicBox";
            this.locPicBox.Size = new System.Drawing.Size(21, 21);
            this.locPicBox.TabIndex = 70;
            this.locPicBox.TabStop = false;
            this.locPicBox.Click += new System.EventHandler(this.picBoxs_Click);
            this.locPicBox.DoubleClick += new System.EventHandler(this.locPicBox_DoubleClick);
            this.locPicBox.MouseEnter += new System.EventHandler(this.picBoxs_MouseEnter);
            this.locPicBox.MouseLeave += new System.EventHandler(this.picBoxs_MouseLeave);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_file,
            this.tsmi_option,
            this.tsmi_help});
            this.menuStrip.Location = new System.Drawing.Point(11, 1);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip.Size = new System.Drawing.Size(236, 28);
            this.menuStrip.TabIndex = 63;
            // 
            // tsmi_file
            // 
            this.tsmi_file.AutoToolTip = true;
            this.tsmi_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_openFile,
            this.tsmi_history,
            this.tsmi_exit,
            this.tsmi_search});
            this.tsmi_file.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_file.Image")));
            this.tsmi_file.Name = "tsmi_file";
            this.tsmi_file.Size = new System.Drawing.Size(78, 28);
            this.tsmi_file.Text = "文件";
            this.tsmi_file.ToolTipText = "浏览本地音乐文件";
            this.tsmi_file.DropDownOpening += new System.EventHandler(this.tsmi_file_DropDownOpening);
            // 
            // tsmi_openFile
            // 
            this.tsmi_openFile.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_openFile.Image")));
            this.tsmi_openFile.Name = "tsmi_openFile";
            this.tsmi_openFile.Size = new System.Drawing.Size(192, 30);
            this.tsmi_openFile.Text = "打开(&O)";
            this.tsmi_openFile.Click += new System.EventHandler(this.tsmi_openFile_Click);
            // 
            // tsmi_history
            // 
            this.tsmi_history.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_history.Image")));
            this.tsmi_history.Name = "tsmi_history";
            this.tsmi_history.Size = new System.Drawing.Size(192, 30);
            this.tsmi_history.Text = "历史(&H)";
            this.tsmi_history.DropDownOpening += new System.EventHandler(this.tsmi_history_DropDownOpening);
            // 
            // tsmi_exit
            // 
            this.tsmi_exit.Name = "tsmi_exit";
            this.tsmi_exit.Size = new System.Drawing.Size(192, 30);
            this.tsmi_exit.Text = "退出(&Q)";
            this.tsmi_exit.Click += new System.EventHandler(this.tsmi_exit_Click);
            // 
            // tsmi_search
            // 
            this.tsmi_search.Name = "tsmi_search";
            this.tsmi_search.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmi_search.Size = new System.Drawing.Size(192, 30);
            this.tsmi_search.Text = "查找";
            this.tsmi_search.Visible = false;
            this.tsmi_search.Click += new System.EventHandler(this.tsmi_search_Click);
            // 
            // tsmi_option
            // 
            this.tsmi_option.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_theme,
            this.tsmi_cm,
            this.tsmi_sleep,
            this.tsmi_dLyric,
            this.tsmi_moreSet});
            this.tsmi_option.Image = global::FreshMedia.Properties.Resources.setting_32_32;
            this.tsmi_option.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmi_option.Name = "tsmi_option";
            this.tsmi_option.Size = new System.Drawing.Size(78, 28);
            this.tsmi_option.Text = "选项";
            this.tsmi_option.DropDownOpening += new System.EventHandler(this.tsmi_option_DropDownOpening);
            // 
            // tsmi_theme
            // 
            this.tsmi_theme.Name = "tsmi_theme";
            this.tsmi_theme.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tsmi_theme.Size = new System.Drawing.Size(301, 29);
            this.tsmi_theme.Text = "主题设置";
            // 
            // tsmi_cm
            // 
            this.tsmi_cm.Name = "tsmi_cm";
            this.tsmi_cm.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tsmi_cm.Size = new System.Drawing.Size(301, 29);
            this.tsmi_cm.Text = "循环模式(&C)";
            this.tsmi_cm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsmi_sleep
            // 
            this.tsmi_sleep.CheckOnClick = true;
            this.tsmi_sleep.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_sleep.Image")));
            this.tsmi_sleep.Name = "tsmi_sleep";
            this.tsmi_sleep.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tsmi_sleep.Size = new System.Drawing.Size(301, 29);
            this.tsmi_sleep.Text = "Sleeping Model(&S)";
            this.tsmi_sleep.Click += new System.EventHandler(this.tsmi_sleep_Click);
            // 
            // tsmi_dLyric
            // 
            this.tsmi_dLyric.Name = "tsmi_dLyric";
            this.tsmi_dLyric.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tsmi_dLyric.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.L)));
            this.tsmi_dLyric.Size = new System.Drawing.Size(301, 29);
            this.tsmi_dLyric.Text = "打开桌面歌词";
            this.tsmi_dLyric.Click += new System.EventHandler(this.tsmi_dLyric_Click);
            // 
            // tsmi_moreSet
            // 
            this.tsmi_moreSet.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_moreSet.Image")));
            this.tsmi_moreSet.Name = "tsmi_moreSet";
            this.tsmi_moreSet.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tsmi_moreSet.Size = new System.Drawing.Size(301, 29);
            this.tsmi_moreSet.Text = "控制面板(&S)";
            this.tsmi_moreSet.Click += new System.EventHandler(this.tsmi_moreSet_Click);
            // 
            // tsmi_help
            // 
            this.tsmi_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_userHelp,
            this.tsmi_about});
            this.tsmi_help.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_help.Image")));
            this.tsmi_help.Name = "tsmi_help";
            this.tsmi_help.Size = new System.Drawing.Size(78, 28);
            this.tsmi_help.Text = "帮助";
            this.tsmi_help.DropDownOpening += new System.EventHandler(this.tsmi_help_DropDownOpening);
            // 
            // tsmi_userHelp
            // 
            this.tsmi_userHelp.Name = "tsmi_userHelp";
            this.tsmi_userHelp.Size = new System.Drawing.Size(153, 30);
            this.tsmi_userHelp.Text = "帮助";
            this.tsmi_userHelp.Click += new System.EventHandler(this.tsmi_userHelp_Click);
            // 
            // tsmi_about
            // 
            this.tsmi_about.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsmi_about.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_about.Image")));
            this.tsmi_about.Name = "tsmi_about";
            this.tsmi_about.Size = new System.Drawing.Size(153, 30);
            this.tsmi_about.Text = "关于(&A)";
            this.tsmi_about.Click += new System.EventHandler(this.tsmi_about_Click);
            // 
            // sleepLabel
            // 
            this.sleepLabel.AutoSize = true;
            this.sleepLabel.BackColor = System.Drawing.Color.Transparent;
            this.sleepLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sleepLabel.ForeColor = System.Drawing.Color.Green;
            this.sleepLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sleepLabel.Location = new System.Drawing.Point(750, 1);
            this.sleepLabel.Name = "sleepLabel";
            this.sleepLabel.Size = new System.Drawing.Size(16, 16);
            this.sleepLabel.TabIndex = 62;
            this.sleepLabel.Text = " ";
            this.sleepLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sleepLabel.SizeChanged += new System.EventHandler(this.timeLabel_SizeChanged);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Font = new System.Drawing.Font("SimSun", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.timeLabel.ForeColor = System.Drawing.Color.Green;
            this.timeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.timeLabel.Location = new System.Drawing.Point(833, 1);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(0, 14);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.timeLabel.SizeChanged += new System.EventHandler(this.timeLabel_SizeChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.ClientSize = new System.Drawing.Size(1551, 961);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.MinimumSize = new System.Drawing.Size(911, 600);
            this.Name = "MainForm";
            this.Opacity = 0.85D;
            this.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fresh Media";
            this.Activated += new System.EventHandler(this.this_Activated);
            this.Deactivate += new System.EventHandler(this.this_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.this_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.this_FormClosed);
            this.Load += new System.EventHandler(this.this_Load);
            this.Shown += new System.EventHandler(this.this_Shown);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.controlSplitContainer.Panel1.ResumeLayout(false);
            this.controlSplitContainer.Panel1.PerformLayout();
            this.controlSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.controlSplitContainer)).EndInit();
            this.controlSplitContainer.ResumeLayout(false);
            this.infPanel.ResumeLayout(false);
            this.infPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aheadPicBox)).EndInit();
            this.listSplitContainer.Panel2.ResumeLayout(false);
            this.listSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listSplitContainer)).EndInit();
            this.listSplitContainer.ResumeLayout(false);
            this.cms_main.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locPicBox)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cms_main;
        private System.Windows.Forms.MenuStrip menuStrip;

        private System.Windows.Forms.ToolStripMenuItem tsmi_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_exit;
        private System.Windows.Forms.ToolStripMenuItem tsmi_option;
        private System.Windows.Forms.ToolStripMenuItem tsmi_help;
        private System.Windows.Forms.ToolStripMenuItem tsmi_cm;

        private System.Windows.Forms.ToolStripMenuItem tsmi_search;
        private System.Windows.Forms.ToolStripMenuItem tsmi_theme;
        private System.Windows.Forms.ToolStripMenuItem tsmi_sleep;
        private System.Windows.Forms.ToolStripMenuItem tsmi_moreSet;

        private System.Windows.Forms.ToolStripMenuItem tsmi_about;
        private System.Windows.Forms.ToolStripMenuItem tsmi_fLyric;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dLyric;
        private System.Windows.Forms.ToolStripMenuItem tsmi_history;
        private System.Windows.Forms.Label locbarLabel;
        private System.Windows.Forms.Label lb_state;

        private System.Windows.Forms.PictureBox aheadPicBox;

        private System.Windows.Forms.ToolStripMenuItem tsmi_showCurList;
        private System.Windows.Forms.ToolStripMenuItem tsmi_opacity;
        private System.Windows.Forms.ToolStripMenuItem tsmi_userHelp;


        private System.Windows.Forms.PictureBox locPicBox;
        private System.Windows.Forms.PictureBox searchPicBox;
        private System.Windows.Forms.PictureBox cmPicBox;

        private System.Windows.Forms.Button playPauseButton;
        private System.Windows.Forms.Button playLastButton;
        private System.Windows.Forms.Button playNextButton;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.Label volDownLabel;
        private System.Windows.Forms.Label volUpLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label sleepLabel;
        private System.Windows.Forms.Label labelLyric;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label aNameLabel;
        private System.Windows.Forms.Label aInfLabel;
        private System.Windows.Forms.Label aTimeLabel;
        private System.Windows.Forms.Label aLengthLabel;
        private System.Windows.Forms.Label aIndexLabel;
        private System.Windows.Forms.Label aKbpsLabel;
        private System.Windows.Forms.SplitContainer listSplitContainer;
        private System.Windows.Forms.Panel infPanel;
        private System.Windows.Forms.SplitContainer controlSplitContainer;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
    }
}

