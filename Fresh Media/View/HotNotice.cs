
using System;
using System.Drawing;
using System.Windows.Forms;
using NgNet.UI.Forms;
namespace FreshMedia.View
{
    class HotNotice
    {
        #region private fileds
        private FormEx _f  = null;
        private Label  _label = null;
        private string _message = string.Empty;
        // 显示消息窗体的容器
        private Control _ctrParent;
        private Timer _timer = null;
        // 计时器interval
        private uint _interval = 300;
        // 已经显示的时间
        private uint _showedTime = 0;
        // 消息可以显示的时间
        private uint _showTime = 8000;
        #endregion

        #region public properties
        /// <summary>
        /// 指示窗口是否已经显示
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return FormHelper.IsLoaded(_f);
            }
        }
        #endregion

        #region constructor destructor 
        /// <summary>
        /// 消息显示时长
        /// </summary>
        /// <param name="showTime"></param>
        public HotNotice(uint showTime, Control ctrParent)
        {
            this._showTime = showTime;
            this._ctrParent = ctrParent;
        }

        #endregion

        #region public method
        /// <summary>
        /// 在控件上显示通知
        /// </summary>
        /// <param name="msg"></param>
        public void Show(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
                return;
            if (!this.IsLoaded)
            {
                this._f = new FormEx();
                this._label = new Label();
                this._timer = new System.Windows.Forms.Timer();
                //动态调整窗口位置
                this._ctrParent.SizeChanged += new EventHandler(setLocation);
                this._ctrParent.LocationChanged += new EventHandler(setLocation);
                _f.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) =>
                {
                    this._ctrParent.SizeChanged -= new EventHandler(setLocation);
                    this._ctrParent.LocationChanged -= new EventHandler(setLocation);
                });
                //label
                this._label.BackColor = System.Drawing.Color.Transparent;
                this._label.Dock = System.Windows.Forms.DockStyle.Fill;
                this._label.Font = new System.Drawing.Font(string.Empty, 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this._label.ForeColor = _ctrParent.ForeColor;
                this._label.Location = new System.Drawing.Point(0, 0);
                this._label.Size = new System.Drawing.Size(100, 20);
                this._label.TextAlign = ContentAlignment.MiddleCenter;
                this._label.AutoSize = true;
                this._label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                //fm
                this._f.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
                this._f.BackColor = _ctrParent.BackColor;
                this._f.ClientSize = new System.Drawing.Size(100, 20);
                this._f.Controls.Add(this._label);
                this._f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this._f.Opacity = 0.7D;
                this._f.AutoSize = true;
                this._f.MaximumSize = new Size(389, 40);
                this._f.TopLevel = false;
                this._f.ShowIcon = false;
                this._f.ShowInTaskbar = false;
                this._f.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                this._f.Size = _label.Size;
                this._f.Load += new System.EventHandler((object sender, EventArgs e) => { this._timer.Enabled = true; });
                this._f.Shown += new EventHandler((object sender, EventArgs e) => { this.setLocation(null, null); });
                this._f.ResumeLayout(false);
                this._f.Controls.Add(_label);
                //timer
                this._timer.Interval = (int)_interval;
                this._timer.Tick += new System.EventHandler(this.t_Tick);
                _ctrParent.Controls.Add(_f);
                this._f.Show();
            }           
            this._label.Text = msg;
            this._showedTime = 0;
        }
        #endregion

        #region private method
        private void setLocation(object sender, EventArgs e)
        {
            if (IsLoaded)
                _f.Location = new Point((_ctrParent.Width - _label.Width) / 2, 10);
        }

        private void Close()
        {
            if (IsLoaded)
            {
                _f.Close();
            }
        }

        private void t_Tick(object sender, EventArgs e)
        {
            _showedTime += _interval;
            _label.ForeColor = _showedTime % (2 * _interval) == 0 ? _ctrParent.ForeColor : _ctrParent.BackColor;
            if (this._showedTime > _showTime)
            {
                _timer.Enabled = false;
                this.Close();
            }
        }
        #endregion
    }
}
