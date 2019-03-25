using System;

namespace FreshMedia.Controller
{
    class MainController : IFreshMedia
    {
        #region private fields
        private NgNet.UI.Forms.HotMessageBox _hotmsg = new NgNet.UI.Forms.HotMessageBox(3000);

        private View.VSetting.ConfigForm _configForm;
        #endregion

        #region public properties
        public AppConfigs Configs { get; private set; }

        public View.AppNotfyIcon NotiryIcon { get; private set; }

        public PlayController PlayController { get; private set; }

        public List.ListManager MyLists { get; private set; }

        public HotkeysManager HotkeyManager { get; private set; }

        public Plus.SleepMode SleepMode { get; private set; }

        public Lyric.LyricManager LyricManager { get; private set; }

        public View.MainForm MainForm { get; private set; }

        public View.VList.ViewManager ListViewManager { get; private set; }

        public View.ThemeManager Theme { get; private set; }

        public Data.BackupMamager BackupManager { get; private set; }

        public List.OpenHistory OpenHistoryManager { get; private set; }

        public Data.DataManager DataManager { get; private set; }
        #endregion

        #region constructor destructor 
        public MainController()
        {
            
        }

        public void Init()
        {
            this.DataManager = new Data.DataManager();
            this.DataManager.Init();

            this.OpenHistoryManager = new FreshMedia.List.OpenHistory();
            this.Theme = new View.ThemeManager();
            this.MyLists = new List.ListManager();

            this.Configs = new AppConfigs();
            this.PlayController = new PlayController(this);
            this.LyricManager = new Lyric.LyricManager(this);
            this.LyricManager.Init();
            //先于mainform;
            this.SleepMode = new Plus.SleepMode(this);
            this.MainForm = new View.MainForm(this);

            this.ListViewManager = new View.VList.ViewManager(this);
            this.ListViewManager.Init();

            this.HotkeyManager = new HotkeysManager(this, MainForm.Handle);
            this.BackupManager = new Data.BackupMamager(this);

            this.NotiryIcon = new View.AppNotfyIcon(this);
            this.NotiryIcon.Init();
        }
        #endregion

        #region properties
        public System.Drawing.Icon FormIcon
        {
            get
            {
                return NgNet.ConvertHelper.Bitmap2Icon(Properties.Resources.apple_green);
            }
        }

        MainController IFreshMedia.Controller
        {
            get
            {
                return this;
            }
        }
        #endregion

        #region private method

        #endregion

        #region public methods 

        /// <summary>
        /// 显示热消息
        /// </summary>
        /// <param name="msg"></param>
        public void ShowHotMessage(string msg)
        {
            _hotmsg.Show(msg);
        }
    
        /// <summary>
        /// 显示控制面板
        /// </summary>
        /// <param name="tab">面板</param>
        /// <param name="page"></param>
        public void ShowConfigDialog(View.VSetting.SettingTabs tab, uint page)
        {
            Action action = delegate {
                if (_configForm != null && _configForm.IsLoaded)
                    _configForm.ShowTab(tab, page);
                else
                {
                    _configForm = new View.VSetting.ConfigForm(tab, page, this);
                    _configForm.ShowDialog(MainForm);
                }
            };       
            MainForm.BeginInvoke(action);
        }

        public void ShowMediaInfoBox(string path, List.MyLib scrlib, string scrlist)
        {
            if (FreshMedia.Controller.PlayController.FileTest(path))
            {
                View.InfoForm aif = new View.InfoForm(path, scrlib, scrlist, this);
                aif.ShowDialog();
            }
        }

        /// <summary>
        /// 显示主窗口
        /// </summary>
        public void ShowMainForm()
        {
            if (MainForm.Visible == false)
                MainForm.Show();
            else if (MainForm.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                MainForm.WindowState = System.Windows.Forms.FormWindowState.Normal;
            MainForm.Activate();
        }

        /// <summary>
        /// 主线程执行委托
        /// </summary>
        /// <param name="method"></param>
        public void MainThreadInvoke(Delegate method)
        {
            MainForm.Invoke(method);
        }

        /// <summary>
        /// 主线程异步执行委托
        /// </summary>
        /// <param name="method"></param>
        public void MainThreadBeginInvoke(Delegate method)
        {
            MainForm.BeginInvoke(method);
        }
        #endregion
    }

}
