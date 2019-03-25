using NgNet.UI;
using NgNet.UI.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FreshMedia.View.VList
{
    class ListView
    {
        #region private fileds
        private Controller.MainController _mc;

        private IFreshMedia iFreshMedia;

        private ListMenu listMenu;

        private StringBuilder hotinfSb = new StringBuilder();

        private ContextMenuStrip hotinfCms = new ContextMenuStrip();
        #endregion

        #region public properties
        public TreeViewEx PlayingListTreeView { get; }

        public DataGridView ListDataGridView { get; }

        public List.MyLib ShowedLib { get; set; }//当前正在查看的库

        public string ShowedList { get; set; }//当前正在查看的列表

        public string SelectedPath { get; private set; }

        public event BeginLoadingListEventHandler BeginLoadingListEvent;

        public event StopLoadingListEventHandler StopLoadingListEvent;

        /// <summary>
        /// 当前显示的列表发生改变事件
        /// </summary>
        public event ShowedListChangedEventHandler ShowedListChangedEvent;
        #endregion

        #region constructor
        public ListView(IFreshMedia iFreshMedia)
        {
            this.iFreshMedia = iFreshMedia;
            _mc = iFreshMedia.Controller;
            ShowedLib = FreshMedia.List.MyLib.None;
            ShowedList = null;
            SelectedPath = null;

            ShowedListChangedEvent = new ShowedListChangedEventHandler((ShowedListChangedEventArgs e) => { });
            BeginLoadingListEvent = new BeginLoadingListEventHandler((ListLoadingEventArgs e) => { });
            StopLoadingListEvent = new StopLoadingListEventHandler(() => { });
            PlayingListTreeView = new TreeViewEx();
            ListDataGridView = new DataGridView();
   
            init();
        }

        public void Init()
        {
            listMenu = new ListMenu(iFreshMedia);
            _mc.MyLists.LibResetedEvent += new List.LibResetedEventHandler(LibResetedEvent);
            _mc.MyLists.ListCleanedEvent += new List.ListCleanedEventHandler(listCleanedEvent);
            _mc.MyLists.ListItemsChangedEvent += new List.ListItemsChangedEventHandler(listItemsChangedEvent);
            _mc.MyLists.ListNameChangedEvent += new List.ListNameChangedEventHandler(ListNameChangedEvent);
            _mc.MyLists.ListRemovedEvent += new List.ListRemovedEventHandler(listRemovedEvent);
            _mc.Theme.ThemeChanged += new ThemeChangedEventHandler(themeChangedEvent);
            _mc.PlayController.myPlayer.PlayStateChangedEvent += new Player.PlayStateChangedEventHandler(playStateChangedEvent);
            _mc.PlayController.myPlayer.URLChangedEvent += new Player.URLChangedEventHandler(uRLChangedEvent);
        }
        #endregion

        #region private methods
        private void init()
        {        
            #region playingListTreeView
            PlayingListTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            PlayingListTreeView.Font = new System.Drawing.Font(string.Empty, 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            PlayingListTreeView.ForeColor = System.Drawing.Color.DarkGreen;
            PlayingListTreeView.FullRowSelect = true;
            PlayingListTreeView.Dock = DockStyle.Fill;
            PlayingListTreeView.ImageList = CommControls.CommImglist;
            PlayingListTreeView.Indent = 16;
            PlayingListTreeView.ItemHeight = 16;
            PlayingListTreeView.ShowLines = false;
            PlayingListTreeView.ShowPlusMinus = false;
            PlayingListTreeView.ShowRootLines = false;
            PlayingListTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(playingListTreeNode_NodeMouseDoubleClick);
            PlayingListTreeView.MouseLeave += new System.EventHandler(playingListTreeNode_MouseLeave);
            PlayingListTreeView.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(playingListTreeNode_NodeMouseHover);
            PlayingListTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(tv_playing_AfterSelect);
            PlayingListTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tv_playing_NodeMouseClick);

            #endregion

            #region listDataGridView
            DataGridViewCellStyle alternatingRowsCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle columnHeadersCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle rowsCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewTextBoxColumn nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn artistColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn titleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn albumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn yearColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lengthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            alternatingRowsCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            alternatingRowsCellStyle.BackColor = System.Drawing.Color.LightCyan;
            alternatingRowsCellStyle.Font = new System.Drawing.Font(string.Empty, 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            alternatingRowsCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
            alternatingRowsCellStyle.NullValue = string.Empty;
            alternatingRowsCellStyle.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            alternatingRowsCellStyle.SelectionForeColor = System.Drawing.Color.Crimson;
            columnHeadersCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            columnHeadersCellStyle.BackColor = System.Drawing.Color.Azure;
            columnHeadersCellStyle.NullValue = string.Empty;
            columnHeadersCellStyle.Font = new System.Drawing.Font(string.Empty, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            columnHeadersCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            columnHeadersCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            columnHeadersCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            rowsCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowsCellStyle.BackColor = System.Drawing.Color.Azure;
            rowsCellStyle.Font = new System.Drawing.Font(string.Empty, 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            rowsCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
            rowsCellStyle.NullValue = string.Empty;
            rowsCellStyle.SelectionBackColor = System.Drawing.Color.PaleTurquoise;
            rowsCellStyle.SelectionForeColor = System.Drawing.Color.MediumVioletRed;

            ListDataGridView.AllowUserToAddRows = false;
            ListDataGridView.AllowUserToDeleteRows = false;
            ListDataGridView.AllowUserToResizeRows = false;
            ListDataGridView.AlternatingRowsDefaultCellStyle = alternatingRowsCellStyle;
            ListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            ListDataGridView.BackgroundColor = System.Drawing.Color.Azure;
            ListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            ListDataGridView.CausesValidation = false;
            ListDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            ListDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            ListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            ListDataGridView.ColumnHeadersDefaultCellStyle = columnHeadersCellStyle;
            ListDataGridView.ColumnHeadersHeight = 17;
            ListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
              nameColumn,
              artistColumn,
              titleColumn,
              albumColumn,
              yearColumn,
              lengthColumn});
            ListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            ListDataGridView.EnableHeadersVisualStyles = false;
            ListDataGridView.Location = new System.Drawing.Point(1, 1);
            ListDataGridView.ReadOnly = true;
            ListDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            ListDataGridView.RowHeadersVisible = false;
            ListDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            ListDataGridView.RowsDefaultCellStyle = rowsCellStyle;
            ListDataGridView.RowTemplate.DefaultCellStyle.NullValue = string.Empty;
            ListDataGridView.RowTemplate.Height = 19;
            ListDataGridView.RowTemplate.ReadOnly = true;
            ListDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            ListDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            ListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            ListDataGridView.ShowCellErrors = false;
            ListDataGridView.ShowEditingIcon = false;
            ListDataGridView.ShowRowErrors = false;
            ListDataGridView.Size = new System.Drawing.Size(716, 344);
            ListDataGridView.TabIndex = 55;
            ListDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(listDataGridView_DataError);  
            ListDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(listDataGridView_CellMouseDoubleClick);
            ListDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(listDataGridView_CellMouseDown);
            #region column

            nameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            nameColumn.FillWeight = 149.1591F;
            nameColumn.HeaderText = "文件名";
            nameColumn.Name = "nameColumn";
            nameColumn.ReadOnly = true;
            nameColumn.Width = 168;

            artistColumn.FillWeight = 93.22446F;
            artistColumn.HeaderText = "艺术家";
            artistColumn.Name = "artistColumn";
            artistColumn.ReadOnly = true;

            titleColumn.FillWeight = 93.22446F;
            titleColumn.HeaderText = "标题";
            titleColumn.Name = "titleColumn";
            titleColumn.ReadOnly = true;

            albumColumn.HeaderText = "唱片集";
            albumColumn.Name = "albumColumn";
            albumColumn.ReadOnly = true;

            yearColumn.HeaderText = "年代";
            yearColumn.Name = "yearColumn";
            yearColumn.ReadOnly = true;

            lengthColumn.FillWeight = 93.22446F;
            lengthColumn.HeaderText = "音乐时长";
            lengthColumn.Name = "lengthColumn";
            lengthColumn.ReadOnly = true;
            #endregion

            #endregion
        }

        #endregion

        #region public methods
        /// <summary>
        ///  列表加载时设置需要的行数
        /// </summary>
        /// <param name="count">指定的行数</param>
        public void SetRowsCount(int count)
        {
            ListDataGridView.ScrollBars = ScrollBars.None;
            int ct = ListDataGridView.Rows.Count;
            //判断当前行数是否超过指定行数
            if (ct < count)
                ListDataGridView.Rows.Add(count - ct);
            else
            {
                int left = ct - count;
                if (left > ct * 2)
                {
                    ListDataGridView.Rows.Clear();
                    ListDataGridView.Rows.Add(count);
                }
                else
                    for (int i = 0; i < left; i++)
                        ListDataGridView.Rows.RemoveAt(count);
            }
            ListDataGridView.ScrollBars = ScrollBars.Vertical;
        }

        /// <summary>
        /// 刷新正在播放列表正在播放项的外观
        /// </summary>
        /// <param name="itemname"></param>
        /// <param name="imageKey"></param>
        /// <param name="selectedImageKey"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        public void SetCurrentItemStyle(string itemname, int imageKey, int selectedImageKey, Color foreColor, Color backColor)
        {
            int itemIndex = PlayingListTreeView.Nodes.IndexOfKey(itemname);
            if (itemIndex == -1)
                return;
            else
            {
                PlayingListTreeView.Nodes[itemIndex].BackColor = backColor;
                PlayingListTreeView.Nodes[itemIndex].ForeColor = foreColor;
                PlayingListTreeView.Nodes[itemIndex].ImageIndex = imageKey;
                PlayingListTreeView.Nodes[itemIndex].SelectedImageIndex = selectedImageKey;
            }
        }

        /// <summary>
        /// 查看列表时加载每项信息到显示网格
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        public void SetDatagridviewRow(string path, int index)
        {
            ListDataGridView.Rows[index].Tag = path;
            if (File.Exists(path))
            {
                int itemindex = List.ListManager.DataBase[0].IndexOf(path);
                if (itemindex == -1)
                {
                    _mc.MyLists.AddDataBase(path);
                    SetDatagridviewRow(path, index);
                }
                else
                {
                    ListDataGridView.Rows[index].Cells[0].Value = List.ListManager.DataBase[6][itemindex];
                    ListDataGridView.Rows[index].Cells[1].Value = List.ListManager.DataBase[1][itemindex];
                    ListDataGridView.Rows[index].Cells[2].Value = List.ListManager.DataBase[2][itemindex];
                    ListDataGridView.Rows[index].Cells[3].Value = List.ListManager.DataBase[3][itemindex];
                    ListDataGridView.Rows[index].Cells[4].Value = List.ListManager.DataBase[4][itemindex];
                    ListDataGridView.Rows[index].Cells[5].Value = List.ListManager.DataBase[5][itemindex];
                }
            }
            else
            {
                ListDataGridView.Rows[index].DefaultCellStyle.ForeColor = _mc.Theme.ItemUnexistColor;
                ListDataGridView.Rows[index].Cells[0].Value = Path.GetFileName(path);
                ListDataGridView.Rows[index].Cells[1].Value = "艺术家";
                ListDataGridView.Rows[index].Cells[2].Value = "标题";
                ListDataGridView.Rows[index].Cells[3].Value = "唱片集";
                ListDataGridView.Rows[index].Cells[4].Value = "年代";
                ListDataGridView.Rows[index].Cells[5].Value = "时长";
            }
        }

        /// <summary>
        /// 加载指定数据库的指定列表
        /// </summary>
        /// <param name="lib">列表所在数据库</param>
        /// <param name="listName">列表名</param>
        /// <param name="force">是否强制加载（如果已经加载是否刷新）</param>
        public void LoadAudioList(List.MyLib lib, string listName, bool force)
        {
            if (lib == ShowedLib
                && listName == ShowedList
                && force == false)
                return; //不强制刷新

            BeginLoadingListEvent(new ListLoadingEventArgs(lib, listName, "正在加载..."));
            int fristDisplayIndex = 0;
            int listIndex = _mc.MyLists.GetListIndex(ShowedLib, ShowedList);

            #region 记录当前显示列表的firstshowIndex
            if (listIndex == -1)
                goto BeginLoading;
            List.ILibList iLibList = null;
            switch (ShowedLib)
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
                    if (listIndex != -1)
                        iLibList = _mc.MyLists.Favo[listIndex];
                    break;
                case List.MyLib.RecentlyAdded:
                    iLibList = _mc.MyLists.RecentlyAdded[listIndex];
                    break;
                case List.MyLib.MostlyPlayed:
                    iLibList = _mc.MyLists.Times;
                    break;
            }
            iLibList.IntTag = ListDataGridView.FirstDisplayedScrollingRowIndex;
        #endregion

        #region 加载列表
        BeginLoading:
            ListDataGridView.ScrollBars = ScrollBars.None;
            listIndex = _mc.MyLists.GetListIndex(lib, listName);
            if (listIndex == -1)
                throw new List.ListNotFoundException(lib, listName);
            List.AudioLib audioLib = new List.AudioLib();
            switch (lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    audioLib = _mc.MyLists.Playing;
                    break;
                case List.MyLib.Local:
                    audioLib = _mc.MyLists.Local;
                    break;
                case List.MyLib.History:
                    audioLib = _mc.MyLists.History;
                    break;
                case List.MyLib.Favorite:
                    audioLib = _mc.MyLists.Favo;
                    break;
                case List.MyLib.RecentlyAdded:
                    audioLib = _mc.MyLists.RecentlyAdded;
                    break;
                case List.MyLib.MostlyPlayed:
                    SetRowsCount(_mc.MyLists.Times.Count);
                    int index;
                    for (int i = _mc.MyLists.Times.Count - 1; i >= 0; i--)
                    {
                        index = _mc.MyLists.Times.Count - 1 - i;
                        SetDatagridviewRow(_mc.MyLists.Times.Values[i], index);
                        ListDataGridView.Rows[index].Cells[0].Value
                            = string.Format("[{0}]{1}"
                            , _mc.MyLists.Times.Keys[i].Substring(0, _mc.MyLists.Times.Keys[i].IndexOf("|"))
                            , ListDataGridView.Rows[index].Cells[0].Value);
                    }
                    fristDisplayIndex = _mc.MyLists.Times.IntTag;
                    goto EndLoading;
                default:
                    break;
            }
            List.AudioList al = audioLib[listIndex];
            SetRowsCount(al.Count);
            for (int i = 0; i < al.Count; i++)
            {
                SetDatagridviewRow(al[i], i);
            }
            fristDisplayIndex = al.IntTag;

        EndLoading:
            ListDataGridView.ScrollBars = ScrollBars.Vertical;
            ShowedLib = lib;
            ShowedList = listName;
            #endregion
            if (ListDataGridView.Rows.Count > fristDisplayIndex && fristDisplayIndex >= 0)
            {
                ListDataGridView.FirstDisplayedScrollingRowIndex = fristDisplayIndex;
            }
            ShowedListChangedEvent(new ShowedListChangedEventArgs(lib, listName));
            StopLoadingListEvent();
        }

        /// <summary>
        /// 设置选中的条目
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        public void SetSelectedMedias(int startIndex, int length)
        {
            if(ListDataGridView.Rows.Count < startIndex + 1)
            {
                return;
            }
            ListDataGridView.ClearSelection();
            if (ListDataGridView.RowCount == 0)
                return;
            ListDataGridView.FirstDisplayedScrollingRowIndex = startIndex;
            for (int i = startIndex; i < startIndex + length; i++)
                ListDataGridView.Rows[i].Selected = true;
        }

        public bool GotoCurrentItem()
        {
            int CurrentAudioIndex = PlayingListTreeView.Nodes.IndexOfKey(_mc.PlayController.myPlayer.currentURL);//获取当前项索引
            if (CurrentAudioIndex == -1)//判断指定项是否存在
                _mc.MainForm.ShowNotice("在列表中无法检索到此音乐");
            else
            {
                PlayingListTreeView.SelectedNode = null;//清空选中项
                PlayingListTreeView.SelectedNode = PlayingListTreeView.Nodes[CurrentAudioIndex];//定位到选中项
            }
            return CurrentAudioIndex != -1;
        }
        #endregion

        #region events

        #region playingTreeView
        private void tv_playing_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PlayingListTreeView.SelectedNode = e.Node;
        }

        private void tv_playing_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedPath = e.Node.Name;
        }

        private void playingListTreeNode_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Controller.PlayController.FileTest(SelectedPath))
            {
                if (_mc.PlayController.myPlayer.IsInterimPlay == false)
                    if (string.Compare(_mc.PlayController.myPlayer.settings.URL, SelectedPath, true) == 0)
                        if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                            _mc.PlayController.myPlayer.ctControls.pause();
                        else if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                            _mc.PlayController.myPlayer.ctControls.play();
                        else
                            _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, SelectedPath);
                    else
                        _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, SelectedPath);
                else
                    _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, SelectedPath);
            }
        }

        private void playingListTreeNode_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            hotinfCms.ShowImageMargin = false;
            hotinfCms.Renderer = _mc.Theme.MenuRender;
            hotinfCms.Items.Clear();
            hotinfCms.Items.Add(new ToolStripMenuItem());
            hotinfSb.Clear();
            hotinfSb.Append("文 件 名：");
            hotinfSb.Append(e.Node.Text);
            hotinfSb.Append("\r\n\r类    型：");
            hotinfSb.Append(Path.GetExtension(e.Node.Name));
            hotinfSb.Append("\r\n\r大    小：");
            if (File.Exists(e.Node.Name))
            {
                hotinfSb.Append(NgNet.IO.FileHelper.LengthFormat(new FileInfo(e.Node.Name).Length));
                hotinfSb.Append("\r\n\r\npath  =    ");
                hotinfSb.Append(e.Node.Name);
            }
            else
            {
                hotinfSb.Append("\r\n\r\npath  =    (文件丢失)");
                hotinfSb.Append(e.Node.Name);
            }
            hotinfCms.Items[0].Text = hotinfSb.ToString();
            hotinfCms.Show(PlayingListTreeView.PointToScreen(new System.Drawing.Point(-1, PlayingListTreeView.Height)));
        }

        private void playingListTreeNode_MouseLeave(object sender, EventArgs e)
        {
            hotinfCms.Close();
        }


        #endregion

        #region listDataGridView
        private void listDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void listDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.Button == MouseButtons.Left)
            {
                ListDataGridView.ClearSelection();
                ListDataGridView.Rows[e.RowIndex].Selected = true;
            }
            else
            {
                if (!ListDataGridView.Rows[e.RowIndex].Selected)
                {
                    ListDataGridView.ClearSelection();
                    ListDataGridView.Rows[e.RowIndex].Selected = true;
                }
            }

            // this.SelectedAudioIndex = e.RowIndex;
            //获取当前项的音乐路径
            SelectedPath = ListDataGridView.Rows[e.RowIndex].Tag.ToString();
            ListDataGridView.Rows[e.RowIndex].ContextMenuStrip = listMenu.cms;
        }

        private void listDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            switch (ShowedLib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    if (string.Compare(SelectedPath, _mc.PlayController.myPlayer.settings.URL, true) == 0)
                        if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                            _mc.PlayController.myPlayer.ctControls.pause();
                        else if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                            _mc.PlayController.myPlayer.ctControls.play();
                        else
                            _mc.PlayController.ListPlay(ShowedLib, ShowedList, SelectedPath);
                    else
                        _mc.PlayController.ListPlay(ShowedLib, ShowedList, SelectedPath);
                    break;
                case List.MyLib.Local:
                case List.MyLib.History:
                case List.MyLib.Favorite:
                case List.MyLib.RecentlyAdded:
                case List.MyLib.MostlyPlayed:
                    _mc.PlayController.ListPlay(ShowedLib, ShowedList, SelectedPath);
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region list
        private void LibResetedEvent(List.LibResetedEventArgs e)
        {
            if (ShowedLib == e.Lib)
                LoadAudioList(ShowedLib, ShowedList, true);
        }

        private void ListNameChangedEvent(List.ListNameChangedEventArgs e)
        {
            if (ShowedLib == e.Lib && ShowedList == e.OldName)
                ShowedList = e.NewName;
        }

        private void listItemsChangedEvent(List.ListItemsChangedEventArgs e)
        {
            int listIndex = _mc.MyLists.GetListIndex(e.Lib, e.List);

            if (e.Lib == List.MyLib.Playing)
            {
                if (listIndex == -1)
                    return;
                foreach (string item in e.NewItems)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = System.IO.Path.GetFileName(item);
                    tn.Name = item;
                    tn.BackColor = _mc.Theme.BackColor;
                    tn.ForeColor = _mc.Theme.ForeColor;
                    tn.ContextMenuStrip = listMenu.cms;
                    PlayingListTreeView.Nodes.Add(tn);
                }
                foreach (string item in e.RemovedItems)
                {
                    PlayingListTreeView.Nodes.RemoveByKey(item);
                }
                if (!_mc.PlayController.myPlayer.IsInterimPlay)
                {
                    Player.PlayStates playState = _mc.PlayController.myPlayer.settings.PlayState;
                    if (playState == Player.PlayStates.playing
                        || playState == Player.PlayStates.paused
                        || playState == Player.PlayStates.stoped)
                    {
                        if (e.RemovedItems.Contains(_mc.PlayController.myPlayer.settings.URL))
                            _mc.PlayController.myPlayer.ctControls.runCycleMode(_mc.PlayController.myPlayer.settings.CycleMode);
                        if (playState == Player.PlayStates.stoped)
                            _mc.PlayController.myPlayer.ctControls.stop();
                    }
                }
            }
            if (e.Lib == ShowedLib && e.List == ShowedList)
            {
                LoadAudioList(e.Lib, e.List, true);
            }
        }

        private void listCleanedEvent(List.ListCleanedEventArgs e)
        {
            int listIndex = _mc.MyLists.GetListIndex(e.Lib, e.ListName);
            switch (e.Lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    if (listIndex == -1)
                        throw new List.ListNotFoundException(e.Lib, e.ListName);
                    PlayingListTreeView.Nodes.Clear();
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
            if (ShowedLib == e.Lib && ShowedList == e.ListName)
                LoadAudioList(e.Lib, e.ListName, true);
        }

        private void listRemovedEvent(List.ListRemovedEventArgs e)
        {    
            if (ShowedLib == e.Lib && ShowedList == e.ListName)
                LoadAudioList(List.MyLib.Local, List.ListManager.NAME_LIST_DEFAULT, true);
        }

        private void themeChangedEvent(ThemeChangedEventArgs e)
        {
            PlayingListTreeView.BackColor = e.ThemeClass.BackColor;
            PlayingListTreeView.NodeSelectedBackColor = (e.ThemeClass as ThemeManager).ButonDownColor;
            PlayingListTreeView.NodeSelectedForeColor = NgNet.Drawing.ColorHelper.GetOppositeColor((e.ThemeClass as ThemeManager).ButonDownColor);
            foreach (TreeNode tn0 in PlayingListTreeView.Nodes)
            {
                tn0.BackColor = e.ThemeClass.BackColor;
                tn0.ForeColor = e.ThemeClass.ForeColor;
            }
            //处理正在播放项颜色
            int itemindex = PlayingListTreeView.Nodes.IndexOfKey(_mc.PlayController.myPlayer.currentURL);
            if (itemindex != -1)
            {
                PlayingListTreeView.Nodes[itemindex].ForeColor = (e.ThemeClass as ThemeManager).CurrentItemForeColor;
                PlayingListTreeView.Nodes[itemindex].BackColor = (e.ThemeClass as ThemeManager).CurrentItemBackColor;
            }
            //处理Dgv
            ListDataGridView.BackgroundColor = e.ThemeClass.BackColor;
            ListDataGridView.ColumnHeadersDefaultCellStyle.BackColor = e.ThemeClass.BackColor;
            ListDataGridView.RowsDefaultCellStyle.BackColor = e.ThemeClass.BackColor;
            ListDataGridView.RowsDefaultCellStyle.SelectionBackColor = (e.ThemeClass as ThemeManager).ButonDownColor;
            ListDataGridView.AlternatingRowsDefaultCellStyle.BackColor = (e.ThemeClass as ThemeManager).CurrentItemBackColor;
            ListDataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = (e.ThemeClass as ThemeManager).ButonDownColor;
            listMenu.cms.Renderer = (e.ThemeClass as ThemeManager).MenuRender;
            ContextMenuStripHelper.SetOpacity(listMenu.cms, e.ThemeClass.Opacity);
        }

        private void uRLChangedEvent(Player.URLChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.CurrentURL))
                return;
            if (_mc.PlayController.myPlayer.IsInterimPlay)
                return;
            GotoCurrentItem();
        }
        // 播放状态改变事件
        private void playStateChangedEvent(Player.PlayStateChangedEventArgs e)
        {
            switch (e.PlayState)
            {
                case Player.PlayStates.playing:
                    SetCurrentItemStyle(_mc.PlayController.myPlayer.settings.URL, 3, 3, _mc.Theme.CurrentItemForeColor, _mc.Theme.CurrentItemBackColor);
                    break;
                case Player.PlayStates.paused:
                    SetCurrentItemStyle(_mc.PlayController.myPlayer.settings.URL, 4, 4, _mc.Theme.CurrentItemForeColor, _mc.Theme.CurrentItemBackColor);
                    break;
                case Player.PlayStates.transitioning:
                case Player.PlayStates.stoped:
                case Player.PlayStates.mediaEnd:
                    SetCurrentItemStyle(_mc.PlayController.myPlayer.settings.URL, 0, 0, _mc.Theme.ForeColor, _mc.Theme.BackColor);
                    break;
                case Player.PlayStates.closed:
                    break;
                default:
                    break;
            }

        }
        #endregion
        #endregion
    }
}
