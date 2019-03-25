using System;
using System.Collections.Generic;
using System.ComponentModel;
using NgNet.UI.Forms;
using System.Drawing;
using System.IO;
using NgNet.UI;
using System.Windows.Forms;

namespace FreshMedia.View
{
    class AppNotfyIcon
    {
        #region private filed
        private Controller.MainController _mc;
        private NotifyIcon _notifyIcon = new NotifyIcon();//通知栏图标
        private ContextMenuStrip cms = new ContextMenuStrip();//通知栏菜单
        //  验证列表是否被修改

        private int favoHash;//收藏列表的哈希值
        private int defaultHash;//默认列表的哈希值
        private ToolStripMenuItem tsmi_favo = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_default = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_mainForm = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_last = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_playPause = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_next = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_exit = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_ctrPnl = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_sleepMode = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_deskLyric = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_cycleMode = new ToolStripMenuItem();
        #endregion

        #region constructor
        public AppNotfyIcon(IFreshMedia iFreshMedia)
        {
            // 字段初始化
            _mc = iFreshMedia.Controller;
            init();
        }
        #endregion

        #region private methods

        public void Init()
        {
            _mc.Theme.ThemeChanged += new ThemeChangedEventHandler(themeChangedEvent);
        }

        private void init()
        {
            _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            _notifyIcon.BalloonTipTitle = "~﹏~";
            _notifyIcon.Text = Application.ProductName;
            _notifyIcon.ContextMenuStrip = cms;
            _notifyIcon.Visible = true;
            _notifyIcon.Icon = _mc.FormIcon;
            _notifyIcon.MouseClick += new MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                if (e.Button == MouseButtons.Left)
                    _mc.ShowMainForm();
            });

            // 
            // tsmi_playPause
            // 
            tsmi_playPause.ImageAlign = ContentAlignment.MiddleRight;
            tsmi_playPause.Text = "播放";
            tsmi_playPause.Click += new EventHandler(_mc.MainForm.playCtlButtons_Click);
            // 
            // tsmi_last
            // 
            tsmi_last.ImageAlign = ContentAlignment.MiddleLeft;
            tsmi_last.Text = "上一首(&L)";
            tsmi_last.Click += new EventHandler(_mc.MainForm.playCtlButtons_Click);
            // 
            // tsmi_next
            // 
            tsmi_next.ImageAlign = ContentAlignment.MiddleRight;
            tsmi_next.Text = "下一首(&N)";
            tsmi_next.Click += new EventHandler(_mc.MainForm.playCtlButtons_Click);
            // 
            // tsmi_mainForm
            // 
            tsmi_favo.Text = "我的收藏";
            tsmi_favo.DropDownOpening += new EventHandler(tsmi_favo_DropDownOpening);
            // 
            // tsmi_default
            // 
            tsmi_default.Text = "默认列表";
            tsmi_default.DropDownOpening += new EventHandler(tsmi_default_DropDownOpening);
            // 
            // tsmi_mainForm
            // 
            tsmi_mainForm.ImageAlign = ContentAlignment.TopLeft;
            tsmi_mainForm.Text = "打开主界面(&M)";
            tsmi_mainForm.Click += new EventHandler(tsmi_mainFrom_Click);
            //
            //
            //
            tsmi_exit.Text = "退出";
            tsmi_exit.Click += new EventHandler(tsmi_exit_Click);
            //
            //
            //
            tsmi_ctrPnl.Text = "控制面板";
            tsmi_ctrPnl.Click += new EventHandler(tsmi_ctrPanel_Click);
            //
            //
            //
            tsmi_sleepMode.Text = "睡眠模式";
            tsmi_sleepMode.Click += new EventHandler(tsmi_sleepMode_Click);
            //
            //
            //
            tsmi_deskLyric.Text = "桌面歌词";
            tsmi_deskLyric.Click += new EventHandler(tsmi_deskLyric_Click);
            //
            //
            //
            tsmi_cycleMode.Text = "循环模式";
            tsmi_cycleMode.DropDownOpening += new EventHandler(tsmi_cycleMode_DropDowmOpening);
            // 
            // cms
            // 
            cms.Items.AddRange(new ToolStripItem[] {
            tsmi_playPause,
            tsmi_last,
            tsmi_next,
            tsmi_favo,
            tsmi_default,
            tsmi_mainForm });
            cms.Size = new Size(157, 136);
            cms.Opening += new CancelEventHandler(cms_Opening);

