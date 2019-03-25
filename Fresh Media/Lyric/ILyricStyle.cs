using System.Drawing;
namespace FreshMedia.Lyric
{
    public interface ILyricStyle
    {
        Font Font { get; set; }
        Color PlayedColor { get; set; }
        Color PrepColor { get; set; }
        Color Color { get; set; }
        Color GradualChangeColor { get; set; }

        float MinFontSize { get; }
        float MaxFontSize { get; }

        event LyricStyleChangedEventHandler LyricStyleChangedEvent;
    }
}
