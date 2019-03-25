using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace FreshMedia.List
{
    class OpenHistory
    {
        #region public filed
        public string LastOpenFolder { get; set; }//上次打开文件的文件夹

        public NgNet.Collections.SignleCollection<string> FolderOpenHistory { get; }//打开文件夹的目录列表

        #endregion

        #region constructer destructer
        public OpenHistory()
        {
            LastOpenFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//上次打开文件的文件夹
            FolderOpenHistory = new NgNet.Collections.SignleCollection<string>();
        }
        #endregion

        #region 添加打开历史
        public void AddFolder(string path)
        {
            //路径非法则返回
            if (NgNet.IO.PathHelper.IsPath(path) == false)
                return;
            path = path.Trim();
            //更新记录上次打开文件夹路径
            LastOpenFolder = path;
            //将新路径置顶
            FolderOpenHistory.Remove(path);
            FolderOpenHistory.Insert(0, path);
        }
        #endregion
    }
}
