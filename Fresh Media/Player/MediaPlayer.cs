using System;
using System.Collections.Generic;
using System.Linq;

namespace FreshMedia.Player
{
    public sealed class MediaPlayer : PlayerBase, IPlayer, ISettings, ICtControls, IMedia, IPlayList
    {
        #region private fileds
        private MCIPlayer _mciPlayer = new MCIPlayer();

        private Mpeg4Player _mpeg4player = new Mpeg4Player();

        private PlayerBase _currentPlayer;

        private CycleModes _CycleMode = CycleModes.AllOnce;

        private string _lastURL = string.Empty;

        private bool _IsInterimPlay = false;
        //  timer
        private System.Windows.Forms.Timer currentPositionChangedTimer;
        #endregion

        #region interface
        public ISettings settings
        {
            get;
        }
        public ICtControls ctControls
        {
            get;
        }
        public IMedia currentMedia
        {
            get;
        }
        public IPlayList playList { get; }
        #endregion

        #region constructor destructor 
        public MediaPlayer()
        {
            init();
            settings = this;
            ctControls = this;
            currentMedia = this;
            playList = this;

            InnerPlayStateChangedEvent += new PlayStateChangedEventHandler(playStateChangedEvent);
            VolumeChangedEvent += new VolumeChangedEventHandler((VolumeChangedEventArgs e) => { });
            MuteStateChangedEvent += new MuteStateChangedEventHandler((MuteStateChangedEventArgs e) => { });
            CycleModeChangedEvent += new CycleModeChangedEventHandler((CycleModeChangedEventArgs e) => { });
            CurrentPositionChangedEvent += new CurrentPositionChangedEventHandler((CurrentPositionChangedEventArgs e) => { });
            MediaListChangedEvent += new MediaListChangedEventHandler((MediaListChangedEventArgs e) => { });
            URLChangedEvent += new URLChangedEventHandler((URLChangedEventArgs e)=> { });
    }
        #endregion

        #region events
        private event PlayStateChangedEventHandler InnerPlayStateChangedEvent
        {
            add
            {
                _mciPlayer.InnerPlayStateChangedEvent += value;
            }
            remove
            {
                _mciPlayer.InnerPlayStateChangedEvent -= value;
            }
        } 

        public override event PlayStateChangedEventHandler PlayStateChangedEvent
        {
            add
            {
                _mciPlayer.PlayStateChangedEvent += value;
            }
            remove
            {
                _mciPlayer.PlayStateChangedEvent -= value;
            }
        }

        public event VolumeChangedEventHandler VolumeChangedEvent;

        public event MuteStateChangedEventHandler MuteStateChangedEvent;

        public event CycleModeChangedEventHandler CycleModeChangedEvent;

        public event CurrentPositionChangedEventHandler CurrentPositionChangedEvent;

        public event MediaListChangedEventHandler MediaListChangedEvent;

        public event URLChangedEventHandler URLChangedEvent;

        public event PlayErrorEventHandler PlayErrorEvent;

        #endregion

        #region PlayerBase
        public override long currentPosition
        {
            get
            {
                return _currentPlayer.currentPosition;
            }
            set
            {
                long tmp = _currentPlayer.currentPosition;
                _currentPlayer.currentPosition = value;
                if (tmp != _currentPlayer.currentPosition)
                    CurrentPositionChangedEvent(new CurrentPositionChangedEventArgs(_currentPlayer.currentPosition, PositionChangeModes.UserChange));
            }
        }

        public override long mediaLength
        {
            get
            {
                return _currentPlayer.mediaLength;
            }
        }

        public override bool Mute
        {
            get
            {
                return base.Mute;
            }
            set
            {
                bool tmp = base.Mute;
                _currentPlayer.Mute = value;
                _mciPlayer.Mute = _currentPlayer.Mute;
                _mpeg4player.Mute = _currentPlayer.Mute;
                base.Mute = _currentPlayer.Mute;
                if (tmp != base.Mute)
                    MuteStateChangedEvent(new MuteStateChangedEventArgs(value));
            }
        }

