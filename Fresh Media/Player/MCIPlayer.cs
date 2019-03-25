using System;
using System.Runtime.InteropServices;

namespace FreshMedia.Player
{
    sealed class MCIPlayer : PlayerBase
    {
        #region private filed
        //  定义API函数使用的字符串变量 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        private string shortPath = string.Empty;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        private string durLength = string.Empty;
        [MarshalAs(UnmanagedType.LPTStr, SizeConst = 128)]
        private string tmpStr = string.Empty;
        //  错误类型
        private int errorId = 0;
        //  返回消息
        private callBack cBack;
        #endregion

        #region private properties
        private PlayStates _PlayState
        {
            set
            {
                if (value == base._playState)
                    return;
                base._playState = value;
                PlayStateChangedEvent.Invoke(new PlayStateChangedEventArgs(value));
                switch (value)
                {
                    case PlayStates.playing:
                        break;
                    case PlayStates.paused:
                        break;
                    case PlayStates.stoped:
                        break;
                    case PlayStates.mediaEnd:
                        currentPosition = 0;
                        break;
                    case PlayStates.transitioning:
                        break;
                    case PlayStates.closed:
                        break;
                    default:
                        break;
                }
                InnerPlayStateChangedEvent.Invoke(new PlayStateChangedEventArgs(base._playState));
            }
            get
            {
                return base._playState;
            }
        }
        #endregion

        #region constructor destructor 
        public MCIPlayer()
        {
            this.cBack = new callBack(this);
            PlayStateChangedEvent = new PlayStateChangedEventHandler((PlayStateChangedEventArgs e)=> { });
            InnerPlayStateChangedEvent = new PlayStateChangedEventHandler((PlayStateChangedEventArgs e) => { });
        }
        #endregion

        #region private method
        /// <summary>
        /// 处理得到的短文件名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string procShortPath(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;
            name = name.Trim();
            name = name.Substring(0, name.Length - 1);
            return name;
        }

        private int _play()
        {
            tmpStr = "";
            tmpStr = tmpStr.PadLeft(128, ' ');
            errorId = MciUtils.mciSendString("play media notify", tmpStr, tmpStr.Length, (int)cBack.Handle);
            return errorId;
        }

        private int _pause()
        {
            tmpStr = "";
            tmpStr = tmpStr.PadLeft(128, ' ');
            errorId = MciUtils.mciSendString("pause media", tmpStr, tmpStr.Length, (int)cBack.Handle);
            return errorId;
        }

        private int _stop()
        {
            tmpStr = "";
            tmpStr = tmpStr.PadLeft(128, ' ');
            errorId = MciUtils.mciSendString("stop media", tmpStr, tmpStr.Length, (int)cBack.Handle);
            errorId = MciUtils.mciSendString("seek media to start", tmpStr, tmpStr.Length, (int)cBack.Handle);
            return errorId;
        }

        private int _close()
        {
            tmpStr = "";
            tmpStr = tmpStr.PadLeft(128, ' ');
            errorId = MciUtils.mciSendString("close media", tmpStr, tmpStr.Length, (int)cBack.Handle);
            return errorId;
        }
        #endregion

        #region PlayerBase
        public override long mediaLength
        {
            get
            {
                durLength = "";
                durLength = durLength.PadLeft(128, Convert.ToChar(" "));
                MciUtils.mciSendString("status media length", durLength, durLength.Length, 0);
                durLength = durLength.Trim();

                return NgNet.ConvertHelper.ToLong(durLength, 0);

            }
        }

        public override long currentPosition
        {
            get
            {
                durLength = "";
                durLength = durLength.PadLeft(128, ' ');
                MciUtils.mciSendString("status media position", durLength, durLength.Length, (int)cBack.Handle);
                durLength = durLength.Trim();
                return NgNet.ConvertHelper.ToLong(durLength, 0);
            }
            set
            {
                value = value < 0 ? 0 : value;
                string mciCommand = string.Format("seek media to {0}", value);
                errorId = MciUtils.mciSendString(mciCommand, value.ToString(), 0, 0);
                if (errorId == 0)
                {
                    if (PlayState == PlayStates.playing)
                        play();
                }
            }
        }

