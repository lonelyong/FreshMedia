using System.Runtime.InteropServices;
namespace FreshMedia.Player
{
    public class MciUtils
    {
        #region win32 Api
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
        string lpszLongPath,
        string shortFile,
        int cchBuffer
        );

        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(
        string lpstrCommand,
        string lpstrReturnString,
        int uReturnLength,
        int hwndCallback
        );

        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        public static extern bool mciGetErrorString(int fdwError, string lpszErrorText, int cchErrorText);
        #endregion

        /// <summary>  
        /// 根据文件名，确定设备  
        /// </summary>  
        /// <param name="ff">文件名</param>  
        /// <returns></returns>  
        public static string GetDeviceType(string ff)
        {
            string result = "";
            switch (System.IO.Path.GetExtension(ff.Trim()).ToUpper())
            {
                case ".MID":
                case ".MIDI":
                case ".RMI":
                case ".IDI":
                    result = "Sequencer";
                    break;
                case ".WAV":
                    result = "WaveAudio";
                    break;
                case ".AVI":
                    result = "AviVideo";
                    break;
                case ".CDA":
                    result = "CDAudio";
                    break;
                case ".MOV":
                case ".MP2":
                case ".MP3":
                case ".MPE":
                case ".MPG":
                case ".M4A":
                case ".AAC":
                case ".MPEG":
                case ".WMA":
                    result = "MPEGVideo";
                    break;
                case ".ASF":
                case ".ASX":
                case ".IVF":
                case ".LSF":
                case ".LSX":
                case ".P2V":
                case ".WAX":
                case ".WVX":
                case ".WM":
                case ".WMP":
                case ".WMV":
                case ".WMX":
                    result = "MPEGVideo2";
                    break;

                case ".RM":
                case ".RAM":
                case ".RA":
                case ".MVB":
                    result = "RealPlay";
                    break;
                default:
                    result = "MPEGVideo";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="errorId"></param>
        /// <returns></returns>
        public static string GetErrorString(int errorId)
        {
            string error = "";
            error = error.PadLeft(256, ' ');
            mciGetErrorString(errorId, error, 256);
            return error;
        }
    }
}
