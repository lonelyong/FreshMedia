
using NgNet.Applications;
using System;
using System.Text;
using System.Windows.Forms;

namespace FreshMedia.Controller
{
    class HotkeysManager
    {
        #region private fields
        private Controller.MainController _mc;

        private HotkeyManager _hotkeyManager;
        #endregion

        #region public properties
        public const string SK_LIST_SEARCH = "Control + S";
        public Hotkey Forward { get; }
        public Hotkey Rewind { get; }
        public Hotkey PausePlay { get; }
        public Hotkey VolumeUp { get; }
        public Hotkey VolumeDown { get; }
        public Hotkey PlayLast { get; }
        public Hotkey PlayNext { get; }

        public Hotkey[] Hotkeys { get; }

        
        #endregion

        #region method
        /// <summary>
        /// 注册全局快捷键
        /// </summary>
        /// <returns></returns>
        public bool Register()
        {
            var sb = new StringBuilder("以下全局热键被占用：\r\n   ");
            int falseCount = 0;
            for (int i = 0; i < Hotkeys.Length; i++)
            {
                if (!_hotkeyManager.Register(ref Hotkeys[i]))
                {
                    falseCount++;
                    sb.Append(string.Format("\r\n{0}   {1}", Hotkeys[i].Name, Hotkeys[i].Value));
                }
            }
            if (falseCount > 0)
            {
                NgNet.UI.Forms.MessageBox.Show(sb.ToString(), "全局热键注册提示", MessageBoxButtons.OK, DialogResult.OK, 12);
                return false;
            }
            return true;
        }

        /// <summary>
        ///取消注册全局快捷键
        /// </summary>
        public void UnRegister()
        {
            foreach (NgNet.Applications.Hotkey item in Hotkeys)
            {
                _hotkeyManager.UnRegister(item.CallBack);
            }
        }

        public void ProcessHotkey(Message m)
        {
            _hotkeyManager.ProcessHotkey(m);
        }
        #endregion

        #region private
        private void playNext()
        {
            _mc.PlayController.myPlayer.ctControls.next();
            if (_mc.PlayController.myPlayer.lastURL != _mc.PlayController.myPlayer.currentURL)
                _mc.ShowHotMessage("下一首");
        }

        private void playLast()
        {

            _mc.PlayController.myPlayer.ctControls.last();
            if (_mc.PlayController.myPlayer.lastURL != _mc.PlayController.myPlayer.currentURL)
                _mc.ShowHotMessage("上一首");
        }

        private void volumeUp()
        {
            _mc.PlayController.myPlayer.ctControls.volUp();
            _mc.ShowHotMessage("音量+：" + _mc.PlayController.myPlayer.settings.Volume);
        }

        private void volumeDown()
        {
            _mc.PlayController.myPlayer.ctControls.volDown();
            _mc.ShowHotMessage("音量-：" + _mc.PlayController.myPlayer.settings.Volume);
        }

        private void playPause()
        {
            _mc.PlayController.PlayOrPause();
            if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                _mc.ShowHotMessage("暂停");
            else if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                _mc.ShowHotMessage("播放");
        }

        private void rewind()
        {
            _mc.PlayController.myPlayer.ctControls.rewind();
            if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing || _mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                _mc.ShowHotMessage(string.Format("{0}/{1}", _mc.PlayController.myPlayer.currentPositionString, _mc.PlayController.myPlayer.currentMedia.mediaLengthString));
        }

        private void forward()
        {
            _mc.PlayController.myPlayer.ctControls.forward();
            if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing || _mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                _mc.ShowHotMessage(string.Format("{0}/{1}", _mc.PlayController.myPlayer.currentPositionString, _mc.PlayController.myPlayer.currentMedia.mediaLengthString));
        }
        #endregion

        #region constructor destructor 
        public HotkeysManager(MainController _controller, IntPtr intPtr)
        {
            _mc = _controller;
            _hotkeyManager = new HotkeyManager(intPtr);
            Hotkeys = new Hotkey[7];
            Forward = new Hotkey("     快进", (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Shift, Keys.Right, forward);
            Rewind = new Hotkey("     快退", (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Shift, Keys.Left, rewind);
            PausePlay = new Hotkey("暂停播放", (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.Space, playPause);
            PlayLast = new Hotkey("   上一首", (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.Left, playLast);
            PlayNext = new Hotkey("   下一首", (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.Right, playNext);
            VolumeUp = new Hotkey("   音量加", (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.Up, volumeUp);
            VolumeDown = new Hotkey("   音量减", (int)HotkeyModifiers.Control + (int)HotkeyModifiers.Alt, Keys.Down, volumeDown);
            init();
        }
        #endregion

        #region private methods
        private void init()
        {
            Hotkeys[0] = Forward;
            Hotkeys[1] = Rewind;
            Hotkeys[2] = PausePlay;
            Hotkeys[3] = PlayLast;
            Hotkeys[4] = PlayNext;
            Hotkeys[5] = VolumeUp;
            Hotkeys[6] = VolumeDown;
        }
        #endregion
    }
}