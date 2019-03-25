using System.Drawing;
using System;
namespace FreshMedia.Lyric
{
    class DesktopLyric : LyricBase, IDesktopLyric, IFreshMedia
    {
        #region private filed
        private LyricManager _mLyric;

        private Controller.MainController _mc;

        private IDesktopLyricF deskLyricF;

        private Type type; 
        //  获取窗口上行或下行的大小
        private Size clientSize { get { return deskLyricF.LyricPanelSize; } }
        //  同步歌词计时器
        private System.Timers.Timer syncTimer = new System.Timers.Timer();

        private System.Threading.Thread _dThread;

        private Point _ClientLocation = new Point((System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - 888) / 2, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 128);
        //  获取或设置一个值该值指示当前句歌词在上面还是下面
        private bool _bDrawTop = true;

        private bool _showDefaultLyric = false;
        #endregion

        #region public field
        /// <summary>
        /// 设置或获取面板上行歌词
        /// </summary>
        public string TopLyric { get; private set; }
        /// <summary>
        /// 获取或设置面板下行歌词
        /// </summary>
        public string DownLyric { get; private set; }
        #endregion

        #region constructor destructor 
        public DesktopLyric(LyricManager _mLyric)
        {
            //只进行字段赋值
            this._mLyric = _mLyric;
            this._mc = ((IFreshMedia)_mLyric).Controller;
            base.Font = new Font(base.Font.Name, 24f, base.Font.Style);
            base._MaxFontSize = 36;
            base._MinFontSize = 12;
        }

        public void Init()
        {
            Locked = true;
            RefreshRate = 20;

            VisibleChangedEvent += new LyricVisibleChangedEventHandler((LyricVisibleChangedEventArgs e) => { });
            LyricStyleChangedEvent += new LyricStyleChangedEventHandler(lyricStyleChangedEvent);

            syncTimer.Enabled = false;
            syncTimer.Elapsed += new System.Timers.ElapsedEventHandler((object sender, System.Timers.ElapsedEventArgs e) =>
            {
                syncLyric();
            });
            deskLyricF = new View.DesktopLyricF(this);
            _mc.PlayController.myPlayer.PlayStateChangedEvent += new Player.PlayStateChangedEventHandler(playStateChangedEvent);
        }
        #endregion

        #region private method
        private void start()
        {
            _dThread = new System.Threading.Thread(() =>
            {
                deskLyricF = type.Assembly.CreateInstance(
                    type.FullName, 
                    true, 
                    System.Reflection.BindingFlags.Default, 
                    null, 
                    new object[] { this }, 
                    null, 
                    null) as IDesktopLyricF;
                deskLyricF.TopLyricPaintEvent += new System.Windows.Forms.PaintEventHandler(topLyricPaint);
                deskLyricF.DownLyricPaintEvent += new System.Windows.Forms.PaintEventHandler(downLyricPaint);
                deskLyricF.Show();
            });
            _dThread.IsBackground = true;
            _dThread.SetApartmentState(System.Threading.ApartmentState.MTA);
            _dThread.Priority = System.Threading.ThreadPriority.AboveNormal;
            _dThread.Start();
        }

        private void playStateChangedEvent(Player.PlayStateChangedEventArgs e)
        {
            switch (e.PlayState)
            {
                case Player.PlayStates.playing:
                case Player.PlayStates.paused:
                    break;
                case Player.PlayStates.stoped:
                case Player.PlayStates.mediaEnd:
                case Player.PlayStates.transitioning:
                    CurrentLyric = LyricManager.DEFAULTLYRIC;
                    _DrawPosition = 0;
                    break;
            }
        }

        private void lyricStyleChangedEvent(LyricStyleChangedEventArgs e)
        {
            _ColorBlend.Colors = new Color[4] { e.LyricStyle.PlayedColor, e.LyricStyle.GradualChangeColor, e.LyricStyle.PrepColor, e.LyricStyle.PrepColor };
            CurrentLyric = CurrentLyric;
        }
        #endregion

        #region syncLyric

        protected override void LyricPaint(Graphics g)
        {
            //当前句歌词为空时退出
            if (string.IsNullOrWhiteSpace(CurrentLyric))
                return;
            //当前句歌词显示到的位置的百分比
            _RelativityPosition = _DrawPosition / (_CurrentLyricSizeF.Width - _CurrentLyricSizeF.Height);
            /// 设置颜色显示范围，三个区域： Blue区，Blue向Red过度区（过度区很短）
            _ColorBlendPosition[1] = _RelativityPosition;
            _ColorBlendPosition[2] = _RelativityPosition + 0.0001f;
            _ColorBlend.Positions = _ColorBlendPosition;
            // 定义好颜色格式
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
               _BeginPointF
             , _EndPointF
             , PlayedColor
             , PrepColor);
            brush.InterpolationColors = _ColorBlend;
            //开始绘制文字
            g.DrawString(CurrentLyric, Font, brush, _BeginPointF);
        }

