namespace FreshMedia.Player
{
    public abstract class PlayerBase : IPlayerBase
    {
        #region private fileds
        long _currentPosition = 0;
        long _mediaLength = 0;
        bool _Mute = false;
        string _URL = string.Empty;
        byte _Volume = 50;
        byte _MaxVolume = 100;
        #endregion

        #region protected files
        protected PlayStates _playState = PlayStates.closed;
        #endregion

        public virtual long currentPosition
        {
            get
            {
                return _currentPosition;
            }
            set
            {
                _currentPosition = value;
            }
        }

        public virtual long mediaLength
        {
            get
            {
                return _mediaLength;
            }
        }

        public virtual bool Mute
        {
            get
            {
                return _Mute;
            }
            set
            {
                _Mute = value;
            }
        }

        public virtual PlayStates PlayState
        {
            get
            {
                return _playState;
            }
        }

        public virtual string URL
        {
            get
            {
                return _URL;
            }
            set
            {
                setURL(value);
            }
        }

        public virtual int setURL(string url)
        {
            _URL = url;
            return 0;
        }

        public virtual byte Volume
        {
            get
            {
                return _Volume;
            }

            set
            {
                if (value > MaxVolume)
                    value = MaxVolume;
                _Volume = value;
            }
        }

        public virtual byte MaxVolume
        {
            get
            {
                return _MaxVolume;
            }
        }

        public abstract bool pause();

        public abstract bool play();

        public abstract bool stop();

        public abstract bool close();

        public abstract void rewind(long millisecond);

        public abstract void forward(long millisecond);

        public abstract event PlayStateChangedEventHandler PlayStateChangedEvent;
    }   
}