        public override string URL
        {
            get
            {
                return _currentPlayer.URL;
            }
            set
            {
                int errID = 0;
                // 判断为null去除首位空格
                value = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
                _lastURL = base.URL;
                _currentPlayer = getPlayer(value);
                errID = _currentPlayer.setURL(value);
                //先执行playstatechangedevent
                if (string.Compare(lastURL, value, true) != 0)
                    URLChangedEvent(new URLChangedEventArgs(value, lastURL));
                if (errID != 0)
                {
                    PlayErrorEvent(new PlayErrorEventArgs(errID, MciUtils.GetErrorString(errID), value));
                    return;
                }
                play();
            }
        }

        public override byte Volume
        {
            get
            {
                return base.Volume;
            }
            set
            {
                byte tmp = base.Volume;
                _currentPlayer.Volume = value;
                _mciPlayer.Volume = _currentPlayer.Volume;
                _mpeg4player.Volume = _currentPlayer.Volume;
                base.Volume = _currentPlayer.Volume;
                if (tmp != base.Volume)
                    VolumeChangedEvent(new VolumeChangedEventArgs(value));
            }
        }

        public override PlayStates PlayState
        {
            get
            {
                return _currentPlayer.PlayState;
            }
        }

        public override bool close()
        {
            return _currentPlayer.close();
        }

        public override bool pause()
        {
            return _currentPlayer.pause();
        }

        public override bool play()
        {
            return _currentPlayer.play();
        }

        public override bool stop()
        {
             return _currentPlayer.stop();
        }

        public override void rewind(long millisecond)
        {
            currentPosition -= millisecond;
        }

        public override void forward(long millisecond)
        {
            currentPosition += millisecond;
        }
        #endregion

        #region IMedia
        public string mediaLengthString
        {
            get { return NgNet.ConvertHelper.ToTimeString(mediaLength / 1000); }
        }
        #endregion

        #region ICtControls
        public void runCycleMode(CycleModes cm)
        {
            string url = string.Empty;
            switch (cm)
            {
                case CycleModes.OneOnce:
                    return;
                case CycleModes.OneCycle:
                    url = currentURL;
                    break;
                case CycleModes.AllOnce:
                    int itemIndex = innerList.IndexOf(currentURL);//获取当前播放项索引
                    if (itemIndex == -1)
                        return;
                    if (itemIndex == innerList.Count - 1)
                        return;
                    else
                        url = innerList[itemIndex + 1];
                    break;
                case CycleModes.AllCycle:
                    itemIndex = innerList.IndexOf(currentURL);//获取当前播放项索引
                    if (itemIndex == -1)
                        return;
                    if (itemIndex == innerList.Count - 1)
                        url = innerList[0];
                    else
                        url = innerList[itemIndex + 1];
                    break;
                case CycleModes.Random:
                    if (innerList.Count == 0)
                        return;
                    if (innerList.Count == 1)
                        url = innerList[0];
                    else
                    {
                        Random ran = new Random();
                        int index = ran.Next(0, innerList.Count);
                        url = innerList[index];
                    }
                    break;
            }
            if (string.IsNullOrWhiteSpace(url))
                return;
            else if (System.IO.File.Exists(url))
                listPlay(url);
            else
            {
                URL = url;
                runCycleMode(cm);
            }
        }

        public void interimPlay(string url)
        {
            _IsInterimPlay = true;
            URL = url;
        }

        public void listPlay(string url)
        {
            _IsInterimPlay = false;
            URL = url;
        }

        public bool beginMute()
        {
            Mute = true;
            return Mute == true;
        }

        public bool endMute()
        {
            Mute = false;
            return Mute == false;
        }

        public void next()
        {
            if (IsInterimPlay)
                return;
            //判断当前播放状态
            if (PlayState == PlayStates.playing
                || PlayState == PlayStates.paused
                || PlayState == PlayStates.stoped)
            {
                //判断循环模式
                if (CycleMode == CycleModes.Random)
                {
                    runCycleMode(CycleModes.Random);
                }
                else
                    //下一首
                    runCycleMode(CycleModes.AllCycle);
            }
        }

        public void last()
        {
            if (IsInterimPlay)
                return;
            //判断当前播放状态
            if (PlayState == Player.PlayStates.playing
                || PlayState == Player.PlayStates.paused
                || PlayState == Player.PlayStates.stoped)
            {
                //判断循环模式
                if (CycleMode == Player.CycleModes.Random)
                {
                    runCycleMode(Player.CycleModes.Random);
                    return;
                }
                //获取当前播放列表索引
                string url = string.Empty;
                int itemindex = innerList.IndexOf(currentURL);
                if (itemindex == 0)
                    url = innerList[innerList.Count - 1];
                else if (itemindex == -1)
                    return;
                else
                    url = innerList[itemindex - 1];
                listPlay(url);
            }
        }