            initCommands();
        }

        private void initCommands()
        {
            tsmi_last.Tag = Controller.Commands.PC_PLAYLAST;
            tsmi_playPause.Tag = Controller.Commands.PC_PLAYPAUSE;
            tsmi_next.Tag = Controller.Commands.PC_PLAYNEXT;
        }

        private void themeChangedEvent(ThemeChangedEventArgs e)
        {
            cms.Renderer = (e.ThemeClass as ThemeManager).MenuRender;
            ContextMenuStripHelper.SetOpacity(cms, e.ThemeClass.Opacity);
        }

        private void setNotiiconFavoList(ToolStripMenuItem tsmiParent)
        {
            if (_mc.MyLists.Favo.Count == 0)
                return;
            //验证列表是否被修改
            if (favoHash == _mc.MyLists.Favo.GetHashCode())
                return;
            favoHash = _mc.MyLists.Favo.GetHashCode();
            List<ToolStripMenuItem> tsmis = new List<ToolStripMenuItem>();
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            //菜单左击响应事件
            MouseEventHandler meh = new MouseEventHandler((object sender1, MouseEventArgs e1) =>
            {
                if (e1.Button != MouseButtons.Left)
                    return;
                tsmi = (ToolStripMenuItem)sender1;
                int itemindex = _mc.MyLists.Favo[0].IndexOf(tsmi.Name);
                if (itemindex == -1) { tsmiParent.DropDownItems.Remove(tsmi); }//点击的音乐不存在 ，删除该项
                else
                {
                    _mc.PlayController.ListPlay(List.MyLib.Favorite, List.ListManager.NAME_LIST_FAVO, tsmi.Name);
                }
            });
            foreach (string item in _mc.MyLists.Favo[0])
            {
                tsmi = new ToolStripMenuItem();
                tsmi.AutoSize = false;
                tsmi.Width = 337;
                tsmi.AutoToolTip = true;
                tsmi.Text = Path.GetFileName(item);
                tsmi.Name = item;
                tsmi.MouseUp += meh;
                tsmis.Add(tsmi);
            }
            tsmiParent.DropDownItems.Clear();
            tsmiParent.DropDownItems.AddRange(tsmis.ToArray());
            tsmiParent.DropDown.MaximumSize = new Size(tsmi.Width + 1, Screen.PrimaryScreen.WorkingArea.Height);
            tsmiParent.DropDown.MinimumSize = new Size(tsmi.Width + 1, tsmiParent.DropDown.Height);
        }

        private void setNotiiconDefaultList(ToolStripMenuItem tsmiParent)
        {
            int listIndex = _mc.MyLists.GetListIndex(List.MyLib.Local, List.ListManager.NAME_LIST_DEFAULT);
            if (listIndex == -1)
                throw new List.ListNotFoundException(List.MyLib.Local, List.ListManager.NAME_LIST_DEFAULT);
            if (_mc.MyLists.Local[listIndex].Count == 0)
                return;
            //验证列表是否被更改
            if (_mc.MyLists.Local[listIndex].GetHashCode() == defaultHash)
                return;
            defaultHash = _mc.MyLists.Local[listIndex].GetHashCode();
            List<ToolStripMenuItem> tsmis = new List<ToolStripMenuItem>();
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            //菜单左击响应事件
            MouseEventHandler meh = new MouseEventHandler((object sender1, MouseEventArgs e1) =>
            {
                if (e1.Button != MouseButtons.Left) return;
                tsmi = (ToolStripMenuItem)sender1;
                int itemindex = _mc.MyLists.Local[listIndex].IndexOf(tsmi.Name);
                if (itemindex == -1) { tsmiParent.DropDownItems.Remove(tsmi); }//点击的音乐不存在 ，删除该项
                else
                {
                    _mc.PlayController.ListPlay(List.MyLib.Local, List.ListManager.NAME_LIST_DEFAULT, tsmi.Name);
                }
            });
            //加载通知栏默认列表
            foreach (string item in _mc.MyLists.Local[listIndex])
            {
                tsmi = new ToolStripMenuItem();
                tsmi.AutoSize = false;
                tsmi.Width = 337;
                tsmi.AutoToolTip = true;
                tsmi.Name = item;
                tsmi.Text = Path.GetFileName(item);
                tsmi.MouseUp += meh;
                tsmis.Add(tsmi);
            }
            tsmiParent.DropDownItems.Clear();
            tsmiParent.DropDownItems.AddRange(tsmis.ToArray());
            tsmiParent.DropDown.MaximumSize = new Size(tsmi.Width + 1, Screen.PrimaryScreen.WorkingArea.Height);
            tsmiParent.DropDown.MinimumSize = new Size(tsmi.Width + 1, tsmiParent.DropDown.Height);
        }

        #endregion

        #region cms_notiIcon
        private void tsmi_mainFrom_Click(object sender, EventArgs e)
        {
            _mc.ShowMainForm();
        }

        private void tsmi_favo_DropDownOpening(object sender, EventArgs e)
        {
            setNotiiconFavoList((ToolStripMenuItem)sender);
        }

        private void tsmi_default_DropDownOpening(object sender, EventArgs e)
        {
            setNotiiconDefaultList((ToolStripMenuItem)sender);
        }

        private void tsmi_exit_Click(object sender, EventArgs e)
        {
            _mc.MainForm.Close();
        }

        private void tsmi_ctrPanel_Click(object sender, EventArgs e)
        {
            _mc.ShowConfigDialog(VSetting.SettingTabs.General, 0);
        }

        private void tsmi_sleepMode_Click(object sender, EventArgs e)
        {
            if (_mc.SleepMode.IsSleeping)
                _mc.SleepMode.Stop();
            else
                _mc.SleepMode.Start(_mc.SleepMode.DiySleeptimeDialog(_mc.MainForm, _mc.SleepMode.SleepTime));
        }

        private void tsmi_deskLyric_Click(object sender, EventArgs e)
        {
            _mc.LyricManager.DLyric.Visible = !_mc.LyricManager.DLyric.Visible;
        }

        private void tsmi_cycleMode_DropDowmOpening(object sender, EventArgs e)
        {
            tsmi_cycleMode.DropDownItems.Clear();
            foreach (Player.CycleModes item in Enum.GetValues(typeof(Player.CycleModes)))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = Enum.GetName(typeof(Player.CycleModes), item);
                tsmi.Checked = item == _mc.PlayController.myPlayer.CycleMode;
                tsmi.Tag = item;
                tsmi.Click += new EventHandler((object sender1, EventArgs e1)=> 
                {
                    _mc.PlayController.myPlayer.CycleMode = ((Player.CycleModes)((ToolStripMenuItem)sender1).Tag);
                });
                tsmi_cycleMode.DropDownItems.Add(tsmi);
            }
        }

        private void cms_Opening(object sender, CancelEventArgs e)
        {
            cms.Items.AddRange(new ToolStripMenuItem[]{
                    tsmi_last,
                    tsmi_playPause,
                    tsmi_next,
                    tsmi_default,
                    tsmi_favo,
                    tsmi_deskLyric,
                    tsmi_sleepMode,
                    tsmi_cycleMode,
                    tsmi_ctrPnl,
                    tsmi_exit,
                    tsmi_mainForm});

            //播放状态
            switch (_mc.PlayController.myPlayer.settings.PlayState)
            {
                case Player.PlayStates.playing:
                    tsmi_playPause.Text = ControlTexts.TITLE_PLAYSTATE_PAUSE;
                    break;
                case Player.PlayStates.paused:
                    tsmi_playPause.Text = ControlTexts.TITLE_PLAYSTATE_CONTINUE;
                    break;
                case Player.PlayStates.stoped:
                case Player.PlayStates.mediaEnd:
                case Player.PlayStates.transitioning:
                case Player.PlayStates.closed:
                default:
                    tsmi_playPause.Text = ControlTexts.TITLE_PLAYSTATE_PLAY;
                    break;
            }
            //睡眠模式
            tsmi_sleepMode.Text = _mc.SleepMode.IsSleeping ? "点击取消睡眠模式" : "Start sleep mode";
            //歌词设置
            tsmi_deskLyric.Text = _mc.LyricManager.DLyric.Visible ? "Hide desktop lyric" : "Show desktop lyric";
        }
        #endregion

        #region public methods
        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="showTime"></param>
        /// <param name="tipTitle"></param>
        /// <param name="tip"></param>
        /// <param name="tti"></param>
        public void ShowNotice(int showTime, string tipTitle, string tip, ToolTipIcon tti)
        {
            _notifyIcon.ShowBalloonTip(showTime, tipTitle, tip, tti);
        }

        /// <summary>
        /// 设置通知栏图标文字
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            _notifyIcon.Text = text;
        }
        #endregion
    }
}
