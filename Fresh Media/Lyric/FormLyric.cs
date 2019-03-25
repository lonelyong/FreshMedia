using System;
using System.Drawing;
using System.Windows.Forms;
namespace FreshMedia.Lyric
{
    sealed class FormLyric :LyricBase, IFormLyric
    {
        #region private filed
        private LyricManager _mLyric;
        /// <summary>
        /// 同步计时器
        /// </summary>
        private Timer _syncTimer = new Timer();
        /// <summary>
        ///  当前句歌词显示面板的大小
        /// </summary>
        private Size _clientSize { get { return _lyricLabel.Size; } }
        /// <summary>
        /// 显示当前句歌词的Label
        /// </summary>
        private Label _lyricLabel = new Label();
        #endregion

        #region constructor destructor 
        public FormLyric(LyricManager _mLyric)
        {
            //只进行字段赋值
            this._mLyric = _mLyric;
            base.Font = new Font(base.Font.Name, 20f, base.Font.Style);
            base._MaxFontSize = 36;
            base._MinFontSize = 12;

        }

        public void Init()
        {
            RefreshRate = 10;
            _syncTimer.Enabled = false;
            _syncTimer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                syncLyric();
            });
            VisibleChangedEvent += new LyricVisibleChangedEventHandler((visibleChangedEvent));
            LyricStyleChangedEvent += new LyricStyleChangedEventHandler(lyricStyleChangedEvent);
            //Reset();
        }
        #endregion

        #region syncLyric
        protected override void LyricPaint(Graphics g)
        {
            if (string.IsNullOrWhiteSpace(CurrentLyric))
                return;
            //开始绘制的坐标
            _BeginPointF.X = (_clientSize.Width - _CurrentLyricSizeF.Width) / 2;
            _BeginPointF.Y = (_clientSize.Height - _CurrentLyricSizeF.Height) / 2;
            _EndPointF.X = _BeginPointF.X + _CurrentLyricSizeF.Width;
            _EndPointF.Y = _BeginPointF.Y;
            //设置颜色显示范围，三个区域： Blue区，Blue向Red过度区（过度区很短)
            _RelativityPosition = _DrawPosition / (_CurrentLyricSizeF.Width - _CurrentLyricSizeF.Height);
            _ColorBlendPosition[1] = _RelativityPosition;
            _ColorBlendPosition[2] = _RelativityPosition + 0.0001f;
            _ColorBlend.Positions = _ColorBlendPosition;
            //定义好颜色格式

            System.Drawing.Drawing2D.LinearGradientBrush _brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                  _BeginPointF
                , _EndPointF
                , PlayedColor
                , PrepColor);
            _brush.InterpolationColors = _ColorBlend;
            g.DrawString(CurrentLyric, Font, _brush, _BeginPointF);
        }

        private void lyricPaint(object sender, PaintEventArgs e)
        {
            LyricPaint(e.Graphics);
        }

        private void syncLyric()
        {
            if (_DrawPosition < _CurrentLyricSizeF.Width)
                _DrawPosition += _DrawLengthOfOnce;
            else
                _DrawPosition = 0;
            RefreshCurrentLyric();
        }
        #endregion

        #region IFormLyric
        /// <summary>
        /// 设置歌词显示的控件
        /// </summary>
        /// <param name="label"></param>
        public void SetLabel(Label label)
        {
            if (label == null)
                throw new ArgumentNullException("label不能为null");
            _lyricLabel = label;
            _lyricLabel.Font = Font;
            _lyricLabel.ForeColor = PrepColor;
            _lyricLabel.Paint -= new PaintEventHandler(lyricPaint);
            _lyricLabel.Paint += new PaintEventHandler(lyricPaint);
        }
        /// <summary>
        /// 刷新绘制当前歌词
        /// </summary>
        public void RefreshCurrentLyric() { _lyricLabel.Refresh(); }
        /// <summary>
        /// 设置歌词
        /// </summary>
        /// <param name="lyric0">前两句</param>
        /// <param name="lyric1">前一句</param>
        /// <param name="lyric2">当前歌词</param>
        /// <param name="lyric3">后一句</param>
        /// <param name="lyric4">后两句</param>
        public void SetLyric(string lyric0, string lyric1, string lyric2, string lyric3, string lyric4)
        {
            _lyricLabel.Text = string.Format("{0}\r\n\r\n{1}\r\n\r\n\r\n\r\n{2}\r\n\r\n{3}", lyric0, lyric1, lyric3, lyric4);
        }
        #endregion

        #region private methods
        private void visibleChangedEvent(LyricVisibleChangedEventArgs e)
        {

        }

        private void lyricStyleChangedEvent(LyricStyleChangedEventArgs e)
        {
            _lyricLabel.Font = e.LyricStyle.Font;
            _lyricLabel.ForeColor = e.LyricStyle.Color;
            CurrentLyric = CurrentLyric;
        }
        #endregion

        #region override
        #region ILyric
        /// <summary>
        /// 是否同步（启用）窗口歌词
        /// </summary>
        public override bool Enable
        {
            set
            {
                base.Enable = value && Visible;
                _syncTimer.Enabled = base.Enable;
            }
            get
            {
                return base.Enable;
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
                _DrawLengthOfOnce = _CurrentLyricSizeF.Width / ((float)RefreshRate * _mLyric.CurrentLyricTimeLength / 1000);
                double timetmp = 1 - (double)_mLyric.GetCurrentLyricLeftTime() / _mLyric.CurrentLyricTimeLength;
                _DrawPosition = (float)(timetmp * _CurrentLyricSizeF.Width);
                RefreshCurrentLyric();
            }
        }

        /// <summary>
        /// 获取或设置当前句歌词的刷新频率
        /// </summary>
        public override int RefreshRate
        {
            set
            {
                base.RefreshRate = value;
                _syncTimer.Interval = 1000 / value;
            }
            get
            {
                return base.RefreshRate;
            }
        }

        public override void Reset()
        {
            SetLyric(null, null, LyricManager.DEFAULTLYRIC, null, null);
            CurrentLyric = LyricManager.DEFAULTLYRIC;
        }
        #endregion

        #region ILyricStyle
      
        #endregion
        #endregion
    }
}
