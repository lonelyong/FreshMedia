using System;

namespace FreshMedia.Lyric
{
    public delegate void LyricVisibleChangedEventHandler(LyricVisibleChangedEventArgs e);

    public class LyricVisibleChangedEventArgs : EventArgs
    {
        public bool Visible { get; }

        #region constructor destructor 
        public LyricVisibleChangedEventArgs(bool visible)
        {
            this.Visible = visible;
        }
        #endregion
    }

    public delegate void LyricStyleChangedEventHandler(LyricStyleChangedEventArgs e);

    public class LyricStyleChangedEventArgs : System.EventArgs
    {
        public ILyricStyle LyricStyle { get; }

        #region constructor
        public LyricStyleChangedEventArgs(ILyricStyle lyricStyle)
        {
            LyricStyle = lyricStyle;
        }
        #endregion
    }
}
