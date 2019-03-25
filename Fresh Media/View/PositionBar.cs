using System;
using System.Windows.Forms;

namespace FreshMedia.View
{
    class PositionBar : IDisposable
    {
        #region private filed
        private Control barCtr;
        private ToolTip toolTip;
        private System.Windows.Forms.Timer tipTimer;
        private int bar_lastloc = 0;       //上次光标位置
        private int bar_eX;                //鼠标位置
        private int bar_height;            //进度条高度
        private int bar_width;             //进度条长度
        private Controller.MainController _controller;
        #endregion

        #region constructor destructor 
        public PositionBar(Control bar, ToolTip toolTip, Controller.MainController _controller)
        {
            this.toolTip = toolTip;
            this.barCtr = bar;
            this._controller = _controller;
            this._controller.PlayController.myPlayer.CurrentPositionChangedEvent += new Player.CurrentPositionChangedEventHandler(currentPositionChangedEvent);
            this._controller.PlayController.myPlayer.PlayStateChangedEvent += new Player.PlayStateChangedEventHandler(playStateChangedEvent);
            //posTimer
            tipTimer = new System.Windows.Forms.Timer();
            tipTimer.Enabled = false;
            tipTimer.Interval = 30;
            tipTimer.Tick += new EventHandler(tipTimer_Tick);

            barCtr.MouseEnter += new EventHandler(barCtr_MouseEnter);
            barCtr.MouseMove += new MouseEventHandler(barCtr_MouseMove);
            barCtr.MouseDown += new MouseEventHandler(barCtr_MouseDown);
            barCtr.MouseLeave += new EventHandler(barCtr_MouseLeave);
            barCtr.Paint += new PaintEventHandler(barCtr_Paint);
        }

        ~PositionBar()
        {
            _controller.PlayController.myPlayer.CurrentPositionChangedEvent -= new Player.CurrentPositionChangedEventHandler(currentPositionChangedEvent);
            _controller.PlayController.myPlayer.PlayStateChangedEvent -= new Player.PlayStateChangedEventHandler(playStateChangedEvent);
            barCtr.MouseEnter -= new EventHandler(barCtr_MouseEnter);
            barCtr.MouseMove -= new MouseEventHandler(barCtr_MouseMove);
            barCtr.MouseDown -= new MouseEventHandler(barCtr_MouseDown);
            barCtr.MouseLeave -= new EventHandler(barCtr_MouseLeave);
            barCtr.Paint -= new PaintEventHandler(barCtr_Paint);
            GC.Collect();
        }
        #endregion

        #region private method
        private void currentPositionChangedEvent(Player.CurrentPositionChangedEventArgs e)
        {
            barCtr.Refresh();
        }

        private void playStateChangedEvent(Player.PlayStateChangedEventArgs e)
        {
            barCtr.Refresh();
        }

        private void tipTimer_Tick(object sender, EventArgs e)
        {
            if (bar_lastloc == bar_eX)
                return;
            if (_controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing || _controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
            {
                toolTip.SetToolTip(barCtr, NgNet.ConvertHelper.ToTimeString((_controller.PlayController.myPlayer.currentMedia.mediaLength / 1000 * bar_lastloc / bar_width)));
            }
            else
            {
                toolTip.SetToolTip(barCtr, "单击跳到歌曲指定进度");
            }
            bar_lastloc = bar_eX;
        }

        private void barCtr_Paint(object sender, PaintEventArgs e)
        {
            //记录进度条的长和宽
            bar_height = barCtr.Height;
            bar_width = barCtr.Width;
            if (_controller.PlayController.myPlayer.currentMedia.mediaLength == 0)
                return;
            if (_controller.PlayController.myPlayer.settings.PlayState != Player.PlayStates.playing && _controller.PlayController.myPlayer.settings.PlayState != Player.PlayStates.paused)
                return;
            CommControls.CommPen.Color = _controller.Theme.ButonDownColor;
            //更新进度条长度
            e.Graphics.DrawRectangle(
                  CommControls.CommPen
                , 0
                , 0
                , (int)(bar_width * ((double)_controller.PlayController.myPlayer.ctControls.currentPosition / _controller.PlayController.myPlayer.currentMedia.mediaLength))
                , bar_height - 1);
        }

        private void barCtr_MouseDown(object sender, MouseEventArgs e)
        {
            //右键不处理
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            //播放到鼠标单击的位置
            if (_controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing || _controller.PlayController.myPlayer.settings.PlayState == Player.PlayStates.paused)
                _controller.PlayController.myPlayer.ctControls.currentPosition = _controller.PlayController.myPlayer.currentMedia.mediaLength * e.X / bar_width;
        }

        private void barCtr_MouseMove(object sender, MouseEventArgs e)
        {
            bar_eX = e.X;
        }

        private void barCtr_MouseEnter(object sender, EventArgs e)
        {
            tipTimer.Enabled = true;
        }

        private void barCtr_MouseLeave(object sender, EventArgs e)
        {
            tipTimer.Enabled = false;
        }
        #endregion

        #region IDisposible
        public void Dispose()
        {
            barCtr.Dispose();
            toolTip.Dispose();
            tipTimer.Dispose();

            GC.Collect();
        }
        #endregion
    }
}
