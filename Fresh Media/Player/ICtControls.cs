namespace FreshMedia.Player
{
    public interface ICtControls
    {
        long currentPosition { set; get; }
        void runCycleMode(CycleModes cm);
        void listPlay(string url);
        void interimPlay(string url);
        bool play();
        bool pause();
        bool stop();
        bool close();
        bool beginMute();
        bool endMute();
        void next();
        void last();
        void playOrPause();
        void rewind(long millisecond);
        void rewind();
        void forward(long millisecond);
        void forward();
        void volUp();
        void volDown();
    }
}
