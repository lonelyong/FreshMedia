using System.Drawing;
namespace FreshMedia.Lyric
{
    public interface IDesktopLyric : ILyric
    {
        bool Locked { get; set; }
        Point ClientLocation { get; set; }

        void SetLyric(int currentIndex, string current, string next);
        void Refresh();

    }
}
