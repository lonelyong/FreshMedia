using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NgNet.UI.Forms;
using System.Runtime.InteropServices;
namespace FreshMedia.View
{
    sealed partial class DesktopLyricF : Form, Lyric.IDesktopLyricF
    {
        #region private fields
        //背景窗体
        private FormEx backgroundF;

        private Lyric.IDesktopLyric desktopLyric;

        private Controller.MainController _mc;

        private FormHelper hForm;
        #endregion

        #region public properties
        /// <summary>
        /// 指示窗体是否显示
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return FormHelper.IsLoaded(this);
            }
        }
        #endregion

        #region constructor destructor 
        public DesktopLyricF(Lyric.IDesktopLyric desktopLyric)
        {
            InitializeComponent();
            this.desktopLyric = desktopLyric;
            _mc = ((IFreshMedia)desktopLyric).Controller;
            hForm = new FormHelper(this);
            MaximumSize = MinimumSize = Size;
            hForm.SetFormRoundRgn(3, 3);
        }
        #endregion

        #region override
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);    // 不显示在TaskBar
                cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt-Tab
                return cp;
            }
        }
        #endregion

        #region move form
        private Point mouseOff;//鼠标移动位置变量

        private bool leftFlag;//标签是否为左键

        private void bgf_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
                Cursor.Clip = new Rectangle(Screen.PrimaryScreen.WorkingArea.Left, Screen.PrimaryScreen.WorkingArea.Top, Screen.PrimaryScreen.WorkingArea.Right, Screen.PrimaryScreen.WorkingArea.Bottom);
            }
        }

        private void bgf_MouseMove(object sender, MouseEventArgs e)
        {
            if (!leftFlag || _mc.LyricManager.DLyric.Locked)
                return;
            Point p = Cursor.Position;
            p.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置

            if (p.X < Screen.PrimaryScreen.WorkingArea.Left)
                p.X = Screen.PrimaryScreen.WorkingArea.Left;
            if (p.X > Screen.PrimaryScreen.WorkingArea.Right - Size.Width)
                p.X = Screen.PrimaryScreen.WorkingArea.Right - Size.Width;
            if (p.Y < Screen.PrimaryScreen.WorkingArea.Top)
                p.Y = Screen.PrimaryScreen.WorkingArea.Top;
            if (p.Y > Screen.PrimaryScreen.WorkingArea.Bottom - Size.Height)
                p.Y = Screen.PrimaryScreen.WorkingArea.Bottom - Size.Height;
            if (p.X <= Screen.PrimaryScreen.WorkingArea.Left
                 || p.X >= Screen.PrimaryScreen.WorkingArea.Right - Size.Width
                 || p.Y <= Screen.PrimaryScreen.WorkingArea.Top
                 || p.Y >= Screen.PrimaryScreen.WorkingArea.Bottom - Size.Height)
            { //mouseOff = new Point(-e.X, -e.Y);
            }
            backgroundF.Location = p;
            Location = p;

        }

        private void bgf_MouseUp(object sender, MouseEventArgs e)
        {
            leftFlag = false;
            Cursor.Clip = Screen.PrimaryScreen.Bounds;
        }
        #endregion

        #region cms_lyric
        private void tsmi_fontSize_items_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Text == "自定义")
            {
                _mc.MainThreadBeginInvoke(new Action(()=> { tsmi_moreSet_Click(sender, e); }));
            }
            else
            {
                _mc.MainThreadBeginInvoke( new Action(() => 
                    {
                        desktopLyric.Font = new Font(desktopLyric.Font.Name, 
                            (int)(tsmi.Tag), 
                            desktopLyric.Font.Style); }));  
            }
        }

        private void tsmi_lrcOpacity_items_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            this.Opacity = (float)tsmi.Tag;
        }

        private void tsmi_lock_Click(object sender, EventArgs e)
        {
            if (!desktopLyric.Locked)
            {
                _mc.NotiryIcon.ShowNotice(6, "~﹏~", "桌面歌词已锁定，请鼠标右键桌面歌词点此解锁歌词项解锁！", ToolTipIcon.Info);
                this.setTransparent(true);
            }
            desktopLyric.Locked = !desktopLyric.Locked;
            this.lockButton.Text = desktopLyric.Locked ? "Unlock" : "Lock";
        }

        private void tsmi_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmi_moreSet_Click(object sender, EventArgs e)
        {
            _mc.MainThreadBeginInvoke(new Action(()=> { _mc.ShowConfigDialog(VSetting.SettingTabs.Lyric, 1); }));         
        }

        private void cms_lrc_Opening(object sender, CancelEventArgs e)
        {
            this.tmsi_lock.Text = desktopLyric.Locked ? "解锁歌词" : "锁定歌词";
        }
        #endregion

        #region move form & form hide

        private void setTransparent(bool transparent)
        {
            backgroundF.TopMost = true;
            if (transparent)
            {
                backgroundF.BackColor = SystemColors.Control;
                ctlPanel.Visible = false;
            }
            else
            {
                backgroundF.BackColor = Color.Black;
                ctlPanel.Visible = true;
            }
        }

        private void bgf_MouseEnter(object sender, EventArgs e)
        {
            if (desktopLyric.Locked)
                toolTip.SetToolTip((Control)sender, "右鍵解鎖");
            else
                setTransparent(false);
        }

        private void bgf_MouseLeave(object sender, EventArgs e)
        {
            if (Cursor.Position.X < this.Left + 1
                || Cursor.Position.Y < this.Top + 1
                || Cursor.Position.X > this.Left + this.Width - 1
                || Cursor.Position.Y > this.Top + this.Height - 1)
            {
                setTransparent(true);
            }
        }

        private void btns_Click(object sender, EventArgs e)
        {
            if (sender == this.lockButton)
            {
                tsmi_lock_Click(sender, e);
            }

            else if (sender == this.mainformButton)
            {
                _mc.MainThreadInvoke(new Action(()=> { _mc.ShowMainForm(); }));
            }

            else if (sender == this.fsUpButton)
            {
                Font font = desktopLyric.Font;
                _mc.MainThreadBeginInvoke(new Action(() => {
                    desktopLyric.Font = font.Size + 2 < desktopLyric.MaxFontSize 
                    ? new Font(font.Name, font.Size + 2, font.Style)
                    : new Font(font.Name, desktopLyric.MaxFontSize, font.Style); }));
            }

            else if (sender == this.fsDownButton)
            {
                Font font = desktopLyric.Font;
                _mc.MainThreadBeginInvoke(new Action(() => {
                    desktopLyric.Font = font.Size - 2 > desktopLyric.MinFontSize
                    ? new Font(font.Name, font.Size - 2, font.Style)
                    : new Font(font.Name, desktopLyric.MinFontSize, font.Style);
                }));
            }

            else if (sender == this.closeButton)
            {
                tsmi_exit_Click(sender, e);
            }

            else if (sender == this.playLastButton || sender == this.nextButton || sender == this.pauseButton)
            {
                _mc.MainForm.BeginInvoke(new Action(() => { _mc.MainForm.playCtlButtons_Click(sender, e); }));
            }
        }
        #endregion

        #region this
        new public void Show()
        {
            if (FormHelper.IsLoaded(backgroundF))
            {
                backgroundF.Invoke(new Action(() => { backgroundF.Activate(); }));
                return;
            }
            backgroundF = new FormEx();
            backgroundF.ShowInTaskbar = false;
            backgroundF.ShowIcon = false;
            backgroundF.FormBorderStyle = FormBorderStyle.None;
            backgroundF.Size = Size;
            backgroundF.StartPosition = FormStartPosition.Manual;
            backgroundF.Location = desktopLyric.ClientLocation;
            backgroundF.TopMost = true;
            backgroundF.Cursor = Cursors.SizeAll;
            backgroundF.ContextMenuStrip = cms_setting;
            backgroundF.MaximumSize = Size;

            backgroundF.SizeChanged += new EventHandler((object sender, EventArgs e) => { Size = backgroundF.Size; });
            backgroundF.MouseEnter += new EventHandler(bgf_MouseEnter);
            backgroundF.MouseLeave += new EventHandler(bgf_MouseLeave);
            backgroundF.MouseDown += new MouseEventHandler(bgf_MouseDown);
            backgroundF.MouseMove += new MouseEventHandler(bgf_MouseMove);
            backgroundF.MouseUp += new MouseEventHandler(bgf_MouseUp);
            backgroundF.Load += new EventHandler((object sender, EventArgs e) =>
            {
                Owner = backgroundF;
                Location = backgroundF.Location;
                topLabel.MouseEnter += new EventHandler(bgf_MouseEnter);
                bottomLabel.MouseEnter += new EventHandler(bgf_MouseEnter);
                topLabel.MouseDown += new MouseEventHandler(bgf_MouseDown);
                bottomLabel.MouseDown += new MouseEventHandler(bgf_MouseDown);
                topLabel.MouseMove += new MouseEventHandler(bgf_MouseMove);
                bottomLabel.MouseMove += new MouseEventHandler(bgf_MouseMove);
                topLabel.MouseUp += new MouseEventHandler(bgf_MouseUp);
                bottomLabel.MouseUp += new MouseEventHandler(bgf_MouseUp);
                base.Show();
            });
            backgroundF.TransparencyKey = TransparencyKey;
            backgroundF.BackColor = SystemColors.Control;
            backgroundF.Opacity = 0.28d;

            FormHelper formHelper = new FormHelper(backgroundF);
            formHelper.SetFormRoundRgn(3, 3);
            backgroundF.ShowDialog();
        }

        new public void Close()
        {
            base.Close();
            backgroundF.Close();
        }

        private void setMenuStrip()
        {
            //字体大小菜单
            ToolStripMenuItem tsmi;
            for (int i = 12; i <= 28; i += 2)
            {
                tsmi = new ToolStripMenuItem();
                tsmi.Text = i.ToString();
                tsmi.Tag = i;
                tsmi.Click += new EventHandler(tsmi_fontSize_items_Click);
                tsmi.ForeColor = Color.DarkGreen;
                tsmi_fontSize.DropDownItems.Add(tsmi);
            }
            tsmi = new ToolStripMenuItem();
            tsmi.Text = "自定义";
            tsmi.Click += new EventHandler(this.tsmi_fontSize_items_Click);
            tsmi.ForeColor = Color.DarkGreen;
            tsmi_fontSize.DropDownItems.Add(tsmi);
            tsmi_fontSize.DropDown.Opacity = 0.66d;
            // 歌词透明度菜单
            for (float i = 0f; i <= 0.5f; i += 0.1f)
            {
                tsmi = new ToolStripMenuItem();
                tsmi.Text = i.ToString("0%");
                tsmi.Tag = 1 - i;
                tsmi.Click += new EventHandler(tsmi_lrcOpacity_items_Click);
                tsmi.ForeColor = Color.DarkGreen;
                tsmi_lrcOpacity.DropDownItems.Add(tsmi);
            }
            tsmi_lrcOpacity.DropDown.Opacity = 0.66;
            cms_setting.Opacity = 0.66d;
        }

        private void setCtlButton()
        {
            playLastButton.Tag = Controller.Commands.PC_PLAYLAST;
            nextButton.Tag = Controller.Commands.PC_PLAYNEXT;
            pauseButton.Tag = Controller.Commands.PC_PLAYPAUSE;
        }

        private void setParent()
        {
            IntPtr hDesktop = NgNet.Windows.Apis.User32.FindWindow("Progman", "Program   Manager");
            NgNet.Windows.Apis.User32.SetParent(backgroundF.Handle.ToInt32(), hDesktop.ToInt32());
        }

        private void this_Load(object sender, EventArgs e)
        {
            // 初始化界面      
            //   设置界面
            setTransparent(desktopLyric.Locked);
            // 显示桌面时不最小化
            setParent();
            // 设置菜单
            setMenuStrip();
            // 根据tag来判断按钮的功能
            setCtlButton();
            // 关闭窗体时设置窗体未显示
            _mc.PlayController.myPlayer.PlayStateChangedEvent += new Player.PlayStateChangedEventHandler(playStateChangedEvent);
            _mc.LyricManager.DLyric.LyricStyleChangedEvent += new Lyric.LyricStyleChangedEventHandler(lyricStyleChangedEvent);
            FormClosed += new FormClosedEventHandler((object obj, FormClosedEventArgs fcea) =>
            {
                _mc.PlayController.myPlayer.PlayStateChangedEvent -= new Player.PlayStateChangedEventHandler(playStateChangedEvent);
                _mc.LyricManager.DLyric.LyricStyleChangedEvent -= new Lyric.LyricStyleChangedEventHandler(lyricStyleChangedEvent);
                _mc.NotiryIcon.ShowNotice(6, "桌面歌词", "桌面歌词已关闭，你可以重新打开", ToolTipIcon.Info);
            });
            //   应用歌词样式
            lyricStyleChangedEvent(new Lyric.LyricStyleChangedEventArgs(desktopLyric));
        }

        private void this_LocationChanged(object sender, EventArgs e)
        {
            desktopLyric.ClientLocation = Location;//記錄窗體位置
        }
        #endregion

        #region private method
        /// <summary>
        /// 线程安全的
        /// </summary>
        /// <param name="e"></param>
        private void playStateChangedEvent(Player.PlayStateChangedEventArgs e)
        {
            if (IsLoaded)
            {
                MethodInvoker mi = delegate
                {
                    applyPlayState(e.PlayState);
                };
                Invoke(mi);
            }
            else
                applyPlayState(e.PlayState);

        }
        /// <summary>
        /// 线程安全的
        /// </summary>
        /// <param name="e"></param>
        private void lyricStyleChangedEvent(Lyric.LyricStyleChangedEventArgs e)
        {       
            if (IsLoaded)
            {
                MethodInvoker mi = delegate
                {
                    setStyle(e.LyricStyle);
                };
                Invoke(mi);
            }
            else 
                setStyle(e.LyricStyle);
        }

        private void refreshLyric(bool bTop)
        {
            if (bTop)
                topLabel.Refresh();
            else
                bottomLabel.Refresh();
        }

        private void setLyric(string topLyric, string downLyric)
        {
            topLabel.Text = topLyric;
            bottomLabel.Text = downLyric;
        }

        private void setStyle(Lyric.ILyricStyle lyricStyle)
        {
            topLabel.Font = lyricStyle.Font;
            bottomLabel.Font = lyricStyle.Font;
            topLabel.ForeColor = lyricStyle.PrepColor;
            bottomLabel.ForeColor = lyricStyle.PrepColor;
        }

        private void applyPlayState(Player.PlayStates playState)
        {
            switch (playState)
            {
                case Player.PlayStates.playing:
                    pauseButton.Text = ControlTexts.SIGN_PLAYSTATE_PAUSE;
                    break;
                case Player.PlayStates.paused:
                    pauseButton.Text = ControlTexts.SIGN_PLAYSTATE_PLAY;
                    break;
                case Player.PlayStates.stoped:
                case Player.PlayStates.mediaEnd:
                case Player.PlayStates.transitioning:
                    pauseButton.Text = ControlTexts.SIGN_PLAYSTATE_PLAY;
                    setLyric(string.Empty, string.Empty);
                    refreshLyric(true);
                    refreshLyric(false);
                    break;
            }
        }
        #endregion

        #region IDesktopLyricF
        event PaintEventHandler Lyric.IDesktopLyricF.TopLyricPaintEvent
        {
            add
            {
                topLabel.Paint += value;
            }

            remove
            {
                topLabel.Paint -= value;
            }
        }

        event PaintEventHandler Lyric.IDesktopLyricF.DownLyricPaintEvent
        {
            add
            {
                bottomLabel.Paint += value;
            }

            remove
            {
                bottomLabel.Paint -= value;
            }
        }

        Point Lyric.IDesktopLyricF.Location
        {
            get
            {
                return Location;
            }

            set
            {
                Location = value;
            }
        }

        Size Lyric.IDesktopLyricF.LyricPanelSize
        {
            get
            {
                return topLabel.Size;
            }
        }

        bool Lyric.IDesktopLyricF.IsLoaded
        {
            get
            {
                return IsLoaded;
            }
        }

        /// <summary>
        /// 线程安全的
        /// </summary>
        /// <param name="e"></param>
        void Lyric.IDesktopLyricF.SetLyric(string topLyric, string downLyric)
        {
            if (IsLoaded)
            {
                MethodInvoker mi = delegate
                {
                    setLyric(topLyric, downLyric);
                };
                BeginInvoke(mi);
            }
            else
                setLyric(topLyric, downLyric);
        }

        /// <summary>
        /// 线程安全的
        /// </summary>
        /// <param name="e"></param>
        void Lyric.IDesktopLyricF.RefreshLyric(bool bTop)
        {
            if (IsLoaded)
            {
                MethodInvoker mi = delegate
                {
                    refreshLyric(bTop);
                };
                BeginInvoke(mi);
            }
            else
                refreshLyric(bTop);
        }

        void Lyric.IDesktopLyricF.Show()
        {
            Show();
        }

        void Lyric.IDesktopLyricF.Close()
        {
            Close();
        }

        void Lyric.IDesktopLyricF.BeginInvoke(Delegate method)
        {
            this.BeginInvoke(method);
        }
        #endregion
    }


}