        public void playOrPause()
        {
            if (PlayState == PlayStates.playing)
                pause();
            else if (PlayState == PlayStates.paused || PlayState == PlayStates.stoped)
                play();
        }

        public void volUp()
        {
            Volume++;
        }

        public void volDown()
        {
            Volume--;
        }

        public void rewind()
        {
            _currentPlayer.rewind(RewindForwardTime);
        }

        public void forward()
        {
            _currentPlayer.forward(RewindForwardTime);
        }
        #endregion

        #region ISettings
        public CycleModes CycleMode
        {
            get
            {
                return _CycleMode;
            }
            set
            {
                if (value != _CycleMode)
                {
                    _CycleMode = value;
                    CycleModeChangedEvent(new CycleModeChangedEventArgs(value));
                }

            }
        }

        public int RewindForwardTime { get; set; }
        #endregion

        #region IPlayList
        private NgNet.Collections.SignleCollection<string> innerList = new NgNet.Collections.SignleCollection<string>();

        public IEnumerable<string> AddMeidas(IEnumerable<string> collection)
        {
            if (collection == null)
                return null;
            IEnumerable<string> existedItems;
            IEnumerable<string> addedItems;
            innerList.AddRange(collection, out existedItems, out addedItems);

            if (addedItems.Count<string>() > 0)
                MediaListChangedEvent(new MediaListChangedEventArgs());
            return existedItems;
        }

        public void AddMedia(string path)
        {
            bool existed;
            innerList.Add(path, out existed);
            if (existed == false)
                MediaListChangedEvent(new MediaListChangedEventArgs());
        }

        public void RemoveMedias(IEnumerable<string> collection)
        {
            if (collection == null)
                return;
            int currentCount = innerList.Count;
            foreach (string item in collection)
            {
                innerList.Remove(item);
                if (string.Compare(URL, item, true) == 0)
                    close();
            }
            if (currentCount != innerList.Count)
                MediaListChangedEvent(new MediaListChangedEventArgs());
        }

        public void Clean()
        {
            int tmp = innerList.Count;
            if (IsInterimPlay == false)
                close();
            innerList.Clear();
            if (tmp != innerList.Count)
                MediaListChangedEvent(new MediaListChangedEventArgs());
        }

        NgNet.Collections.IReadOnlyList<string> IPlayList.Medias
        {
            get
            {
                return innerList;
            }
        }
        #endregion

        #region  IPlayer
        public bool IsInterimPlay
        {
            get
            {
                return _IsInterimPlay;
            }
        }

        public string currentPositionString
        {
            get { return NgNet.ConvertHelper.ToTimeString(currentPosition / 1000); }
        }

        public string currentURL
        {
            get { return URL; }
        }

        public string lastURL
        {
            get
            {
                return _lastURL;
            }
        }
        #endregion

        #region private methods
        private void init()
        {
            currentPositionChangedTimer = new System.Windows.Forms.Timer();
            currentPositionChangedTimer.Enabled = false;
            currentPositionChangedTimer.Interval = 1000;
            currentPositionChangedTimer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                CurrentPositionChangedEvent(new CurrentPositionChangedEventArgs(currentPosition, PositionChangeModes.General));
            });
            _currentPlayer = _mciPlayer;
        }

        private PlayerBase getPlayer(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return _currentPlayer;
            switch (System.IO.Path.GetExtension(filePath).ToLower())
            {
                case ".mp3":
                case ".wav":
                case ".wma":
                    return _mciPlayer;
                //case ".m4a":
                //    return _mpeg4player;
                default:
                    return _mciPlayer;
            }
        }

        private void playStateChangedEvent(PlayStateChangedEventArgs e)
        {
            currentPositionChangedTimer.Enabled = e.PlayState == PlayStates.playing;
            if(e.PlayState == PlayStates.mediaEnd)
            {
                if(IsInterimPlay == false)
                    runCycleMode(CycleMode);
            }
        }
        #endregion
    }
}
