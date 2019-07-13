
using System;
using System.IO;
namespace FreshMedia.Lyric
{
    /// <summary>
    /// 歌词同步管理类
    /// </summary>
    sealed class LyricManager : IFreshMedia
    {
        #region IFreshMedia
        Controller.MainController IFreshMedia.Controller
        {
            get
            {
                return iFreshMedia.Controller;
            }
        }
        #endregion

        #region private filed
        private IFreshMedia iFreshMedia;
        private System.Windows.Forms.Timer syncTimer = new System.Windows.Forms.Timer();
        private string _CurrentLyric = LyricManager.DEFAULTLYRIC;
        private int _startIndex = 0;
        private int _tmpIndex = -1;
        #endregion

        #region public properties
        /// <summary>
        /// 歌词文件读取类
        /// </summary>
        public LyricDocument LyricDoc { get; private set; }
        /// <summary>
        /// 窗口歌词显示管理类
        /// </summary>
        public FormLyric FLyric { get; private set; }
        /// <summary>
        /// 桌面歌词显示管理类
        /// </summary>
        public DesktopLyric DLyric { get; private set; }
        /// <summary>
        /// 当前句歌词的索引  -1
        /// </summary>
        public int CurrentLyricIndex { get; private set; }
        /// <summary>
        /// 当前句歌词的时长  0
        /// </summary>
        public long CurrentLyricTimeLength { get; private set; }
        /// <summary>
        /// 上一句歌词  empty
        /// </summary>
        public string LastLyric_1 { get; private set; }
        /// <summary>
        /// 上第二句歌词  empty
        /// </summary>
        public string LastLyric_2 { get; private set; }
        /// <summary>
        ///  默认歌词显示窗口显示的歌词
        /// </summary>
        public const string DEFAULTLYRIC = "Audio World";
        /// <summary>
        /// 当前句歌词
        /// </summary>
        public string CurrentLyric
        {
            set
            {
                _CurrentLyric = value;
                FLyric.CurrentLyric = value;
                DLyric.CurrentLyric = value;

            }
            get { return _CurrentLyric; }
        }
        /// <summary>
        /// 下一句歌词  empty
        /// </summary>
        public string NextLyric_1 { get; private set; }
        /// <summary>
        /// 下第二句歌词  empty
        /// </summary>
        public string NextLyric_2 { get; private set; }
        /// <summary>
        /// 歌词结束
        /// </summary>
        public const string GOODBYELYRIC = "THE END";
        /// <summary>
        ///  歌词开头
        /// </summary>
        public const string HELLOLYRIC = "hello";
        /// <summary>
        ///  歌词同步总开关
        /// </summary>
        public bool EnableSync
        {
            set
            {
                bool tmp = LyricDoc.IsExisted
                    && iFreshMedia.Controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing
                    && (DLyric.Visible || FLyric.Visible);
                syncTimer.Enabled = tmp;
                if (tmp)
                    RefreshLyric(false);
                DLyric.Enable = tmp;
                FLyric.Enable = tmp;
            }
            get
            {
                return syncTimer.Enabled;
            }
        }
        #endregion

        #region constructor destructor 
        public LyricManager(IFreshMedia iFreshMedia)
        {
            //只进行字段赋值
            this.iFreshMedia = iFreshMedia;
        }

