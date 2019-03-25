using System;
using System.Drawing;
using System.Windows.Forms;
using NgNet.UI.Forms;

namespace FreshMedia.View
{
    class VolumePanel : IDisposable
    {
        #region private filed
        Controller.MainController _controller;
        Form owner;
        FormEx fm;
        Label muteLabel;
        Panel pnl;
        Graphics g;
        Color[] colorLevels = new Color[V_LEVEL_COUNT];
        System.Windows.Forms.Timer drawTimer;
        #endregion

        #region public filed
        public bool IsLoaded
        {
            get
            {
                return FormHelper.IsLoaded(fm);
            }
        }
        #endregion

        #region constructor destructor 
        public VolumePanel(Form owner, Controller.MainController _controller)
        {
            this._controller = _controller;
            this.owner = owner;
        }
        #endregion

        #region public method
        public void Show(Point p)
        {
            if (IsLoaded)
                return;

            fm = new FormEx();
            pnl = new Panel();
            muteLabel = new Label();
            drawTimer = new System.Windows.Forms.Timer();

            pnl.Dock = DockStyle.Fill;
            pnl.BackColor = _controller.Theme.BackColor;
            pnl.Controls.Add(muteLabel);
            pnl.Paint += new PaintEventHandler(pnl_Paint);
            pnl.MouseDown += new MouseEventHandler(pnl_MouseDown);
            pnl.MouseMove += new MouseEventHandler(pnl_MouseMove);
            pnl.MouseUp += new MouseEventHandler(pnl_MouseUp);
            pnl.MouseWheel += new MouseEventHandler(pnl_MouseWheel);

            muteLabel.Text = "√";
            muteLabel.Size = new Size(pnl.Width, 13);
            muteLabel.Dock = DockStyle.Bottom;
            muteLabel.TextAlign = ContentAlignment.MiddleCenter;
            muteLabel.BackColor = _controller.Theme.BorderColor;
            muteLabel.ForeColor = _controller.Theme.BackColor;
            muteLabel.Click += new EventHandler((object sender, EventArgs e) =>
            {
                _controller.PlayController.myPlayer.settings.Mute = !_controller.PlayController.myPlayer.settings.Mute;
                setMute(_controller.PlayController.myPlayer.settings.Mute);
            });

            drawTimer.Enabled = false;
            drawTimer.Interval = 20;
            drawTimer.Tick += new EventHandler((object sender, EventArgs e) => { drawTimer_Tick(sender, e); });

            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Opacity = 0.8;
            fm.ShowIcon = false;
            fm.ShowInTaskbar = false;
            fm.Owner = owner;
            fm.BackColor = _controller.Theme.BorderColor;
            fm.Padding = new Padding(1, 1, 1, 1);
            fm.Width = 22;
            fm.Height = V_HEIGTH + muteLabel.Height + START_POSI + fm.Padding.Top + fm.Padding.Bottom;
            fm.StartPosition = FormStartPosition.Manual;
            fm.Location = new Point(p.X - fm.Width / 2, p.Y - fm.Height);
            fm.Controls.Add(pnl);
            fm.Deactivate += new EventHandler((object sender, EventArgs e) => { Close(); });

            int hraf = colorLevels.Length / 2;
            for (int i = 0; i < colorLevels.Length; i++)
            {
                if (i < hraf)
                    colorLevels[i] = NgNet.Drawing.ColorHelper.GetSimilarColor(_controller.Theme.BorderColor, false, (NgNet.Level)(hraf - i));
                if (i == hraf)
                    colorLevels[i] = _controller.Theme.BorderColor;
                if (i > hraf)
                    colorLevels[i] = NgNet.Drawing.ColorHelper.GetSimilarColor(_controller.Theme.BorderColor, true, (NgNet.Level)(i - hraf));
            }
            fm.Show();
        }

        public void Close()
        {
            if (IsLoaded)
                fm.Close();
        }
        #endregion

