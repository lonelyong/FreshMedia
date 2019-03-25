using NgNet.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FreshMedia.View.VSetting
{
    partial class ConfigForm : NgNet.UI.Forms.TitleableForm
    {
        #region private fields
        const string DESCRIPTION = "Description ";
        const string VALUE = "Value";
        Controller.MainController _mc;
        Label[] pageTitleLabel;
        Label[] pageHeadLabel;
        List<uint> sleepTimes = new List<uint>() { 10, 20, 30, 60, 80, 120 };
        DataTable _lyricColors = new DataTable();
        List<FontStyle> lyricFS = new List<FontStyle>() { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, FontStyle.Bold | FontStyle.Italic };
        List<NgNet.UI.Forms.ComboBoxHelper> _ComboBoxeHelpers = new List<NgNet.UI.Forms.ComboBoxHelper>();
        SettingTabs _tabToShow;
        uint _pageToShow;
        #endregion

        #region private properties
        // lrc
        private Color _formLyricPlayedColor
        {
            get
            {          
                return Color.FromArgb(int.Parse(formlrcPlayedColorComboBox.SelectedValue.ToString()));
            }
            set
            {
                displayColor(formlrcPlayedColorComboBox, value);
            }
        }

        private Color _formLyricPrepplayColor
        {
            get
            {
                return Color.FromArgb(int.Parse(formlrcCurrlineColorComboBox.SelectedValue.ToString()));
            }
            set
            {
                displayColor(formlrcCurrlineColorComboBox, value);
            }
        }

        private Color _formLyricColor
        {
            get
            {
                return Color.FromArgb(int.Parse(formlrcColorComboBox.SelectedValue.ToString()));
            }
            set
            {
                displayColor(formlrcColorComboBox, value);
            }
        }

        private Font _formLyricFont
        {
            set
            {
                formlrcFontComboBox.SelectedItem = value.Name;
                formLyricFontStyleComboBox.SelectedValue = value.Style;
                formlrcFontSizeComboBox.SelectedItem = value.Size;
                formlrcPreviewLabel.Font = _formLyricFont;
                refreshPreviewStyle();
            }
            get
            {
                return new Font(formlrcFontComboBox.SelectedItem.ToString(), (float)formlrcFontSizeComboBox.SelectedItem, (FontStyle)formLyricFontStyleComboBox.SelectedValue);
            }
        }

        private bool _deskLyricVisible
        {
            get
            {
                return desklrcVisibleCheckBox.Checked;
            }
            set
            {
                desklrcVisibleCheckBox.Checked = value;
            }
        }

        private Color _deskLyricPlayedColor
        {
            get
            {
                return Color.FromArgb(int.Parse(desklrcPlayedComboBox.SelectedValue.ToString()));
            }
            set
            {
                displayColor(desklrcPlayedComboBox, value);
            }
        }

        private Color _deskLyricPrepplayColor
        {
            get
            {
                return Color.FromArgb(int.Parse(desklrcColorComboBox.SelectedValue.ToString()));
            }
            set
            {
                displayColor(desklrcColorComboBox, value);
            }
        }

        private Font _deskLyricFont
        {
            set
            {
                desklrcFontComboBox.SelectedItem = value.Name;
                desklrcFontStyleComboBox.SelectedValue = value.Style;
                desklrcFontSizeComboBox.SelectedItem = value.Size;
                desklrcPreviewLabel.Font = _deskLyricFont;
                refreshPreviewStyle();
            }
            get
            {
                return new Font(desklrcFontComboBox.SelectedItem.ToString(), (float)desklrcFontSizeComboBox.SelectedItem, (FontStyle)desklrcFontStyleComboBox.SelectedValue);
            }
        }

        // 常规
        private bool _autoRun
        {
            get
            {
                return autoRunCheckBox.Checked;
            }
            set
            {
                autoRunCheckBox.Checked = value;
            }
        }

        private bool _blendable
        {
            get
            {
                return blendableCheckBox.Checked;
            }
            set
            {
                blendableCheckBox.Checked = value;
            }
        }

        private bool _autoPlay
        {
            get
            {
                return autoPlayCheckBox.Checked;
            }
            set
            {
                autoPlayCheckBox.Checked = value;
            }
        }

        private PresetTheme _theme
        {
            get
            {
                return (PresetTheme)themeComboBox.SelectedValue;
            }
            set
            {
                themeComboBox.SelectedValue = value;
            }
        }

        private bool _sleepEnable
        {
            get
            {
                return sleepCheckBox.Checked;
            }
            set
            {
                sleepCheckBox.Checked = value;
            }
        }

        private bool _startBoxVisible
        {
            get
            {
                return startBoxCheckBox.Checked;
            }
            set
            {
                startBoxCheckBox.Checked = value;
            }
        }

        private bool _enableShutDown
        {
            get
            {
                return shutdownCheckBox.Checked;
            }
            set
            {
                shutdownCheckBox.Checked = value;
            }
        }

        private uint _sleepTime
        {
            get
            {
                return uint.Parse(NgNet.Text.RegexHelper.GetTxtFigures(sleepTimeComboBox.SelectedItem.ToString()));
            }
            set
            {
                if (sleepTimes.Contains(value))
                    sleepTimeComboBox.SelectedItem = value;
                else sleepTimeComboBox.Items[sleepTimeComboBox.Items.Count - 1] = string.Format("自定义:<{0} min>", value);
            }
        }

        private NgNet.UI.Forms.ExitStyles _exitStyle
        {
            get
            {
                switch (_mc.Configs.ExitStyle)
                {
                    case NgNet.UI.Forms.ExitStyles.ExitChoose:
                    case NgNet.UI.Forms.ExitStyles.ExitDirectly:
                        return exitStyleCheckBox.Checked ? NgNet.UI.Forms.ExitStyles.ExitChoose : NgNet.UI.Forms.ExitStyles.ExitDirectly;
                    case NgNet.UI.Forms.ExitStyles.MinsizeChoose:
                    case NgNet.UI.Forms.ExitStyles.MinsizeDirectly:
                        return exitStyleCheckBox.Checked ? NgNet.UI.Forms.ExitStyles.MinsizeChoose : NgNet.UI.Forms.ExitStyles.MinsizeDirectly;
                    default:
                        return _mc.Configs.ExitStyle;
                }
            }
            set
            {
                switch (_mc.Configs.ExitStyle)
                {
                    case NgNet.UI.Forms.ExitStyles.ExitChoose:
                    case NgNet.UI.Forms.ExitStyles.ExitDirectly:
                        exitStyleCheckBox.Checked = true;
                        break;
                    case NgNet.UI.Forms.ExitStyles.MinsizeChoose:
                    case NgNet.UI.Forms.ExitStyles.MinsizeDirectly:
                        exitStyleCheckBox.Checked = false;
                        break;
                }
            }
        }
        //play
        private Player.CycleModes _cycelMode
        {
            get
            {
                return (Player.CycleModes)cycleComboBox.SelectedValue;
            }
            set
            {
                cycleComboBox.SelectedValue = value;
            }
        }

        private int _rfTime
        {
            get
            {
                return (int)rfTimeComboBox.SelectedItem;
            }
            set
            {
                rfTimeComboBox.SelectedItem = value;
            }
        }
        #endregion

        #region constructor destructor 
        public ConfigForm(SettingTabs tab, uint page, IFreshMedia iFreshMedia)
        {
            InitializeComponent();
            _mc = iFreshMedia.Controller;
            FormHelper.SetFormRoundRgn(3, 3);
            pageTitleLabel = new Label[6];
            pageHeadLabel = new Label[pageTitleLabel.Length];
            //移动窗体
            TitleBar.Icon = NgNet.ConvertHelper.Bitmap2Icon(Properties.Resources.setting_32_32);
            //显示指定的设置页
            tabInit();
            _tabToShow = tab;
            _pageToShow = page;
            init();
        }

        private void init()
        {
            DataColumn _dc = new DataColumn(DESCRIPTION);
            _lyricColors.Columns.Add(_dc);
            _dc = new DataColumn(VALUE);
            _lyricColors.Columns.Add(_dc);

            DataRow _dr;
            foreach (PropertyInfo item in typeof(Color).GetProperties())
            {
                if (item.PropertyType != typeof(Color))
                    continue;
                _dr = _lyricColors.NewRow();
                _dr[DESCRIPTION] = item.Name;
                _dr[VALUE] = ((Color)item.GetValue(null, null)).ToArgb();
                _lyricColors.Rows.Add(_dr);
            }
        }
        #endregion

        #region override  
        new public void Close()
        {
            Hide();
        }
        #endregion

        #region tab switch
        private SettingTabs _currentTab;

        private Control tabCurrent = new Label(), pageCurrent = new Label(), pageCurrent0 = new Label();

        public enum controlType : int { tabCurrent = 0, pageCurrent = 1, pageCurrent0 = 2 }

        public void ShowTab(SettingTabs tab, uint page)
        {
            _currentTab = tab;
            tabCurrent.BackColor = getControlColor(controlType.tabCurrent, 0);
            desklrcGbox.Visible = false;
            formlrcGbox.Visible = false;
            hotkeyGbox.Visible = false;
            playGbox.Visible = false;
            sleepGbox.Visible = false;
            startGbox.Visible = false;

            foreach (var item in pageTitleLabel)
            {
                item.Text = null;
            }
            switch (tab)
            {
                case SettingTabs.General:
                    pageTitleLabel[0].Text = startGbox.Text;
                    pageTitleLabel[1].Text = sleepGbox.Text;
                    pageTitleLabel[2].Text = hotkeyGbox.Text;
                    startGbox.Visible = page == 0;
                    sleepGbox.Visible = page == 1;
                    hotkeyGbox.Visible = page == 2;
                    tabCurrent = tabGeneralLabel;
                    tabCurrent.Tag = new KeyValuePair<SettingTabs, uint>(tab, page);
                    break;
                case SettingTabs.Play:
                    pageTitleLabel[0].Text = playGbox.Text;
                    playGbox.Visible = page == 0;
                    tabCurrent = tabPlayLabel;
                    tabCurrent.Tag = new KeyValuePair<SettingTabs, uint>(tab, page);
                    break;
                case SettingTabs.Lyric:
                    pageTitleLabel[0].Text = formlrcGbox.Text;
                    pageTitleLabel[1].Text = desklrcGbox.Text;
                    formlrcGbox.Visible = page == 0;
                    desklrcGbox.Visible = page == 1;
                    tabCurrent = tabLyricLabel;
                    tabCurrent.Tag = new KeyValuePair<SettingTabs, uint>(tab, page);
                    break;
            }
            tabCurrent.BackColor = getControlColor(controlType.tabCurrent, 1);

            pageCurrent.Font = new Font(pageTitleLabel[0].Font.Name, pageTitleLabel[0].Font.Size, FontStyle.Regular, GraphicsUnit.Pixel);
            pageCurrent.BackColor = getControlColor(controlType.pageCurrent, 0);
            pageCurrent = pageTitleLabel[page];
            pageCurrent.Font = new Font(pageTitleLabel[0].Font.Name, pageTitleLabel[0].Font.Size, FontStyle.Bold, GraphicsUnit.Pixel);
            pageCurrent.BackColor = getControlColor(controlType.pageCurrent, 1);

            pageCurrent0.BackColor = getControlColor(controlType.pageCurrent0, 0);
            pageCurrent0 = pageHeadLabel[page];
            pageCurrent0.BackColor = getControlColor(controlType.pageCurrent0, 1);

            Activate();
        }

        private void tabInit()
        {
            for (int i = 0; i < pageHeadLabel.Length; i++)
            {
                pageHeadLabel[i] = new Label();
                pageHeadLabel[i].BorderStyle = BorderStyle.None;
                pageHeadLabel[i].Parent = pagePanel;

                pageTitleLabel[i] = new Label();
                pageTitleLabel[i].Tag = i;
                pageTitleLabel[i].BorderStyle = BorderStyle.None;
                pageTitleLabel[i].TextAlign = ContentAlignment.MiddleCenter;
                pageTitleLabel[i].BackColor = Color.Transparent;
                pageTitleLabel[i].Parent = pagePanel;
                pageTitleLabel[i].Click += new EventHandler(pages_Click);
                pageTitleLabel[i].MouseEnter += new EventHandler(pages_MouseEnter);
                pageTitleLabel[i].MouseLeave += new EventHandler(pages_MouseLeave);
            }

            tabGeneralLabel.Tag = new KeyValuePair<SettingTabs, uint>(SettingTabs.General, 0);
            tabPlayLabel.Tag = new KeyValuePair<SettingTabs, uint>(SettingTabs.Play, 0);
            tabLyricLabel.Tag = new KeyValuePair<SettingTabs, uint>(SettingTabs.Lyric, 0);
        }

        private void tabs_Click(object sender, EventArgs e)
        {
            KeyValuePair<SettingTabs, uint> kvp = (KeyValuePair<SettingTabs, uint>)((Control)sender).Tag;
            ShowTab(kvp.Key, kvp.Value);
        }

        private void tabs_MouseEnter(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Equals(tabCurrent))
                return;
            ctr.BackColor = getControlColor(controlType.tabCurrent, 2);
        }

        private void tabs_MouseLeave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Equals(tabCurrent))
                return;
            ctr.BackColor = ctr.BackColor = getControlColor(controlType.tabCurrent, 0);
        }

        private void pages_MouseEnter(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Equals(pageCurrent) || string.IsNullOrWhiteSpace(ctr.Text))
                return;
            ctr.BackColor = getControlColor(controlType.pageCurrent, 2);

            pageHeadLabel[(int)ctr.Tag].BackColor = getControlColor(controlType.pageCurrent0, 2);
        }

        private void pages_MouseLeave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Equals(pageCurrent) || string.IsNullOrWhiteSpace(ctr.Text))
                return;
            ctr.BackColor = getControlColor(controlType.pageCurrent, 0);
            pageHeadLabel[(int)ctr.Tag].BackColor = getControlColor(controlType.pageCurrent0, 0);
        }

        private void pages_Click(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (string.IsNullOrWhiteSpace(ctr.Text))
                return;
            ShowTab(_currentTab, Convert.ToUInt32(ctr.Tag.ToString()));
        }

        private Color getControlColor(controlType ct, int flag)
        {
            switch (ct)
            {
                case controlType.tabCurrent:
                    if (flag == 0)
                        return Color.Transparent;
                    else if (flag == 1)
                        return NgNet.Drawing.ColorHelper.GetSimilarColor(BackColor, true, NgNet.Level.Level4);
                    else if (flag == 2)
                        return NgNet.Drawing.ColorHelper.GetSimilarColor(BackColor, true, NgNet.Level.Level2);
                    else
                        return Color.Transparent;
                case controlType.pageCurrent:
                    if (flag == 0)
                        return Color.Transparent;
                    else if (flag == 1)
                        return BackColor;
                    else if (flag == 2)
                        return NgNet.Drawing.ColorHelper.GetSimilarColor(BackColor, true, NgNet.Level.Level2);
                    else
                        return Color.Transparent;
                case controlType.pageCurrent0:
                    if (flag == 0)
                        return BorderColor;
                    else if (flag == 1)
                        return NgNet.Drawing.ColorHelper.GetSimilarColor(BorderColor, false, NgNet.Level.Level4);
                    else if (flag == 2)
                        return NgNet.Drawing.ColorHelper.GetSimilarColor(BorderColor, false, NgNet.Level.Level2);
                    else
                        return BorderColor;
            }
            return Color.Transparent;
        }
        #endregion

        #region general  
        //自行设置睡眠时间
        private void diyStopTimeButton_Click(object sender, EventArgs e)
        {
            uint stime = _mc.SleepMode.DiySleeptimeDialog(this, _sleepTime);
            if (stime == 0)
            {
                sleepTimeComboBox.SelectedItem = _sleepTime;
                return;
            }
            _sleepTime = stime;
        }
        //切换主题
        private void showHotkeys()
        {
            //显示快捷键
            TextBox tb;
            Label lb;
            for (int i = 0; i < _mc.HotkeyManager.Hotkeys.Length; i++)
            {
                tb = new TextBox();
                tb.BackColor = _mc.Theme.BackColor;
                tb.BorderStyle = BorderStyle.None;
                tb.ForeColor = _mc.HotkeyManager.Hotkeys[i].Registered ? SystemColors.Highlight : Color.DarkGray;
                tb.Location = new Point(111, 27 + i * 31);
                tb.Name = _mc.HotkeyManager.Hotkeys[i].Name;
                tb.ReadOnly = true;
                tb.ShortcutsEnabled = false;
                tb.Size = new Size(360, 14);
                tb.TabIndex = 100 + i;
                tb.Text = _mc.HotkeyManager.Hotkeys[i].Value;
                tb.Parent = hotkeyGbox;

                lb = new Label();
                lb.AutoSize = true;
                lb.Text = _mc.HotkeyManager.Hotkeys[i].Name;
                lb.Parent = this.hotkeyGbox;
                lb.Location = new Point(tb.Left - lb.Width - 18, 27 + i * 31);
            }
        }
        #endregion

        #region lyric
        #region common
        //歌词选项卡窗口歌词子选项卡
        private const string TITLE_DIY = "自定义";

        private void lyricStylePreview(Graphics g, Color played, Color unplayed, Font f, System.Drawing.Drawing2D.ColorBlend cb, string text)
        {
            SizeF ls = g.MeasureString(text, f);
            PointF sp = new PointF((desklrcPreviewLabel.Width - ls.Width) / 2, ((desklrcPreviewLabel.Height - ls.Height) / 2));
            PointF ep = new PointF(sp.X + ls.Width, sp.Y);
            //当前句歌词为空时退出
            if (sp.X == ep.X) { return; }
            /// 设置颜色显示范围，三个区域： Blue区，Blue向Red过度区（过度区很短
            cb.Positions = new float[4] { 0, 0.5f, 0.5001f, 1.0f };
            // 定义好颜色格式
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                  sp
                , ep
                , played
                , unplayed);
            brush.InterpolationColors = cb;
            //开始绘制文字
            g.DrawString(text, f, brush, sp);
        }

        private void formlrcStylePreview(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.ColorBlend cb = new System.Drawing.Drawing2D.ColorBlend();
            cb.Colors = new Color[4] { _formLyricPlayedColor, _formLyricColor, _formLyricPrepplayColor, _formLyricPrepplayColor };
            lyricStylePreview(e.Graphics, _formLyricPlayedColor, _formLyricPrepplayColor, _formLyricFont, cb, formlrcCurrentPreviewLabel.Tag.ToString());
        }

        private void desklrcPreview(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.ColorBlend cb = new System.Drawing.Drawing2D.ColorBlend();
            cb.Colors = new Color[4] { _deskLyricPlayedColor, _deskLyricPlayedColor, _deskLyricPlayedColor, _deskLyricPrepplayColor };
            lyricStylePreview(e.Graphics, _deskLyricPlayedColor, _deskLyricPrepplayColor, _deskLyricFont, cb, desklrcCurrentPreviewLabel.Tag.ToString());
        }

        private void refreshPreviewStyle()
        {
            formlrcPreviewLabel.ForeColor = _formLyricColor;
            desklrcPreviewLabel.ForeColor = _deskLyricPrepplayColor;
            formlrcCurrentPreviewLabel.Refresh();
            desklrcCurrentPreviewLabel.Refresh();
        }

        private Color diyLyricColor(Color c)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = c;
            cd.AllowFullOpen = true;
            cd.AnyColor = true;
            cd.CustomColors = new int[] { 0, 1 };
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            return cd.Color;
        }

        private void displayColor(ComboBox comboBox, Color c)
        {
            DataTable _dt = comboBox.DataSource as DataTable;
            DataRow[] _drs = _dt.Select($"[{DESCRIPTION}] = '{c.Name}'");
            comboBox.SelectedValue = c.ToArgb();           
        }
        #endregion

        #region formLyric
        private void formlrcComboBoxs_SelectedValueChanged(object sender, EventArgs e)
        {

            //窗口歌词字体
            if (sender == formlrcFontComboBox)
                refreshPreviewStyle();
            //窗口歌词字号
            else if (sender == formlrcFontSizeComboBox)
                refreshPreviewStyle();
            //窗口歌词字体样式
            else if (sender == formLyricFontStyleComboBox)
                refreshPreviewStyle();
            //窗口歌词颜色
            else if (sender == formlrcPlayedColorComboBox
                || sender == formlrcCurrlineColorComboBox
                || sender == formlrcColorComboBox)
                refreshPreviewStyle();
        }
        #endregion

        #region deskLyric
        private void desklrcComboBoxs_SelectedValueChanged(object sender, EventArgs e)
        {
            if(sender == desklrcFontComboBox)
                refreshPreviewStyle();
            //桌面歌词字体大小
            else if (sender == desklrcFontSizeComboBox)
                refreshPreviewStyle();
            //桌面歌词字体样式
            else if (sender == desklrcFontStyleComboBox)
                refreshPreviewStyle();
            //桌面歌词已经播放颜色
            else if (sender == desklrcPlayedComboBox
                || sender == desklrcColorComboBox)
                refreshPreviewStyle();
        }
        #endregion
        #endregion

        #region setting
        /// <summary>
        /// 加载窗体时界面初始化
        /// </summary>
        private void initialization()
        {

            //设置预设时间
            foreach (int item in sleepTimes)
                sleepTimeComboBox.Items.Add(string.Format("{0} min", item));
            sleepTimeComboBox.Items.Add("自定义 [未设置]");
            NgNet.UI.Forms.ComboBoxHelper _h = new NgNet.UI.Forms.ComboBoxHelper(sleepTimeComboBox);
            _ComboBoxeHelpers.Add(_h);

            //设置预设主题
            _h = new NgNet.UI.Forms.ComboBoxHelper(themeComboBox);
            _h.SetEnum(typeof(PresetTheme));
            _ComboBoxeHelpers.Add(_h);

            //预设RFTime
            int rftime = 1000;
            while (rftime <= 16000)
                rfTimeComboBox.Items.Add(rftime += 1000);
            _h = new NgNet.UI.Forms.ComboBoxHelper(rfTimeComboBox);
            _ComboBoxeHelpers.Add(_h);

            //预设循环模式
            _h= new NgNet.UI.Forms.ComboBoxHelper(cycleComboBox);
            _h.SetEnum(typeof(Player.CycleModes));
            _ComboBoxeHelpers.Add(_h);

            //字体样式
            _h = new NgNet.UI.Forms.ComboBoxHelper(desklrcFontStyleComboBox);
            _h.SetEnumArray(lyricFS);
            _ComboBoxeHelpers.Add(_h);
            _h = new NgNet.UI.Forms.ComboBoxHelper(formLyricFontStyleComboBox);
            _h.SetEnumArray(lyricFS);
            _ComboBoxeHelpers.Add(_h);

            //字体名称
            System.Drawing.Text.InstalledFontCollection inc = new System.Drawing.Text.InstalledFontCollection();
            foreach (FontFamily ff in inc.Families)
            {
                desklrcFontComboBox.Items.Add(ff.Name);
                formlrcFontComboBox.Items.Add(ff.Name);
            }
            _h = new NgNet.UI.Forms.ComboBoxHelper(desklrcFontComboBox);
            _ComboBoxeHelpers.Add(_h);
            _h = new NgNet.UI.Forms.ComboBoxHelper(formlrcFontComboBox);
            _ComboBoxeHelpers.Add(_h);


            //字体大小
            for (float i = 12; i <= 36; i += 1)
            {
                desklrcFontSizeComboBox.Items.Add(i);
                formlrcFontSizeComboBox.Items.Add(i);
            }
            _h = new NgNet.UI.Forms.ComboBoxHelper(desklrcFontSizeComboBox);
            _ComboBoxeHelpers.Add(_h);
            _h = new NgNet.UI.Forms.ComboBoxHelper(formlrcFontSizeComboBox);
            _ComboBoxeHelpers.Add(_h);

            // 歌词颜色
            ComboBox[] cbbs = new ComboBox[] { desklrcPlayedComboBox, desklrcColorComboBox, formlrcPlayedColorComboBox, formlrcColorComboBox, formlrcCurrlineColorComboBox };
            foreach (ComboBox item in cbbs)
            {
                _h = new NgNet.UI.Forms.ComboBoxHelper(item);
                _ComboBoxeHelpers.Add(_h);
                item.DataSource = _lyricColors.Copy();
                item.DisplayMember = DESCRIPTION;
                item.ValueMember = VALUE;
            }
        }
        /// <summary>
        /// 显示现有配置信息
        /// </summary>
        private void onLoading()
        {
            desklrcFontStyleComboBox.SelectedValueChanged += new EventHandler(desklrcComboBoxs_SelectedValueChanged);
            desklrcFontComboBox.SelectedValueChanged += new EventHandler(desklrcComboBoxs_SelectedValueChanged);
            formLyricFontStyleComboBox.SelectedValueChanged += new EventHandler(formlrcComboBoxs_SelectedValueChanged);
            desklrcFontSizeComboBox.SelectedValueChanged += new EventHandler(desklrcComboBoxs_SelectedValueChanged);
            desklrcPlayedComboBox.SelectedValueChanged += new EventHandler(desklrcComboBoxs_SelectedValueChanged);
            desklrcColorComboBox.SelectedValueChanged += new EventHandler(desklrcComboBoxs_SelectedValueChanged);
            formlrcCurrlineColorComboBox.SelectedValueChanged += new EventHandler(formlrcComboBoxs_SelectedValueChanged);
            formlrcFontSizeComboBox.SelectedValueChanged += new EventHandler(formlrcComboBoxs_SelectedValueChanged);
            formlrcColorComboBox.SelectedValueChanged += new EventHandler(formlrcComboBoxs_SelectedValueChanged);
            formlrcFontComboBox.SelectedValueChanged += new EventHandler(formlrcComboBoxs_SelectedValueChanged);
            formlrcPlayedColorComboBox.SelectedValueChanged += new EventHandler(formlrcComboBoxs_SelectedValueChanged);

            #region 初始化界面

            startGbox.Parent = ContentPanel;
            sleepGbox.Parent = ContentPanel;
            playGbox.Parent = ContentPanel;
            hotkeyGbox.Parent = ContentPanel;
            formlrcGbox.Parent = ContentPanel;
            desklrcGbox.Parent = ContentPanel;

            #endregion

            #region 显示现有常规设置
            //启用启动画面
            _startBoxVisible = _mc.Configs.StartboxEnable;
            //淡入淡出
            _blendable = _mc.Theme.Blendable;
            //退出模式
            _exitStyle = _mc.Configs.ExitStyle;
            //是否允许关机
            _enableShutDown = _mc.SleepMode.ToShutDown;
            //主界面主题
            _theme = _mc.Theme.Current;
            //自动播放
            _autoPlay = _mc.Configs.AutoPlay;
            //自启动
            _autoRun = NgNet.Applications.Current.IsAutoRun();
            //睡眠模式
            sleepCheckBox.Checked = _mc.SleepMode.IsSleeping;
            if (sleepTimes.Contains(_mc.SleepMode.SleepTime))
            {
                sleepTimeComboBox.SelectedItem = _mc.SleepMode.SleepTime.ToString() + "  min";
            }
            else
            {
                sleepTimeComboBox.SelectedItem = _mc.SleepMode.SleepTime > 0 ? _mc.SleepMode.SleepTime : 20;
            }
            #endregion

            #region 显示现有播放设置
            //循环模式
            _cycelMode = _mc.PlayController.myPlayer.settings.CycleMode;

            _rfTime = _mc.PlayController.myPlayer.RewindForwardTime;
            #endregion

            #region 显示现有Lyric设置
            #region 桌面歌词
            //是否显示桌面歌词
            _deskLyricVisible = _mc.LyricManager.DLyric.Visible;
            //桌面歌词字体
            _deskLyricFont = _mc.LyricManager.DLyric.Font;
            //桌面歌词已播放颜色
            _deskLyricPlayedColor = _mc.LyricManager.DLyric.PlayedColor;
            //桌面歌词未播放颜色
            _deskLyricPrepplayColor = _mc.LyricManager.DLyric.PrepColor;
            #endregion

            #region 窗口歌词
            //窗口歌词字体名称及大小
            _formLyricFont = _mc.LyricManager.FLyric.Font;
            //窗口歌词已播放颜色
            _formLyricPlayedColor = _mc.LyricManager.FLyric.PlayedColor;
            //窗口歌词未播放颜色
            _formLyricColor = _mc.LyricManager.FLyric.Color;
            //窗口歌词准备播放颜色
            _formLyricPrepplayColor = _mc.LyricManager.FLyric.PrepColor;
            #endregion
            #endregion
        }

        private void this_Load(object sender, EventArgs e)
        {
            ContentPanel.SizeChanged += new EventHandler(this_SizeChanged);
            Size = new Size(1000, 700);
            NgNet.UI.Forms.FormHelper.CenterToOwner(this);
            initialization();

            onLoading();

            //自动应用主题
            _mc.Theme.ThemeChanged+= new ThemeChangedEventHandler(themeChangedEvent);
            _mc.SleepMode.OnSleepingEvent += new Plus.OnSleepingEventHandler(onSleepingEvent);
            this.FormClosed += new FormClosedEventHandler((object sender1, FormClosedEventArgs e1) =>
            {
                _mc.Theme.ThemeChanged -= new ThemeChangedEventHandler(themeChangedEvent);
                _mc.SleepMode.OnSleepingEvent -= new Plus.OnSleepingEventHandler(onSleepingEvent);
            });
        }

        private void this_SizeChanged(object sender, EventArgs e)
        {
            int l = pagePanel.Width + 6, t = 4, b = 8, h = 6, v = 12;
            tabGeneralLabel.Location = new Point(b, t);
            tabPlayLabel.Location = new Point(tabGeneralLabel.Right + h, t);
            tabLyricLabel.Location = new Point(tabPlayLabel.Right + h, t);

            pagePanel.Location = new Point(0, tabGeneralLabel.Bottom + t);
            pagePanel.Height = ContentPanel.Height - pagePanel.Top;
            var size = new Size(pageTitleLabel.First().Font.Height * 5, pageTitleLabel.First().Font.Height * 2);
            var size0 = new Size(pageHeadLabel.First().Font.Height, pagePanel.Height / pageHeadLabel.Length);
            for (int i = 0; i < pageHeadLabel.Length; i++)
            {
                pageHeadLabel[i].Size = size0;
                pageHeadLabel[i].Location = new Point(0, size0.Height * i);

                pageTitleLabel[i].Size = size;
                pageTitleLabel[i].Location = new Point(pagePanel.Width - size.Width, pageHeadLabel[i].Top + (pageHeadLabel[i].Height - size.Height) / 2);
            }
            //设置位置

            cancelButton.Location = new Point(ContentPanel.Width - cancelButton.Width - b, ContentPanel.Height - cancelButton.Height - b);
            applyButton.Location = new Point(cancelButton.Left - applyButton.Width + 1, cancelButton.Top);
            okButton.Location = new Point(applyButton.Left - okButton.Width + 1, cancelButton.Top);

            startGbox.Location = new Point(l, tabGeneralLabel.Bottom + v);
            hotkeyGbox.Location = startGbox.Location;
            sleepGbox.Location = startGbox.Location;
            playGbox.Location = startGbox.Location;
            formlrcGbox.Location = startGbox.Location;
            desklrcGbox.Location = startGbox.Location;

            startGbox.Size = new Size(ContentPanel.Width - startGbox.Left - b, okButton.Top - startGbox.Top - v);
            sleepGbox.Size = startGbox.Size;
            playGbox.Size = startGbox.Size;
            hotkeyGbox.Size = startGbox.Size;
            formlrcGbox.Size = startGbox.Size;
            desklrcGbox.Size = startGbox.Size;
        }

        private void this_Shown(object sender, EventArgs e)
        {

            ShowTab(_tabToShow, _pageToShow);
            showHotkeys(); 
        }
        #endregion

        #region events
        private void themeChangedEvent(ThemeChangedEventArgs e)
        {
            BorderColor = e.ThemeClass.BorderColor;
            BackColor = e.ThemeClass.BackColor;

            foreach (Control item in pageHeadLabel)
            {
                item.BackColor = BorderColor;
            }

            foreach (Control item in ContentPanel.Controls)
            {
                item.ForeColor = e.ThemeClass.ForeColor;
            }

            foreach (var item in _ComboBoxeHelpers)
            {
                item.Theme = this;
            }
 
            pagePanel.BackColor = NgNet.Drawing.ColorHelper.GetSimilarColor(e.ThemeClass.BackColor, true, NgNet.Level.Level4);
            tabCurrent.BackColor = getControlColor(controlType.tabCurrent, 1);
            pageCurrent.BackColor = getControlColor(controlType.pageCurrent, 1);
            pageCurrent0.BackColor = getControlColor(controlType.pageCurrent0, 1);
        }

        private void onSleepingEvent(Plus.OnSleepingEventArgs e)
        {
            if (e.LeftTime == 0)
            {
                sleepLefttimeLabel.Text = null;
                sleepCheckBox.Checked = false;
            }
            else
                sleepLefttimeLabel.Text = NgNet.ConvertHelper.ToTimeString(e.LeftTime);
        }
        #endregion

        #region apply and exit
        //保存修改并应用修改
        private void applySetting()
        {
            #region nor
            #region nor
            _mc.Configs.StartboxEnable = _startBoxVisible;
            _mc.Theme.Blendable= _blendable;
            _mc.Configs.AutoPlay = _autoPlay;
            _mc.Configs.ExitStyle = _exitStyle;
            if (_autoRun)
                NgNet.Applications.Current.AutoRun("");
            else
                NgNet.Applications.Current.CancelAutoRun();
            _mc.Theme.Current = _theme;

            #endregion

            #region sleep
            //应用随眠模式
            _mc.SleepMode.ToShutDown = _enableShutDown;
            if (_sleepEnable && _sleepTime > 0)//激活睡眠模式
            {
                if (_mc.SleepMode.IsSleeping && _mc.SleepMode.SleepTime != _sleepTime)//当前已经激活睡眠模式，当前重新设置睡眠时间
                {
                    _mc.SleepMode.Start(_sleepTime);
                    _mc.NotiryIcon.ShowNotice(6, "~﹏~", string.Format("睡眠时间已重设，将重新计时<{0} min>", _sleepTime), ToolTipIcon.Info);
                }
                else if (!_mc.SleepMode.IsSleeping)//未激活睡眠模式时
                {
                    _mc.SleepMode.Start(_sleepTime);
                    _mc.NotiryIcon.ShowNotice(6, "~﹏~", string.Format("已启用睡眠模式<{0} min>", _sleepTime), ToolTipIcon.Info);
                }
            }
            else if (!_sleepEnable && _mc.SleepMode.IsSleeping)//不激活睡眠模式
            {
                _mc.SleepMode.Stop();
                sleepLefttimeLabel.Text = string.Empty;
               
            }
            #endregion
            #endregion

            #region Play
            _mc.PlayController.myPlayer.settings.CycleMode = this._cycelMode;
            _mc.PlayController.myPlayer.settings.RewindForwardTime = this._rfTime;
            #endregion

            #region Lyric
            //保存窗口歌词样式
            _mc.LyricManager.FLyric.Font = _formLyricFont;
            _mc.LyricManager.FLyric.PlayedColor = _formLyricPlayedColor;
            _mc.LyricManager.FLyric.Color = _formLyricColor;
            _mc.LyricManager.FLyric.PrepColor = _formLyricPrepplayColor;

            //保存桌面歌词样式
            _mc.LyricManager.DLyric.Font = _deskLyricFont;
            _mc.LyricManager.DLyric.PlayedColor = _deskLyricPlayedColor;
            _mc.LyricManager.DLyric.PrepColor = _deskLyricPrepplayColor;
            _mc.LyricManager.DLyric.Visible = _deskLyricVisible;//应用桌面歌词设置 应先保存桌面歌词样式
            #endregion
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            applySetting();
            Close();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            applySetting();
        }
        #endregion
    }
}