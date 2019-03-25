namespace FreshMedia.Player
{
    public interface ISettings
    {
        bool Mute { get; set; }
        PlayStates PlayState { get; }
        CycleModes CycleMode { get; set; }
        string URL { get; set; }
        byte Volume { get; set; }
        int RewindForwardTime { get; set; }
    }
}
