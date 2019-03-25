using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.Player
{
    public class Types
    {
        public const string EXT_ACC = ".aac";
        public const string EXT_AMR = ".amr";
        public const string EXT_APE = ".ape";
        public const string EXT_FLAC = ".flac";
        public const string EXT_M4A = ".m4a";
        public const string EXT_MID = ".mid";
        public const string EXT_MP2 = ".mp2";
        public const string EXT_MP3 = ".mp3";
        public const string EXT_OGG = ".ogg";
        public const string EXT_WAV = ".wav";
        public const string EXT_WMA = ".wma";

        #region 文件信息
        /// <summary>
        /// 支持的文件格式
        /// </summary>
        public static string[] SupportedTypes { get; } = init();

        public const string FILTER = "*.mp3;*.wma;*.wav;*.m4a|常用音乐类型|*.mp2;*.mp3|MPEG音频|*.wav|波形声音|*.wma|Windows Media Audio音频|*.m4a|MPEG4音频|*.AAC|AAC音频|.cda|CD音轨";
        #endregion

        #region constructor destructor 
        static Types()
        {
            init();
        }
        #endregion

        #region private method
        private static string[] init()
        {
            return new string[]
            {
                EXT_ACC,
                EXT_AMR,
                EXT_M4A,
                EXT_MID,
                EXT_MP2,
                EXT_MP3,
                EXT_WAV,
                EXT_WMA
            };
        }
        #endregion

    }
}
