using System.Drawing;
using System;

namespace FreshMedia.Lyric
{
    interface IDesktopLyricF
    {
        Point Location { get; set; }
        Size LyricPanelSize { get; }
        bool IsLoaded { get; }

        void SetLyric(string top, string down);
        void RefreshLyric(bool bTop);
        void Show();
        void Close();
        void BeginInvoke(Delegate method);

        event System.Windows.Forms.PaintEventHandler TopLyricPaintEvent;
        event System.Windows.Forms.PaintEventHandler DownLyricPaintEvent;
    }
}
