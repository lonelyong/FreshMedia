using NgNet.UI;
using NgNet.UI.Forms;
using System;

namespace FreshMedia.View
{
    /******************************
    //恢复备份数据用户界面
    //
    //
    ******************************/
    class RestoreView
    {
        public bool IsLoaded
        {
            get
            {
                return FormHelper.IsLoaded(f);
            }
        }

        #region private fileds
        const string FILTER = "*.backup|NgNet Audio备份文件";

        string bakPath;

        private int[] typeValues;

        Controller.MainController _controller;

        Data.BackupMamager _backup;

        FormHelper formHelper;
        //控件
        TitleBar titleBar;
        System.Windows.Forms.Form f;
        System.Windows.Forms.Label metaLabel;
        System.Windows.Forms.Panel mPanel;
        System.Windows.Forms.Label scrLabel;
        System.Windows.Forms.TextBox scrTextBox;
        System.Windows.Forms.Button scrButton;

        System.Windows.Forms.Button okButton;
        System.Windows.Forms.Button exitButtion;

        System.Windows.Forms.Label[] labels;
        System.Windows.Forms.CheckBox[] checkBoxs;
        #endregion

        #region private methods
        private void init()
        {
            f = new System.Windows.Forms.Form();
            mPanel = new System.Windows.Forms.Panel();
            scrLabel = new System.Windows.Forms.Label();
            scrButton = new System.Windows.Forms.Button();
            scrTextBox = new System.Windows.Forms.TextBox();
            okButton = new System.Windows.Forms.Button();
            exitButtion = new System.Windows.Forms.Button();
            metaLabel = new System.Windows.Forms.Label();
            titleBar = new TitleBar(f, TitleBarStyles.MinMaxEnd);
            formHelper = new FormHelper(f);
            checkBoxs = new System.Windows.Forms.CheckBox[typeValues.Length];
            labels = new System.Windows.Forms.Label[typeValues.Length];

            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            f.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            f.Text = "恢复列表及设置";
            f.Load += new EventHandler(f_Load);
            f.SizeChanged += new EventHandler(f_SizeChanged);
            f.Controls.Add(mPanel);

            mPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                    okButton,
                    exitButtion,
                    scrButton,
                    scrLabel,
                    metaLabel,
                    scrTextBox,
                });

            scrLabel.Text = "路径：";
            scrLabel.AutoSize = true;

            scrButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            scrButton.Text = "浏览";
            scrButton.Click += new EventHandler(scrButton_Click);

            scrTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            scrTextBox.Text = null;
            scrTextBox.ReadOnly = true;

            metaLabel.AutoSize = false;
            metaLabel.Height = 52;
            metaLabel.Text = "信息";
            metaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = new System.Windows.Forms.Label();
                labels[i].Text = Data.BakDefinition.GetBakTypeDesc((Data.BakDefinition.BakType)typeValues[i]) + "：";

