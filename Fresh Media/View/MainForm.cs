
using NgNet.UI;
using NgNet.UI.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FreshMedia.View
{
    partial class MainForm : TitleableForm
    {
        #region private fields
        private Controller.MainController _mc;
        // 启动画面线程
        private System.Threading.Thread _startBoxThread = null;
        // 启动窗口
        private StartBox _startBox;
        // 帮助窗口
        private NgNet.UI.Forms.TextBox userHelpBox;
        // 系统时间显示timer
        private Timer timeTimer;                              //formHelper
        // 音量调节窗口
        private VolumePanel volumePnl { get; }                                //音量面板
        // 通知消息类
        private HotNotice ntc { get; }
        //主窗口内小提示
        private PositionBar posiBar { get; }                                  //进度条
        //指示是否显示正在播放的列表
        private bool _showPlaying = false;
        // 程序退出提示窗口
        private ExitBox exitBox;                              //程序退出提示窗口
        #endregion

        #region public properties
        public bool IsActivated { get; private set; }      //判断主前窗体是否激活

        public ProgressMessageBox pcs { get; }                    //加载提示窗口
        #endregion

        #region constructor destructor 
        public MainForm(Controller.MainController _mc)
        {
            //允许跨线程操控控件
            //Control.CheckForIllegalCrossThreadCalls = false;
            //初始化窗体及控件
            InitializeComponent();
            this._mc = _mc;
            // 消息类提示
            ntc = new HotNotice(6000, ContentPanel);
            
            // 处理进度类
            pcs = new ProgressMessageBox(this);
            // 音量调整类
            volumePnl = new VolumePanel(this, _mc);
            //  进度条
            posiBar = new PositionBar(locbarLabel, toolTip, _mc);
            // 窗体圆角
            FormHelper.SetFormRoundRgn(3, 3);
            TaskbarSetWindowState = true;
            Resizeable = true;
            Name = ProductName;
            Text = Name;
            Icon = NgNet.ConvertHelper.Bitmap2Icon(Properties.Resources.apple_green);
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea; //移动窗体
            FormHelper.Move(ContentPanel);
            FormHelper.Move(labelLyric);
            // 设置任务栏菜单
            // setupSystemMenu();
            // 初始化自身
            initThis();
            initToolStripMenu();
        }
        #endregion

        #region override
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x312:
                    _mc.HotkeyManager.ProcessHotkey(m);
                    break;
                //任务栏菜单
                case WM_SYSCOMMAND:
                    // processSystemMenu(ref m);
                    break;
            }
        }

        private bool sureToClose()
        {
            bool ishide = false;
            if (_mc.Configs.ExitStyle == ExitStyles.MinsizeChoose
                || _mc.Configs.ExitStyle == ExitStyles.ExitChoose)//显示对话框
            {
                NgNet.UI.Forms.ExitStyles exitStyle;
                if (exitBox == null || exitBox.IsLoaded == false)
                {
                    exitBox = new NgNet.UI.Forms.ExitBox();
                    exitBox.SetTheme(_mc.Theme);
                    exitBox.BarColor = _mc.Theme.ButtonEnterColor;
        
                    exitBox.ExitStyle = _mc.Configs.ExitStyle;
                    exitBox.Title = null;
                    exitBox.Logo = Properties.Resources.apple_green;
                    exitStyle = exitBox.Show(this);
                }
                else
                {
                    exitBox.Focus();
                    return false;
                }
                switch (exitStyle)
                {
                    case ExitStyles.None:
                        return false;
                    case ExitStyles.ExitChoose:
                    case ExitStyles.ExitDirectly:
                        _mc.Configs.ExitStyle = exitStyle;
                        return true;
                    case ExitStyles.MinsizeChoose:
                    case ExitStyles.MinsizeDirectly:
                        _mc.Configs.ExitStyle = exitStyle;
                        ishide = true;
                        break;
                    default:
                        break;
                }
            }
            else if (_mc.Configs.ExitStyle == ExitStyles.ExitDirectly) //不显示对话框
                return true;
            else if (_mc.Configs.ExitStyle == ExitStyles.MinsizeDirectly)
                ishide = true;
            if (ishide)
                Hide();
            return false;
        }
        #endregion

        #region startBox
        private void showStartBox()
        {
            _startBoxThread = new System.Threading.Thread(() => {
                _startBox = new StartBox();
                _startBox.BackColor = _mc.Theme.BackColor;
                _startBox.BorderColor = _mc.Theme.BorderColor;
                _startBox.ForeColor = _mc.Theme.ForeColor;
                _startBox.Logo = Properties.Resources.back1;
                _startBox.Text = "欢 迎 使 用\r\n请稍候..";
                _startBox.Icon = NgNet.ConvertHelper.Bitmap2Icon(Properties.Resources.apple_green);
                _startBox.SendMessage("已经读取配置信息");
                if(_startBox.DialogResult == DialogResult.OK)
                {
                    Application.Exit();
                    Application.ExitThread();
                }
                _startBox.Show(null);
            });
            _startBoxThread.IsBackground = true;
            _startBoxThread.SetApartmentState(System.Threading.ApartmentState.STA);
            _startBoxThread.Start();
            while (_startBox == null || _startBox.IsLoaded == false)
                System.Threading.Thread.Sleep(10);

        }
        #endregion

        #region part 1 - 用户界面事件响应
        #region 控件初始化 <timer , menu , opactity ,command & tooltip>
        private void initToolStripMenu()
        {
            //设置tsmi_opacity子菜单
            ToolStripMenuItem tsmi = new ToolStripMenuItem("不透明", null, tsmi_opacity_items_Click);
            tsmi.Tag = 1d;
            this.tsmi_opacity.DropDownItems.Add(tsmi);
            for (int i = 1; i < 5; i++)
            {
                tsmi = new ToolStripMenuItem(string.Format("{0:D2} %", 100 - 5 * i), null, tsmi_opacity_items_Click);
                tsmi.Tag = 1d - 0.05d * i;
                tsmi.Checked = i == 3;
                this.tsmi_opacity.DropDownItems.Add(tsmi);
            }

            //设置tsmi_theme子菜单
            foreach (View.PresetTheme item in Enum.GetValues(typeof(View.PresetTheme)))
            {
                tsmi = new ToolStripMenuItem(Enum.GetName(typeof(View.PresetTheme), item), null, tsmi_theme_items_Click);
                tsmi.Tag = item;
                this.tsmi_theme.DropDownItems.Add(tsmi);
            }
            tsmi_theme.DropDownOpening += new EventHandler((object sender, EventArgs e) =>
            {
                foreach (ToolStripMenuItem item in tsmi_theme.DropDownItems)
                {
                    item.Checked = (View.PresetTheme)item.Tag == _mc.Theme.Current;
                }
            });
            //设置tsmi_cycelModel子菜单
            foreach (Player.CycleModes item in Enum.GetValues(typeof(Player.CycleModes)))
            {
                tsmi = new ToolStripMenuItem(item.ToString(), null, this.tsmi_cycelMode_items_Click);
                tsmi.Tag = item;
                tsmi_cm.DropDownItems.Add(tsmi);
            }
            this.tsmi_cm.DropDownOpening += new EventHandler((object sender, EventArgs e) =>
            {
                foreach (ToolStripMenuItem item in tsmi_cm.DropDownItems)
                {
                    item.Checked = _mc.PlayController.myPlayer.settings.CycleMode == (Player.CycleModes)item.Tag;
                }
            });
        }

        private void initOpacity(double opacity)
        {
            //Opacity = opacity;
            opacity -= 0.05;
            ContextMenuStrip[] cmss = new ContextMenuStrip[] { cms_main };
            foreach (ContextMenuStrip cms in cmss)
            {
                ContextMenuStripHelper.SetOpacity(cms, opacity);
            }
            MenuHelper.SetOpacity(menuStrip.Items, opacity);
        }

        private void initTimers()
        {
            #region timetimer
            //timeTimer
            byte _byte = 0;
            DateTime _now = DateTime.Now;
            DateTime _dateTime = new DateTime();
            TimeSpan _timeSpan = new TimeSpan();
            NgNet.Date.CNDate cnDate = new NgNet.Date.CNDate(DateTime.Now);
            timeTimer = new System.Windows.Forms.Timer();
            timeTimer.Interval = 5200;
            timeTimer.Enabled = true;
            #region timeTimer.Tick
            timeTimer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                if (DateTime.Today.Date != _now.Date)
                    _now = DateTime.Now;
                cnDate.Reset(_now);

                if (_byte == 0)//显示公历日期-时间-中国时间（天干地支）
                {
                    timeLabel.Text = string.Format("公元 {0}\n{1} {2} {3}",
                        _now.ToShortDateString(),
                        DateTime.Now.ToShortTimeString(),
                        cnDate.WeekDayString,
                        cnDate.Constellation);
                    _byte++;
                }
                else if (_byte == 1) //显示农历日期-十二生肖
                {
                    string _lunarYear = cnDate.IsLunarLeapYear ? "闰年" : "";
                    timeLabel.Text = string.Format("农历 {0}\n{1}年  {2}",
                        cnDate.LunarDateString,
                        cnDate.AnimalString,
                        _lunarYear);
                    _byte++;
                }
                else if (_byte == 2)
                {
                    timeLabel.Text = string.Format("{0} {1}\n{2}",
                        cnDate.GanZhiDateString,
                        cnDate.ChineseHour,
                        cnDate.ChineseConstellation);
                    _byte++;
                }
                else if (_byte == 3)// 显示二十四节气
                {
                    string _temp = cnDate.ChineseTwentyFourDayString;
                    int tmp;
                    if (string.IsNullOrWhiteSpace(_temp))
                    {
                        _temp = cnDate.LastChineseTwentyFourDay(out tmp, out _dateTime);
                        _timeSpan = DateTime.Now - _dateTime;
                        _temp = string.Format("{0}<第{1}天>",
                            _temp,
                            _timeSpan.Days + 1);
                        string nextTf = cnDate.NextChineseTwentyFourDay(out tmp, out _dateTime);
                        _timeSpan = _dateTime - DateTime.Now;
                        _temp += string.Format(" {0}<{1}天后>",
                            nextTf,
                            _timeSpan.Days);
                    }
                    timeLabel.Text = string.Format("{0}\n",
                        _temp);
                    _byte++;
                }
                else if (_byte == 4)// 显示节日
                {
                    string tmpHolidays = string.Empty;
                    if (!string.IsNullOrWhiteSpace(cnDate.DateHoliday))
                        tmpHolidays = cnDate.DateHoliday;
                    if (!string.IsNullOrWhiteSpace(cnDate.LunarHoliday))
                    {
                        if (string.IsNullOrWhiteSpace(tmpHolidays))
                            tmpHolidays = cnDate.LunarHoliday;
                        else
                            tmpHolidays = string.Format("{0}; {1}",
                                tmpHolidays,
                                cnDate.LunarHoliday);
                    }
                    if (!string.IsNullOrWhiteSpace(cnDate.WeekDayHoliday))
                    {
                        if (string.IsNullOrWhiteSpace(tmpHolidays))
                            tmpHolidays = cnDate.WeekDayHoliday;
                        else
                            tmpHolidays = string.Format("{0}; {1}",
                                tmpHolidays,
                                cnDate.WeekDayHoliday);
                    }
                    if (string.IsNullOrWhiteSpace(tmpHolidays))
                    {
                        tmpHolidays = cnDate.NextHolidays(out _dateTime);
                        _timeSpan = _dateTime - DateTime.Now;
                        int tmpInt = _timeSpan.Days + 1;
                        tmpHolidays = string.Format("{0}<{1}>",
                            tmpHolidays,
                            tmpInt == 1 ? "明天" : (tmpInt == 2 ? "后天" : tmpInt.ToString() + "天后"));
                    }
                    timeLabel.Text = tmpHolidays;
                    _byte = 0;
                }
            });

            #endregion

            #region timeLabel
            timeLabel.Text = null;
            timeLabel.DoubleClick += new EventHandler((object sender, EventArgs e) =>
            {
                timeTimer.Enabled = !timeTimer.Enabled;
            });
            #endregion
            #endregion

            #region timeLabel
            timeLabel.DoubleClick += new EventHandler((object sender, EventArgs e) =>
            {
                timeTimer.Enabled = !timeTimer.Enabled;
            });
            #endregion

            #region sleepLabel
            sleepLabel.SizeChanged += new EventHandler(sleepLabel_SizeChanged);
            #endregion
        }

        private void initCommondAndTooltip()
        {
            //播放控制命令
            playLastButton.Tag = Controller.Commands.PC_PLAYLAST;
            playNextButton.Tag = Controller.Commands.PC_PLAYNEXT;
            playPauseButton.Tag = Controller.Commands.PC_PLAYPAUSE;
            volUpLabel.Tag = Controller.Commands.PC_VOLUMEUP;
            volDownLabel.Tag = Controller.Commands.PC_VOLUMEDOWN;
            volumeLabel.Tag = Controller.Commands.PC_VOLUMEMUTE;
            //设置控件提示
            toolTip.SetToolTip(playLastButton, string.Format("{0}({1}) ", _mc.HotkeyManager.PlayLast.Name, _mc.HotkeyManager.PlayLast.Value));
            toolTip.SetToolTip(playNextButton, string.Format("{0}({1}) ", _mc.HotkeyManager.PlayNext.Name, _mc.HotkeyManager.PlayNext.Value));
            toolTip.SetToolTip(playPauseButton, string.Format("{0}({1}) ", _mc.HotkeyManager.PausePlay.Name, _mc.HotkeyManager.PausePlay.Value));
            toolTip.SetToolTip(volDownLabel, string.Format("{0}({1}) ", _mc.HotkeyManager.VolumeDown.Name, _mc.HotkeyManager.VolumeDown.Value));
            toolTip.SetToolTip(volUpLabel, string.Format("{0}({1}) ", _mc.HotkeyManager.VolumeUp.Name, _mc.HotkeyManager.VolumeUp.Value));
            toolTip.SetToolTip(volumeLabel, "静音");
            toolTip.SetToolTip(searchPicBox, string.Format("搜索({0}) ", Controller.HotkeysManager.SK_LIST_SEARCH));
            toolTip.SetToolTip(locPicBox, "定位音乐");
            toolTip.SetToolTip(cmPicBox, "切换循环模式");
        }

        private void initThis()
        {
            // 自动调整padding
            AutoBorder();
            TitleBar.Cms = cms_main;
            aNameLabel.Height = aNameLabel.Font.Height * 2;
            aTimeLabel.Padding = new Padding(0, (aNameLabel.Height - aTimeLabel.Height)/2, 0, (aNameLabel.Height - aTimeLabel.Height) / 2);
        }
        #endregion

        #region toolStripMenu
        #region menuStrip(主菜单)
        private void tsmi_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmi_openFile_Click(object sender, EventArgs e)
        {
            string scrpath = null;
            //判断上次打开的路径是否为空
            if (Directory.Exists(_mc.OpenHistoryManager.LastOpenFolder) == false)
                _mc.OpenHistoryManager.LastOpenFolder = NgNet.Windows.SpecialFolders.MyMusic;
            else
            {
                NgNet.UI.Forms.OpenFileDialog od = new NgNet.UI.Forms.OpenFileDialog();
                od.Filter = Player.Types.FILTER;
                od.FilterIndex = 0;
                od.Enterpath = _mc.OpenHistoryManager.LastOpenFolder;
                od.SetTheme(_mc.Theme);
                od.Title = "选择喜欢的音乐";
                scrpath = od.Show(this);
            }
            //返回路劲为空就退出
            if (String.IsNullOrWhiteSpace(scrpath))
                return;
            else
            {
                //临时播放选择的音乐     
                _mc.PlayController.InterimPlay(scrpath);
                //刷新打开文件夹列表
                _mc.OpenHistoryManager.AddFolder(System.IO.Path.GetDirectoryName(scrpath));
            }
        }

        private void tsmi_search_Click(object sender, EventArgs e)
        {
            picBoxs_Click(searchPicBox, e);
        }

        private void tsmi_cycelMode_items_Click(object sender, EventArgs e)
        {
            _mc.PlayController.myPlayer.settings.CycleMode = (Player.CycleModes)((ToolStripMenuItem)sender).Tag;
        }

        private void tsmi_about_Click(object sender, EventArgs e)
        {
            NgNet.UI.Forms.AboutBox.Show(this, Properties.Resources.apple_green);
        }

        private void tsmi_userHelp_Click(object sender, EventArgs e)
        {
            String help = "欢迎使用！";
            help += "\r\n" + NgNet.Text.Pinyin.GetPinyin(help);
            if (null == this.userHelpBox || this.userHelpBox.IsLoaded == false)
            {

                this.userHelpBox = new NgNet.UI.Forms.TextBox();
                this.userHelpBox.CurrentText = help;
                this.userHelpBox.Editable = false;
                this.userHelpBox.UserMenus = new ToolStripMenuItem[] {  };
                this.userHelpBox.Icon = this.Icon;
                this.userHelpBox.FixedTitle = string.Format("帮助 - {0}", Application.ProductName);
                this.userHelpBox.Blendable = _mc.Theme.Blendable;
                this.userHelpBox.BackColor = BackColor;
                this.userHelpBox.ForeColor = ForeColor;
                this.userHelpBox.BorderColor = BorderColor;
            }
            this.userHelpBox.Show(this);
        }

        private void tsmi_theme_items_Click(object sender, EventArgs e)
        {
            _mc.Theme.Current = (PresetTheme)(((ToolStripMenuItem)sender).Tag);
        }

        private void tsmi_sleep_Click(object sender, EventArgs e)
        {
            if (_mc.SleepMode.IsSleeping)
            {
                _mc.SleepMode.Stop();
            }
            else
            {
                uint stime = _mc.SleepMode.DiySleeptimeDialog(this, _mc.SleepMode.SleepTime);
                if (stime == 0) { return; }
                _mc.SleepMode.Start(stime);
                _mc.NotiryIcon.ShowNotice(8, "~﹏~", string.Format("已启用睡眠模式 <{0} min>", stime), ToolTipIcon.Info);
            }
        }

        private void tsmi_dLyric_Click(object sender, EventArgs e)
        {
            _mc.LyricManager.DLyric.Visible = !_mc.LyricManager.DLyric.Visible;
        }

        private void tsmi_moreSet_Click(object sender, EventArgs e)
        {
            _mc.ShowConfigDialog(VSetting.SettingTabs.General, 0);
        }

        private void tsmi_history_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi0 = (ToolStripMenuItem)sender;
            ToolStripMenuItem tsmi1;
            tsmi0.DropDownItems.Clear();
            foreach (string item in _mc.OpenHistoryManager.FolderOpenHistory)
            {
                if (!tsmi0.DropDownItems.ContainsKey(item))
                {
                    tsmi1 = new ToolStripMenuItem();
                    tsmi1.Text = item;
                    tsmi1.Name = item;
                    tsmi1.Click += new EventHandler((object sender1, EventArgs e1) =>
                    {
                        ToolStripItem tsi = (ToolStripItem)sender1;
                        string scrpath = NgNet.UI.Forms.OpenFileDialog.Show(this, tsi.Name, Player.Types.FILTER, 0, "选择喜欢的音乐");
                        if (string.IsNullOrWhiteSpace(scrpath))
                            return;
                        else
                        {
                            //临时播放选择的音乐     
                            _mc.PlayController.InterimPlay(scrpath);
                            _mc.OpenHistoryManager.AddFolder(System.IO.Path.GetDirectoryName(scrpath));//存储最近打开列表 并更新历史菜单
                        }
                    });
                    tsmi0.DropDownItems.Add(tsmi1);
                }
                else
                {
                    _mc.OpenHistoryManager.FolderOpenHistory.Remove(item);
                }
            }
            tsmi1 = new ToolStripMenuItem();
            tsmi1.Text = " 清空打开历史";
            tsmi1.Click += new EventHandler((object sender1, EventArgs e1) =>
            {
                if (NgNet.UI.Forms.MessageBox.Show(this, "是否清空最近使用过的目录？", "清空目录历史", MessageBoxButtons.YesNo, DialogResult.No) != DialogResult.Yes)
                    return;
                _mc.OpenHistoryManager.FolderOpenHistory.Clear();
                this.tsmi_history.DropDownItems.Clear();
                this.tsmi_history.DropDownItems.Add(tsmi1);
            });
            tsmi0.DropDownItems.Add(tsmi1);
        }

        private void tsmi_option_DropDownOpening(object sender, EventArgs e)
        {
            //添加项
            this.tsmi_option.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmi_cm,
                this.tsmi_theme,
                this.tsmi_sleep,
                this.tsmi_dLyric,
                this.tsmi_moreSet});

            //睡眠模式
            this.tsmi_sleep.Checked = _mc.SleepMode.IsSleeping;
            this.tsmi_dLyric.Checked = _mc.LyricManager.DLyric.Visible;
            this.tsmi_sleep.Text = _mc.SleepMode.IsSleeping ? ControlTexts.SLEEPMODEL_ON : ControlTexts.SLEEPMODEL_OFF;
            this.tsmi_dLyric.Text = _mc.LyricManager.DLyric.Visible ? ControlTexts.DESKTOPLYRIC_ON : ControlTexts.DESKTOPLYRIC_OFF;
        }

        private void tsmi_file_DropDownOpening(object sender, EventArgs e)
        {
            this.tsmi_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmi_openFile,
                this.tsmi_history,
                this.tsmi_exit,
                this.tsmi_search});
        }

        private void tsmi_help_DropDownOpening(object sender, EventArgs e)
        {
            this.tsmi_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsmi_userHelp,
                this.tsmi_about});
        }
        #endregion

        #region cms_main
        //cms_main
        private void tsmi_fLyric_Click(object sender, EventArgs e)
        {
            _mc.LyricManager.FLyric.Visible = !_mc.LyricManager.FLyric.Visible;
        }

        private void tsmi_showPlaying_Click(object sender, EventArgs e)
        {
            this.showPlayingOrAudioInf = !showPlayingOrAudioInf;
        }

        private void tsmi_opacity_items_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in this.tsmi_opacity.DropDownItems)
            {
                item.Checked = false;
            }
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            _mc.Theme.Opacity = (double)tsmi.Tag;
            tsmi.Checked = true;
        }

        private void cms_main_Opening(object sender, CancelEventArgs e)
        {
            this.cms_main.Items.Clear();
            //快捷菜单项更新
            this.cms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.tsmi_openFile,
                    this.tsmi_history,
                    this.tsmi_exit});
            this.cms_main.Items.Add("-");
            this.cms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    this.tsmi_cm,
                    this.tsmi_theme,
                    this.tsmi_sleep,
                    this.tsmi_dLyric,
                    this.tsmi_opacity ,
                    this.tsmi_moreSet});
            this.cms_main.Items.Add("-");
            this.cms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[]{
                    this.tsmi_fLyric,
                    this.tsmi_showCurList});

            //更新信息

            this.tsmi_sleep.Checked = _mc.SleepMode.IsSleeping;
            this.tsmi_dLyric.Checked = _mc.LyricManager.DLyric.Visible;
            this.tsmi_fLyric.Text = _mc.LyricManager.FLyric.Visible ? ControlTexts.FORMLYRIC_ON : ControlTexts.FORMLYRIC_OFF;
            this.tsmi_showCurList.Text = showPlayingOrAudioInf ? ControlTexts.CURRENTLIST_ON : ControlTexts.CURRENTLIST_OFF;
            this.tsmi_sleep.Text = _mc.SleepMode.IsSleeping ? ControlTexts.SLEEPMODEL_ON : ControlTexts.SLEEPMODEL_OFF;
            this.tsmi_dLyric.Text = _mc.LyricManager.DLyric.Visible ? ControlTexts.DESKTOPLYRIC_ON : ControlTexts.DESKTOPLYRIC_OFF;
        }
        //panel_main
        private void ContentPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                this.cms_main.Show(Cursor.Position);
        }
        #endregion
        #endregion

        #region  播放控制
        #region volume setup
        private void volCtlLabels_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Tag.ToString())
            {
                case Controller.Commands.PC_VOLUMEUP:
                    _mc.PlayController.myPlayer.ctControls.volUp();
                    break;
                case Controller.Commands.PC_VOLUMEDOWN:
                    _mc.PlayController.myPlayer.ctControls.volDown();
                    break;
            }
        }

        private void volCtlLabels_DoubleClick(object sender, EventArgs e)
        {
            switch (((Label)sender).Tag.ToString())
            {
                case Controller.Commands.PC_VOLUMEUP:
                    _mc.PlayController.myPlayer.settings.Volume += 4;
                    break;
                case Controller.Commands.PC_VOLUMEDOWN:
                    _mc.PlayController.myPlayer.settings.Volume -= 4;
                    break;
            }
        }

        private void valLabel_Click(object sender, EventArgs e)
        {
            if (this.volumePnl.IsLoaded)
                this.volumePnl.Close();
            else
                this.volumePnl.Show(((Control)sender).PointToScreen(new Point(((Control)sender).Width / 2, 0)));
        }
        #endregion

        #region switch audio
        //切换歌曲暂停
        public void playCtlButtons_Click(object sender, EventArgs e)
        {
            string tagString = null;
            //区别对待操作来源
            if (sender is Control)
                tagString = ((Control)sender).Tag.ToString();
            else if (sender is ToolStripMenuItem)
                tagString = ((ToolStripMenuItem)sender).Tag.ToString();
            //辨别是上下
            switch (tagString)
            {
                case Controller.Commands.PC_PLAYLAST:
                    _mc.PlayController.myPlayer.ctControls.last();
                    break;
                case Controller.Commands.PC_PLAYPAUSE:
                    _mc.PlayController.PlayOrPause();
                    break;
                case Controller.Commands.PC_PLAYNEXT:
                    _mc.PlayController.myPlayer.ctControls.next();
                    break;
            }

        }
        #endregion      
        #endregion

        #region this （主窗体）
        private void this_Load(object sender, EventArgs e)
        {
            Hide();
            #region 事件绑定
            _mc.PlayController.myPlayer.VolumeChangedEvent += new Player.VolumeChangedEventHandler(volumeChangedEvent);
            _mc.PlayController.myPlayer.PlayStateChangedEvent += new Player.PlayStateChangedEventHandler(playStateChangeEvent);
            _mc.PlayController.myPlayer.MuteStateChangedEvent += new Player.MuteStateChangedEventHandler(muteStateChangedEvent);
            _mc.PlayController.myPlayer.CycleModeChangedEvent += new Player.CycleModeChangedEventHandler(cycleModeChangedEvent);
            _mc.PlayController.myPlayer.URLChangedEvent += new Player.URLChangedEventHandler(uRLChangedEvent);
            _mc.PlayController.myPlayer.CurrentPositionChangedEvent += new Player.CurrentPositionChangedEventHandler(currentPositionChangedEvent);
            _mc.PlayController.myPlayer.PlayErrorEvent += new Player.PlayErrorEventHandler(Error.ErrorHandle.MCIErrorEvent);
            _mc.PlayController.myPlayer.MediaListChangedEvent += new Player.MediaListChangedEventHandler(mediaListChangedEvnet);
            //自动应用主题
            _mc.Theme.ThemeChanged += new ThemeChangedEventHandler(themeChangedEvent);
            FormClosed += new FormClosedEventHandler((object sender1, FormClosedEventArgs e1) =>
            {
                _mc.Theme.ThemeChanged -= new ThemeChangedEventHandler(themeChangedEvent);
            });
            _mc.LyricManager.FLyric.VisibleChangedEvent += new Lyric.LyricVisibleChangedEventHandler(showFLyricOrList);
            // 处理列表加载事件
            _mc.ListViewManager.MyListView.BeginLoadingListEvent += new BeginLoadingListEventHandler((ListLoadingEventArgs e1) => { pcs.Show(e1.Message); });
            _mc.ListViewManager.MyListView.StopLoadingListEvent += new StopLoadingListEventHandler(() => { pcs.Close(); });
            _mc.ListViewManager.MySearchBox.BeginSearchingListEvent += new BeginLoadingListEventHandler((ListLoadingEventArgs e1) => { pcs.Show(e1.Message); });
            _mc.ListViewManager.MySearchBox.StopSearchingListEvent += new StopLoadingListEventHandler(() => { pcs.Close(); });

            SizeChanged += new EventHandler(this_SizeChanged);
            
            #endregion

            #region 读取程序配置及主题
            if (_mc.DataManager.AppConfigRead(_mc) == false)
            {
                _mc.DataManager.AppConfigCreate(_mc);
                _mc.DataManager.AppConfigRead(_mc);
            }
            if (_mc.DataManager.ThemeRead(_mc.Theme) == false)
            {
                _mc.DataManager.ThemeCreate(_mc.Theme);
                _mc.DataManager.ThemeRead(_mc.Theme);
            }
            #endregion
            //
            //启动画面        
            if (_mc.Configs.StartboxEnable)
                showStartBox();

            if (_mc.Configs.StartboxEnable)
                _startBox.SendMessage("正在初始化。。。"); 

            // 设置窗口歌词显示的Label
            _mc.LyricManager.FLyric.SetLabel(labelLyric);
            _mc.LyricManager.DLyric.SetDesktopLyricF(typeof(DesktopLyricF));
            // 设置睡眠模式剩余时间显示控件
            _mc.SleepMode.SetShowControl(sleepLabel);
            // 将列表显示控件添加到窗体
            listSplitContainer.Panel1.Controls.Add(_mc.ListViewManager.MyLibView.LibTreeView);


            initCommondAndTooltip();

            #region 读取歌词配置及打开历史
            if (_mc.Configs.StartboxEnable)
                _startBox.SendMessage("正在读取数据"); 
            if (_mc.DataManager.LyricRead(_mc.LyricManager)== false)
            {
                _mc.DataManager.LyricCreate(_mc.LyricManager);
                _mc.DataManager.LyricRead (_mc.LyricManager);
            }
            if(_mc.DataManager.OpenHistoryRead(_mc.OpenHistoryManager) == false)
            {
                _mc.DataManager.OpenHistoryCreate(_mc.OpenHistoryManager);
                _mc.DataManager.OpenHistoryRead(_mc.OpenHistoryManager);
            }
            #endregion
            //
            #region 读取媒体列表
            //读取音乐列表文件
            if (_mc.Configs.StartboxEnable)
                _startBox.SendMessage("正在加载列表");
            //初始化本地列表数据库
            _mc.DataManager.libLocalRead(_mc.MyLists);
            _mc.DataManager.libHistoryRead(_mc.MyLists);
            _mc.DataManager.libMostlyPlayedRead(_mc.MyLists);
            _mc.DataManager.libRecentlyAddedRead(_mc.MyLists);
            _mc.DataManager.libFavoriteRead(_mc.MyLists);
            _mc.DataManager.libCurrentRead(_mc.MyLists);
            _mc.DataManager.InfoCocahRead();
            #endregion

            _mc.ListViewManager.MyLibView.LibTreeView.ExpandAll();
            //设置首次进入时显示的列表
            if (_mc.Configs.StartboxEnable)
                _startBox.SendMessage("欢迎使用！"); 
         
            // 关闭启动画面
            if (_mc.Configs.StartboxEnable)
                 _startBox.Close();            //Hide();
            this_SizeChanged(sender, e);
            controlSplitContainer_SizeChanged(sender, e);
            controlSplitContainerContentPanel1_SizeChanged(sender, e);
            splitContainer1ContentPanel2_SizeChanged(sender, e);

        }

        private void this_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mc.DataManager.ThemeCreate(_mc.Theme);
            _mc.DataManager.LyricCreate(_mc.LyricManager);
            _mc.DataManager.libLocalCreat(_mc.MyLists);
            _mc.DataManager.libCurrentCreat(_mc.MyLists);
            _mc.DataManager.libFavoriteCreat(_mc.MyLists);
            _mc.DataManager.libHistoryCreat(_mc.MyLists);
            _mc.DataManager.libRecentlyAddedCreat(_mc.MyLists);
            _mc.DataManager.libMostlyPlayedCreat(_mc.MyLists);
            _mc.DataManager.InfoCocahCreat();
            _mc.DataManager.AppConfigCreate(_mc);

            _mc.DataManager.OpenHistoryCreate(_mc.OpenHistoryManager);
            _mc.DataManager.HotkeysCreat(_mc.HotkeyManager);
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.TaskManagerClosing
                || e.CloseReason == CloseReason.UserClosing
                || e.CloseReason == CloseReason.WindowsShutDown)
            {
                e.Cancel = !sureToClose();
                return;
            }
        }

        private void this_SizeChanged(object sender, EventArgs e)
        { 
            //搜索定位循环模式 - 工具
            cmPicBox.Top = ContentPanel.Height - cmPicBox.Height - 4;
            locPicBox.Top = cmPicBox.Top;
            searchPicBox.Top = cmPicBox.Top;
            //窗体大小
            _mc.Configs.StartSize = Size;
            //音乐列表
            mainSplitContainer.Location = new Point(16, menuStrip.Bottom + 4);
            mainSplitContainer.Size = new Size(ContentPanel.Width - mainSplitContainer.Left * 2, cmPicBox.Top - mainSplitContainer.Top - 4);



            timeLabel_SizeChanged(sender, e);
        }

        private void this_Deactivate(object sender, EventArgs e)
        {
            this.IsActivated = false;
        }

        private void this_Activated(object sender, EventArgs e)
        {
            this.IsActivated = true;
        }

        private void this_Shown(object sender, EventArgs e)
        {
            Activate();
            _mc.LyricManager.FLyric.Visible = true;
            _mc.LyricManager.FLyric.Visible = false;
            // 初始化界面
            initTimers();
            // 初始化歌词面板大小
            splitContainer1ContentPanel2_SizeChanged(sender, e);
            // 注册快捷键
            _mc.HotkeyManager.Register();
            // 自动播放，有参数命令启动时无效
            autoPlay();
            // runCommandArgs           
            responseCommandArgs();
            //new View.Settings.Settings(_controller).ShowDialog();

        }
        #endregion

        #region  音乐切换按钮、音量切换按钮界面控制
        private void ctlButton_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).ForeColor = Color.DarkGreen;
        }

        private void ctlButton_MouseDown(object sender, MouseEventArgs e)
        {
            ((Control)sender).ForeColor = Color.DarkRed;
        }

        private void ctlButton_MouseEnter(object sender, EventArgs e)
        {
            ((Control)sender).ForeColor = Color.DeepPink;
        }
        #endregion

        #region 搜索 、 切换循环模式 、定位到当前播放
        private void picBoxs_Click(object sender, EventArgs e)
        {
            //搜索
            if (sender == this.searchPicBox)
            {
                _mc.ListViewManager.MySearchBox.Show();
            }
            //定位
            else if (sender == this.locPicBox)
            {
                if (string.IsNullOrWhiteSpace(_mc.PlayController.myPlayer.currentURL))
                    ntc.Show("当前未播放音乐");
                //如果是临时播放则返回
                else if (_mc.PlayController.myPlayer.IsInterimPlay)
                    ntc.Show("正在临时播放");
                else
                    if (_mc.ListViewManager.MyListView.GotoCurrentItem())
                    showPlayingOrAudioInf = true;

            }
            //循环模式
            else if (sender == this.cmPicBox)
            {
                ContextMenuStrip cms = new ContextMenuStrip();
                ToolStripMenuItem tsmi;
                cms.Opacity = this.Opacity;
                cms.Font = this.Font;
                cms.Renderer = _mc.Theme.MenuRender;
                foreach (Player.CycleModes item in Enum.GetValues(typeof(Player.CycleModes)))
                {
                    tsmi = new ToolStripMenuItem(item.ToString(), null, this.tsmi_cycelMode_items_Click);
                    tsmi.Tag = item;
                    cms.Items.Add(tsmi);
                }
                cms.Opening += new CancelEventHandler((object sender1, CancelEventArgs e1) =>
                {
                    foreach (ToolStripMenuItem item in cms.Items)
                    {
                        item.Checked = _mc.PlayController.myPlayer.settings.CycleMode == (Player.CycleModes)item.Tag;
                    }
                });

                cms.Show(cmPicBox.PointToScreen(new Point(cmPicBox.Width, -cms.Height)));
            }
        }

        private void locPicBox_DoubleClick(object sender, EventArgs e)
        {
            showPlayingOrAudioInf = false;
        }

        private void picBoxs_MouseEnter(object sender, EventArgs e)
        {
            if (sender == this.searchPicBox)
            {
                searchPicBox.BackgroundImage = Properties.Resources.search_Down;
            }
            else if (sender == this.locPicBox)
            {
                locPicBox.Image = Properties.Resources.location_down;
                locPicBox.ForeColor = Color.LightPink;
            }
            else if (sender == this.cmPicBox)
            {
                switch (_mc.PlayController.myPlayer.settings.CycleMode)
                {
                    case Player.CycleModes.OneOnce:
                        cmPicBox.Text = "→";
                        this.toolTip.SetToolTip(locbarLabel, "单曲播放");
                        break;
                    case Player.CycleModes.OneCycle:
                        cmPicBox.BackgroundImage = Properties.Resources.playmode_repeat_single_down;
                        this.toolTip.SetToolTip(locbarLabel, "单曲循环");
                        break;
                    case Player.CycleModes.AllOnce:
                        cmPicBox.BackgroundImage = Properties.Resources.playmode_sequence_down;
                        this.toolTip.SetToolTip(locbarLabel, "顺序播放");
                        break;
                    case Player.CycleModes.AllCycle:
                        cmPicBox.BackgroundImage = Properties.Resources.playmode_repeat_all_Down;
                        this.toolTip.SetToolTip(locbarLabel, "列表循环");
                        break;
                    case Player.CycleModes.Random:
                        cmPicBox.BackgroundImage = Properties.Resources.playmode_random_down;

                        this.toolTip.SetToolTip(locbarLabel, "随机播放");
                        break;
                    default:
                        break;
                }
            }
        }

        private void picBoxs_MouseLeave(object sender, EventArgs e)
        {
            if (sender == this.searchPicBox)
            {
                searchPicBox.BackgroundImage = Properties.Resources.search;
            }
            else if (sender == this.locPicBox)
            {
                locPicBox.Image = Properties.Resources.location;
                locPicBox.ForeColor = Color.Teal;
            }
            else if (sender == this.cmPicBox)
            {
                switch (_mc.PlayController.myPlayer.settings.CycleMode)
                {
                    case Player.CycleModes.OneOnce:
                        cmPicBox.Text = "→";
                        break;
                    case Player.CycleModes.OneCycle:
                        cmPicBox.BackgroundImage = Properties.Resources.playmode_repeat_single;
                        break;
                    case Player.CycleModes.AllOnce:
                        cmPicBox.BackgroundImage = Properties.Resources.playmode_sequence;
                        break;
                    case Player.CycleModes.AllCycle:
                        cmPicBox.BackgroundImage = Properties.Resources.playmode_repeat_all;
                        break;
                    case Player.CycleModes.Random:
                        cmPicBox.BackgroundImage = Properties.Resources.playmode_random;
                        break;
                    default:
                        break;
                }
            }

        }

        private void cmPicBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch (_mc.PlayController.myPlayer.settings.CycleMode)
            {
                case Player.CycleModes.OneOnce:
                    cmPicBox.Text = "→";
                    break;
                case Player.CycleModes.OneCycle:
                    cmPicBox.BackgroundImage = Properties.Resources.playmode_repeat_single_down;
                    break;
                case Player.CycleModes.AllOnce:
                    cmPicBox.BackgroundImage = Properties.Resources.playmode_sequence_down;
                    break;
                case Player.CycleModes.AllCycle:
                    cmPicBox.BackgroundImage = Properties.Resources.playmode_repeat_all_Down;
                    break;
                case Player.CycleModes.Random:
                    cmPicBox.BackgroundImage = Properties.Resources.playmode_random_down;
                    break;
                default:
                    break;
            }
            toolTip.SetToolTip(locbarLabel, NgNet.EnumHelper.GetEnumDescription(_mc.PlayController.myPlayer.settings.CycleMode));
        }
        #endregion

        #region 信息展示 + 功能提示
        private void toolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            CommControls.CommSolidBrush.Color = _mc.Theme.ForeColor;
            CommControls.CommPen.Color = _mc.Theme.BorderColor;
            CommControls.CommPointF.X = 0;
            CommControls.CommPointF.Y = 0;
            ((ToolTip)sender).BackColor = _mc.Theme.BackColor;
            e.DrawBackground();
            e.Graphics.DrawRectangle(CommControls.CommPen, 0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);
            e.Graphics.DrawString(e.ToolTipText, this.Font, CommControls.CommSolidBrush, CommControls.CommPointF);
        }

        private void picBox_head_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (File.Exists(_mc.PlayController.myPlayer.currentURL))
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    _mc.ShowMediaInfoBox(_mc.PlayController.myPlayer.currentURL, List.MyLib.Playing, List.ListManager.NAME_LIB_CURRENT);
                }
            }
        }

        private void sleepLabel_SizeChanged(object sender, EventArgs e)
        {
            sleepLabel.Left = timeLabel.Left - sleepLabel.Width - 8;
        }

        private void timeLabel_SizeChanged(object sender, EventArgs e)
        {
            timeLabel.Left = mainSplitContainer.Right - timeLabel.Width;
            // 系统时间 + 睡眠剩余时间
            sleepLabel.Left = timeLabel.Left - sleepLabel.Width - 8;
        }
        #endregion

        #region splitContainer
        private void splitContainer1ContentPanel2_SizeChanged(object sender, EventArgs e)
        {
            labelLyric.Location = new Point(0, 0);
            labelLyric.Size = listSplitContainer.Panel2.ClientSize;
        }

        private void controlSplitContainerContentPanel1_SizeChanged(object sender, EventArgs e)
        {
            aTimeLabel.Left = 4;
            aTimeLabel.Top = 4;
            //splitcontainer2.panel
            aNameLabel.Width = controlSplitContainer.Panel1.Width - aTimeLabel.Width - 16;
            aNameLabel.Left = aTimeLabel.Right + 8;
            aNameLabel.Top = aTimeLabel.Top;
            //音量
            volUpLabel.Location = new Point(
                controlSplitContainer.Panel1.Width - volUpLabel.Width - 4, 
                controlSplitContainer.Panel1.Height - volUpLabel.Height - 4);
            volumeLabel.Location = new Point(
                volUpLabel.Left - volumeLabel.Width,
                volUpLabel.Top + (volUpLabel.Height - volumeLabel.Height) / 2);
            volDownLabel.Location = new Point(
                volumeLabel.Left - volDownLabel.Width, 
                volUpLabel.Top);

            locbarLabel.Left = 8;
            locbarLabel.Width = controlSplitContainer.Panel1.Width - locbarLabel.Left * 2;
            aLengthLabel.Top = aNameLabel.Bottom + 4;
            aIndexLabel.Top = aLengthLabel.Top;
            aKbpsLabel.Top = aLengthLabel.Top;
            aLengthLabel.Left = controlSplitContainer.Panel1.Width - aLengthLabel.Width - 4;
            aIndexLabel.Left = aLengthLabel.Left - aIndexLabel.Width - 16;
            aKbpsLabel.Left = aIndexLabel.Left - aKbpsLabel.Width - 16;
        
            playLastButton.Top = controlSplitContainer.Panel1.Height - playLastButton.Height - 4;
            playPauseButton.Top = playLastButton.Top;
            playNextButton.Top = playLastButton.Top;
            locbarLabel.Top = playLastButton.Top - 16 - locbarLabel.Height;

            playLastButton.Left = aTimeLabel.Left;
            playPauseButton.Left = playLastButton.Right;
            playNextButton.Left = playPauseButton.Right;
        }

        private void controlSplitContainer_SizeChanged(object sender, EventArgs e)
        {
            if (controlSplitContainer.SplitterDistance > controlSplitContainer.Width - controlSplitContainer.Panel2MinSize)
            {
                if (WindowState != FormWindowState.Minimized)
                    controlSplitContainer.SplitterDistance = controlSplitContainer.Width - controlSplitContainer.Panel2MinSize;
            }
        }

        private void splitContainer1ContentPanel1_Paint(object sender, PaintEventArgs e)
        {
            CommControls.CommPen.Width = 1;
            CommControls.CommPen.Color = _mc.Theme.BorderColor;
            e.Graphics.DrawRectangle(CommControls.CommPen, 0, 0, listSplitContainer.Panel1.Width - 1, listSplitContainer.Panel1.Height - 1);
        }

        private void splitContainer1ContentPanel2_Paint(object sender, PaintEventArgs e)
        {
            CommControls.CommPen.Width = 1;
            CommControls.CommPen.Color = _mc.Theme.BorderColor;
            e.Graphics.DrawRectangle(CommControls.CommPen, 0, 0, listSplitContainer.Panel2.Width - 1, listSplitContainer.Panel2.Height - 1);
        }

        private void controlSplitContainerContentPanel1_Paint(object sender, PaintEventArgs e)
        {
            CommControls.CommPen.Width = 1;
            CommControls.CommPen.Color = _mc.Theme.BorderColor;
            e.Graphics.DrawRectangle(CommControls.CommPen, 0, 0, controlSplitContainer.Panel1.Width - 1, controlSplitContainer.Panel1.Height - 1);
        }

        private void controlSplitContainerContentPanel2_Paint(object sender, PaintEventArgs e)
        {
            CommControls.CommPen.Width = 1;
            CommControls.CommPen.Color = _mc.Theme.BorderColor;
            e.Graphics.DrawRectangle(CommControls.CommPen, 0, 0, controlSplitContainer.Panel2.Width - 1, controlSplitContainer.Panel2.Height - 1);
        }

        #endregion

        #region taskbar menu setup
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_SEPARATOR = 0x800;
        private const int MF_STRING = 0x0;
        private const int NEWMENU_OPEN = 10000;
        private const int NEWMENU_LAST = 10001;
        private const int NEWMENU_POP = 10002;
        private const int NEWMENU_NEXT = 10003;

        /// <summary>
        /// 添加任务栏菜单
        /// </summary>
        private void setupSystemMenu()
        {
            IntPtr sysMenuHandle = NgNet.Windows.Apis.User32.GetSystemMenu(Handle, false);
            NgNet.Windows.Apis.User32.AppendMenu(sysMenuHandle, MF_SEPARATOR, 10001, "-");//添加一个分隔线   
            NgNet.Windows.Apis.User32.AppendMenu(sysMenuHandle, MF_STRING, NEWMENU_OPEN, "打开");
            NgNet.Windows.Apis.User32.AppendMenu(sysMenuHandle, MF_STRING, NEWMENU_LAST, "上一首");
            NgNet.Windows.Apis.User32.AppendMenu(sysMenuHandle, MF_STRING, NEWMENU_POP, "暂停/播放");
            NgNet.Windows.Apis.User32.AppendMenu(sysMenuHandle, MF_STRING, NEWMENU_NEXT, "下一首");

            int WS_SYSMENU = 0x00080000;
            int windowLong = NgNet.Windows.Apis.User32.GetWindowLong(new System.Runtime.InteropServices.HandleRef(this, this.Handle), -16);
            NgNet.Windows.Apis.User32.SetWindowLong(new System.Runtime.InteropServices.HandleRef(this, Handle), -16, windowLong | WS_SYSMENU);
        }

        /// <summary>
        /// 任务栏菜单消息处理
        /// </summary>
        /// <param name="m"></param>
        private void processSystemMenu(ref Message m)
        {
            switch (m.WParam.ToInt32())
            {
                case NEWMENU_OPEN:
                    tsmi_openFile_Click(tsmi_openFile, new EventArgs());
                    break;
                case NEWMENU_LAST:
                    playCtlButtons_Click(playLastButton, new EventArgs());
                    break;
                case NEWMENU_POP:
                    playCtlButtons_Click(playPauseButton, new EventArgs());
                    break;
                case NEWMENU_NEXT:
                    playCtlButtons_Click(playNextButton, new EventArgs());
                    break;
                default:
                    break;
            }
        }
        #endregion
        #endregion

        #region  events & funcs
        #region public methods
        public void ShowNotice(string notice)
        {
            ntc.Show(notice);
        }
        #endregion

        #region privates methods
        /// <summary>
        /// 响应程序启动命令
        /// </summary>
        private void responseCommandArgs()
        {
            if (Program.HasCommandArgs)
            {
                try
                {
                    _mc.PlayController.myPlayer.ctControls.close();
                    _mc.MyLists.RemoveMedias(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, Program.Audios);
                    _mc.MyLists.AddMedias(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, Program.Audios);
                    _mc.PlayController.ListPlay(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, Program.Audios[0]);
                    _mc.ListViewManager.MyListView.LoadAudioList(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, true);
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 处理autoPlay事件
        /// </summary>
        private void autoPlay()
        {
            if (Program.HasCommandArgs)//如果指定了打开的文件则不自动播放
                return;
            else if (_mc.Configs.AutoPlay)
                _mc.PlayController.PlayOrPause();
        }
        #endregion

        #region diy events
        /// <summary>
        /// 主题改变事件
        /// </summary>
        /// <param name="e"></param>
        private void themeChangedEvent(ThemeChangedEventArgs e)
        {
            SuspendLayout();
            //设置主窗体颜色
            ContentPanel.BackgroundImage = (e.ThemeClass as ThemeManager).BackImage;
            BackColor = e.ThemeClass.BackColor;
            ForeColor = e.ThemeClass.ForeColor;
            BorderColor = e.ThemeClass.BorderColor;
            pcs.SetTheme(e.ThemeClass);

            mainSplitContainer.BackColor = e.ThemeClass.BackColor;

            playLastButton.FlatAppearance.BorderColor = e.ThemeClass.BackColor;
            playLastButton.FlatAppearance.MouseOverBackColor = e.ThemeClass.BackColor;
            playLastButton.FlatAppearance.MouseDownBackColor = e.ThemeClass.BackColor;
            playPauseButton.FlatAppearance.BorderColor = e.ThemeClass.BackColor;
            playPauseButton.FlatAppearance.MouseOverBackColor = e.ThemeClass.BackColor;
            playPauseButton.FlatAppearance.MouseDownBackColor = e.ThemeClass.BackColor;
            playNextButton.FlatAppearance.BorderColor = e.ThemeClass.BackColor;
            playNextButton.FlatAppearance.MouseOverBackColor = e.ThemeClass.BackColor;
            playNextButton.FlatAppearance.MouseDownBackColor = e.ThemeClass.BackColor;

            menuStrip.Renderer = (e.ThemeClass as ThemeManager).MenuRender;
            cms_main.Renderer = (e.ThemeClass as ThemeManager).MenuRender;
            initOpacity(e.ThemeClass.Opacity);
            ResumeLayout();
        }
        /// <summary>
        /// 播放文件更改事件
        /// </summary>
        /// <param name="e"></param>
        private void uRLChangedEvent(Player.URLChangedEventArgs e)
        {
            StringBuilder sb = new StringBuilder("标  题\r\n\r\n艺术家\r\n\r\n专  辑\r\n\r\n年  代\r\n\r\n描  述");
            Player.AudioInfo ai = new Player.AudioInfo(e.CurrentURL);
            Image head = Properties.Resources.apple_green;
            if (File.Exists(e.CurrentURL))
            {
                switch (Path.GetExtension(e.CurrentURL).ToLower())
                {
                    case ".asf":

                        break;
                    default:
                        #region get id3
                        try
                        {
                            // create an empty frame model, to use if we don't parse anything better
                            libMedia.ID3.TagModel tagModel = new libMedia.ID3.TagModel();
                            libMedia.ID3.TagHandler id3;
                            using (FileStream sourceStream = new FileInfo(e.CurrentURL).Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                // all the header calculations use UInt32; 
                                // this guarantees all the file offsets we have to deal with fit in a UInt32
                                if (sourceStream.Length > UInt32.MaxValue)
                                    throw new libMedia.InvalidFrameException("mp3 file can't be bigger than 4gb");

                                // try to read an ID3v1 block.
                                // If ID3v2 block exists, its values overwrite these
                                // Otherwise, if ID3V1 block exists, its values are used
                                // The audio is anything that's left after all the tags are excluded.
                                try
                                {
                                    libMedia.ID3.ID3v1 id3v1 = new libMedia.ID3.ID3v1();
                                    id3v1.Deserialize(sourceStream);
                                    // fill in ID3v2 block from the ID3v1 data
                                    tagModel = id3v1.FrameModel;
                                }
                                catch (libMedia.ID3.Exceptions.TagNotFoundException)
                                {
                                    // ignore "no ID3v1 block"
                                    // everything else isn't caught here, and throws out to the caller
                                }

                                try
                                {
                                    sourceStream.Seek(0, SeekOrigin.Begin);
                                    tagModel = libMedia.ID3.TagManager.Deserialize(sourceStream);
                                }
                                catch (libMedia.ID3.Exceptions.TagNotFoundException)
                                {
                                    // ignore "no ID3v2 block"
                                    // everything else isn't caught here, and throws out to the caller
                                }
                                // create a taghandler to hold the tagmodel we've parsed, if any
                                id3 = new libMedia.ID3.TagHandler(tagModel);
                            } // closes sourceStream
                            #endregion
                            if (id3.Picture != null)
                                head = id3.Picture;
                        }
                        catch (Exception)
                        {
                        }

                        sb.Clear();
                        sb.Append(string.Format("标  题：  {0}", ai.Title));
                        sb.Append("\r\n\r\n");
                        sb.Append(string.Format("艺术家：  {0}", ai.Artist));
                        sb.Append("\r\n\r\n");
                        sb.Append(string.Format("专  辑：  {0}", ai.Album));
                        sb.Append("\r\n\r\n");
                        sb.Append(string.Format("年  代：  {0}", ai.Year));
                        sb.Append("\r\n\r\n");
                        sb.Append(string.Format("描  述：  {0}", ""));
                        break;
                }
                aLengthLabel.Text = string.IsNullOrWhiteSpace(ai.Length) ? "unknown" : ai.Length;
                aKbpsLabel.Text = string.IsNullOrWhiteSpace(ai.BitRate) ? "unknown" : ai.BitRate;
                aNameLabel.Text = string.IsNullOrWhiteSpace(ai.Name) ? "unknown" : ai.Name;
            }
            else
            {
                aLengthLabel.Text = ControlTexts.TITLE_AUDIOINFO_LENGTH;
                aKbpsLabel.Text = ControlTexts.TITLE_AUDIOINFO_KBPS;
                aNameLabel.Text = ai.Name;
            }
            aheadPicBox.Image = head;
            aInfLabel.Text = sb.ToString();
            if (_mc.PlayController.myPlayer.IsInterimPlay)
                aIndexLabel.Text = "1/1";
            else
                aIndexLabel.Text =
                         string.Format("{0}/{1}", _mc.PlayController.myPlayer.playList.Medias.IndexOf(_mc.PlayController.myPlayer.currentURL) + 1, _mc.PlayController.myPlayer.playList.Medias.Count);
            // 更改在任务管理器中的名称
            Text = string.Format("{0} - {1}", ai.Name, System.Windows.Forms.Application.ProductName);
            if (NgNet.IO.PathHelper.IsPath(e.CurrentURL))
            {
                // 刷新历史记录
                _mc.MyLists.AddMedias(List.MyLib.History, List.ListManager.NAME_LIST_HISTORY, new string[] { e.CurrentURL });
                // 刷新播放次数
                _mc.MyLists.AddMedias(List.MyLib.MostlyPlayed, List.ListManager.NAME_LIST_MOSTLYPLAYED, new string[] { e.CurrentURL });
            }
        }
        /// <summary>
        /// 音量更改事件
        /// </summary>
        /// <param name="e"></param>
        private void volumeChangedEvent(Player.VolumeChangedEventArgs e)
        {
            volumeLabel.Text = e.Volume.ToString();
        }
        /// <summary>
        /// 静音状态更改事件
        /// </summary>
        /// <param name="e"></param>
        private void muteStateChangedEvent(Player.MuteStateChangedEventArgs e)
        {
            Font f = volumeLabel.Font;
            f = new Font(f.Name, f.Size, e.MuteState ? FontStyle.Strikeout : FontStyle.Regular, GraphicsUnit.Point);
            volumeLabel.Font = f;
        }
        /// <summary>
        /// 循环模式更改事件
        /// </summary>
        /// <param name="e"></param>
        private void cycleModeChangedEvent(Player.CycleModeChangedEventArgs e)
        {
            toolTip.SetToolTip(cmPicBox, "CycleMode：" + e.CycleMode);
            switch (e.CycleMode)
            {
                case Player.CycleModes.OneOnce:
                    //picBox_cm.BackgroundImage = null;
                    cmPicBox.Text = "→";
                    break;
                case Player.CycleModes.OneCycle:
                    cmPicBox.BackgroundImage = Properties.Resources.playmode_repeat_single;
                    break;
                case Player.CycleModes.AllOnce:
                    cmPicBox.BackgroundImage = Properties.Resources.playmode_sequence;
                    break;
                case Player.CycleModes.AllCycle:
                    cmPicBox.BackgroundImage = Properties.Resources.playmode_repeat_all;
                    break;
                case Player.CycleModes.Random:
                    cmPicBox.BackgroundImage = Properties.Resources.playmode_random;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 播放时间更改事件
        /// </summary>
        /// <param name="e"></param>
        private void currentPositionChangedEvent(Player.CurrentPositionChangedEventArgs e)
        {
            aTimeLabel.Text = NgNet.ConvertHelper.ToTimeString(e.CurrentPosition / 1000);
        }
        /// <summary>
        /// 播放状态更改事件
        /// </summary>
        /// <param name="e"></param>
        private void playStateChangeEvent(Player.PlayStateChangedEventArgs e)
        {
            switch (e.PlayState)
            {
                case Player.PlayStates.mediaEnd:
                    onMediaEnd();
                    break;
                case Player.PlayStates.paused:
                    onPaused();
                    break;
                case Player.PlayStates.playing:
                    onPlaying();
                    break;
                case Player.PlayStates.stoped:
                    onStoped();
                    break;
                case Player.PlayStates.transitioning:
                    onTransitioning();
                    break;
                case Player.PlayStates.closed:
                    onClosed();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 播放列表更改事件
        /// </summary>
        /// <param name="e"></param>
        private void mediaListChangedEvnet(Player.MediaListChangedEventArgs e)
        {
            if (_mc.PlayController.myPlayer.settings.PlayState != Player.PlayStates.closed)
                if (_mc.PlayController.myPlayer.IsInterimPlay == false)
                    aIndexLabel.Text =
                            string.Format(
                                "{0}/{1}",
                                _mc.PlayController.myPlayer.playList.Medias.IndexOf(_mc.PlayController.myPlayer.currentURL) + 1,
                                _mc.PlayController.myPlayer.playList.Medias.Count);
        }

        /// <summary>
        /// 播放状态转为播放
        /// </summary>
        private void onPlaying()
        {
            // 更新窗口歌词界面信息           
            playPauseButton.Text = ControlTexts.SIGN_PLAYSTATE_PAUSE;
            // 获取播放数据          
            string atmp = Path.GetFileName(_mc.PlayController.myPlayer.currentURL);
            _mc.NotiryIcon.SetText(
                string.Format("正在播放{0} - ", _mc.PlayController.myPlayer.IsInterimPlay ? "[临时]" : "[列表]")
                + NgNet.Text.Utils.GetShorterText(atmp, 20, '.')
                + string.Format("\r\n位置：{0}\r\n音量：{1}", aIndexLabel.Text, _mc.PlayController.myPlayer.settings.Volume));
        }
        /// <summary>
        /// 播放状态转为暂停
        /// </summary>
        private void onPaused()
        {
            // 更新界面信息
            playPauseButton.Text = ControlTexts.SIGN_PLAYSTATE_PLAY;
            string atmp = Path.GetFileName(_mc.PlayController.myPlayer.currentURL);
            _mc.NotiryIcon.SetText(
                string.Format("已暂停{0} - "
                , _mc.PlayController.myPlayer.IsInterimPlay ? "[临时]" : "[列表]")
                + NgNet.Text.Utils.GetShorterText(atmp, 20, '.')
                + string.Format("\r\n位置：{0}\r\n音量：{1}", aIndexLabel.Text, _mc.PlayController.myPlayer.settings.Volume));
        }
        /// <summary>
        /// 播放状态转为停止
        /// </summary>
        private void onStoped()
        {
            audioStoped();
        }
        /// <summary>
        /// 播放状态转为播放完
        /// </summary>
        private void onMediaEnd()
        {
            audioStoped();
        }
        /// <summary>
        /// 状态为切换音乐
        /// </summary>
        private void onTransitioning()
        {
            audioStoped();
        }
        /// <summary>
        /// 停止播放当前音乐需要处理的事件
        /// </summary>
        private void audioStoped()
        {
            playPauseButton.Text = ControlTexts.SIGN_PLAYSTATE_PLAY;
            aTimeLabel.Text = ControlTexts.TITLE_AUDIOINFO_LENGTH;
            _mc.NotiryIcon.SetText(string.Format("已停止 - {0}", NgNet.Text.Utils.GetShorterText(_mc.PlayController.myPlayer.settings.URL, 50, '.')));
        }
        /// <summary>
        /// 关闭播放器
        /// </summary>
        private void onClosed()
        {
            playPauseButton.Text = ControlTexts.SIGN_PLAYSTATE_PLAY;
            aNameLabel.Text = "请选择一首音乐";
            aKbpsLabel.Text = ControlTexts.TITLE_AUDIOINFO_KBPS; ;
            aTimeLabel.Text = ControlTexts.TITLE_AUDIOINFO_LENGTH;
            aIndexLabel.Text = ControlTexts.TITLE_AUDIOINFO_INDEX;
            aLengthLabel.Text = ControlTexts.TITLE_AUDIOINFO_LENGTH;
            Text = Application.ProductName;
            _mc.NotiryIcon.SetText(string.Format("未播放 - {0}", Application.ProductName));
        }
        /// <summary>
        /// 显示正在播放列表或者音乐信息
        /// </summary>
        private bool showPlayingOrAudioInf
        {
            set
            {
                if (value == _showPlaying)
                    return;
                controlSplitContainer.Panel2.Controls.Clear();
                if (value)
                {
                    controlSplitContainer.Panel2.Controls.Add(_mc.ListViewManager.MyListView.PlayingListTreeView);
                    mainSplitContainer.IsSplitterFixed = false;
                    mainSplitContainer.FixedPanel = FixedPanel.None;
                }
                else
                {
                    controlSplitContainer.Panel2.Controls.Add(infPanel);
                    mainSplitContainer.SplitterDistance = mainSplitContainer.Panel1MinSize;
                    mainSplitContainer.IsSplitterFixed = true;
                    mainSplitContainer.FixedPanel = FixedPanel.Panel1;
                }
                _showPlaying = value;
            }
            get { return _showPlaying; }
        }
        /// <summary>
        /// 显示窗口歌词或者列表
        /// </summary>
        /// <param name="e"></param>
        private void showFLyricOrList(Lyric.LyricVisibleChangedEventArgs e)
        {
            listSplitContainer.Panel2.Controls.Clear();
            if (e.Visible)
                listSplitContainer.Panel2.Controls.AddRange(new Control[]{
                labelLyric});
            else
            {
                listSplitContainer.Panel2.Controls.Add(_mc.ListViewManager.MyListView.ListDataGridView);
            }
        }
        #endregion
        #endregion
    }
}