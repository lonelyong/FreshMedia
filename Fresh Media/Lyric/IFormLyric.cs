using System;

namespace FreshMedia.Lyric
{
    interface IFormLyric : ILyric
    {
        void SetLabel(System.Windows.Forms.Label label);
        void SetLyric(string lyric0, string lyric1, string lyric2, string lyric3, string lyric4);
        void RefreshCurrentLyric();
    }
}