        #region private method
        private void pnl_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            this.showVolume(_controller.PlayController.myPlayer.settings.Volume);
            this.setMute(_controller.PlayController.myPlayer.settings.Mute);
            g = pnl.CreateGraphics();
        }

        private void pnl_MouseDown(object sender, MouseEventArgs e)
        {
            this.drawTimer.Enabled = true;
            if (e.Y < START_POSI)
                tmp = 0;
            else if (e.Y > START_POSI + V_HEIGTH)
                tmp = V_HEIGTH;
            else
                tmp = e.Y - START_POSI;
            v = (byte)((1 - (float)tmp / V_HEIGTH) * V_MAX);
        }

        private void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < START_POSI)
                tmp = 0;
            else if (e.Y > START_POSI + V_HEIGTH)
                tmp = V_HEIGTH;
            else
                tmp = e.Y - START_POSI;
            v = (byte)((1 - (float)tmp / V_HEIGTH) * V_MAX);
        }

        private void pnl_MouseUp(object sender, MouseEventArgs e)
        {
            this.drawTimer.Enabled = false;
        }

        private void pnl_MouseWheel(object sender, MouseEventArgs e)
        {
            int _v;
            _v = _controller.PlayController.myPlayer.settings.Volume + (e.Delta > 0 ? 2 : -2);
            if (_v >= 100)
                _v = 100;
            if (_v <= 0)
                _v = 0;
            setVolume((byte)_v);
        }

        private void drawTimer_Tick(object sender, EventArgs e)
        {
            if (v == v_tmp)
                return;
            setVolume(v);
            v_tmp = v;
        }

        int tmp;
        byte v_tmp;
        byte v;
        byte v_level = 0;
        byte v_left = 0;
        const byte V_LEVEL_HEIGHT = 10;
        const byte V_LEVEL_LENGTH = 10;
        const byte V_LEVEL_COUNT = 10;
        const int V_HEIGTH = 100;
        const byte V_MAX = 100;
        const byte START_POSI = 10;

        SolidBrush sBrush = new SolidBrush(Color.Aqua);

        private void showVolume(byte v)
        {
            this.v_level = (byte)(v / V_LEVEL_LENGTH);
            this.v_left = (byte)(v % V_LEVEL_LENGTH);
            sBrush.Color = _controller.Theme.BackColor;
            g.FillRectangle(sBrush, 0, START_POSI, pnl.Width, V_HEIGTH);
            for (int i = 0; i < v_level; i++)
            {
                sBrush.Color = this.colorLevels[i];
                g.FillRectangle(sBrush, 0, START_POSI + V_HEIGTH - V_LEVEL_LENGTH * (i + 1), pnl.Width, V_LEVEL_HEIGHT);
            }
            if (this.v_left > 0)
            {
                sBrush.Color = this.colorLevels[v_level];
                float tmp = V_LEVEL_HEIGHT * ((float)v_left / V_LEVEL_LENGTH);
                g.FillRectangle(sBrush, 0, START_POSI + (V_LEVEL_COUNT - v_level) * V_LEVEL_HEIGHT - tmp, pnl.Width, tmp);
            }
        }

        private void setMute(bool tf)
        {
            _controller.PlayController.myPlayer.settings.Mute = tf;
            muteLabel.Font = new Font(muteLabel.Font.Name, muteLabel.Font.Size, tf ? FontStyle.Strikeout : FontStyle.Regular, GraphicsUnit.Pixel);
        }

        private void setVolume(byte v)
        {
            _controller.PlayController.myPlayer.settings.Volume = v;
            setMute(_controller.PlayController.myPlayer.settings.Volume == 0);
            showVolume(_controller.PlayController.myPlayer.settings.Volume);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            fm.Dispose();
            muteLabel.Dispose();
            pnl.Dispose();
            g.Dispose();
            drawTimer.Dispose();
            sBrush.Dispose();
        }
        #endregion
    }
}
