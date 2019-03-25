using NgNet.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FreshMedia.View.VList
{
    class SearchBox
    {
        #region private filed
        Form ownerForm;
        FormEx fm = new FormEx();
        TextBox kwTxtBox;
        Button searchButton;
        const string NOTICE = "输入搜索关键词:";
        ContextMenuStrip cms;
        Padding padding;
        Control backgroundControl;

        Controller.MainController _mc;
        #endregion

        #region public filed
        public string KeyWord { get; private set; }
        #endregion

        #region properties
        public bool IsLoaded
        {
            get
            {
                return NgNet.UI.Forms.FormHelper.IsLoaded(fm);
            }
        }

        public event BeginLoadingListEventHandler BeginSearchingListEvent;

        public event StopLoadingListEventHandler StopSearchingListEvent;
        #endregion

        #region constructor destructor 
        public SearchBox(Form owner, Control background, IFreshMedia freshMedia)
        {
            _mc = freshMedia.Controller;
            KeyWord = string.Empty;
            BeginSearchingListEvent = new BeginLoadingListEventHandler((ListLoadingEventArgs e) => { });
            StopSearchingListEvent = new StopLoadingListEventHandler(() => { });
            this.ownerForm = owner;
            this.backgroundControl = background;
            this.initMenu();
        }

        public void Init()
        {

        }
        #endregion

        #region private methed
        private void initMenu()
        {
            this.cms = new ContextMenuStrip();
            this.cms.Opacity = _mc.Theme.Opacity;
            this.cms.ShowImageMargin = false;
            this.cms.Opening += new System.ComponentModel.CancelEventHandler((object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                this.cms.Items[0].Enabled = !string.IsNullOrWhiteSpace(Clipboard.GetText());
                this.cms.Items[1].Enabled = !string.IsNullOrWhiteSpace(kwTxtBox.Text);
                this.cms.Items[2].Enabled = !string.IsNullOrEmpty(kwTxtBox.Text);
            });
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = "粘贴";
            tsmi.Click += new EventHandler((object sender, EventArgs e) => { if (string.IsNullOrWhiteSpace(Clipboard.GetText())) return; this.kwTxtBox.Paste(Clipboard.GetText()); });
            this.cms.Items.Add(tsmi);
            tsmi = new ToolStripMenuItem();
            tsmi.Text = "复制";
            tsmi.Click += new EventHandler((object sender, EventArgs e) => { if (string.IsNullOrWhiteSpace(kwTxtBox.Text)) return; Clipboard.SetText(kwTxtBox.Text); });
            this.cms.Items.Add(tsmi);
            tsmi = new ToolStripMenuItem();
            tsmi.Text = "清空";
            tsmi.Click += new EventHandler((object sender, EventArgs e) => { this.kwTxtBox.Clear(); });
            this.cms.Items.Add(tsmi);
            tsmi = new ToolStripMenuItem();
            tsmi.Text = "关闭搜索框";
            tsmi.Click += new EventHandler((object sender, EventArgs e) => { this.searchButton_Click(sender, e); });
            this.cms.Items.Add(tsmi);
            tsmi = new ToolStripMenuItem();
            tsmi.Text = "帮助";
            tsmi.Click += new EventHandler((object sender, EventArgs e) => { NgNet.UI.Forms.MessageBox.Show(fm, "切换列表可在不同列表中搜索！"); });
            this.cms.Items.Add(tsmi);
        }

        private void themeChangedEvent(ThemeChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                searchButton.FlatAppearance.BorderColor = e.ThemeClass.BorderColor;
                searchButton.ForeColor = e.ThemeClass.BackColor;
                searchButton.BackColor = e.ThemeClass.BorderColor;
                kwTxtBox.BackColor = e.ThemeClass.BackColor;
                fm.BackColor = e.ThemeClass.BorderColor;
                cms.Renderer = (e.ThemeClass as ThemeManager).MenuRender;
            }
        }

        private void ownerLocationChanged(object sender, EventArgs e)
        {
            if (this.IsLoaded)
            {
                Point p = backgroundControl.PointToScreen(new Point(0, backgroundControl.Height));
                this.fm.Location = new Point(p.X, p.Y - fm.Height);
            }
        }

        private void ownerWidthChanged(object sender, EventArgs e)
        {
            if (this.IsLoaded)
            {
                fm.Width = backgroundControl.Width;
            }
        }

        private void ownerVisibleChanged(object sender, EventArgs e)
        {
            fm.Visible = fm.Owner.Visible;
        }

        private void fm_Load(object sender, EventArgs e)
        {
            NgNet.UI.Forms.FormHelper hForm = new NgNet.UI.Forms.FormHelper(fm);
            hForm.SetFormRoundRgn(3, 3);
            ownerLocationChanged(sender, e);
            ownerWidthChanged(sender, e);

            //动态调整搜索框宽度
            backgroundControl.SizeChanged += new EventHandler(ownerWidthChanged);
            //动态调整搜索框位置
            backgroundControl.SizeChanged += new EventHandler(ownerLocationChanged);
            ownerForm.LocationChanged += new EventHandler(ownerLocationChanged);
            ownerForm.VisibleChanged += new EventHandler(ownerVisibleChanged);
            _mc.Theme.ThemeChanged += new ThemeChangedEventHandler(themeChangedEvent);
            fm.FormClosed += new FormClosedEventHandler((object sender1, FormClosedEventArgs e1) =>
            {
                ownerForm.VisibleChanged -= new EventHandler(ownerVisibleChanged);
                ownerForm.LocationChanged -= new EventHandler(ownerLocationChanged);
                backgroundControl.SizeChanged -= new EventHandler(ownerWidthChanged);
                backgroundControl.SizeChanged -= new EventHandler(ownerLocationChanged);
                _mc.Theme.ThemeChanged -= new ThemeChangedEventHandler(themeChangedEvent);
                backgroundControl.Padding = padding;
            });
            padding = backgroundControl.Padding;
            backgroundControl.Padding = new Padding(padding.Left, padding.Top, padding.Right, padding.Bottom + fm.Height);
        }

        private void fm_SizeChanged(object sender, EventArgs e)
        {
            kwTxtBox.Width = fm.Width - searchButton.Width - fm.Padding.Left - fm.Padding.Right;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.fm.Close();
        }

        private void kwTxtBox_Leave(object sender, EventArgs e)
        {
            kwTxtBox.Text = string.IsNullOrWhiteSpace(kwTxtBox.Text) ? NOTICE : kwTxtBox.Text;
        }

        private void kwTxtBox_Enter(object sender, EventArgs e)
        {
            kwTxtBox.Text = kwTxtBox.Text.Equals(NOTICE) ? string.Empty : kwTxtBox.Text;
        }

        private void kwTxtBox_TextChanged(object sender, EventArgs e)
        {
            So(_mc.ListViewManager.MyListView.ShowedLib, _mc.ListViewManager.MyListView.ShowedList, kwTxtBox.Text);
        }

        private void kwTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //空格键无效
            e.Handled = Char.IsWhiteSpace(e.KeyChar);
        }
        #endregion

        #region public method
        public void Show()
        {
            if (IsLoaded)
                return;//如果已经打开搜索界面则返回
            fm = new FormEx();
            kwTxtBox = new TextBox();
            searchButton = new Button();

            //tb
            kwTxtBox.BorderStyle = BorderStyle.None;
            kwTxtBox.Location = new Point(0, 0);
            kwTxtBox.Multiline = true;
            kwTxtBox.MaxLength = 12;
            kwTxtBox.ContextMenuStrip = cms;
            kwTxtBox.Text = NOTICE;
            kwTxtBox.ScrollBars = ScrollBars.None;
            kwTxtBox.Dock = DockStyle.Left;
            kwTxtBox.ShortcutsEnabled = false;
            kwTxtBox.Enter += new EventHandler(kwTxtBox_Enter);
            kwTxtBox.Leave += new EventHandler(kwTxtBox_Leave);
            kwTxtBox.TextChanged += new EventHandler(kwTxtBox_TextChanged);
            kwTxtBox.KeyPress += new KeyPressEventHandler(kwTxtBox_KeyPress);
            //bn
            searchButton.FlatStyle = FlatStyle.Flat;
            searchButton.Text = "X";
            searchButton.Dock = DockStyle.Right;
            searchButton.Height = 26;
            searchButton.Width = 20;
            searchButton.Click += new EventHandler(searchButton_Click);
            //sh
            fm.ShowInTaskbar = false;
            fm.ShowIcon = false;
            fm.StartPosition = FormStartPosition.Manual;
            fm.Height = 32;
            fm.Owner = ownerForm;
            fm.Padding = new Padding(5, 3, 2, 3);
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Opacity = ownerForm.Opacity;
            fm.Load += new EventHandler(fm_Load);
            fm.SizeChanged += new EventHandler(fm_SizeChanged);
            fm.Controls.AddRange(new Control[] { kwTxtBox, searchButton });
            fm.Leave += new EventHandler(kwTxtBox_Leave);
            fm.Enter += new EventHandler(kwTxtBox_Enter);
            fm.Show();

        }

        public void So(List.MyLib lib, string listName, string wd)
        {
            if (string.IsNullOrWhiteSpace(wd) || NOTICE.Equals(wd))
            {
                KeyWord = wd;
                _mc.ListViewManager.MyListView.LoadAudioList(lib, listName, true);
                return;
            }
            _mc.ListViewManager.MyListView.ShowedLib = lib;
            _mc.ListViewManager.MyListView.ShowedList = listName;
            BeginSearchingListEvent(new ListLoadingEventArgs(lib, listName, "正在搜索..."));
            //如果面板未显示则显示面板
            if (IsLoaded == false)
                Show();
            wd = wd.Replace(" ", string.Empty);
            fm.Activate();
            kwTxtBox.Text = wd;
            kwTxtBox.Focus();
            kwTxtBox.SelectionStart = wd.Length;
            KeyWord = wd;
            //显示显示结果面板
            #region 搜索并显示
            _mc.LyricManager.FLyric.Visible = false;
            _mc.ListViewManager.MyListView.ListDataGridView.Rows.Clear();
            int itemcount = 0;
            int listindex = _mc.MyLists.GetListIndex(lib, listName);
            if (listindex == -1)
                throw new List.ListNotFoundException(lib, listName);
            IEnumerable<string> tmp = null;
            switch (lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    tmp = _mc.MyLists.Playing[listindex];
                    break;
                case List.MyLib.Local:
                    tmp = _mc.MyLists.Local[listindex];
                    break;
                case List.MyLib.History:
                    tmp = _mc.MyLists.History[listindex];
                    break;
                case List.MyLib.Favorite:
                    tmp = _mc.MyLists.Favo[listindex];
                    break;
                case List.MyLib.RecentlyAdded:
                    tmp = _mc.MyLists.RecentlyAdded[listindex];
                    break;
                case List.MyLib.MostlyPlayed:
                    tmp = _mc.MyLists.Times.Values; ;
                    break;
                default:
                    break;
            }
            foreach (string item in tmp)
            {
                if (Path.GetFileName(item).Contains(wd))
                {
                    _mc.ListViewManager.MyListView.ListDataGridView.Rows.Add();
                    _mc.ListViewManager.MyListView.SetDatagridviewRow(item, itemcount);
                    itemcount++;
                }
            }
            #endregion

            #region 当前显示列表的外观
            _mc.ListViewManager.MyLibView.SetShowingListNodeStyle(lib, listName);
            #endregion

            StopSearchingListEvent();
        }

        public void Close()
        {
            if (IsLoaded)
            {
                fm.Close();
            }
        }
        #endregion
    }
}
