using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace FreshMedia.Lyric
{
    struct LyricPair
    {
        long Time;

        string Lyric;

        public LyricPair(long time, string lyric)
        {
            Time = time;
            Lyric = lyric;
        }
    }


    /// <summary>
    /// 打开lrc文件类
    /// </summary>
    public class LyricDocument : IDisposable
    {
        #region lyric file api
        #region constructor destructor 
        public LyricDocument(string path)
        {
            Offset = -1;
            Lyrics = _Lyrics;
            LyricTimes = _LyricTimes;
            LoadFile(path);
        }
        #endregion

        #region private filed
        private string _path = null;
        //string _Title = "";
        ///// <summary>
        ///// 艺术家
        ///// </summary>
        //string _Artist = "";
        ///// <summary>
        ///// 专辑
        ///// </summary>
        //string _Album = "";
        ///// <summary>
        ///// 歌词作者
        ///// </summary>
        //string _Author = "";
        ///// <summary>
        ///// 标记
        ///// </summary>
        //string _Sign = "";
        ///// <summary>
        ///// 偏移量
        ///// </summary>
        //int _Offset = 0;

        private List<string> _Lyrics = new List<string>();

        private List<long> _LyricTimes = new List<long>();
        #endregion

        #region attribute
        /// <summary>
        /// 按时间顺序排列的歌词，其与时间集合是一一对应的
        /// </summary>
        public IReadOnlyList<string> Lyrics { get; private set; }  //存储排序后的时间对应的歌词文本
        /// <summary>
        /// 按时间顺序排列的歌词时间，其与歌词集合是一一对应的
        /// </summary>
        public IReadOnlyList<long> LyricTimes { get; private set; }//存储排序后的歌词时间
        /// <summary>
        /// 获取歌词句数
        /// </summary>
        public int LyricsCount { get { return LyricTimes.Count; } }

        /// <summary>
        /// 当前实例的歌词路径
        /// </summary>
        public string path
        {
            get
            {
                return _path; 
            }
        }
        /// <summary>
        ///   歌词文件是否存在
        /// </summary>
        public bool IsExisted
        {
            get { return File.Exists(path); }
        }
        /// <summary>
        /// 歌曲
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 专辑
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// 歌词作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 标记
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 偏移量
        /// </summary>
        public int Offset { get; set; }
        #endregion

        #region public method
        /// <summary>
        /// 像当前歌词尾添加歌词
        /// </summary>
        /// <param name="lyricTime">该歌词的时间，单位：毫秒</param>
        /// <param name="lyric">该歌词内容</param>
        public void AddLyric(long lyricTime, string lyric)
        {
            this._LyricTimes.Add(lyricTime);
            this._Lyrics.Add(lyric);
        }
        /// <summary>
        /// 添加ID标签
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="tagValue"></param>
        public void AddTag(string tagName, string tagValue)
        {
            //LyricsTime.Insert(0, -1);
            _Lyrics.Insert(0, tagValue);
        }
        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="baseTag"></param>
        /// <param name="tagValue"></param>
        public void AddTag(LyricApi.BaseTags baseTag, string tagValue)
        {
            string tagName = Enum.GetName(typeof(LyricApi.BaseTags), baseTag);
            AddTag(tagName, tagValue);
        }
        /// <summary>
         /// 移除指定索引的歌词
         /// </summary>
         /// <param name="index"></param>
        public void RemoveLyric(int index)
        {
            this._Lyrics.RemoveAt(index);
            this._LyricTimes.RemoveAt(index);
        }
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="lyricPath"></param>
        public void LoadFile(string lyricPath)
        {
            this.Dispose();
            this._path = lyricPath;
            this.formatLyricArray(this.readLyricFile(lyricPath)); //获取当前歌曲歌词 
        }
        #endregion

        #region private method
        /// <summary>
        /// 读取歌词文件到数组
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        private string[] readLyricFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            string[] lrcArray = System.IO.File.ReadAllLines(path, Encoding.Default);
            StringBuilder sb = new StringBuilder();
            //去除回车
            foreach (string item in lrcArray)
            {
                sb.Append(item);
            }
            return sb.ToString().Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 将读取的歌词按时间顺序排序并将歌词和时间分开
        /// </summary>
        /// <param name="lrcArray"></param>
        /// <returns></returns>
        private void formatLyricArray(string[] lrcArray)
        {
            _Lyrics.Clear();
            _LyricTimes.Clear();
            if (lrcArray == null) //無歌詞文件
                return;

            //將時間和歌詞分開
            List<long> timestmp = new List<long>();
            bool proced = false;
            for (int i = 0; i < lrcArray.Length; i++)
            {
                #region ID Tag
                proced = false;
                foreach (LyricApi.BaseTags item in Enum.GetValues(typeof(LyricApi.BaseTags)))
                {
                    string tagName = Enum.GetName(typeof(LyricApi.BaseTags), item);
                    if (lrcArray[i].TrimStart().StartsWith(tagName + ":", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string tagTitle = LyricApi.GetBaseTagTitle(tagName);
                        string tagValue = getTagValue(lrcArray[i]);

                        //AddTag(item, tagValue);
                        if (string.IsNullOrWhiteSpace(tagValue) == false)
                            if (string.Compare(tagName, Enum.GetName(typeof(LyricApi.BaseTags), LyricApi.BaseTags.offset), true) == 0)
                                Offset = NgNet.ConvertHelper.ToInt(tagValue, 0);
                            else
                            {
                                _Lyrics.Add(tagTitle + " : " + tagValue);
                                _LyricTimes.Add(-1);
                            }
                        proced = true;
                        break;
                    }
                }
                //不是时间标签
                if (proced)
                    continue;
                #endregion

                #region timeTag
                if (LyricApi.IsDate(lrcArray[i]))
                {
                    timestmp.Add(LyricApi.FormLyricTimeString(lrcArray[i]));
                }
                else
                {
                    for (int j = 0; j < timestmp.Count; j++)
                    {
                        _LyricTimes.Add(timestmp[j]);
                        _Lyrics.Add(lrcArray[i]);
                    }
                    timestmp.Clear();
                }
                #endregion
            }

            #region 對讀取的歌詞排序
            long timeTmp;
            string lyricTmp;
            for (int j = 0; j < LyricTimes.Count - 1; j++)
            {
                for (int i = j; i < LyricTimes.Count; i++)
                {
                    if (LyricTimes[j] > LyricTimes[i])
                    {
                        timeTmp = LyricTimes[j];
                        lyricTmp = Lyrics[j];
                        _LyricTimes[j] = LyricTimes[i];
                        _Lyrics[j] = Lyrics[i];
                        _LyricTimes[i] = timeTmp;
                        _Lyrics[i] = lyricTmp;
                    }
                }
            }
            #endregion

            #region 处理ID Tag显示时间
            int firstLyricIndex = 0;//存储第一句歌词的索引
            long firstLyricTime = 0;//存储第一句歌词出现的时间
            //获取第一句歌词的suoying=与时长
            for (int i = 0; i < LyricTimes.Count; i++)
            {
                if (LyricTimes[i] != -1)
                {
                    firstLyricIndex = i;
                    firstLyricTime = LyricTimes[i];
                    break;
                }
            }

            if (firstLyricTime == 0 || firstLyricIndex == 0)
                for (int i = 0; i < firstLyricIndex; i++)
                    _LyricTimes[i] = 0;
            else
            {
                double timeSpan = firstLyricTime / firstLyricIndex;
                for (int i = 0; i < firstLyricIndex; i++)
                {
                    _LyricTimes[i] = (long)(timeSpan * i);
                }
            }
            //libDialog.MessageBox(null,NgNet.Collections.KVCollection<long, string>.ToString(LyricsTime, Lyrics, "{0}", "{0}", "    ", "\n"));
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private string getTagValue(string tag)
        {
            int firstIndex = tag.IndexOf(':');
            if (firstIndex == -1)
                throw new FormatException("tag格式不合法。 xx:xxxx 为合法格式");
            return tag.Substring(firstIndex + 1);
        }
        #endregion
        #endregion

        #region IDisposable
        public void Dispose()
        {
            this._Lyrics.Clear();
            this._LyricTimes.Clear();
            this._path = null;
            this.Album = null;
            this.Artist = null;
            this.Author = null;
            this.Offset = 0;
            this.Sign = null;
            this.Title = null;
            GC.Collect();
        }
        #endregion
    }


}
