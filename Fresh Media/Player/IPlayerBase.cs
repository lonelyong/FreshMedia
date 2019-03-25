namespace FreshMedia.Player
{
    interface IPlayerBase
    {
        byte MaxVolume { get; }

        string URL { get; set; }
        byte Volume { get; set; }
        bool Mute { get; set; }
        long currentPosition { get; set; }
        long mediaLength { get; }
        PlayStates PlayState { get; }

        bool play();
        bool pause();
        bool stop();
        bool close();

        void rewind(long millisecond);
        void forward(long millisecond);

        event PlayStateChangedEventHandler PlayStateChangedEvent;
    }
}
