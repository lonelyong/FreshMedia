using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FreshMedia.View.VList
{
    class ListMenu
    {
        #region public properties 
        public ContextMenuStrip cms { get; }
        #endregion

        #region private field
        private ToolStripMenuItem tsmi_play = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_linshiPlay = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_openBy = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_fileRemove = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_fileDel = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_fRmAlllist = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_fileInfo = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_addTo = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_sendTo = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_fileRename = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_audioLoc = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_openLrc = new ToolStripMenuItem();

        private Controller.MainController _mc;
        #endregion

        #region constructor destructor 
        public ListMenu(IFreshMedia freshMedia)
        {
            cms = new ContextMenuStrip();
            _mc = freshMedia.Controller;
            initMenu();
        }
        #endregion

        #region  events
        private void initMenu()
        {
            //cms.Font = new System.Drawing.Font("新宋体", 10.5F);
            cms.Items.AddRange(new ToolStripMenuItem[]{
                    tsmi_play,
                    tsmi_linshiPlay,
                    tsmi_openBy,
                    tsmi_fileRemove,
                    tsmi_fileDel,
                    tsmi_fRmAlllist,
                    tsmi_fileInfo,
                    tsmi_addTo,
                    tsmi_sendTo,
                    tsmi_fileRename,
                    tsmi_audioLoc,
                    tsmi_openLrc});
            cms.Opening += new CancelEventHandler(cms_Opening);

            tsmi_play.Text = "欣赏";
            tsmi_play.Click += new EventHandler(listPlayEvent);

            tsmi_linshiPlay.Text = "临时播放";
            tsmi_linshiPlay.Click += new EventHandler(interimPlayEvent);

            tsmi_openBy.Text = "打开方式";
            tsmi_openBy.Click += new EventHandler(openByEvent);

            tsmi_fileRemove.Text = "移除";
            tsmi_fileRemove.Click += new EventHandler(fileRemoveEvent);

            tsmi_fileDel.Text = "删除(包过文件)";
            tsmi_fileDel.Click += new EventHandler(fileDeleteEvent);

            tsmi_fRmAlllist.Text = "移除( 所有列表中)";
            tsmi_fRmAlllist.Click += new EventHandler(removeAudioInAllListEvent);

            tsmi_fileInfo.Text = "音乐信息";
            tsmi_fileInfo.Click += new EventHandler(showAudioInfoEvent);

            tsmi_addTo.Text = "添加到";
            tsmi_addTo.Click += new EventHandler(addToOtherListEvent);

            tsmi_sendTo.Text = "发送到";
            tsmi_sendTo.Click += new EventHandler(sendToRemovableDiskEvent);

            tsmi_fileRename.Text = "重命名";
            tsmi_fileRename.Click += new EventHandler(fileRenameEvent);

            tsmi_audioLoc.Text = "所在文件夹";
            tsmi_audioLoc.Click += new EventHandler(audioLocationEvent);

            tsmi_openLrc.Text = "显示歌词文件";
            tsmi_openLrc.Click += new EventHandler(openLyricEvent);

            tsmi_sendTo.DropDownOpening += new EventHandler(tsmi_sendToRemovableDisk_DropDownOpening);

            tsmi_addTo.DropDownOpening += new EventHandler(tsmi_addToOtherList_DropDownOpening);
        }

        private void interimPlayEvent(object sender, EventArgs e)
        {
            //临时播放选择的音乐     
            _mc.PlayController.InterimPlay(_mc.ListViewManager.MyListView.SelectedPath);
        }

        private void openByEvent(object sender, EventArgs e)
        {
            if (Controller.PlayController.FileTest(_mc.ListViewManager.MyListView.SelectedPath))
            {
                NgNet.IO.FileHelper.OpenAs(_mc.ListViewManager.MyListView.SelectedPath);
            }
        }

        private void listPlayEvent(object sender, EventArgs e)
        {
            ToolStripMenuItem srctsmi = (ToolStripMenuItem)sender;
            if (Controller.PlayController.FileTest(_mc.ListViewManager.MyListView.SelectedPath) == false)
                return;
            if (cms.SourceControl == _mc.ListViewManager.MyListView.ListDataGridView)
            {
                if (_mc.ListViewManager.MyListView.ShowedLib == FreshMedia.List.MyLib.Playing)
                    if (_mc.PlayController.myPlayer.IsInterimPlay == false)
                        if (string.Compare(_mc.PlayController.myPlayer.settings.URL, _mc.ListViewManager.MyListView.SelectedPath, true) == 0)
                            if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                                _mc.PlayController.myPlayer.ctControls.pause();
                            else if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                                _mc.PlayController.myPlayer.ctControls.play();
                            else
                                _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, _mc.ListViewManager.MyListView.SelectedPath);
                        else
                            _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, _mc.ListViewManager.MyListView.SelectedPath);
                    else
                        _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, _mc.ListViewManager.MyListView.SelectedPath);
                else
                    _mc.PlayController.ListPlay(_mc.ListViewManager.MyListView.ShowedLib, _mc.ListViewManager.MyListView.ShowedList, _mc.ListViewManager.MyListView.SelectedPath);

            }
            else if (cms.SourceControl == _mc.ListViewManager.MyListView.PlayingListTreeView)
            {
                if (_mc.PlayController.myPlayer.IsInterimPlay == false)
                    if (string.Compare(_mc.PlayController.myPlayer.settings.URL, _mc.ListViewManager.MyListView.SelectedPath, true) == 0)
                        if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                            _mc.PlayController.myPlayer.ctControls.pause();
                        else if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                            _mc.PlayController.myPlayer.ctControls.play();
                        else
                            _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, _mc.ListViewManager.MyListView.SelectedPath);
                    else
                        _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, _mc.ListViewManager.MyListView.SelectedPath);
                else
                    _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, _mc.ListViewManager.MyListView.SelectedPath);
            }
        }

        private IEnumerable<string> fileRemoveDialog(int removeType)
        {
            //获取删除提示信息
            StringBuilder sb = new StringBuilder();
            List<string> paths = new List<string>();
            //删除操作来源与Dgv_ListItem并且Dgv_List有多个选中行
            if (cms.SourceControl == _mc.ListViewManager.MyListView.ListDataGridView)
            {
                foreach (DataGridViewRow item in _mc.ListViewManager.MyListView.ListDataGridView.SelectedRows)
                {
                    paths.Add(item.Tag.ToString());
                    sb.Append("\r\n  " + item.Tag.ToString());
                }
            }
            //删除操作来自于正在播放列表（Tv_ListItem）或者来自于Dgv_ListItem并且Dgv_List只有一个选中行
            else if (cms.SourceControl == _mc.ListViewManager.MyListView.PlayingListTreeView)
            {
                paths.Add(_mc.ListViewManager.MyListView.SelectedPath);
                sb.Append("\r\n  " + _mc.ListViewManager.MyListView.SelectedPath);
            }
            sb.Insert(0, string.Format("是否要 *{0}* 选定的< {1} >首音乐？", removeType == 0 ? "移除" : (removeType == 1 ? "删除(包过文件)" : "在所有列表中移除"), paths.Count));
            switch (_mc.ListViewManager.MyListView.ShowedLib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    sb.Append("\r\n所属音乐库：[正在播放]");
                    break;
                case List.MyLib.Local:
                    sb.Append("\r\n所属音乐库：[本地列表]\r\n所属列表：  [" + _mc.ListViewManager.MyListView.ShowedList + "]");
                    break;
                case List.MyLib.History:
                    sb.Append("\r\n所属音乐库：[历史播放]");
                    break;
                case List.MyLib.Favorite:
                    sb.Append("\r\n所属音乐库：[我的收藏]");
                    break;
                case List.MyLib.RecentlyAdded:
                    sb.Append("\r\n所属音乐库：[最近添加]");
                    break;
                case List.MyLib.MostlyPlayed:
                    sb.Append("\r\n所属音乐库：[最多播放]");
                    break;
                default:
                    break;
            }
            //提示是否真的移除
            Form owner = _mc.ListViewManager.MyListView.ListDataGridView.FindForm();
            if (NgNet.UI.Forms.MessageBox.Show(owner, sb.ToString(), "音乐移除(删除)", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return null;
            return paths;
        }

        private void fileRemoveEvent(object sender, EventArgs e)
        {
            IEnumerable<string> paths = fileRemoveDialog(0);
            if (paths == null)
                return;
            if (cms.SourceControl == _mc.ListViewManager.MyListView.ListDataGridView)
            {
                _mc.MyLists.RemoveMedias(_mc.ListViewManager.MyListView.ShowedLib, _mc.ListViewManager.MyListView.ShowedList, paths);
                _mc.ListViewManager.MyListView.ListDataGridView.ClearSelection();
            }
            else if (cms.SourceControl == _mc.ListViewManager.MyListView.PlayingListTreeView)
                _mc.MyLists.RemoveMedias(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, paths);

        }

        private void fileDeleteEvent(object sender, EventArgs e)
        {
            IEnumerable<string> paths = fileRemoveDialog(1);
            if (paths == null) return;

        }

        private void removeAudioInAllListEvent(object sender, EventArgs e)
        {
            IEnumerable<string> paths = fileRemoveDialog(2);
            if (paths == null)
                return;
            _mc.MyLists.RemoveAudioInAllList(paths);
        }

        private void audioLocationEvent(object sender, EventArgs e)
        {
            if (Controller.PlayController.FileTest(_mc.ListViewManager.MyListView.SelectedPath))
                NgNet.IO.PathHelper.ShowInExplorer(_mc.ListViewManager.MyListView.SelectedPath, true);
        }

        private void openLyricEvent(object sender, EventArgs e)
        {
            _mc.LyricManager.OpenLyricByAudioPath(_mc.ListViewManager.MyListView.SelectedPath);
        }

        private void addToOtherListEvent(object sender, EventArgs e)
        {
            //获取要添加的路径
            List<string> paths = new List<string>();
            if (cms.Tag == _mc.ListViewManager.MyListView.ListDataGridView)
            {
                foreach (DataGridViewRow item in _mc.ListViewManager.MyListView.ListDataGridView.SelectedRows)
                {
                    paths.Add(item.Tag.ToString());
                }
            }
            else if (cms.Tag == _mc.ListViewManager.MyListView.PlayingListTreeView)
            {
                paths.Add(_mc.ListViewManager.MyListView.SelectedPath);
            }
            else
            {
                return;
            }
            //判断添加到哪里
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            switch ((List.MyLib)tsmi.Tag)
            {
                case List.MyLib.Playing:
                case List.MyLib.Local:
                case List.MyLib.Favorite:
                    _mc.MyLists.AddMedias((List.MyLib)tsmi.Tag, tsmi.Name, paths);
                    break;
            }
        }

        private void sendToRemovableDiskEvent(object sender, EventArgs e)
        {
            //获取要添加的路径
            string dName = ((ToolStripMenuItem)sender).Name;
            List<string> paths = new List<string>();
            if (cms.Tag == _mc.ListViewManager.MyListView.ListDataGridView)
            {
                foreach (DataGridViewRow item in _mc.ListViewManager.MyListView.ListDataGridView.SelectedRows)
                {
                    paths.Add(item.Tag.ToString());
                }
            }
            else if (cms.Tag == _mc.ListViewManager.MyListView.PlayingListTreeView)
            {
                paths.Add(_mc.ListViewManager.MyListView.SelectedPath);
            }
            else
            {
                return;
            }
            //判断指定的文件是否存在
            List<string> unExistsFiles = new List<string>();
            List<string> existsFiles = new List<string>();
            List<string> existedFiles = new List<string>();
            foreach (string item in paths)
            {
                if (System.IO.File.Exists(item))
                {
                    existsFiles.Add(item);
                    existedFiles.Add(dName + System.IO.Path.GetFileName(item));
                }
                else
                {
                    unExistsFiles.Add(item);
                }
            }
            StringBuilder sb = new StringBuilder();
            if (existsFiles.Count > 0)
            {
                sb.Append(string.Format("即将发送以下文件到 <{0}>:\r\n", dName));
                foreach (string item in existsFiles)
                {
                    sb.Append(item);
                    sb.Append("\r\n");
                }
            }
            sb.Append("\r\n");
            if (unExistsFiles.Count > 0)
            {
                sb.Append(string.Format("由于文件不存在不会发送以下文件:\r\n"));
                foreach (string item in unExistsFiles)
                {
                    sb.Append(item);
                    sb.Append("\r\n");
                }
            }
            Form owner = _mc.ListViewManager.MyListView.ListDataGridView.FindForm();
            DialogResult dr = NgNet.UI.Forms.MessageBox.Show(owner, sb.ToString(), "发送文件到可移动磁盘", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                for (int i = 0; i < existsFiles.Count; i++)
                {
                    NgNet.IO.FileHelper.Copy(existsFiles[i], existedFiles[i], Application.ProductName);
                }
            }
        }

        private void showAudioInfoEvent(object sender, EventArgs e)
        {
            _mc.ShowMediaInfoBox(_mc.ListViewManager.MyListView.SelectedPath, _mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList);
        }

        private void fileRenameEvent(object sender, EventArgs e)
        {
            string noti = string.Empty;
            //检测文件是否存在
            Form _owner = _mc.ListViewManager.MyListView.ListDataGridView.FindForm();
            if (File.Exists(_mc.ListViewManager.MyListView.SelectedPath))
            {
                //检测当前文件是否在使用
                if (string.Compare(_mc.PlayController.myPlayer.settings.URL, _mc.ListViewManager.MyListView.SelectedPath, true) == 0)
                {
                    NgNet.UI.Forms.MessageBox.Show(_owner, "当前音乐正在被播放，无法重命名，请停止播放后再试！");
                    return;
                }
            }
            else //文件不存在
                noti = "[文件不存在]";
            Re:
            string newName = NgNet.UI.Forms.InputBox.Show(
                _owner,
                "请输入新名称" + noti,
                Path.GetFileNameWithoutExtension(_mc.ListViewManager.MyListView.SelectedPath),
                string.Format("重命名< {0} >", Path.GetFileNameWithoutExtension(_mc.ListViewManager.MyListView.SelectedPath)));
            //检测是否有输入新文件名
            if (string.IsNullOrWhiteSpace(newName))
                return;
            //检测名称是否有改动
            if (string.Compare(Path.GetFileNameWithoutExtension(_mc.ListViewManager.MyListView.SelectedPath), newName, true) == 0) return;
            //检测文件名是否合法
            if (!NgNet.IO.PathHelper.IsName(newName))
            {
                NgNet.UI.Forms.MessageBox.Show(_owner, "您输入的文件名不合法，请重新输入！", "重命名提示");
                goto Re;
            }
            string newpath = Path.GetDirectoryName(_mc.ListViewManager.MyListView.SelectedPath) + @"\" + newName + Path.GetExtension(_mc.ListViewManager.MyListView.SelectedPath);
            //检测新文件名是否和已有文件冲突
            if (File.Exists(newpath))
            {
                NgNet.UI.Forms.MessageBox.Show(_owner, "新文件名与已有文件名冲突！\r\n已存在文件[" + newpath + "]", "重命名提示");
                goto Re;
            }
            else if (Directory.Exists(newpath))
            {
                NgNet.UI.Forms.MessageBox.Show(_owner, "新文件名与已有文件夹名冲突！\r\n已存在文件夹[" + newpath + "]", "重命名提示");
                goto Re;
            }
            //进行重命名
            NgNet.UI.Forms.MessageBox.Show(_owner, "该功能正在实现中,待续。。。");
            switch (_mc.ListViewManager.MyListView.ShowedLib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    break;
                case List.MyLib.Local:
                    break;
                case List.MyLib.History:
                    break;
                case List.MyLib.Favorite:
                    break;
                case List.MyLib.RecentlyAdded:
                    break;
                case List.MyLib.MostlyPlayed:
                    break;
                default:
                    break;
            }
        }

        private void tsmi_addToOtherList_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem srctsmi = (ToolStripMenuItem)sender;
            srctsmi.DropDownItems.Clear();
            //收藏项
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = List.ListManager.NAME_LIST_FAVO;
            tsmi.Name = tsmi.Text;
            tsmi.Click += new EventHandler(addToOtherListEvent);
            tsmi.Tag = List.MyLib.Favorite;
            tsmi.Enabled = !(_mc.ListViewManager.MyListView.ShowedLib == List.MyLib.Favorite && cms.Tag == _mc.ListViewManager.MyListView.ListDataGridView);
            srctsmi.DropDownItems.Add(tsmi);
            //正在播放项
            tsmi = new ToolStripMenuItem();
            tsmi.Text = List.ListManager.NAME_LIST_CURRENT;
            tsmi.Name = tsmi.Text;
            tsmi.Click += new EventHandler(addToOtherListEvent);
            tsmi.Tag = List.MyLib.Playing;
            tsmi.Enabled = !(_mc.ListViewManager.MyListView.ShowedLib == List.MyLib.Playing || cms.Tag == _mc.ListViewManager.MyListView.PlayingListTreeView);
            srctsmi.DropDownItems.Add(tsmi);
            srctsmi.DropDownItems.Add("-");
            //本地列表

            foreach (List.AudioList item in _mc.MyLists.Local)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.Name))
                    continue;
                tsmi = new ToolStripMenuItem();
                tsmi.Text = string.Format("{0}    <{1}>", item.Name, List.ListManager.NAME_LIB_LOCAL);
                tsmi.Click += new EventHandler(addToOtherListEvent);
                tsmi.Name = item.Name;
                tsmi.Tag = List.MyLib.Local;
                tsmi.Enabled = !(_mc.ListViewManager.MyListView.ShowedLib == List.MyLib.Local
                              && _mc.ListViewManager.MyListView.ShowedList == item.Name
                              && cms.Tag == _mc.ListViewManager.MyListView.ListDataGridView);
                srctsmi.DropDownItems.Add(tsmi);
            }
        }

        private void tsmi_sendToRemovableDisk_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem srctsmi = (ToolStripMenuItem)sender;
            srctsmi.DropDownItems.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            int dri_num = 0;
            foreach (DriveInfo di in drives)
            {
                if (di.IsReady)
                    if (di.DriveType == DriveType.Removable)
                    {
                        dri_num++;
                        ToolStripMenuItem tsmi = new ToolStripMenuItem();
                        tsmi.Name = di.Name;
                        string text = (string.IsNullOrWhiteSpace(di.VolumeLabel) ? NgNet.IO.DriveHelper.GetDriveTypeDescription(di.DriveType) : di.VolumeLabel)
                            + string.Format("<{0}>", di.Name.Substring(0, 1));
                        tsmi.Text = (di.IsReady ? null : "<未就绪> ") + text;
                        tsmi.Click += new EventHandler(sendToRemovableDiskEvent);
                        srctsmi.DropDownItems.Add(tsmi);
                    }
            }
            if (dri_num == 0)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = "未检测到移动设备";
                srctsmi.DropDownItems.Add(tsmi);
                tsmi.Click += new EventHandler((Object sd, EventArgs ea) =>
                {
                    NgNet.UI.Forms.MessageBox.Show(_mc.ListViewManager.MyListView.ListDataGridView.FindForm(),
                        "请插入移动设备后再试！",
                        null,
                        MessageBoxButtons.OK,
                        DialogResult.OK, 12);
                });
            }
        }

        private void cms_Opening(object sender, CancelEventArgs e)
        {
            cms.Tag = cms.SourceControl;
            cms.Items.Clear();
            //从列表显示器中弹出此菜单
            if (cms.SourceControl == _mc.ListViewManager.MyListView.ListDataGridView)
            {
                //当前选中的行数为1
                if (_mc.ListViewManager.MyListView.ListDataGridView.SelectedRows.Count == 1)
                {
                    this.cms.Items.AddRange(new ToolStripMenuItem[]{
                                this.tsmi_play,
                                this.tsmi_linshiPlay,
                                this.tsmi_openBy,
                                this.tsmi_fileRemove,
                                this.tsmi_fileDel,
                                this.tsmi_fRmAlllist});
                }
                //当前选中的行为大于1
                else if (_mc.ListViewManager.MyListView.ListDataGridView.SelectedRows.Count > 1)
                {
                    this.cms.Items.AddRange(new ToolStripMenuItem[]{
                                this.tsmi_openBy,
                                this.tsmi_fileRemove,
                                this.tsmi_addTo,
                                this.tsmi_sendTo,
                                this.tsmi_fileDel,
                                this.tsmi_fRmAlllist});
                    return;//不继续执行
                }
            }
            //从正在播放列表弹出此菜单
            else if (this.cms.SourceControl == _mc.ListViewManager.MyListView.PlayingListTreeView)
            {
                this.cms.Items.AddRange(new ToolStripMenuItem[]{
                    this.tsmi_play,
                    this.tsmi_linshiPlay,
                    this.tsmi_openBy,
                    this.tsmi_fileRemove,
                    this.tsmi_fileDel,
                    this.tsmi_fRmAlllist});
            }
            this.cms.Items.AddRange(new ToolStripMenuItem[]{
                    this.tsmi_addTo,
                    this.tsmi_sendTo,
                    this.tsmi_fileRename,
                    this.tsmi_fileInfo,
                    this.tsmi_audioLoc,
                    this.tsmi_openLrc});
            if (this.cms.SourceControl == _mc.ListViewManager.MyListView.PlayingListTreeView)
                if (((IFreshMedia)_mc.ListViewManager).Controller.PlayController.myPlayer.IsInterimPlay == false)
                    if (string.Compare(((IFreshMedia)_mc.ListViewManager).Controller.PlayController.myPlayer.settings.URL, _mc.ListViewManager.MyListView.SelectedPath, true) == 0)
                        if (((IFreshMedia)_mc.ListViewManager).Controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                            tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PAUSE;
                        else if (((IFreshMedia)_mc.ListViewManager).Controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                            tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_CONTINUE;
                        else
                            tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
                    else
                        tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
                else
                    tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
            else if (this.cms.SourceControl == _mc.ListViewManager.MyListView.ListDataGridView)
                if (_mc.ListViewManager.MyListView.ShowedLib == FreshMedia.List.MyLib.Playing)
                    if (((IFreshMedia)_mc.ListViewManager).Controller.PlayController.myPlayer.IsInterimPlay == false)
                        if (string.Compare(((IFreshMedia)_mc.ListViewManager).Controller.PlayController.myPlayer.settings.URL, _mc.ListViewManager.MyListView.SelectedPath, true) == 0)
                            if (((IFreshMedia)_mc.ListViewManager).Controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                                tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PAUSE;
                            else if (((IFreshMedia)_mc.ListViewManager).Controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                                tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_CONTINUE;
                            else
                                tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
                        else
                            tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
                    else
                        tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
                else
                    tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
            else
                tsmi_play.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
        }
        #endregion
    }
}