        public override bool play()
        {
            errorId = _play();
            if (errorId == 0)
            {
                Volume = Volume;
                Mute = Mute;
                _PlayState = PlayStates.playing;
                return true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(URL))
                    close();
                else
                    stop();
                return false;
            }
        }

        public override bool pause()
        {
            errorId = _pause();
            if (errorId == 0)
            {
                _PlayState = PlayStates.paused;
                return true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(URL))
                    close();
                else
                    stop();
                return false;
            }
        }

        public override bool stop()
        {
            errorId = _stop();
            if (errorId == 0)
            {
                _PlayState = PlayStates.stoped;
                return true;
            }
            else
            {
                close();
                return false;
            }
        }

        public override bool close()
        {
            errorId = _close();
            URL = string.Empty;
            _PlayState = PlayStates.closed;
            return true;
        }

        public override void rewind(long millisecond)
        {
            currentPosition -= millisecond;
        }

        public override void forward(long millisecond)
        {
            currentPosition += millisecond;
        }

        public override bool Mute
        {
            get
            {
                return base.Mute;
            }
            set
            {
                string OnOff = value ? "off" : "on";
                if (PlayState == PlayStates.playing || PlayState == PlayStates.paused || PlayState == PlayStates.stoped)
                {
                    errorId = MciUtils.mciSendString("setaudio Media " + OnOff, null, 0, 0);
                    if (errorId == 0)
                        base.Mute = value;
                }
                else
                    base.Mute = value;
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
                value = value > MaxVolume ? MaxVolume : value;
                string MciCommand = string.Format("setaudio media volume to {0}", value * 10);
                if (PlayState == PlayStates.playing || PlayState == PlayStates.paused || PlayState == PlayStates.stoped)
                {
                    errorId = MciUtils.mciSendString(MciCommand, null, 0, 0);
                    if (errorId == 0)
                        base.Volume = value;
                }
                else
                    base.Volume = value;
            }
        }

        public override PlayStates PlayState
        {
            get
            {
                return _PlayState;
            }
        }

        public override int setURL(string url)
        {
            // 判断为null去除首位空格
            url = string.IsNullOrWhiteSpace(url) ? string.Empty : url.Trim();
            if (PlayState != PlayStates.closed)
            {
                errorId = _close();
                if (errorId == 0)
                    _PlayState = PlayStates.closed;
                else
                    return errorId;
            }
            
            // 判断路径是不是空
            if (!string.IsNullOrWhiteSpace(url))
            {
                if (!string.IsNullOrWhiteSpace(base.URL))
                    _PlayState = PlayStates.transitioning;
                base.setURL(url);
            }

            // 获取短文件名
            shortPath = string.Empty;
            shortPath = shortPath.PadLeft(260, ' ');
            MciUtils.GetShortPathName(url, shortPath, shortPath.Length);
            shortPath = procShortPath(shortPath);
            // mci命令    
            // 播放当前url
            tmpStr = "";
            tmpStr = tmpStr.PadLeft(128, ' ');
            string MCICommand = string.Format("open \"{0}\" type {1} alias media", shortPath, MciUtils.GetDeviceType(url));
            errorId = MciUtils.mciSendString(MCICommand, tmpStr, tmpStr.Length, (int)cBack.Handle);
            // 判断是否打开成功
            if (errorId == 0)
                errorId = MciUtils.mciSendString("set media time format milliseconds", null, 0, (int)cBack.Handle);
            else
                _PlayState = PlayStates.stoped;

            return errorId;
        }

        public override string URL
        {
            get
            {
                return base.URL;
            }
            set
            {
                setURL(value);
            }
        }

        public override event PlayStateChangedEventHandler PlayStateChangedEvent;
        #endregion

        #region public fileds
        /// <summary>
        /// 用于内部事件,在PlayStateChangedEvent事件执行完之后执行
        /// </summary>
        public event PlayStateChangedEventHandler InnerPlayStateChangedEvent;
        #endregion

        private class callBack : System.Windows.Forms.Control
        {
            MCIPlayer _mci;

            #region const
            const int MM_MCINOTIFY = 0x3B9;
            #endregion

            #region constructor destructor 
            public callBack(MCIPlayer mci)
            {
                _mci = mci;
                Size = new System.Drawing.Size(2, 2);
                Show();
                Hide();
            }
            #endregion

            #region override
            protected override void DefWndProc(ref System.Windows.Forms.Message m)
            {
                base.DefWndProc(ref m);
                if (m.Msg == MM_MCINOTIFY) //判断指令是不是MM_MCINOTIFY
                {
                    if (_mci.currentPosition == _mci.mediaLength && _mci.currentPosition != 0)
                        _mci._PlayState = PlayStates.mediaEnd;
                }
            }
            #endregion
        }
    }
}
