using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
namespace FreshMedia.Lyric
{

    /// <summary>
    /// lyric type foramt
    /// </summary>
    public class LyricApi
    {
        public enum tagTypes
        {
            meta = 0,
            time = 1
        }

        public struct tagInfo
        {
            tagTypes tagType { get; set; }
            string tagName { get; set; }
            string tagTitle { get; set; }
            string tagValue { get; set; }
        }

        #region private filed
        const string baseTagsString = "艺术家|标题|专辑|制作者|标记|时长|补偿";
        static string[] baseTagArray;
        #endregion

        #region public filed
        public enum BaseTags : int { other = -1, ar = 0, ti = 1, al = 2, by = 3, sign = 4, total = 5, offset = 6 }
        #endregion

        #region constructor destructor 
        static LyricApi()
        {
            init();
        }
        #endregion

        #region private method
        private static void init()
        {
            baseTagArray = baseTagsString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region public method
        /// <summary>
        /// 获取指定音乐路径的lrc格式的歌词路径
        /// </summary>
        /// <param name="audioPath">音乐完整路径</param>
        /// <returns></returns>
        public static string getLyricPathByAudio(string audioPath, bool testExist = false)
        {
            if (string.IsNullOrWhiteSpace(audioPath))
                return null;
            string tmp = Path.ChangeExtension(audioPath, ".lrc");
            if (testExist)
            {
                if (File.Exists(tmp))
                    return tmp;
                return null;
            }
            return tmp;
        }

        /// <summary>
        /// 判断读取的歌词数组中的某元素是否是歌词时间
        /// </summary>
        /// <param name="lrcTimeString"></param>
        /// <returns></returns>
        public static bool IsDate(string lrcTimeString)
        {
            return Regex.IsMatch(lrcTimeString, @"^\d{2}:\d{2}\.\d{2}$");
        }

        /// <summary>
        /// 将歌词文件里的时间字符串(00:00.00)转换为以毫秒为单位的时间
        /// </summary>
        /// <param name="timeStr">时间字符串</param>
        /// <returns>Double单位：毫秒</returns>
        public static long FormLyricTimeString(string timeStr)
        {
            if (string.IsNullOrWhiteSpace(timeStr))
                return 0;
            uint m, s;
            string[] timeArray = timeStr.Split(new char[] { ':' });
            if (timeArray.Length != 2)
                throw new Exception("不正确的lyric时间字符串，字符串格式为：00:00.00");
            m = Convert.ToUInt32(timeArray[0]) * 60 * 1000;
            s = (uint)(Convert.ToDouble(timeArray[1]) * 1000);
            return m + s;
        }

        /// <summary>
        /// 将以秒为单位的时间转换为 00:00.00形式的字符串
        /// </summary>
        /// <param name="milliSeconds">double时间</param>
        /// <returns>00:00.00</returns>
        public static string ToLyricTimeString(long milliSeconds)
        {
            double seconds = (double)milliSeconds / 1000;
            long m = (long)seconds / 60;
            double s = seconds % 60;

            return string.Format("{0}:{1:D2}", m, s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseTag"></param>
        /// <returns></returns>
        public static string GetBaseTagTitle(BaseTags baseTag)
        {
            int _baseTag = (int)baseTag;
            if (_baseTag == -1)
                return "other";
            else
                return baseTagArray[_baseTag];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseTag"></param>
        /// <returns></returns>
        public static string GetBaseTagTitle(string baseTag)
        {
            BaseTags _baseTag;
            try
            {
                _baseTag = (BaseTags)Enum.Parse(typeof(BaseTags), baseTag, true);
            }
            catch (Exception)
            {
                return baseTag;
            }
            int _intBaseTag = (int)_baseTag;
            if (_intBaseTag == -1)
                return "other";
            else
                return baseTagArray[_intBaseTag];
        }
        #endregion
    }
}