        public void topLyricPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (_bDrawTop)
                if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused || _mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                    if (_mLyric.LyricDoc.IsExisted)
                    {
                        _BeginPointF.X = 0;
                        _BeginPointF.Y = (clientSize.Height - _CurrentLyricSizeF.Height) / 2;
                        _EndPointF.X = _BeginPointF.X + _CurrentLyricSizeF.Width;
                        _EndPointF.Y = _BeginPointF.Y;
                        LyricPaint(e.Graphics);
                    }
        }

        public void downLyricPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused || _mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
            {
                if (_mLyric.LyricDoc.IsExisted)
                {
                    if (_bDrawTop == false)
                        _showDefaultLyric = false;
                    else
                        return;
                }
                else
                    _showDefaultLyric = true;
            }
            else
                _showDefaultLyric = true;
            if (_showDefaultLyric)
            {
                _BeginPointF.X = (clientSize.Width - _CurrentLyricSizeF.Width) / 2;
                _BeginPointF.Y = (clientSize.Height - _CurrentLyricSizeF.Height) / 2;
                _EndPointF.X = _BeginPointF.X + _CurrentLyricSizeF.Width;
                _EndPointF.Y = _BeginPointF.Y;
            }
            else
            {
                _BeginPointF.X = clientSize.Width - _CurrentLyricSizeF.Width;
                _BeginPointF.Y = (clientSize.Height - _CurrentLyricSizeF.Height) / 2;
                _EndPointF.X = clientSize.Width;
                _EndPointF.Y = _BeginPointF.Y;
            }
            LyricPaint(e.Graphics);
        }

        private void syncLyric()
        {
            if (_DrawPosition < _CurrentLyricSizeF.Width)
            {
                _DrawPosition += _DrawLengthOfOnce;
                deskLyricF.RefreshLyric(_bDrawTop);
            }
            else
            {
                _DrawPosition = 0;
                Refresh();
            }
        }
        #endregion

        #region IDesktopLyric
        /// <summary>
        /// 获取或设置一个值该值指示桌面歌词是否锁定
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// 该值存储桌面歌词的位置
        /// </summary>
        public Point ClientLocation
        {
            get
            {
                return _ClientLocation;
            }
            set
            {
                _ClientLocation = value;
            }
        }
        /// <summary>
        /// 设置桌面上下行歌词
        /// </summary>
        public void SetLyric(int currentIndex, string current, string next)
        {
            _bDrawTop = currentIndex % 2 == 0;
            if (_bDrawTop)
            {
                DownLyric = next; TopLyric = null;
            }
            else
            {
                TopLyric = next; DownLyric = null;
            }
            deskLyricF.SetLyric(TopLyric, DownLyric);
            Refresh();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            deskLyricF.RefreshLyric(true);
            deskLyricF.RefreshLyric(false);
        }
        #endregion

        #region IFreshMedia
        Controller.MainController IFreshMedia. Controller
        {
            get
            {
                return this._mc;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// 设置桌面歌词窗口，必须实现IDesktopLyricF接口
        /// </summary>
        /// <param name="desktopLyricF">实现IDesktopLyricF接口</param>
        public void SetDesktopLyricF(Type t)
        {
            object obj = t.Assembly.CreateInstance(t.FullName, true, System.Reflection.BindingFlags.Default, null, new object[] { this }, null, null);
            if (obj is IDesktopLyricF)
                type = t;
            else
                throw new Exception($"Type t没有实现{nameof(IDesktopLyricF)}接口");
        }

        #endregion

        #region override
        #region ILyricStyle

        #endregion

        #region ILyric

        /// <summary>
        /// 设置或获取一个值该值指示桌面歌词是否显示
        /// </summary>
        public override bool Visible
        {
            set
            {
                //在未启动情况下启用桌面歌词
                if (value && deskLyricF.IsLoaded == false)
                {
                    start();
                    //等待桌面歌词窗口启动
                    while (Visible == false)
                        System.Threading.Thread.Sleep(10);
                }
                else if (value == false && deskLyricF.IsLoaded)
                {
                    deskLyricF.BeginInvoke(new Action(()=> { deskLyricF.Close(); }));
                }
                base.Visible = deskLyricF.IsLoaded;
            }
            get
            {
                return deskLyricF.IsLoaded;
            }
        }

        /// <summary>
        ///  获取或设置一个值该值指示是否同步桌面歌词
        /// </summary>
        public override bool Enable
        {
            set
            {
                base.Enable = value && Visible;
                syncTimer.Enabled = base.Enable;
            }
            get
            {
                return base.Enable;
            }
        }

        /// <summary>
        ///  存储歌词当前刷新的频率
        /// </summary>
        public override int RefreshRate
        {
            set
            {
                base.RefreshRate = value;
                syncTimer.Interval = 1000 / value;
            }
            get
            {
                return base.RefreshRate;
            }
        }

        /// <summary>
        /// 获取或设置当前句歌词
        /// </summary>
        public override string CurrentLyric
        {
            get { return base.CurrentLyric; }
            set
            {
                base.CurrentLyric = value;
                _DrawLengthOfOnce
                  = _CurrentLyricSizeF.Width / ((float)RefreshRate * _mLyric.CurrentLyricTimeLength / 1000);
                double timetmp = 1 - (double)_mLyric.GetCurrentLyricLeftTime() / _mLyric.CurrentLyricTimeLength;
                _DrawPosition = (float)(timetmp * _CurrentLyricSizeF.Width);
                Refresh();          
            }
        }

        public override void Reset()
        {
            CurrentLyric = LyricManager.DEFAULTLYRIC;
        }
        #endregion
        #endregion
    }
}
