using NgNet.UI;
using NgNet.UI.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FreshMedia.View.VList
{
    class LibView 
    {
        #region private fields
        private IFreshMedia iFreshMedia;

        private Controller.MainController _mc;

        private LibMenu libMenu;
        #endregion

        #region public properties
        /// <summary>
        /// 当前选中的库
        /// </summary>
        public List.MyLib SelectedLib { get; private set; } 

        /// <summary>
        /// 当前选中的列表
        /// </summary>
        public string SelectedList { get; private set; }

        /// <summary>
        /// 当前正在显示的列表
        /// </summary>
        public TreeNode listShowingTreeNode { get; private set; }

        /// <summary>
        /// 当前选中的列表
        /// </summary>
        public TreeNode listSelectedTreeNode { get; private set; }

        /// <summary>
        /// libTreeView
        /// </summary>
        public TreeViewEx LibTreeView { get; }

        /*库
         */
        /// <summary>
        /// 正在播放
        /// </summary>
        public TreeNode tn_libCurrent { get; }

        /// <summary>
        /// 本地库
        /// </summary>
        public TreeNode tn_libLocal { get; }

        /// <summary>
        /// 历史播放
        /// </summary>
        public TreeNode tn_libHistory { get; }

        /// <summary>
        /// 我的收藏
        /// </summary>
        public TreeNode tn_libFavo { get; }

        /// <summary>
        /// 最近添加
        /// </summary>
        public TreeNode tn_libRecentlyAdded { get; }

        /// <summary>
        /// 最多播放
        /// </summary>
        public TreeNode tn_libMostlyPlayed { get; }
        #endregion

        #region constructor
        public LibView(IFreshMedia iFreshMedia)
        {
            this.iFreshMedia = iFreshMedia;
            _mc = iFreshMedia.Controller;
     
            SelectedLib = List.MyLib.None;
            SelectedList = null;

            LibTreeView = new TreeViewEx();
            tn_libCurrent = new TreeNode();
            tn_libFavo = new TreeNode(); 
            tn_libHistory = new TreeNode(); 
            tn_libLocal = new TreeNode(); 
            tn_libMostlyPlayed = new TreeNode();
            tn_libRecentlyAdded = new TreeNode();
        }

        public void Init()
        {
            libMenu = new LibMenu(iFreshMedia);

            //事件绑定
            _mc.MyLists.LibResetedEvent += new List.LibResetedEventHandler(libResetEvent);
            _mc.MyLists.ListRemovedEvent += new List.ListRemovedEventHandler(listRemovedEvent);
            _mc.MyLists.ListNameChangedEvent += new List.ListNameChangedEventHandler(listNameChangedEvent);
            _mc.MyLists.ListAddedEvent += new List.ListAddedEventHandler(listAddedEvent); 
            _mc.ListViewManager.MyListView.ShowedListChangedEvent += new ShowedListChangedEventHandler(showedListChangedEvent);

            init();
            _mc.Theme.ThemeChanged += new ThemeChangedEventHandler(themeChangedEvent);
        }
        #endregion

        #region events
        #region libTreeView

        private void libTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 选中当前项
            LibTreeView.SelectedNode = e.Node;
            listSelectedTreeNode = e.Node;
            e.Node.ContextMenuStrip = libMenu.cms;
            //
            if (e.Node.Level == 0)
                SelectedLib = (List.MyLib)e.Node.Tag;
            else if (e.Node.Level == 1)
                SelectedLib = (List.MyLib)e.Node.Parent.Tag;

            if (e.Node == this.tn_libCurrent)
                SelectedList = List.ListManager.NAME_LIST_CURRENT;
            else if (e.Node == this.tn_libLocal)
                SelectedList = string.Empty;
            else if (e.Node == this.tn_libHistory)
                SelectedList = List.ListManager.NAME_LIST_HISTORY;
            else if (e.Node == this.tn_libFavo)
                SelectedList = List.ListManager.NAME_LIST_FAVO;
            else if (e.Node == this.tn_libRecentlyAdded)
                SelectedList = List.ListManager.NAME_LIST_RECENTLYADDED;
            else if (e.Node == this.tn_libMostlyPlayed)
                SelectedList = List.ListManager.NAME_LIST_MOSTLYPLAYED;
            else
                SelectedList = e.Node.Name;
            if (e.Button == MouseButtons.Left && e.Node.Level == 1)
                localListClickTest(SelectedLib, SelectedList);
            //判断当前项是否有子项
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node != tn_libLocal)//切换显示列表时
                {
                    //不显示桌面歌词显示列表
                    _mc.LyricManager.FLyric.Visible = false;
                    //加载选定列表             
                    if (_mc.ListViewManager.MySearchBox.IsLoaded)
                        _mc.ListViewManager.MySearchBox.So(SelectedLib, SelectedList, _mc.ListViewManager.MySearchBox.KeyWord);
                    else
                        _mc.ListViewManager.MyListView.LoadAudioList(SelectedLib, SelectedList, false);
                }
            }
        }

        private void libTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.Text = _mc.MyLists.GetLibTitle((List.MyLib)e.Node.Tag);
        }

        private void libTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
                e.Node.Text = string.Format("{0} ...", _mc.MyLists.GetLibTitle((List.MyLib)e.Node.Tag));
        }

        private void themeChangedEvent(ThemeChangedEventArgs e)
        {
            //处理库和列表
            LibTreeView.BackColor = e.ThemeClass.BackColor;
            LibTreeView.NodeSelectedBackColor = (e.ThemeClass as ThemeManager).ButonDownColor;
            LibTreeView.NodeSelectedForeColor = NgNet.Drawing.ColorHelper.GetOppositeColor((e.ThemeClass as ThemeManager).ButonDownColor);
            foreach (TreeNode tn0 in LibTreeView.Nodes)
            {
                tn0.BackColor = e.ThemeClass.BackColor;
                tn0.ForeColor = e.ThemeClass.ForeColor;
                foreach (TreeNode tn1 in tn0.Nodes)
                {
                    tn1.BackColor = e.ThemeClass.BackColor;
                    tn1.ForeColor = e.ThemeClass.ForeColor;
                }
            }
            listShowingTreeNode.BackColor = (e.ThemeClass as ThemeManager).ButtonEnterColor;
            libMenu.cms.Renderer = (e.ThemeClass as ThemeManager).MenuRender;
            ContextMenuStripHelper.SetOpacity(libMenu.cms, e.ThemeClass.Opacity);
        }
        #endregion

        #region lib
        private void libResetEvent(List.LibResetedEventArgs e)
        {
            switch (e.Lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    break;
                case List.MyLib.Local:
                    tn_libLocal.Nodes.Clear();
                    foreach (List.AudioList al in _mc.MyLists.Local)
                    {
                        if (al == null)
                            continue;
                        TreeNode tn = new TreeNode();
                        tn.Text = al.Title;
                        tn.Name = al.Name;
                        tn.BackColor = _mc.Theme.BackColor;
                        tn.ForeColor = _mc.Theme.ForeColor;
                        tn_libLocal.Nodes.Add(tn);
                    }
                    tn_libLocal.Expand();
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

        private void listAddedEvent(List.ListAddedEventArgs e)
        {
            switch (e.Lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:

                    break;
                case List.MyLib.Local:
                    TreeNode tn = new TreeNode();
                    tn.Name = e.ListName;
                    tn.Text = e.ListName;
                    tn.ForeColor = _mc.Theme.ForeColor;
                    tn.BackColor = _mc.Theme.BackColor;
                    tn_libLocal.Nodes.Insert(e.ListIndex, tn);
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

        private void listNameChangedEvent(List.ListNameChangedEventArgs e)
        {
            if (_mc.ListViewManager.MyListView.ShowedLib == e.Lib && _mc.ListViewManager.MyListView.ShowedList == e.OldName)
            {
                SetShowingListNodeStyle(e.Lib, e.NewName);
            }
            if (SelectedLib == e.Lib && SelectedList == e.OldName)
            {
                SelectedLib = e.Lib;
                SelectedList = e.NewName;
            }
        }

        private void listRemovedEvent(List.ListRemovedEventArgs e)
        {
            int listindex;
            switch (e.Lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    break;
                case List.MyLib.Local:
                    listindex = tn_libLocal.Nodes.IndexOfKey(e.ListName);
                    if (listindex == -1)
                        break;
                    tn_libLocal.Nodes[listindex].Remove();
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

        private void showedListChangedEvent(ShowedListChangedEventArgs e)
        {
            #region 外观设置
            SetShowingListNodeStyle(e.Lib, e.ListName);
            #endregion
        }
        #endregion
        #endregion

        #region private methods
        // 用户界面——用户点击列表时检测当前列表
        private void localListClickTest(List.MyLib lib, string listName)
        {
            int listIndex = _mc.MyLists.GetListIndex(List.MyLib.Local, listName);
            if (listIndex == -1)
                throw new List.ListNotFoundException(lib, listName);
            if (_mc.MyLists.Local[listIndex].Count == 0)
            {
                Form owner = LibTreeView.FindForm();
                DialogResult dr = NgNet.UI.Forms.MessageBox.Show(
                    owner,
                    "列表为空，是否现在添加音乐？",
                    "",
                    MessageBoxButtons.YesNo,
                    DialogResult.Yes,
                    6);
                if (dr == DialogResult.Yes)
                {
                    ContextMenuStrip cms = new ContextMenuStrip();
                    cms.Opacity = owner == null ? 1 : owner.Opacity;
                    cms.Renderer = _mc.Theme.MenuRender;
                    cms.ShowImageMargin = false;
                    cms.Font = new Font(string.Empty, 30, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Text = "☆☆☆☆☆☆☆☆☆☆☆☆";
                    tsmi.Enabled = false;
                    cms.Items.Add(tsmi);
                    tsmi = new ToolStripMenuItem();
                    tsmi.Text = "添加本地音乐";
                    tsmi.TextAlign = ContentAlignment.MiddleCenter;
                    tsmi.Click += new EventHandler(libMenu.tsmi_add_file_Click);
                    cms.Items.Add(tsmi);
                    tsmi = new ToolStripMenuItem();
                    tsmi.Text = "添加本地音乐文件夹";
                    tsmi.TextAlign = ContentAlignment.MiddleCenter;
                    tsmi.Click += new EventHandler(libMenu.tsmi_add_dir_Click);
                    cms.Items.Add(tsmi);
                    tsmi = new ToolStripMenuItem();
                    tsmi.Text = "☆☆☆☆☆☆☆☆☆☆☆☆";
                    tsmi.Enabled = false;
                    cms.Items.Add(tsmi);
                    ContextMenuStripHelper.CenterToWindow(cms, _mc.MainForm);

                }
            }
        }

        //将列表加载到libTreeView,但不处理主题
        private void loadLibs()
        {
            foreach (TreeNode item in LibTreeView.Nodes)
            {
                item.Nodes.Clear();
            }
            LibTreeView.Nodes.Clear();
            LibTreeView.Nodes.AddRange(new TreeNode[]{
                tn_libCurrent,
                tn_libLocal,
                tn_libHistory,
                tn_libFavo ,
                tn_libRecentlyAdded,
                tn_libMostlyPlayed});

            List.MyLib lib = List.MyLib.Local;
            switch (lib)
            {
                case List.MyLib.Local:
                    foreach (List.AudioList item in _mc.MyLists.Local)
                    {
                        TreeNode tn = new TreeNode();
                        tn.Name = item.Name;
                        tn.Text = item.Name;
                        tn.ForeColor = _mc.Theme.ForeColor;
                        tn.BackColor = _mc.Theme.BackColor;
                        tn_libLocal.Nodes.Add(tn);
                    }
                    break;
                case List.MyLib.None:
                case List.MyLib.Playing:
                case List.MyLib.History:
                case List.MyLib.Favorite:
                case List.MyLib.RecentlyAdded:
                case List.MyLib.MostlyPlayed:
                default:
                    break;
            }
        }

        private void init()
        {
            listSelectedTreeNode = new TreeNode();
            listShowingTreeNode = new TreeNode();

            //初始化
            LibTreeView.BorderStyle = BorderStyle.None;
            LibTreeView.Dock = DockStyle.Fill;
            LibTreeView.Font = new System.Drawing.Font(System.Drawing.SystemFonts.DefaultFont.FontFamily, 10.5F);
            LibTreeView.FullRowSelect = true;
            LibTreeView.ItemHeight = 22;
            LibTreeView.Location = new System.Drawing.Point(1, 1);
            LibTreeView.ShowLines = false;
            LibTreeView.ShowPlusMinus = false;
            LibTreeView.ShowRootLines = false;
            LibTreeView.Size = new System.Drawing.Size(157, 344);
            LibTreeView.TabIndex = 75;
            LibTreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(libTreeView_NodeMouseClick);
            LibTreeView.AfterExpand += new TreeViewEventHandler(libTreeView_AfterExpand);
            LibTreeView.AfterCollapse += new TreeViewEventHandler(libTreeView_AfterCollapse);
            LibTreeView.ImageList = CommControls.CommImglist;   //更改treeview1Imagelist及其他控件属性
            LibTreeView.ImageIndex = 8;
            LibTreeView.SelectedImageIndex = 8;

            tn_libCurrent.Name = List.ListManager.NAME_LIB_CURRENT;
            tn_libCurrent.Text = List.ListManager.TITLE_LIB_CURRENT;
            tn_libCurrent.Tag = List.MyLib.Playing;
            tn_libFavo.Name = List.ListManager.NAME_LIB_FAVO;
            tn_libFavo.Text = List.ListManager.TITLE_LIB_FAVO;
            tn_libFavo.Tag = List.MyLib.Favorite;
            tn_libHistory.Name = List.ListManager.NAME_LIB_HISTORY;
            tn_libHistory.Text = List.ListManager.TITLE_LIB_HISTORY;
            tn_libHistory.Tag = List.MyLib.History;
            tn_libLocal.Name = List.ListManager.NAME_LIB_LOCAL;
            tn_libLocal.Text = List.ListManager.TITLE_LIB_LOCAL;
            tn_libLocal.Tag = List.MyLib.Local;
            tn_libMostlyPlayed.Name = List.ListManager.NAME_LIB_MOSTPLAYED;
            tn_libMostlyPlayed.Text = List.ListManager.TITLE_LIB_MOSTPLAYED;
            tn_libMostlyPlayed.Tag = List.MyLib.MostlyPlayed;
            tn_libRecentlyAdded.Name = List.ListManager.NAME_LIB_RECENTLYADDED;
            tn_libRecentlyAdded.Text = List.ListManager.TITLE_LIB_RECENTLYADDED;
            tn_libRecentlyAdded.Tag = List.MyLib.RecentlyAdded;

            loadLibs();
        }
        #endregion

        #region public methods
        // 设置正在显示的列表的外观
        public void SetShowingListNodeStyle(List.MyLib lib, string listName)
        {
            int listIndex;
            listShowingTreeNode.BackColor = _mc.Theme.BackColor;
            if (listShowingTreeNode.Level == 0)
                listShowingTreeNode.Text = _mc.MyLists.GetLibTitle((List.MyLib)(listShowingTreeNode.Tag == null ? lib : listShowingTreeNode.Tag));
            else
                listShowingTreeNode.Text = listShowingTreeNode.Name;

            switch (lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    listShowingTreeNode = tn_libCurrent;
                    break;
                case List.MyLib.Local:
                    listIndex = tn_libLocal.Nodes.IndexOfKey(listName);
                    if (listIndex == -1)
                        return;
                    listShowingTreeNode = tn_libLocal.Nodes[listIndex];
                    break;
                case List.MyLib.History:
                    listShowingTreeNode = tn_libHistory;
                    break;
                case List.MyLib.Favorite:
                    listShowingTreeNode = tn_libFavo;
                    break;
                case List.MyLib.RecentlyAdded:
                    listShowingTreeNode = tn_libRecentlyAdded;
                    break;
                case List.MyLib.MostlyPlayed:
                    listShowingTreeNode = tn_libMostlyPlayed;
                    break;
                default:
                    break;
            }

            listShowingTreeNode.BackColor = _mc.Theme.ButtonEnterColor;
            if (listShowingTreeNode.Level == 0)
                listShowingTreeNode.Text = string.Format("☆{0}☆", _mc.MyLists.GetLibTitle((List.MyLib)listShowingTreeNode.Tag));
            else
                listShowingTreeNode.Text = string.Format("☆{0}☆", listShowingTreeNode.Name);
        }

        /// <summary>
        /// 重新加载库
        /// </summary>
        public void ReLoadLibs()
        {
            loadLibs();
            libTreeView_NodeMouseClick(LibTreeView, new TreeNodeMouseClickEventArgs(tn_libLocal.Nodes[0], MouseButtons.Left, 1, 0, 0));
        }
        #endregion
    }
}
