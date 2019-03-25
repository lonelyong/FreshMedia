namespace FreshMedia.Player
{
    public interface IAudioInfo
    {
        string Path { get; set; }
        string Artist { get; }
        string Length { get; }
        string Year { get; }
        string Title { get; }
        string Album { get; }
        string Name { get; }
        string BitRate { get; }
    }

    /// <summary>
    /// 表示一首音乐的信息集合
    /// </summary>
    public struct AudioInfo : IAudioInfo
    {
        #region constructor
        /// <summary>
        /// 获取一首音乐的信息集合
        /// </summary>
        /// <param name="path"></param>
        public AudioInfo(string path)
        {
            _Path = "Path";
            _Name = "文件名";
            _Title = "标题";
            _Artist = "艺术家";
            _Album = "专辑";
            _Year = "年代";
            _KBps = "码率";
            _Length = "时长";

            Path = path;
        }
        #endregion

        #region 信息参数
        string _Path;
        /// <summary>
        /// 音乐文件路径
        /// </summary>
        public string Path
        {
            set
            {
                _Path = value;
                init();
            }
            get
            {
                return _Path;
            }
        }

        string _Artist;
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist
        {
            set
            {
                _Artist = value;
            }
            get
            {
                return _Artist;
            }
        }

        string _Length;
        /// <summary>
        /// 音乐时长
        /// </summary>
        public string Length
        {
            get
            {
                return _Length;
            }
        }

        string _Year;
        /// <summary>
        /// 年代
        /// </summary>
        public string Year
        {
            get
            {
                return _Year;
            }
        }

        string _Title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
        }

        string _Album;
        /// <summary>
        /// 唱片集
        /// </summary>
        public string Album
        {
            get
            {
                return _Album;
            }
        }

        string _Name;
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        string _KBps;
        /// <summary>
        /// 比特率
        /// </summary>
        public string BitRate
        {
            get
            {
                return _KBps;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// 返回信息字符串
        /// </summary>
        /// <returns>分隔符</returns>
        public override string ToString()
        {
            return string.Format("路径：{0}\n文件名{1}\n标题：{2}\n艺术家：{3}\n唱片集：{4}\n年代：{5}\n比特率：{6}\n时长：" ,
                new object[] { Path,Title,Artist,Album,Year, BitRate, Length,});
        }
        #endregion

        #region private methods
        private void init()
        {
            if (string.IsNullOrWhiteSpace(Path) || System.IO.File.Exists(Path) == false)
            {
                _Path = Path;
                _Name = "文件名";
                _Title = "标题";
                _Artist = "艺术家";
                _Album = "专辑";
                _Year = "年代";
                _KBps = "码率";
                _Length = "时长";
                return;
            }
            switch (System.IO.Path.GetExtension(Path))
            {
                case ".mp3":

                    break;
                case ".wav":

                    break;
                default:
                    break;
            }
            Shell32.ShellClass sh = new Shell32.ShellClass();
            Shell32.Folder dir = sh.NameSpace(System.IO.Path.GetDirectoryName(Path));
            Shell32.FolderItem item = dir.ParseName(System.IO.Path.GetFileName(Path));
            _Path = Path;
            _Name = System.IO.Path.GetFileName(Path);
            _Title = dir.GetDetailsOf(item, 21);
            _Artist = dir.GetDetailsOf(item, 13);
            _Album = dir.GetDetailsOf(item, 14);
            _Year = dir.GetDetailsOf(item, 15);
            _KBps = dir.GetDetailsOf(item, 28);
            _Length = dir.GetDetailsOf(item, 27);
        }
        #endregion
    }
}
