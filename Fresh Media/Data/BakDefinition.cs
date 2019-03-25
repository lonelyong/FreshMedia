using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.Data
{
    /******************************
    //
    //
    //
    ******************************/
    class BakDefinition
    {
        public enum BakType : int
        {
            [Description("程序设置")]
            Config = 0,
            [Description("快捷键")]
            Hotkeys = 1,
            [Description("主题设置")]
            Theme = 2,
            [Description("歌词设置")]
            Lyric = 3,
            [Description("当前播放列表")]
            LibCurrent = 4,
            [Description("本地列表")]
            LibLocal = 5,
            [Description("收藏列表")]
            LibFavo = 6,
            [Description("历史播放列表")]
            LibHistory = 7,
            [Description("最近添加列表")]
            LibRecentlyAdded = 8,
            [Description("最多播放列表")]
            LibMostlyPlayed = 9,
            [Description("历史使用目录")]
            OpenHistory = 10
        }

        /// <summary>
        /// 获取指定Types的说明
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetBakTypeDesc(BakType type)
        {
            return NgNet.EnumHelper.GetEnumDescription(type);
        }
    }
}
