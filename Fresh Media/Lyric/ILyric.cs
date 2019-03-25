namespace FreshMedia.Lyric
{
    public interface ILyric : ILyricStyle
    {
        bool Visible { get; set; }
        bool Enable { get; set; }
        int RefreshRate { get; set; }
        string CurrentLyric{ get; set; }

        void Reset();

        event LyricVisibleChangedEventHandler VisibleChangedEvent;
    }
}
