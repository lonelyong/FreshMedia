using System;

namespace FreshMedia.Plus
{   
    class SleepMode
    {
        #region private filed
        private uint LeftSeconds = 0;//睡眠剩余时间

        private System.Windows.Forms.Timer timer;

        private System.Windows.Forms.Control showControl = new System.Windows.Forms.Control();

        private Controller.MainController _mc;

        private NgNet.UI.Forms.InputBox inputBox;
        #endregion

        #region public filed
        public uint SleepTime { get; set; }//睡眠时间

        public bool ToShutDown { get; set; }//允许睡眠模式关机

        public bool IsSleeping
        {
            get { return LeftSeconds > 0; }
        }

        public event OnSleepingEventHandler OnSleepingEvent;
        #endregion

        #region constructor destructor 
        public SleepMode(IFreshMedia iFreshMedia)
        {
            this._mc = iFreshMedia.Controller; ;
            SleepTime = 20;
            ToShutDown = false;
            OnSleepingEvent = new OnSleepingEventHandler((OnSleepingEventArgs e) => { });
            //设置timer属性
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                run();
            });
        }
        #endregion

        #region private
        private void run()
        {
            showControl.Text = string.Format("sleeping:\r\n{0}", NgNet.ConvertHelper.ToTimeString(LeftSeconds));
            OnSleepingEvent(new OnSleepingEventArgs(LeftSeconds, SleepTime));
            if (LeftSeconds-- == 0)
            {
                Stop();
                _mc.PlayController.myPlayer.ctControls.stop();
                if (ToShutDown)
                    new NgNet.UI.Forms.ExitWindowsBox() { RestartOption = NgNet.Windows.RestartOptions.PowerOff, Force = true, WaitTime = 60 }.Show();
            }
        }
        #endregion

        #region public method
        /// <summary>
        /// 停止睡眠
        /// </summary>
        public void Stop()
        {
            LeftSeconds = 0;
            timer.Enabled = false;
            showControl.Text = string.Empty;
            _mc.NotiryIcon.ShowNotice(8, "~﹏~", "已退出睡眠模式！", System.Windows.Forms.ToolTipIcon.Info);
        }
        /// <summary>
        /// 重新开始睡眠
        /// </summary>
        /// <param name="mintes">睡眠时间</param>
        public void Start(uint mintes)
        {
            SleepTime = mintes;
            LeftSeconds = mintes * 60;
            timer.Enabled = true;
        }
        /// <summary>
        /// 自行设置睡眠时间
        /// </summary>
        /// <param name="def">默认睡眠时间</param>
        /// <returns></returns>
        public uint DiySleeptimeDialog(System.Windows.Forms.Form owner, uint def)
        {
            Re:
            if(inputBox != null && inputBox.IsLoaded)
            {
                inputBox.Activate();
                return 0;
            }
            inputBox = new NgNet.UI.Forms.InputBox();
            inputBox.InputType = NgNet.UI.Forms.InputTypes.Int;
            inputBox.Notice = "请输入停止播放的时间（1 - 1440,单位:分钟）：";
            inputBox.Title = "自定义睡眠时间";
            inputBox.Text = def.ToString();

            string time = inputBox.Show(owner);

            if (string.IsNullOrWhiteSpace(time))
            {
                return 0;
            }
            uint stime = 0;
            if (uint.TryParse(time, out stime))
            {
                if (stime > 1440)
                {
                    NgNet.UI.Forms.MessageBox.Show(
                        owner, 
                        "您输入的时间大于1天，你真的需要吗? 请重新输入！", 
                        "", 
                        System.Windows.Forms.MessageBoxButtons.OK, 
                        System.Windows.Forms.DialogResult.OK, 
                        3);
                    goto Re;
                }
            }
            else
            {
                NgNet.UI.Forms.MessageBox.Show(
                    owner, 
                    "您输入的时间非法！请您直接输入数字！", 
                    "", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.DialogResult.OK, 
                    3);
                goto Re;
            }
            return stime;
        }

        public void SetShowControl(System.Windows.Forms.Control ctr)
        {
            showControl = ctr;
        }
        #endregion
    }

}