        public void Init()
        {
            FLyric = new FormLyric(this);
            FLyric.Init();
            DLyric = new DesktopLyric(this);
            DLyric.Init();

            FLyric.VisibleChangedEvent += new LyricVisibleChangedEventHandler(formLyricVisibleChangedEvent);
            DLyric.VisibleChangedEvent += new LyricVisibleChangedEventHandler(desktopLyricVisibleChangedEvent);

            //字段属性初始化
            CurrentLyricIndex = -1;
            CurrentLyricTimeLength = 0;
            LastLyric_1 = string.Empty;
            LastLyric_2 = string.Empty;
            NextLyric_1 = string.Empty;
            NextLyric_2 = string.Empty;
            LyricDoc = new LyricDocument(null);
            //很重要 初始化默认歌词的大小
            CurrentLyric = DEFAULTLYRIC;

            syncTimer.Enabled = false;
            syncTimer.Interval = 300;
            syncTimer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                syncLyric(iFreshMedia.Controller.PlayController.myPlayer.ctControls.currentPosition);
            });
            iFreshMedia.Controller.PlayController.myPlayer.URLChangedEvent += new Player.URLChangedEventHandler(urlChangedEvent);
            Reset();
        }
        #endregion

        #region private method
        private void formLyricVisibleChangedEvent(LyricVisibleChangedEventArgs e)
        {
            EnableSync = e.Visible;
        }

        private void desktopLyricVisibleChangedEvent(LyricVisibleChangedEventArgs e)
        {
            EnableSync = e.Visible;
        }

        private void urlChangedEvent(Player.URLChangedEventArgs e)
        {
            if (string.Compare(LyricDoc.path, e.CurrentURL, true) != 0)
            {
                LyricDoc.LoadFile(LyricApi.getLyricPathByAudio(e.CurrentURL, true));
            }
            if (LyricDoc.IsExisted)
            {
                iFreshMedia.Controller.PlayController.myPlayer.PlayStateChangedEvent += new Player.PlayStateChangedEventHandler(playStateChangedEvent);
                iFreshMedia.Controller.PlayController.myPlayer.CurrentPositionChangedEvent += new Player.CurrentPositionChangedEventHandler(currentPositionChangedEvent);
            }
            else
            {
                iFreshMedia.Controller.PlayController.myPlayer.PlayStateChangedEvent -= new Player.PlayStateChangedEventHandler(playStateChangedEvent);
                iFreshMedia.Controller.PlayController.myPlayer.CurrentPositionChangedEvent -= new Player.CurrentPositionChangedEventHandler(currentPositionChangedEvent);
            }
        }

        private void playStateChangedEvent(Player.PlayStateChangedEventArgs e)
        {
            EnableSync = e.PlayState == Player.PlayStates.playing;
            switch (e.PlayState)
            {
                case Player.PlayStates.playing:
                    break;
                case Player.PlayStates.paused:
                    break;
                case Player.PlayStates.stoped:
                    Reset();
                    break;
                case Player.PlayStates.mediaEnd:
                    Reset();
                    break;
                case Player.PlayStates.transitioning:
                    Reset();
                    break;
                case Player.PlayStates.closed:
                    Reset();
                    break;
            }
        }

        private void currentPositionChangedEvent(Player.CurrentPositionChangedEventArgs e)
        {
            if (e.ChangeMode == Player.PositionChangeModes.UserChange)
                RefreshLyric(true);
        }

        /// <summary>
        /// 同步歌词
        /// </summary>
        /// <param name="audioPosition"></param>
        private void syncLyric(long audioPosition)
        {
            #region 获取歌词索引
            _startIndex = CurrentLyricIndex < 0 ? 0 : CurrentLyricIndex;
            _tmpIndex = -1;
            if(LyricDoc.LyricsCount == 0)
                _tmpIndex = -1;
            else if (LyricDoc.LyricsCount == 1)
                _tmpIndex = 0;
            else if (0 <= audioPosition && audioPosition < LyricDoc.LyricTimes[0])//防止第一句歌词显示时间不为0，在播放到该句歌词之前，无歌词显示
                _tmpIndex = 0;
            else
                for (int i = _startIndex; i < LyricDoc.Lyrics.Count; i++)
                {
                    if (i < LyricDoc.LyricsCount - 1 && audioPosition >= LyricDoc.LyricTimes[i] && audioPosition < LyricDoc.LyricTimes[i + 1])//播放进度在最后一句之前的歌词显示)
                    {
                        if (i == CurrentLyricIndex)
                            break;
                        _tmpIndex = i;
                    }
                    else if (LyricDoc.LyricsCount - 1 == i && audioPosition >= LyricDoc.LyricTimes[i])
                    {
                        if (i == CurrentLyricIndex)
                            break;
                        _tmpIndex = i;
                    }
                }
            if (_tmpIndex == CurrentLyricIndex || _tmpIndex == -1)
                return;
            CurrentLyricIndex = _tmpIndex;
            #endregion

            #region 获取相关歌词
            //当前歌词的上2句歌词
            if (CurrentLyricIndex > 1) //当前歌词位置大于1
            {
                LastLyric_1 = LyricDoc.Lyrics[CurrentLyricIndex - 1];
                LastLyric_2 = LyricDoc.Lyrics[CurrentLyricIndex - 2];
            }
            else if (CurrentLyricIndex == 1)//当前歌词位置等于1
            {
                LastLyric_1 = LyricDoc.Lyrics[0];
                LastLyric_2 = HELLOLYRIC;
            }
            else //当前歌词位置等于0,最上行歌词为空
            {
                LastLyric_1 = LyricManager.HELLOLYRIC;
                LastLyric_2 = string.Empty;
            }
            //当前歌词的后2句歌词
            if (CurrentLyricIndex < LyricDoc.Lyrics.Count - 2)//当前歌词位置不是最后两句
            {
                NextLyric_1 = LyricDoc.Lyrics[CurrentLyricIndex + 1];
                NextLyric_2 = LyricDoc.Lyrics[CurrentLyricIndex + 2];
            }
            else if (CurrentLyricIndex == LyricDoc.Lyrics.Count - 2)//当前歌词位置是倒数第二句
            {
                NextLyric_1 = LyricDoc.Lyrics[CurrentLyricIndex + 1];
                NextLyric_2 = GOODBYELYRIC;
            }
            else//当前歌词位置是倒数第一句
            {
                NextLyric_1 = LyricManager.GOODBYELYRIC;
                NextLyric_2 = string.Empty;
            }
            #endregion
            //(公共)
            //设置歌词时间
            CurrentLyricTimeLength = GetLyricTime(CurrentLyricIndex);
            //存储当前位置歌词
            //下句必须在获取歌词时间之后
            CurrentLyric = LyricDoc.Lyrics[CurrentLyricIndex];
            FLyric.SetLyric(LastLyric_2, LastLyric_1, CurrentLyric, NextLyric_1, NextLyric_2);
            DLyric.SetLyric(CurrentLyricIndex, CurrentLyric, NextLyric_1);
        }
        #endregion

        #region public method
        /// <summary>
        /// 获取指定句歌词的时长
        /// </summary>
        /// <param name="lyricIndex">指定歌词的索引</param>
        /// <returns></returns>
        public long GetLyricTime(int lyricIndex)
        {
            if (lyricIndex < 0 || string.IsNullOrWhiteSpace(iFreshMedia.Controller.PlayController.myPlayer.settings.URL))
                return 1;
            if (lyricIndex < LyricDoc.LyricsCount - 1)
            {
                return LyricDoc.LyricTimes[lyricIndex + 1] - LyricDoc.LyricTimes[lyricIndex];
            }
            else if (lyricIndex == LyricDoc.LyricsCount - 1)
            {
                return iFreshMedia.Controller.PlayController.myPlayer.currentMedia.mediaLength - LyricDoc.LyricTimes[lyricIndex];
            }
            else
            {
                return 1;
            }
        }
        /// <summary>
        /// 获取当前句歌词剩余的时长
        /// </summary>
        /// <returns></returns>
        public long GetCurrentLyricLeftTime()
        {
            if (CurrentLyricIndex < 0 || string.IsNullOrWhiteSpace(iFreshMedia.Controller.PlayController.myPlayer.settings.URL))
                return 0;
            if (CurrentLyricIndex < LyricDoc.Lyrics.Count - 1)
            {
                return LyricDoc.LyricTimes[CurrentLyricIndex + 1] - iFreshMedia.Controller.PlayController.myPlayer.ctControls.currentPosition;
            }
            else if (CurrentLyricIndex == LyricDoc.LyricsCount - 1)
            {
                return iFreshMedia.Controller.PlayController.myPlayer.currentMedia.mediaLength - iFreshMedia.Controller.PlayController.myPlayer.ctControls.currentPosition;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 刷新同步歌词
        /// </summary>
        /// <param name="fromHead"></param>
        public void RefreshLyric(bool fromHead)
        {
            if (fromHead)
                CurrentLyricIndex = -1;
            else
                CurrentLyricIndex--;
            syncLyric(iFreshMedia.Controller.PlayController.myPlayer.ctControls.currentPosition);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Reset()
        {
            //LyricDoc.Dispose();
            LastLyric_1 = null;
            LastLyric_2 = null;
            CurrentLyric = DEFAULTLYRIC;
            NextLyric_1 = null;
            NextLyric_2 = null;
            CurrentLyricIndex = -1;
            CurrentLyricTimeLength = 0;

            EnableSync = false;
            FLyric.Reset();
            DLyric.Reset();
        }
        /// <summary>
        /// 显示Lyric歌词文件
        /// </summary>
        /// <param name="audioPath"></param>
        public void OpenLyricByAudioPath(string audioPath)
        {
            string lrcPath = LyricApi.getLyricPathByAudio(audioPath, true);
            if (string.IsNullOrEmpty(lrcPath))
                if (File.Exists(audioPath))
                    if (NgNet.UI.Forms.MessageBox.Show(iFreshMedia.Controller.MainForm, "无歌词文件，是否创建歌词文件？", null, System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        View.LyricMakerF lm = new View.LyricMakerF(iFreshMedia.Controller);
                        lm.Show(audioPath);
                    }
                    else
                        return;
                else
                {
                    System.Windows.Forms.DialogResult dr = NgNet.UI.Forms.MessageBox.Show(
                        iFreshMedia.Controller.MainForm,
                        "无音乐文件，是否仍要创建歌词文件？\r\n（在没有音乐源文件的情况下，歌词编辑器的部分功能将会被禁用！）",
                        null,
                        System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        View.LyricMakerF lm = new View.LyricMakerF(iFreshMedia.Controller);
                        lm.Show(audioPath);
                    }
                    else
                        return;
                }
            else
            {
                NgNet.UI.Forms.TextBox tb = new NgNet.UI.Forms.TextBox();
                tb.Editable = true;
                tb.Title = "歌词查看";
                tb.OpenFile(lrcPath);
                tb.ForeColor = iFreshMedia.Controller.MainForm.ForeColor;
                tb.BackColor = iFreshMedia.Controller.MainForm.BackColor;
                tb.ForeColor = iFreshMedia.Controller.MainForm.ForeColor;
                tb.Show(null);
            }
        }
        #endregion
    }

}
