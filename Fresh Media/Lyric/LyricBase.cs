using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.Lyric
{
    abstract class LyricBase : ILyric
    {
        #region private fileds
        string _CurrentLyric      = "Audio World";
        bool   _visible           = false;
        bool   _enable            = false;
        int    _refreshRate       = 10;

        Font  _font               = new Font("宋体", 20f, FontStyle.Regular);
        Color _playedColor        = Color.DeepPink;
        Color _gradualChangeColor = Color.Pink;
        Color _color              = Color.DodgerBlue;
        Color _prepColor          = Color.Green;

        Graphics graphics = Graphics.FromImage(new Bitmap(1,1));
        #endregion

        #region protected fileds
        /// <summary>
        /// 最大字号大小
        /// </summary>
        protected float _MaxFontSize = 32f;
        /// <summary>
        /// 最小字号大小
        /// </summary>
        protected float _MinFontSize = 12f;
        /// <summary>
        /// 存储当前句歌词绘制到的位置
        /// </summary>
        protected float _DrawPosition = 0f;
        /// <summary>
        ///  当前句歌词每次刷新绘制的增加的长度
        /// </summary>                            
        protected float _DrawLengthOfOnce = 0f;
        /// <summary>
        /// 开始绘制位置
        /// </summary>
        protected PointF _BeginPointF = new PointF(0f, 0f);
        /// <summary>
        /// 结束绘制位置
        /// </summary>
        protected PointF _EndPointF = new PointF(1f, 1f);
        /// <summary>
        /// 存储当前句歌词的长宽大小
        /// </summary>
        protected SizeF _CurrentLyricSizeF = new SizeF(0, 0);
        /// <summary>
        /// 
        /// </summary>
        protected float _RelativityPosition = 0f;
        /// <summary>
        /// 
        /// </summary>
        protected float[] _ColorBlendPosition = new float[4] { 0f, 0f, 0f, 1f };
        /// <summary>
        /// 实例化的时候必须初始化_colorBlend.Colors
        /// </summary>
        protected System.Drawing.Drawing2D.ColorBlend _ColorBlend = new System.Drawing.Drawing2D.ColorBlend(4);
        #endregion

        #region protected methods
        protected abstract void LyricPaint(Graphics g);
        #endregion

        #region ILyric
        /// <summary>
        /// 是否同步（启用）窗口歌词
        /// </summary>
        public virtual bool Enable
        {
            set
            {
                _enable = value;
            }
            get
            {
                return _enable;
            }
        }

        /// <summary>
        ///  获取或设置一个值该值指示是否显示桌面歌词
        /// </summary>
        public virtual bool Visible
        {
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    VisibleChangedEvent(new LyricVisibleChangedEventArgs(value));
                }
            }
            get { return _visible; }
        }

        /// <summary>
        /// 获取或设置当前句歌词
        /// </summary>
        public virtual string CurrentLyric
        {
            get { return _CurrentLyric; }
            set
            {
                _CurrentLyric = value;
                setCurrentLyricSizeF();
            }
        }

        /// <summary>
        /// 获取或设置当前句歌词的刷新频率
        /// </summary>
        public virtual int RefreshRate
        {
            set { _refreshRate = value; }
            get { return _refreshRate; }
        }

        public abstract void Reset();

        public event LyricVisibleChangedEventHandler VisibleChangedEvent;
        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        public LyricBase()
        {
            LyricStyleChangedEvent = new LyricStyleChangedEventHandler((LyricStyleChangedEventArgs e) => { });
            VisibleChangedEvent = new LyricVisibleChangedEventHandler((LyricVisibleChangedEventArgs e) => { });
            _ColorBlend.Colors = new Color[4] { PlayedColor, GradualChangeColor, PrepColor, PrepColor };
        }
        #endregion

        #region ILyricStyle
        public virtual Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                if (value.Size < MinFontSize)
                    value = new Font(value.Name, MinFontSize, value.Style);
                else if(value.Size > MaxFontSize)
                    value = new Font(value.Name, MaxFontSize, value.Style);
                if (_font.Size != value.Size || _font.Style != value.Style || _font.Name != value.Name)
                {
                    _font = value;
                    LyricStyleChangedEvent(new LyricStyleChangedEventArgs(this));
                }
            }
        }

        public virtual Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (value != _color)
                {
                    _color = value;
                    LyricStyleChangedEvent(new LyricStyleChangedEventArgs(this));
                }
            }
        }

        public virtual Color GradualChangeColor
        {
            get
            {
                return this._gradualChangeColor;
            }
            set
            {
                if (value != _gradualChangeColor)
                {
                    _gradualChangeColor = value;
                    LyricStyleChangedEvent(new LyricStyleChangedEventArgs(this));
                }
            }
        }

        public virtual Color PlayedColor
        {
            get
            {
                return this._playedColor;
            }
            set
            {
                if (value != _playedColor)
                {
                    _playedColor = value;
                    LyricStyleChangedEvent(new LyricStyleChangedEventArgs(this));
                }
            }
        }

        public virtual Color PrepColor
        {
            get
            {
                return _prepColor;
            }
            set
            {
                if (value != _prepColor)
                {
                    _prepColor = value;
                    LyricStyleChangedEvent(new LyricStyleChangedEventArgs(this));
                }
            }
        }

        public virtual float MaxFontSize
        {
            get
            {
                return _MaxFontSize;
            }
        }

        public virtual float MinFontSize
        {
            get
            {
                return _MinFontSize;
            }
        }

        public event LyricStyleChangedEventHandler LyricStyleChangedEvent;
        #endregion

        #region private methods
        private void lyricStyleChangedEvent(LyricStyleChangedEventArgs e)
        {
            _ColorBlend.Colors = new Color[4] { e.LyricStyle.PlayedColor, e.LyricStyle.GradualChangeColor, e.LyricStyle.PrepColor, e.LyricStyle.PrepColor };

            setCurrentLyricSizeF();
        }

        private void setCurrentLyricSizeF()
        {
            _CurrentLyricSizeF = graphics.MeasureString(CurrentLyric, Font);
        }
        #endregion
    }
}
