using System;
using System.Collections.Generic;
using System.ComponentModel;
using NgNet.UI.Forms;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FreshMedia.View.VList
{
    class LibMenu
    {
        #region public properties
        public ContextMenuStrip cms { get; }
        #endregion

        #region private fields
        private ToolStripMenuItem tsmi_add = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_add_file = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_add_dir = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listInfo = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listMove = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listMove_up = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listMove_down = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listRemove = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listRename = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listClean = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listOpen = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listShow = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_newList = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listBackup = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listUp = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_listBack = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_reloadLibs = new ToolStripMenuItem();


        private Controller.MainController _mc;
        #endregion

        #region constructor destructor 
        public LibMenu(IFreshMedia iFreshMedia)
        {
            cms = new ContextMenuStrip();
            _mc = iFreshMedia.Controller;
            init();
        }
        #endregion

        #region private method
        private void init()
        {
            //cms.Font = new System.Drawing.Font("新宋体", 10.5F);
            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                    tsmi_add,
                    tsmi_listInfo,
                    tsmi_listMove,
                    tsmi_listRemove,
                    tsmi_listRename,
                    tsmi_listClean,
                    tsmi_listOpen,
                    tsmi_listShow,
                    tsmi_newList,
                    tsmi_listBackup
            });
            cms.Opening += new CancelEventHandler(cms_Opening);
            // 
            // tsmi_add
            // 
            tsmi_add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                tsmi_add_file,
                tsmi_add_dir});
            tsmi_add.Text = "添加音乐(&A)";
            // 
            // tsmi_add_file
            // 
            tsmi_add_file.Text = "本地文件...";
            tsmi_add_file.Click += new EventHandler(tsmi_add_file_Click);
            // 
            // tsmi_add_dir
            // 
            tsmi_add_dir.Text = "本地文件夹...";
            tsmi_add_dir.ToolTipText = "忽视子文件夹中文件";
            tsmi_add_dir.Click += new EventHandler(tsmi_add_dir_Click);
            // 
            // tsmi_listInfo
            // 
            tsmi_listInfo.Text = "信息(&I)";
            tsmi_listInfo.Click += new EventHandler(tsmi_listInfo_Click);
            // 
            // tsmi_listMove
            // 
            tsmi_listMove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                tsmi_listMove_up,
                tsmi_listMove_down});
            tsmi_listMove.Text = "移动";
            // 
            // tsmi_listMove_up
            // 
            tsmi_listMove_up.Text = "上移";
            tsmi_listMove_up.Click += new EventHandler(tsmi_listMove_items_Click);
            // 
            // tsmi_listMove_down
            // 
            tsmi_listMove_down.Text = "下移";
            tsmi_listMove_down.Click += new EventHandler(tsmi_listMove_items_Click);
            // 
            // tsmi_listRemove
            // 
            tsmi_listRemove.Text = "删除(&D)";
            tsmi_listRemove.Click += new EventHandler(tsmi_listRemove_Click);
            // 
            // tsmi_listRename
            // 
            tsmi_listRename.Text = "重命名(&R)";
            tsmi_listRename.Click += new EventHandler(tsmi_listRename_Click);
            // 
            // tsmi_listClean
            // 
            tsmi_listClean.Text = "清空(&C)";
            tsmi_listClean.Click += new EventHandler(tsmi_listClean_Click);
            // 
            // tsmi_listOpen
            // 
            tsmi_listOpen.Text = "展开";
            tsmi_listOpen.Click += new EventHandler(tsmi_listOpen_Click);
            // 
            // tsmi_listShow
            // 
            tsmi_listShow.Text = "查看（&V）";
            tsmi_listShow.Click += new EventHandler(tsmi_listShow_Click);
            // 
            // tsmi_newList
            // 
            tsmi_newList.Text = "新建列表";
            tsmi_newList.Click += new EventHandler(tsmi_newList_Click);
            // 
            // tsmi_listBackup
            // 
            tsmi_listBackup.BackColor = System.Drawing.SystemColors.Control;
            tsmi_listBackup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                tsmi_listUp,
                tsmi_listBack});
            tsmi_listBackup.Text = "备份与恢复";
            // 
            // tsmi_listUp
            // 
            tsmi_listUp.Text = "备份（&C）";
            tsmi_listUp.Click += new EventHandler(tsmi_backUp_items_Click);
            // 
            // tsmi_listBack
            // 
            tsmi_listBack.Text = "恢复（&B）";
            tsmi_listBack.Click += new EventHandler(tsmi_backUp_items_Click);
            // 
            // tsmi_reloadLibs
            // 
            tsmi_reloadLibs.Text = "重载列表";
            tsmi_reloadLibs.Click += new EventHandler(tsmi_reloadLibs_Click);
        }

        #region cms_list（歌曲文件夹快捷菜单)
        private void tsmi_listClean_Click(object sender, EventArgs e)
        {
            string msg = string.Format("是否要清空此列表（库）[ {0} ]?\r\n此列表下共包含 {1} 条记录"
                , _mc.MyLists.GetLibTitle(_mc.ListViewManager.MyLibView.SelectedLib)
                , _mc.MyLists.GetMediaCount(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList));
            if (NgNet.UI.Forms.MessageBox.Show(_mc.ListViewManager.MyLibView.LibTreeView.FindForm(), msg, "清空列表", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(_mc.ListViewManager.MyLibView.SelectedList) && _mc.ListViewManager.MyLibView.SelectedLib == FreshMedia.List.MyLib.Local)
                    _mc.MyLists.CleanLib(_mc.ListViewManager.MyLibView.SelectedLib);
                else
                    _mc.MyLists.CleanList(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList);
            }
        }

        private void tsmi_listRemove_Click(object sender, EventArgs e)
        {
            string msg = string.Format("是否要删除本列表< {0} >?", _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode.Name);
            Form owner = _mc.ListViewManager.MyLibView.LibTreeView.FindForm();
            DialogResult dr = NgNet.UI.Forms.MessageBox.Show(owner, msg, "删除列表", MessageBoxButtons.YesNo, DialogResult.No);
            if (dr == DialogResult.Yes)
            {
                _mc.MyLists.RemoveList(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList);
                //判断移除的列表是不是正在显示
                if (_mc.ListViewManager.MyListView.ShowedLib == _mc.ListViewManager.MyLibView.SelectedLib && _mc.ListViewManager.MyListView.ShowedList == _mc.ListViewManager.MyLibView.SelectedList)
                {
                    //加载默认列表
                    _mc.ListViewManager.MyListView.LoadAudioList(List.MyLib.Local, List.ListManager.NAME_LIST_DEFAULT, true);
                }
            }

        }

        private void addAudioDialog(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            int listindex = _mc.MyLists.GetListIndex(List.MyLib.Local, _mc.ListViewManager.MyLibView.SelectedList);
            IEnumerable<string> scrPaths = NgNet.UI.Forms.OpenFileDialog.Show2(_mc.MainForm,
                tsmi.Text,
                Player.Types.FILTER,
                0,
                string.Format("添加音乐文件到 < {0} >", _mc.ListViewManager.MyLibView.SelectedList)
                );
            if (scrPaths == null)
                return;
            int oldCount = _mc.MyLists.GetMediaCount(List.MyLib.Local, _mc.ListViewManager.MyLibView.SelectedList);
            int added = _mc.MyLists.AddMedias(List.MyLib.Local, _mc.ListViewManager.MyLibView.SelectedList, scrPaths);
            //刷新最近添加列表
            _mc.MyLists.AddMedias(List.MyLib.RecentlyAdded, List.ListManager.NAME_LIST_RECENTLYADDED, scrPaths);
            //显示并刷新该列表
            _mc.ListViewManager.MyListView.LoadAudioList(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList, true);
            //将添加的音乐选中
            _mc.ListViewManager.MyListView.SetSelectedMedias(oldCount, added);
            string msg = string.Format("已添加音乐：{0}首，\r\n忽略已存在音乐：{1}首！", added, scrPaths.ToArray().Length - added);
            NgNet.UI.Forms.MessageBox.Show(_mc.ListViewManager.MyLibView.LibTreeView.FindForm(), msg, "提示您", MessageBoxButtons.OK, DialogResult.OK, 6);
        }

        public void tsmi_add_file_Click(object sender, EventArgs e)
        {
            //获取最近浏览的音乐文件夹列表
            ContextMenuStrip cms = new ContextMenuStrip();
            //cms.Opacity = _controller._mainForm.Opacity;
            cms.Renderer = _mc.Theme.MenuRender;
            cms.ShowImageMargin = false;

            IEnumerable<string> tmp = _mc.OpenHistoryManager.FolderOpenHistory;
            //无打开历史时载入Windows默认文件夹
            if (tmp.Count<string>() == 0)
                tmp = new string[] { _mc.OpenHistoryManager.LastOpenFolder = NgNet.Windows.SpecialFolders.MyMusic };

            ToolStripMenuItem tsmi;
            tsmi = new ToolStripMenuItem();
            tsmi.Text = "☆☆☆☆☆☆☆☆☆☆☆☆";
            tsmi.Enabled = false;
            cms.Items.Add(tsmi);

            foreach (string item in tmp)
            {
                tsmi = new ToolStripMenuItem();
                tsmi.Text = item;
                tsmi.TextAlign = ContentAlignment.MiddleCenter;
                tsmi.Click += new EventHandler(addAudioDialog);
                cms.Items.Add(tsmi);
            }

            tsmi = new ToolStripMenuItem();
            tsmi.Text = "☆☆☆☆☆☆☆☆☆☆☆☆";
            tsmi.Enabled = false;
            cms.Items.Add(tsmi);
            cms.Font = new Font("幼圆", 30, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            ContextMenuStripHelper.CenterToWindow(cms, _mc.MainForm);
        }

        public void tsmi_add_dir_Click(object sender, EventArgs e)
        {
            Form owner = _mc.ListViewManager.MyLibView.LibTreeView.FindForm();
            string dirPath = NgNet.UI.Forms.DirSelectBox.Show(owner,
                _mc.OpenHistoryManager.LastOpenFolder,
                string.Format("从文件夹导入音乐到 < {0} >", _mc.ListViewManager.MyLibView.SelectedList));

            int listIndex = _mc.MyLists.GetListIndex(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList);
            if (Directory.Exists(dirPath) == false)
                return;

            DirectoryInfo di = new DirectoryInfo(dirPath);
            int newCount = 0, existedCount = 0;
            int oldCount = _mc.MyLists.GetMediaCount(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList);
            List<string> scrPaths = new List<string>();
            foreach (FileInfo fi in di.GetFiles())
            {
                if (Player.Types.SupportedTypes.Contains(fi.Extension.ToLower()))
                {
                    if (_mc.MyLists.Local[listIndex].Contains(fi.FullName)) { existedCount++; }
                    else
                    {
                        newCount++;
                        scrPaths.Add(fi.FullName);
                    }
                }
            }
            _mc.MyLists.AddMedias(List.MyLib.Local, _mc.ListViewManager.MyLibView.SelectedList, scrPaths);
            _mc.MyLists.AddMedias(List.MyLib.RecentlyAdded, List.ListManager.NAME_LIST_RECENTLYADDED, scrPaths);
            _mc.ListViewManager.MyListView.LoadAudioList(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList, true);//重新加载添加的列表
                                                                                                                           //将添加的音乐选中
            _mc.ListViewManager.MyListView.SetSelectedMedias(oldCount, newCount);
            string msg = string.Format("已添加歌曲 {0} 首\r\n忽略已存在歌曲 {1} 首", newCount, existedCount);
            NgNet.UI.Forms.MessageBox.Show(_mc.ListViewManager.MyLibView.LibTreeView.FindForm(), msg, "提示您", MessageBoxButtons.OK, DialogResult.OK, 6);
        }

        private void tsmi_listInfo_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            string msg_2 = "列表名：[{0} - {1}]\r\n歌曲数：{2}";
            string msg_3 = "乐库名：[{0}]\r\n歌曲数（最多*{1}*首）：*{2}*";
            string msg_4 = "乐库名：[{0}]\r\n列表数（最多*{1}*个）：*{2}*";
            switch (_mc.ListViewManager.MyLibView.SelectedLib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    msg = string.Format(
                        msg_3,
                        List.ListManager.TITLE_LIB_CURRENT,
                        "100+",
                        _mc.MyLists.GetMediaCount(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT));
                    break;
                case List.MyLib.Local:
                    switch (_mc.ListViewManager.MyLibView.SelectedList)
                    {
                        case "":
                            int libIndex = _mc.ListViewManager.MyLibView.tn_libLocal.Index;
                            msg = string.Format(
                                msg_4,
                                List.ListManager.TITLE_LIB_LOCAL,
                                List.ListManager.MAX_COUNT_LOCAL,
                                _mc.MyLists.GetListCount(List.MyLib.Local));
                            foreach (TreeNode tn in _mc.ListViewManager.MyLibView.tn_libLocal.Nodes)
                            {
                                msg += string.Format("\r\n    {0}", tn.Text);
                            }
                            break;
                        default:
                            msg = string.Format(
                                msg_2,
                                _mc.ListViewManager.MyLibView.SelectedList,
                                List.ListManager.TITLE_LIB_LOCAL,
                                _mc.MyLists.GetMediaCount(List.MyLib.Local, _mc.ListViewManager.MyLibView.SelectedList));
                            break;
                    }
                    break;
                case List.MyLib.History:
                    msg = string.Format(
                        msg_3,
                        List.ListManager.TITLE_LIB_HISTORY,
                        List.ListManager.MAX_COUNT_HISTORY,
                        _mc.MyLists.GetMediaCount(List.MyLib.History, List.ListManager.NAME_LIST_HISTORY));
                    break;
                case List.MyLib.Favorite:
                    msg = string.Format(
                        msg_3,
                        List.ListManager.TITLE_LIB_FAVO,
                        "100+",
                        _mc.MyLists.GetMediaCount(List.MyLib.Favorite, List.ListManager.NAME_LIST_FAVO));
                    break;
                case List.MyLib.RecentlyAdded:
                    msg = string.Format(
                        msg_3,
                        List.ListManager.TITLE_LIB_RECENTLYADDED,
                        List.ListManager.MAX_COUNT_RENCENTLYADDED,
                        _mc.MyLists.GetMediaCount(List.MyLib.RecentlyAdded, List.ListManager.NAME_LIST_RECENTLYADDED));
                    break;
                case List.MyLib.MostlyPlayed:
                    msg = string.Format(
                        msg_3,
                        List.ListManager.TITLE_LIB_MOSTPLAYED,
                        "100+",
                        _mc.MyLists.GetMediaCount(List.MyLib.MostlyPlayed, List.ListManager.NAME_LIST_MOSTLYPLAYED));
                    break;
                default:
                    break;
            }
            NgNet.UI.Forms.MessageBox.Show(
                _mc.ListViewManager.MyLibView.LibTreeView.FindForm(),
                msg,
                "列表信息",
                MessageBoxButtons.OK);
        }

        private void tsmi_listRename_Click(object sender, EventArgs e)
        {
            List.ILibList iLibList = null;
            int listIndex = _mc.MyLists.GetListIndex(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList);
            if (listIndex == -1)
                throw new List.ListNotFoundException(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList);

            switch (_mc.ListViewManager.MyLibView.SelectedLib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    iLibList = _mc.MyLists.Playing[listIndex];
                    break;
                case List.MyLib.Local:
                    iLibList = _mc.MyLists.Local[listIndex];
                    break;
                case List.MyLib.History:
                    iLibList = _mc.MyLists.History[listIndex];
                    break;
                case List.MyLib.Favorite:
                    iLibList = _mc.MyLists.Favo[listIndex];
                    break;
                case List.MyLib.RecentlyAdded:
                    iLibList = _mc.MyLists.RecentlyAdded[listIndex];
                    break;
                case List.MyLib.MostlyPlayed:
                    iLibList = _mc.MyLists.Times;
                    break;
                default:
                    break;
            }
            string oldName = iLibList.Name;
            Re:
            string newName = NgNet.UI.Forms.InputBox.Show(
                _mc.ListViewManager.MyLibView.LibTreeView.FindForm(),
                string.Format("请输入新名称(不超过{0}字符)：", List.ListManager.MAX_LENGTH_LISTNAME),
                oldName,
                string.Format("重命名 < {0} >", oldName));
            if (string.IsNullOrWhiteSpace(newName))
                return;
            //输入内容为不为空
            newName = newName.Trim();
            if (string.Compare(oldName, newName, false) == 0)
                return;//名字相同
            if (List.ListManager.NameTest(newName) == false)
                goto Re; //输入的名称是否合法

            if (_mc.ListViewManager.MyLibView.listSelectedTreeNode.Parent.Nodes.ContainsKey(newName))
            {
                string msg = string.Format("已存在该列表名 < {0} >，请更改列表名", newName);
                NgNet.UI.Forms.MessageBox.Show(_mc.ListViewManager.MyLibView.LibTreeView.FindForm(), msg, null, MessageBoxButtons.OK, DialogResult.OK, 6);
                goto Re;
            }
            _mc.ListViewManager.MyLibView.listSelectedTreeNode.Name = newName;
            _mc.ListViewManager.MyLibView.listSelectedTreeNode.Text = newName;
            _mc.MyLists.RenameList(_mc.ListViewManager.MyLibView.SelectedLib, oldName, newName);
        }

        private void tsmi_newList_Click(object sender, EventArgs e)
        {
            int libIndex = _mc.ListViewManager.MyLibView.LibTreeView.Nodes.IndexOfKey(_mc.MyLists.GetLibName(_mc.ListViewManager.MyLibView.SelectedLib));
            //获取列表数并判断是否达到上线
            Re: int listCount = _mc.MyLists.GetListCount(List.MyLib.Local);
            if (listCount >= List.ListManager.MAX_COUNT_LOCAL)
            {
                string msg = string.Format("列表数已达到上限，请删除不需要的列表后重试!(最大列表数：{0})，包过系统列表。", List.ListManager.MAX_COUNT_LOCAL);
                NgNet.UI.Forms.MessageBox.Show(_mc.ListViewManager.MyLibView.LibTreeView.FindForm(), msg, "新建列表", MessageBoxButtons.OK, DialogResult.OK, 6);
                return;
            }
            //获取列表数并判断是否达到上线

            string listName = NgNet.UI.Forms.InputBox.Show(
                _mc.ListViewManager.MyLibView.LibTreeView.FindForm(),
                string.Format("请输入列表名称(不超过{0}字符，空格无效)：", List.ListManager.MAX_LENGTH_LISTNAME),
                "MyFavorite",
                "添加列表");
            if (string.IsNullOrWhiteSpace(listName))
                return;
            else
                listName = listName.Trim();
            //判断名称是否合法
            if (List.ListManager.NameTest(listName) == false)
                goto Re;
            if (_mc.ListViewManager.MyLibView.tn_libLocal.Nodes.ContainsKey(listName))
            {
                string msg = string.Format("已存在该列表名 < {0} >，请更改列表名!", listName);
                NgNet.UI.Forms.MessageBox.Show(_mc.ListViewManager.MyLibView.LibTreeView.FindForm(), msg, null, MessageBoxButtons.OK, DialogResult.OK, 6);
                goto Re;
            }
            List.AudioList al = new List.AudioList();
            al.Name = listName;
            al.Title = listName;
            _mc.MyLists.AddList(_mc.ListViewManager.MyLibView.SelectedLib, al, _mc.ListViewManager.MyLibView.listSelectedTreeNode.Index + 1);
        }

        private void tsmi_listShow_Click(object sender, EventArgs e)
        {
            if (_mc.ListViewManager.MyLibView.tn_libLocal == _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode)
                _mc.ListViewManager.MyLibView.tn_libLocal.Expand();
            else
            {
                _mc.ListViewManager.MyListView.LoadAudioList(_mc.ListViewManager.MyLibView.SelectedLib, _mc.ListViewManager.MyLibView.SelectedList, true);
            }
        }

        private void tsmi_listOpen_Click(object sender, EventArgs e)
        {
            _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode.Toggle();
        }

        private void tsmi_listMove_items_Click(object sender, EventArgs e)
        {
            TreeNode tn = _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode.PrevNode;
            TreeNode tn1 = (TreeNode)_mc.ListViewManager.MyLibView.LibTreeView.SelectedNode.Clone();
            List.AudioList lst = _mc.MyLists.Local[_mc.MyLists.GetListIndex(List.MyLib.Local, tn.Name)];
            List.AudioList lst0 = _mc.MyLists.Local[_mc.MyLists.GetListIndex(List.MyLib.Local, tn1.Name)];
            List.AudioList lst1 = new List.AudioList(lst0);

            _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode.Remove();
            _mc.MyLists.Local.Remove(lst0);

            if (sender == tsmi_listMove_up)
            {
                tn.Parent.Nodes.Insert(tn.Index, tn1);
                _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode = tn1;
                _mc.MyLists.Local.Insert(tn.Index, lst1);
            }
            else if (sender == tsmi_listMove_down)
            {
                tn.Parent.Nodes.Insert(tn.Index + 2, tn1);
                _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode = tn1;
                _mc.MyLists.Local.Insert(tn.Index + 2, lst1);
            }
        }

        private void tsmi_backUp_items_Click(object sender, EventArgs e)
        {
            if (sender == tsmi_listUp)
            {
                _mc.BackupManager.Backup();
            }
            else if (sender == tsmi_listBack)
            {

                _mc.BackupManager.Restore();
            }
        }

        private void tsmi_reloadLibs_Click(object sender, EventArgs e)
        {
            _mc.ListViewManager.MyLibView.ReLoadLibs();
        }

        private void cms_Opening(object sender, CancelEventArgs e)
        {
            cms.Items.Clear();
            switch (_mc.ListViewManager.MyLibView.SelectedLib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    cms.Items.AddRange(new ToolStripMenuItem[]{
                        tsmi_listShow,
                        tsmi_listClean,
                        tsmi_listInfo});
                    break;
                case List.MyLib.Local:
                    switch (_mc.ListViewManager.MyLibView.SelectedList)
                    {
                        case List.ListManager.NAME_LIST_DEFAULT://默认列表
                            cms.Items.AddRange(new ToolStripMenuItem[]{
                                tsmi_listShow,
                                tsmi_add,
                                tsmi_newList,
                                tsmi_listClean,
                                tsmi_listInfo});
                            break;
                        case ""://选择的是库不是列表
                            cms.Items.AddRange(new ToolStripMenuItem[]{
                                tsmi_listOpen,
                                tsmi_listShow,
                                tsmi_listClean,
                                tsmi_listInfo});
                            tsmi_listOpen.Text = _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode.IsExpanded ? "折叠" : "展开";
                            break;
                        default:
                            cms.Items.AddRange(new ToolStripMenuItem[]{
                                tsmi_listShow,
                                tsmi_add,
                                tsmi_newList,
                                tsmi_listMove,
                                tsmi_listClean,
                                tsmi_listInfo,
                                tsmi_listRemove,
                                tsmi_listRename});
                            tsmi_listMove_up.Enabled = _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode.Index > 1;
                            tsmi_listMove_down.Enabled = _mc.ListViewManager.MyLibView.LibTreeView.SelectedNode.Index < _mc.ListViewManager.MyLibView.tn_libLocal.GetNodeCount(false) - 1;
                            break;
                    }
                    break;
                case List.MyLib.History:
                    cms.Items.AddRange(new ToolStripMenuItem[]{
                        tsmi_listShow,
                        tsmi_listClean,
                        tsmi_listInfo});
                    break;
                case List.MyLib.Favorite:
                    cms.Items.AddRange(new ToolStripMenuItem[]{
                        tsmi_listShow,
                        tsmi_listClean,
                        tsmi_listInfo});
                    break;
                case List.MyLib.RecentlyAdded:
                    cms.Items.AddRange(new ToolStripMenuItem[]{
                        tsmi_listShow,
                        tsmi_listClean,
                        tsmi_listInfo});
                    break;
                case List.MyLib.MostlyPlayed:
                    cms.Items.AddRange(new ToolStripMenuItem[]{
                        tsmi_listShow,
                        tsmi_listClean,
                        tsmi_listInfo});
                    break;
                default:
                    break;
            }
            cms.Items.Add(tsmi_reloadLibs);
            cms.Items.Add(tsmi_listBackup);
            tsmi_listShow.Text
                = (_mc.ListViewManager.MyLibView.SelectedLib == _mc.ListViewManager.MyListView.ShowedLib && _mc.ListViewManager.MyLibView.SelectedList == _mc.ListViewManager.MyListView.ShowedList)
                ? "刷新" : "查看";
        }
        #endregion
        #endregion
    }
}
