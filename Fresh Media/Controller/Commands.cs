
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.Controller
{
    /// <summary>   
    /// 命令大全   
    /// </summary>   
    class Commands
    {
        public const string PC_PLAYNEXT   = "#P_NEXT";

        public const string PC_PLAYLAST   = "#P_LAST";

        public const string PC_PLAYPAUSE  = "#P_PLAYPAUSE";

        public const string PC_VOLUMEUP   = "#V_UP";

        public const string PC_VOLUMEDOWN = "#V_DOWN";

        public const string PC_VOLUMEMUTE = "#V_MUTE";
         
        public const string PC_FORWARD    = "#PC_FORWARD";

        public const string PC_REWIND     = "#PC_REWIND";

        public const string FC_EXIT       = "#FC_EXIT";

        public const string FC_NORMAX     = "#FC_MAX";

        public const string FC_MIN        = "#FC_MIN";

        public const string LIST_SEARCH   = "#M_SEARCH";

        public const string AUDIO_CURRENTLOC = "#PC_CURRENTLOC";
    }
}