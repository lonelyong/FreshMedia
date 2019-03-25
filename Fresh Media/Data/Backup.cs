using FreshMedia.Controller;
using FreshMedia.View;
using System;
using System.Collections.Generic;
using System.IO;

namespace FreshMedia.Data
{
    class BackupMamager : IFreshMedia
    {
        #region private filed
        Controller.MainController _controller;
        /// <summary>
        /// 要备份的文件列表
        /// </summary>
        List<string> fileNames;

        RestoreView _restoreView;
        #endregion

        #region  constructor destructor 
        public BackupMamager(IFreshMedia iFreshMedia)
        {
            this._controller = iFreshMedia.Controller;
            fileNames = new List<string>();
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_AppConfig));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Lyric));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Theme));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Hotkeys));

            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_FolderHistory));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Lib_Current));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Lib_Favorite));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Lib_History));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Lib_Local));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Lib_MostlyPlayed));
            fileNames.Add(Path.GetFileName(_controller.DataManager.Paths.FilePath_Lib_RecentlyAdded));
            _restoreView = new RestoreView(this);
        }
        #endregion

        #region private metheds
        /// <summary>
        /// 清理备份缓存目录
        /// </summary>
        /// <param name="tempPath">缓存目录</param>
        /// <returns></returns>
        private string resetTemp()
        {
            string _tempPath = $"{Environment.GetEnvironmentVariable("temp")}\\{NgNet.Applications.Current.AssemblyProduct}\\{Guid.NewGuid()}";
            try
            {
                if (Directory.Exists(_tempPath))
                {
                    DirectoryInfo _di = new DirectoryInfo(_tempPath);
                    foreach (FileInfo item in _di.GetFiles())
                        item.Delete();
                    foreach (DirectoryInfo item in _di.GetDirectories())
                        Directory.Delete(item.FullName, true);
                }
                else
                    Directory.CreateDirectory(_tempPath);
                return _tempPath;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 保存备份数据到临时目录
        /// </summary>
        /// <returns></returns>
        private bool saveData(string tmpPath)
        {
            string[] typeNames = Enum.GetNames(typeof(Data.BakDefinition.BakType));
            DataManager _md = new DataManager();
            _md.SetAppdataMode(ApplicationDataModes.UserSet, tmpPath);
            _md.AppConfigCreat(_controller, $"{tmpPath}\\{typeNames[0]}.xml");
            _md.HotkeysCreat(_controller.HotkeyManager, $"{tmpPath}\\{typeNames[1]}.xml");
            _md.ThemeCreate(_controller.Theme, $"{tmpPath}\\{typeNames[2]}.xml");
            _md.LyricCreat(_controller.LyricManager, $"{tmpPath}\\{typeNames[3]}.xml");
            _md.libCurrentCreat(_controller.MyLists, $"{tmpPath}\\{typeNames[4]}.xml");
            _md.libLocalCreat(_controller.MyLists, $"{tmpPath}\\{typeNames[5]}.xml");
            _md.libFavoriteCreat(_controller.MyLists, $"{tmpPath}\\{typeNames[6]}.xml");
            _md.libHistoryCreat(_controller.MyLists, $"{tmpPath}\\{typeNames[7]}.xml");
            _md.libRecentlyAddedCreat(_controller.MyLists, $"{tmpPath}\\{typeNames[8]}.xml");
            _md.libMostlyPlayedCreat(_controller.MyLists, $"{tmpPath}\\{typeNames[9]}.xml");
            _md.OpenHistoryCreat(_controller.OpenHistoryManager, $"{tmpPath}\\{typeNames[10]}.xml");
            System.IO.File.WriteAllText($"{tmpPath}\\warning.txt", "请勿修改文件内容,以及重命名文件");
            return true;
        }

        /// <summary>
        /// 解压备份文件
        /// </summary>
        /// <param name="bakPath"></param>
        /// <param name="tmpPath"></param>
        /// <returns></returns>
        private bool unZipBak(string bakPath, string tmpPath)
        {
            NgNet.IO.ZipHelper.UnpackFiles(bakPath, $"{tmpPath}\\");
            return true;
        }

        /// <summary>
        /// 生成备份文件
        /// </summary>
        /// <param name="bakPath"></param>
        /// <param name="tmpPath"></param>
        /// <returns></returns>
        private bool zipPak(string bakPath, string tmpPath)
        {
            NgNet.IO.ZipHelper.PackFiles(bakPath, tmpPath);
            return true;
        }

        /// <summary>
        /// 获取指定备份文件的meta文件
        /// </summary>
        /// <param name="tmpPath"></param>
        /// <returns></returns>
        private string getMetaPath(string tmpPath)
        {
            return string.Format("{0}\\bakdef.meta", tmpPath);
        }

        /// <summary>
        /// 创建指定备份文件的meta文件
        /// </summary>
        /// <param name="tmpPath"></param>
        private void writeMeta(string tmpPath)
        {
            string _metaPath = getMetaPath(tmpPath);
            string inf = string.Format("备份时间：{0}", DateTime.Now);
            inf = NgNet.Security.RsaCryptoService.JustEncrypt(inf);
            File.WriteAllText(_metaPath, inf);
        }

        /// <summary>
        /// 读取指定备份文件的meta信息
        /// </summary>
        /// <param name="tmpPath"></param>
        /// <returns></returns>
        private string readMeta(string tmpPath)
        {
            string _metaPath = getMetaPath(tmpPath);
            File.ReadAllText(_metaPath);
            bool _boolTmp = File.Exists(_metaPath);
            if (_boolTmp)
                return NgNet.Security.RsaCryptoService.JustDecrypt(File.ReadAllText(_metaPath));
            return null;
        }
        #endregion
        
        #region public methods
        /// <summary>
        /// 分析备份文件
        /// </summary>
        /// <param name="bakPath">备份文件位置</param>
        /// <param name="types">输出备份文件包含的备份类型</param>
        /// <param name="meta">输出备份文件信息</param>
        /// <returns>返回缓存文件夹</returns>
        public string AnalyseBak(string bakPath, out int[] types, out string meta)
        {
            int[] _types;

            string[] _typeNames;
            int[] _typeValues;
            string _inf;
            string _meta;
            string _tempPath;

            #region 判断文件是否存在
            if (File.Exists(bakPath) == false)
            {
                _inf = $"您指定的文件不存在({bakPath})。";
                goto FALSE;
            }
            #endregion

            #region 创建缓存目录
            try
            {
                _tempPath = resetTemp();
            }
            catch (Exception ex)
            {
                _inf = ex.Message;
                goto FALSE;
            }
            #endregion

            #region 解压备份文件到缓存目录
     
            try
            {
                unZipBak(bakPath, _tempPath);
            }
            catch(Exception ex)
            {
                _inf = ex.Message;
                goto FALSE;
            }
            #endregion

            #region 分析解压后文件
            _typeValues = (int[])Enum.GetValues(typeof(BakDefinition.BakType));
            _typeNames = Enum.GetNames(typeof(BakDefinition.BakType));
            _types = new int[_typeValues.Length];
            for (int i = 0; i < _typeValues.Length; i++)
            {
                if (File.Exists($"{_tempPath}\\{_typeNames[i]}.xml"))
                    _types[i] = 1;
                else
                    _types[i] = 0;  
            }
            _meta = readMeta(_tempPath);
            #endregion
            types = _types;
            meta = _meta;
            return _tempPath;
        FALSE:
            types = new int[] { };
            meta = null;
            throw new Exception(_inf);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="bakPath">备份文件位置</param>
        /// <param name="types">要恢复的设置</param>
        public bool Restore(string bakPath, int[] types)
        {
            int[] _types;
            string[] _typeNames;
            string _bakMeta;
            string _inf = null;
            string _tempPath;
            _tempPath = AnalyseBak(bakPath, out _types, out _bakMeta);

            if(types.Length > _types.Length)
            {
                _inf = "指定的备份类型超过了备份文件定义的类型。";
                goto FALSE;
            }
            #region 根据指定的恢复类型恢复指定的文件
            _typeNames = Enum.GetNames(typeof(BakDefinition.BakType));
            for (int i = 0; i < types.Length; i++)
            {
                if(types[i] == 1 && _types[i] == 1)
                {
                    #region
                    switch ((Data.BakDefinition.BakType)i)
                    {
                        case BakDefinition.BakType.Config:
                            _controller.DataManager.AppConfigRead(_controller, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.Hotkeys:
                            _controller.DataManager.HotkeysRead(_controller.HotkeyManager, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.Theme:
                            _controller.DataManager.ThemeRead(_controller.Theme, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.Lyric:
                            _controller.DataManager.LyricRead(_controller.LyricManager, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.LibCurrent:
                            _controller.DataManager.libCurrentRead(_controller.MyLists, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.LibLocal:
                            _controller.DataManager.libLocalRead(_controller.MyLists, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.LibFavo:
                            _controller.DataManager.libFavoriteRead(_controller.MyLists, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.LibHistory:
                            _controller.DataManager.libHistoryRead(_controller.MyLists, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.LibRecentlyAdded:
                            _controller.DataManager.libRecentlyAddedRead(_controller.MyLists, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.LibMostlyPlayed:
                            _controller.DataManager.libMostlyPlayedRead(_controller.MyLists, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        case BakDefinition.BakType.OpenHistory:
                            _controller.DataManager.OpenHistoryRead(_controller.OpenHistoryManager, $"{_tempPath}\\{_typeNames[i]}.xml");
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
            }
            #endregion
            return true;
        FALSE:
            throw new Exception(_inf);
        }
        
        /// <summary>
        /// 备份
        /// </summary>
        public void Backup()
        {
            bool _tmpBool;
            string _uPath;
            string _tempPath;
            string _inf = null;
            #region 浏览备份文件存储位置
            System.Windows.Forms.DialogResult _dr;
            Re:
            NgNet.UI.Forms.DirSelectBox dsb = new NgNet.UI.Forms.DirSelectBox();
            dsb.Title = "设置备份数据保存位置";
            dsb.Enterpath = string.Format("{0}\\", _controller.DataManager.Paths.FolderPath_Backup);
            dsb.BackColor = _controller.Theme.BackColor;
            dsb.BorderColor = _controller.Theme.BorderColor;
            _uPath = dsb.Show(_controller.MainForm);
            if (string.IsNullOrWhiteSpace(_uPath))
                goto CANCEL;
            #endregion

            #region 创建缓存目录
            try
            {
                _tempPath = resetTemp();
            }
            catch (Exception ex)
            {
                _inf = ex.Message;
                goto FALSE;
            }
            #endregion

            #region 判断备份文件路径是否在缓存目录下
            if (string.Compare(_uPath, _tempPath, true) == 0)
            {
                _inf = "请勿将备份文件保存在临时文件夹，请重新选择保存位置，是否重试？";
                _dr = NgNet.UI.Forms.MessageBox.Show(_controller.MainForm, _inf, System.Windows.Forms.MessageBoxButtons.YesNo);
                if (_dr == System.Windows.Forms.DialogResult.Yes)
                    goto Re;
                else
                    goto CANCEL;
            }
            #endregion

            #region 保存备份数据到临时目录
            try
            {
                _tmpBool = saveData(_tempPath);
            }
            catch (Exception ex)
            {
                _tmpBool = false;
                _inf = ex.Message;
                goto FALSE;
            }
            #endregion

            //  生成备份文件的文件名
            _uPath = string.Format("{0}\\[{1} {2}-{3}-{4}].backup", 
                _uPath, 
                GetType().Assembly.GetName().Name, 
                DateTime.Now.Year, 
                DateTime.Now.Month, 
                DateTime.Now.Day);

            #region 判断是否存在今日的备份
            if (File.Exists(_uPath))
            {
                _inf = string.Format("当前目录下已存在今日的备份，是否覆盖？");
                _dr = NgNet.UI.Forms.MessageBox.Show(_controller.MainForm, _inf, System.Windows.Forms.MessageBoxButtons.YesNo);
                if (_dr == System.Windows.Forms.DialogResult.No)
                    goto CANCEL;

            }
            #endregion

            #region 生成备份文件
            try
            {
                writeMeta(_tempPath);
                zipPak(_uPath, _tempPath);
            }
            catch (Exception ex)
            {
                _inf = ex.Message;
                goto FALSE;
            }

            #endregion
            NgNet.UI.Forms.MessageBox.Show(_controller.MainForm, "备份成功");
            return;
            FALSE:
            NgNet.UI.Forms.MessageBox.Show(_controller.MainForm, $"备份失败，可能原因:\r\n    1.{_inf}", "备份失败");
            return;
            CANCEL:
            NgNet.UI.Forms.MessageBox.Show(_controller.MainForm, $" 未完成备份。", "未备份");
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public void Restore()
        {
            _restoreView.Show();
            return;        
        }
        #endregion

        #region IFreshMedia
        MainController IFreshMedia.Controller
        {
            get
            {
                return _controller;
            }
        }
        #endregion
    }
}
