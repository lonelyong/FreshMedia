using System;

namespace FreshMedia.Player
{
    #region delegate
    public delegate void PlayStateChangedEventHandler(PlayStateChangedEventArgs e);

    public delegate void VolumeChangedEventHandler(VolumeChangedEventArgs e);

    public delegate void MuteStateChangedEventHandler(MuteStateChangedEventArgs e);

    public delegate void CycleModeChangedEventHandler(CycleModeChangedEventArgs e);

    public delegate void CurrentPositionChangedEventHandler(CurrentPositionChangedEventArgs e);

    public delegate void MediaListChangedEventHandler(MediaListChangedEventArgs e);

    public delegate void URLChangedEventHandler(URLChangedEventArgs e);

    public delegate void PlayErrorEventHandler(PlayErrorEventArgs e);

    #endregion

    #region event args
    public class PlayStateChangedEventArgs : EventArgs
    {
        #region constructor destructor 
        public PlayStateChangedEventArgs(PlayStates playstate)
        {
            this.PlayState = playstate;
        }
        #endregion

        public PlayStates PlayState { get; }
    }

    public class VolumeChangedEventArgs : EventArgs
    {
        public byte Volume { get; }

        #region constructor destructor 
        public VolumeChangedEventArgs(byte volume)
        {
            Volume = volume;
        }
        #endregion
    }

    public class MuteStateChangedEventArgs : EventArgs
    {
        public bool MuteState { get; }

        #region constructor destructor 
        public MuteStateChangedEventArgs(bool muteState)
        {
            this.MuteState = muteState;
        }
        #endregion
    }

    public class CycleModeChangedEventArgs : EventArgs
    {
        public CycleModes CycleMode { get; }

        #region constructor destructor 
        public CycleModeChangedEventArgs(CycleModes cycleMode)
        {
            this.CycleMode = cycleMode;
        }
        #endregion
    }

    public class MediaListChangedEventArgs : EventArgs
    {
        #region constructor destructor 
        public MediaListChangedEventArgs()
        {

        }
        #endregion
    }

    public class URLChangedEventArgs : EventArgs
    {
        public string CurrentURL { get; }

        public string LastURL { get; }

        #region constructor destructor 
        public URLChangedEventArgs(string currentURL, string lastURL)
        {
            CurrentURL = currentURL;
            LastURL = lastURL;
        }
        #endregion
    }

    public class CurrentPositionChangedEventArgs : EventArgs
    {
        public long CurrentPosition { get; }

        public PositionChangeModes ChangeMode { get; }

        #region constructor destructor 
        public CurrentPositionChangedEventArgs(long currentPosition, PositionChangeModes changedMode)
        {
            CurrentPosition = currentPosition;
            ChangeMode = changedMode;
        }
        #endregion
    }

    public class PlayErrorEventArgs : Exception
    {
        public int ErrorId { get; }

        public string ErrorText { get; }

        public string ErrorURL { get; }

        #region constructor destructor 
        public PlayErrorEventArgs(int errorId, string errorText, string errorURL)
        {
            ErrorId = errorId;
            ErrorText = errorText;
            ErrorURL = errorURL;
        }
        #endregion
    }
    #endregion
}
