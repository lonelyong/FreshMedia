using NgNet.UI;
using NgNet.UI.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace FreshMedia.View
{
    partial class LyricMakerF : NgNet.UI.Forms.TitleableForm
    {
        #region 私有变量
        string audioPath = null;//存储当前的歌词对应的音乐
        string lyricPath
        {
            set
            {
                this.Text = string.Format("{0} - {1}", string.IsNullOrWhiteSpace(value) ? "未命名" : Path.GetFileName(value), "歌词编辑器");
            }
        }
        Controller.MainController _controller;
        #endregion

        #region constructor destructor 
        public LyricMakerF(Controller.MainController _controller)
        {
            InitializeComponent();
            this._controller = _controller;
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            FormHelper.SetFormRoundRgn(3, 3);
            FormHelper.AutoBorder(new BorderSize(), new BorderSize(3, 1, 3, 3));
            //移动窗体
            Icon = NgNet.ConvertHelper.Bitmap2Icon(Properties.Resources.lyricmaker_32_32);
            TitleBar.Icon = Icon;
            Resizeable = true;
            TaskbarSetWindowState = true;
        }
        #endregion

        #region this
        private void this_Load(object sender, EventArgs e)
        {
            //this.setupSystemMenu();
            //自动应用主题
            _controller.Theme.ThemeChanged += new ThemeChangedEventHandler(themeChangedEvent);
            FormClosed += new FormClosedEventHandler((object sender1, FormClosedEventArgs e1) =>
            {
                _controller.Theme.ThemeChanged -= new ThemeChangedEventHandler(themeChangedEvent);
            });
            SizeChanged += new EventHandler(this.this_SizeChanged);
            this_SizeChanged(sender, e);
        }

        private void this_SizeChanged(object sender, EventArgs e)
        {
            this.insertCurrentButton.Top = ContentPanel.Height - insertCurrentButton.Height - 6; 
            this.cleanButton.Top = this.insertCurrentButton.Top;
            this.timeCheckButton.Top = this.insertCurrentButton.Top;
            this.delButton.Top = this.insertCurrentButton.Top;
            this.insertButton.Top = this.insertCurrentButton.Top;
            this.cancelButton.Top = this.insertCurrentButton.Top;
            this.saveButton.Top = this.insertCurrentButton.Top;

            this.editorDataGridView.Top = menuStrip.Bottom + 2;
            this.editorDataGridView.Height = insertCurrentButton.Top - editorDataGridView.Top - 6;
            this.editorDataGridView.Width = ContentPanel.Width - editorDataGridView.Left * 2;

            this.cancelButton.Left = editorDataGridView.Right - cancelButton.Width;
            this.saveButton.Left = cancelButton.Left - saveButton.Width + 1;
        }
        #endregion

        #region private method
        private bool rowTest(DataGridViewRow dgvw)
        {
           
            return true;
        }

        private bool legalityTest()
        {
            foreach (DataGridViewRow item in this.editorDataGridView.Rows)
            {
                if(!this.rowTest(item))
                    return false;
            }
            return true;
        }

        private void themeChangedEvent(ThemeChangedEventArgs e)
        {
            BackColor = e.ThemeClass.BackColor;
            BorderColor = e.ThemeClass.BorderColor;
            menuStrip.Renderer = (e.ThemeClass as ThemeManager).MenuRender;
            editorDataGridView.BackgroundColor = e.ThemeClass.BackColor;
            editorDataGridView.DefaultCellStyle.BackColor = e.ThemeClass.BackColor;
            editorDataGridView.ColumnHeadersDefaultCellStyle.BackColor = e.ThemeClass.BackColor;
            editorDataGridView.AlternatingRowsDefaultCellStyle.BackColor = (e.ThemeClass as ThemeManager).ButtonEnterColor;
        }
        #endregion

        #region public method
        public void Reset()
        {
            
        }
            
        public void Create(string audioPath)
        {
            LyricMakerF lm = new LyricMakerF(_controller);
            lm.lyricPath = Lyric.LyricApi.getLyricPathByAudio(audioPath, false);
            lm.audioPath = audioPath;
            lm.Reset();
            lm.Show();
        }

        public void Show(string audioPath)
        {         
            if (File.Exists(Lyric.LyricApi.getLyricPathByAudio(audioPath)))
            {
                LyricMakerF lm = new LyricMakerF(_controller);
                lm.lyricPath = Lyric.LyricApi.getLyricPathByAudio(audioPath, false);
                lm.audioPath = audioPath;
                lm.ShowDialog();
            }
            else
            {
                LyricMakerF lm = new LyricMakerF(_controller);
                lm.Create(audioPath);
            }
        }
        #endregion

        #region menu

        #region cms_item
        private void cms_item_Opening(object sender, CancelEventArgs e)
        {
            this.tsmi_moveDown.Enabled = this.editorDataGridView.SelectedCells[0].RowIndex != this.editorDataGridView.Rows.Count - 1;
            this.tsmi_moveUp.Enabled = this.editorDataGridView.SelectedCells[0].RowIndex != 0;
            this.tsmi_clean.Enabled = !this.editorDataGridView.SelectedCells[0].ReadOnly;
            this.tsmi_paste.Enabled = !string.IsNullOrWhiteSpace(Clipboard.GetText());
            this.tsmi_paste.Enabled = !this.editorDataGridView.SelectedCells[0].ReadOnly;
            this.tsmi_cut.Enabled = !this.editorDataGridView.SelectedCells[0].ReadOnly;
        }

        private void cms_item_items_Click(object sender, EventArgs e)
        {
            if (sender == this.tsmi_clean)
            {
                this.editorDataGridView.SelectedCells[0].Value = null;
            }
            else if (sender == this.tsmi_copy)
            {
                Clipboard.SetText(this.editorDataGridView.SelectedCells[0].Value.ToString());
            }
            else if (sender == this.tsmi_cut)
            {
                Clipboard.SetText(this.editorDataGridView.SelectedCells[0].Value.ToString());
                this.editorDataGridView.SelectedCells[0].Value = null;
            }
            else if (sender == this.tsmi_del)
            {
         
            }
            else if (sender == this.tsmi_moveDown)
            {
                int rowindex = this.editorDataGridView .SelectedCells [0].RowIndex ;
                object dgvw = this.editorDataGridView.Rows[rowindex + 1].Clone();
                object tmptime = this.editorDataGridView.Rows[rowindex + 1].Cells[0].Value;
                object tmplrc = this.editorDataGridView.Rows[rowindex + 1].Cells[1].Value;
                try
                {
                    this.editorDataGridView.Rows.RemoveAt(rowindex + 1);
                    this.editorDataGridView.Rows.Insert(rowindex, (DataGridViewRow)dgvw);
                    this.editorDataGridView.Rows[rowindex].Cells[0].Value = tmptime;
                    this.editorDataGridView.Rows[rowindex].Cells[1].Value = tmplrc;
                    this.editorDataGridView.Rows[rowindex + 1].Cells[0].Selected = true;
                }
                catch (Exception)
                {

                }
            }
            else if (sender == this.tsmi_moveUp)
            {
                int rowindex = this.editorDataGridView.SelectedCells[0].RowIndex;
                object dgvw = this.editorDataGridView.Rows[rowindex].Clone();
                object tmptime = this.editorDataGridView.Rows[rowindex].Cells[0].Value;
                object tmplrc = this.editorDataGridView.Rows[rowindex].Cells[1].Value;
                try
                {
                    this.editorDataGridView.Rows.RemoveAt(rowindex);
                    this.editorDataGridView.Rows.Insert(rowindex-1, (DataGridViewRow)dgvw);
                    this.editorDataGridView.Rows[rowindex-1].Cells[0].Value = tmptime;
                    this.editorDataGridView.Rows[rowindex-1].Cells[1].Value = tmplrc;
                    this.editorDataGridView.Rows[rowindex - 1].Cells[0].Selected = true;
                }
                catch (Exception)
                {

                }
            }
            else if (sender == this.tsmi_paste)
            {
                this.editorDataGridView.SelectedCells[0].Value = Clipboard.GetText();
            }

        }
        #endregion

        #region editorDataGridView
        private void editorDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            this.editorDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            if (e.Button == MouseButtons.Left)
            {
                this.editorDataGridView.BeginEdit(false);
            }
            else
            {
                this.editorDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = this.cms_item;
            }
        }

        private void editorDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (this.editorDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                this.editorDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value 
                    = this.editorDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
            }
        }
        #endregion

        #region ms_tools
        private void tsmi_ms_items_Click(object sender, EventArgs e)
        {
            if (sender == this.tsmi_ms_check)
            {
                this.editorDataGridView.EndEdit();
                this.legalityTest();
            }
            else if (sender == this.tsmi_ms_help_about)
            {
                NgNet.UI.Forms.MessageBox.Show(this, "LyricMaker beta1.0!", "Lyric编辑器", MessageBoxButtons.OK);
            }
            else if (sender == this.tsmi_ms_tidy)
            {

            }
        }
        #endregion

        #endregion
    }
}