                checkBoxs[i] = new System.Windows.Forms.CheckBox();
                checkBoxs[i].AutoSize = true;
                checkBoxs[i].Text = "不恢复";
                checkBoxs[i].Enabled = false;
                checkBoxs[i].CheckStateChanged += new EventHandler((object sender, EventArgs e) => {
                    System.Windows.Forms.CheckBox _cb = (System.Windows.Forms.CheckBox)sender;
                    _cb.Text = _cb.Checked ? "恢复" : "不恢复";
                });
                mPanel.Controls.Add(labels[i]);
                mPanel.Controls.Add(checkBoxs[i]);
            }

            exitButtion.Text = "退出";
            exitButtion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            exitButtion.Click += new EventHandler((object sender, EventArgs e) => {
                f.Close();
            });
            okButton.Text = "恢复";
            okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            okButton.Click += new EventHandler(okButton_Click);

            f.Size = new System.Drawing.Size(500, 600);

            EventBindingAndUnbindingListener();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int[] _types = new int[labels.Length];
            bool _boolTemp;
            string _inf;

            if(System.IO.File.Exists(bakPath) == false)
            {
                scrButton_Click(sender, e);
                return;
            }

            #region 获取要恢复的数据
            for (int i = 0; i < typeValues.Length; i++)
            {
                _types[i] = checkBoxs[i].Enabled && checkBoxs[i].Checked ? 1 : 0;
            }
            #endregion

            #region 恢复数据
            try
            {
                _boolTemp = _backup.Restore(bakPath, _types);
            }
            catch (Exception ex)
            {
                _inf = ex.Message;
                goto FALSE;
            }
            NgNet.UI.Forms.MessageBox.Show(f, "恭喜您，恢复成功！", "数据恢复");
            return;
            #endregion

        FALSE:
            NgNet.UI.Forms.MessageBox.Show(f, $"恢复失败，可能原因：\n    1.{_inf}", "数据恢复");
            return;
        }

        private void scrButton_Click(object sender, EventArgs e)
        {
            string _scrPath;
            int[] _existTypes;
            string _meta;
            string _inf = null;
            #region 浏览备份文件
            NgNet.UI.Forms.OpenFileDialog _ofd = new NgNet.UI.Forms.OpenFileDialog();
            _ofd.Title = "选择备份文件";
            _ofd.Enterpath = NgNet.Applications.Current.AssemblyLocation;
            _ofd.Filter = FILTER;
            _scrPath = _ofd.Show(f);
            if (string.IsNullOrWhiteSpace(_scrPath))
                return;
            scrTextBox.Text = _scrPath;
            bakPath = _scrPath;
            #endregion

            #region 分析备份文件
            try
            {
                _backup.AnalyseBak(_scrPath, out _existTypes, out _meta);
            }
            catch (Exception ex)
            {
                _inf = ex.Message;
                goto FALSE;
            }
            #endregion

            #region 应用分析结果
            for (int i = 0; i < typeValues.Length; i++)
            {
                checkBoxs[i].Enabled = _existTypes[i] == 1;
                checkBoxs[i].Checked = checkBoxs[i].Enabled;
            }
            metaLabel.Text = _meta;
            okButton.Enabled = true;
            return;
        #endregion
        FALSE:
            okButton.Enabled = false;
            metaLabel.Text = _inf;
            return;

        }

        private void f_Load(object sender, EventArgs e)
        {
            titleBar.Title = f.Text;
            ThemeChangedEvent(new ThemeChangedEventArgs(_controller.Theme));
            formHelper.Move(mPanel);
        }

        private void f_SizeChanged(object sender, EventArgs e)
        {
            scrLabel.Location = new System.Drawing.Point(10, titleBar.Height + 12);
            scrButton.Location = new System.Drawing.Point(mPanel.Width - scrLabel.Left - scrButton.Width, scrLabel.Top + (scrLabel.Height - scrButton.Height) / 2);
            scrButton.Height = scrTextBox.Height;
            scrTextBox.Location = new System.Drawing.Point(scrLabel.Right, scrLabel.Top + (scrLabel.Height - scrTextBox.Height) / 2);
            scrTextBox.Left = scrLabel.Right;
            scrTextBox.Width = scrButton.Left - scrTextBox.Left + 1;

            metaLabel.Left = scrLabel.Left;
            metaLabel.Top = scrLabel.Bottom + (scrLabel.Top - titleBar.Height);
            metaLabel.Width = mPanel.Width - scrLabel.Left * 2;
            for (int i = 0; i < labels.Length; i++)
            {
                if (i == 0)
                    labels[i].Location = new System.Drawing.Point(scrLabel.Left, metaLabel.Bottom + 20);
                else
                    labels[i].Location = new System.Drawing.Point(scrLabel.Left, labels[i - 1].Bottom + 10);

                checkBoxs[i].Location = new System.Drawing.Point(mPanel.Width - scrLabel.Left - checkBoxs[i].Width, labels[i].Top);
            }
         
            exitButtion.Location = new System.Drawing.Point(mPanel.Width - scrLabel.Left - exitButtion.Width, mPanel.Height - (scrLabel.Top - titleBar.Height) - exitButtion.Height);
            okButton.Location = new System.Drawing.Point(exitButtion.Left - okButton.Width + 1, exitButtion.Top);
        }
        #endregion

        #region public methods
        public void Show()
        {
            if (IsLoaded)
                if (f.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                    f.WindowState = System.Windows.Forms.FormWindowState.Normal;
                else
                    f.Focus();
            else
            {
                init();
                f.Show();
            }
        }
        #endregion

        #region IWinformTheme
        public void ThemeChangedEvent(ThemeChangedEventArgs e)
        {
            foreach (System.Windows.Forms.Control item in mPanel.Controls)
            {
                item.ForeColor = e.ThemeClass.ForeColor;
            }
            f.Opacity = e.ThemeClass.Opacity;
            f.BackColor = e.ThemeClass.BorderColor;
            mPanel.BackColor = e.ThemeClass.BackColor;
            scrTextBox.BackColor = e.ThemeClass.BackColor;
            titleBar.BackColor = e.ThemeClass.BorderColor;
            metaLabel.BackColor = NgNet.Drawing.ColorHelper.GetSimilarColor(e.ThemeClass.BackColor, true, NgNet.Level.Level2);
        }
        
        public void EventBindingAndUnbindingListener()
        {
            f.Load += new EventHandler((object sender, EventArgs e) => {
                _controller.Theme.ThemeChanged += new ThemeChangedEventHandler(ThemeChangedEvent);
            });
            f.FormClosed += new System.Windows.Forms.FormClosedEventHandler((object sender, System.Windows.Forms.FormClosedEventArgs e) => {
                _controller.Theme.ThemeChanged -= new ThemeChangedEventHandler(ThemeChangedEvent);
            });
        }
        #endregion

        #region constructor
        public RestoreView(Data.BackupMamager _backup)
        {
            this._controller = ((IFreshMedia)_backup).Controller;
            this._backup = _backup;
            typeValues = (int[])Enum.GetValues(typeof(Data.BakDefinition.BakType));
        }
        #endregion
    }
}
