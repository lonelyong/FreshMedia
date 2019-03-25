using System;

namespace FreshMedia.Data
{
    class DataPaths
    {
        public string FilePath_AppConfig { get; }
        public string FilePath_Theme { get; }
        public string FilePath_Hotkeys { get; }
        public string FilePath_Lyric { get; }
        public string FilePath_FolderHistory { get; }
        public string FilePath_Lib_Local { get; }
        public string FilePath_Lib_Current { get; }
        public string FilePath_Lib_History { get; }
        public string FilePath_Lib_Favorite { get;}
        public string FilePath_Lib_RecentlyAdded { get;}
        public string FilePath_Lib_MostlyPlayed { get; }
        public string FilePath_MI_DataBase { get; }
        public string FolderPath_User { get; }
        public string FolderPath_Config { get; }
        public string FolderPath_Tmep { get; }
        public string FolderPath_Backup { get; }
        public string MyApplicationDatapath { get; }

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appdataMode"></param>
        /// <param name="path">当值为UserSet时必须指定此值</param>
        public DataPaths(ApplicationDataModes appdataMode, string path = null)
        {
        DATAMODEERROR:
            if (appdataMode == ApplicationDataModes.AppdataPath)
                MyApplicationDatapath = string.Format("{0}\\Yong\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), NgNet.Applications.Current.AssemblyProduct);
            else if (appdataMode == ApplicationDataModes.ApplicationPath)
                MyApplicationDatapath = NgNet.Applications.Current.Directory;
            else if (appdataMode == ApplicationDataModes.UserSet)
            {
                if (NgNet.IO.PathHelper.IsPath(path))
                    MyApplicationDatapath = path;
                else
                {
                    appdataMode = ApplicationDataModes.ApplicationPath;
                    goto DATAMODEERROR;
                }
            }
            FolderPath_Config = $"{MyApplicationDatapath}\\Config\\{ NgNet.Applications.Current.AssemblyProduct}";
            FolderPath_User = $"{MyApplicationDatapath}\\Users\\Default\\{ NgNet.Applications.Current.AssemblyProduct}";
            FolderPath_Tmep = $"{Environment.GetEnvironmentVariable("Temp")}\\{ NgNet.Applications.Current.AssemblyProduct}";
            FolderPath_Backup = $"{FolderPath_User}\\Backup";

            FilePath_AppConfig = $"{FolderPath_Config}\\{ NgNet.Applications.Current.AssemblyProduct}.xml";
            FilePath_Theme = $"{FolderPath_User}\\theme.xml";
            FilePath_Hotkeys = $"{FolderPath_User}\\hotkeys.xml";
            FilePath_Lyric = $"{FolderPath_User}\\lyric.xml";
            FilePath_FolderHistory = $"{FolderPath_User}\\openHistory.xml";

            FilePath_Lib_Local = $"{FolderPath_User}\\libLocal.xml";
            FilePath_Lib_Current = $"{FolderPath_User}\\libCurrent.xml";
            FilePath_Lib_History = $"{FolderPath_User}\\libHistory.xml";
            FilePath_Lib_Favorite = $"{FolderPath_User}\\libFavorite.xml";
            FilePath_Lib_RecentlyAdded = $"{FolderPath_User}\\libAddedHistory.xml";
            FilePath_Lib_MostlyPlayed = $"{FolderPath_User}\\libMostlyPlayed.xml";
            FilePath_MI_DataBase = $"{FolderPath_User}\\avtags.txt";
        }
        #endregion

    }
}
