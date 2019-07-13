namespace FreshMedia.View
{
    partial class LyricMakerF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.editorDataGridView = new System.Windows.Forms.DataGridView();
            this.dgv_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_lrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.insertButton = new System.Windows.Forms.Button();
            this.cleanButton = new System.Windows.Forms.Button();
            this.timeCheckButton = new System.Windows.Forms.Button();
            this.delButton = new System.Windows.Forms.Button();
            this.insertCurrentButton = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmi_ms_tidy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ms_check = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ms_help = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ms_help_about = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_item = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_moveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_moveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_clean = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_del = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_cut = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorDataGridView)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.cms_item.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.delButton);
            this.ContentPanel.Controls.Add(this.insertCurrentButton);
            this.ContentPanel.Controls.Add(this.timeCheckButton);
            this.ContentPanel.Controls.Add(this.cleanButton);
            this.ContentPanel.Controls.Add(this.insertButton);
            this.ContentPanel.Controls.Add(this.editorDataGridView);
            this.ContentPanel.Controls.Add(this.cancelButton);
            this.ContentPanel.Controls.Add(this.saveButton);
            this.ContentPanel.Controls.Add(this.menuStrip);
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(5, 34);
            this.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ContentPanel.Size = new System.Drawing.Size(1343, 675);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.Transparent;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.saveButton.Location = new System.Drawing.Point(1108, 632);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 29);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "保存";
            this.saveButton.UseVisualStyleBackColor = false;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.cancelButton.Location = new System.Drawing.Point(1220, 632);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 29);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // editorDataGridView
            // 
            this.editorDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.editorDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.editorDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.editorDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_time,
            this.dgv_lrc});
            this.editorDataGridView.EnableHeadersVisualStyles = false;
            this.editorDataGridView.Location = new System.Drawing.Point(19, 40);
            this.editorDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.editorDataGridView.MultiSelect = false;
            this.editorDataGridView.Name = "editorDataGridView";
            this.editorDataGridView.RowHeadersVisible = false;
            this.editorDataGridView.RowHeadersWidth = 51;
            this.editorDataGridView.RowTemplate.Height = 23;
            this.editorDataGridView.Size = new System.Drawing.Size(1315, 582);
            this.editorDataGridView.TabIndex = 3;
            this.editorDataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.editorDataGridView_CellLeave);
            this.editorDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.editorDataGridView_CellMouseDown);
            // 
            // dgv_time
            // 
            this.dgv_time.FillWeight = 28F;
            this.dgv_time.HeaderText = "时间(00:00.00)";
            this.dgv_time.MinimumWidth = 6;
            this.dgv_time.Name = "dgv_time";
            // 
            // dgv_lrc
            // 
            this.dgv_lrc.HeaderText = "歌词";
            this.dgv_lrc.MinimumWidth = 6;
            this.dgv_lrc.Name = "dgv_lrc";
            // 
            // insertButton
            // 
            this.insertButton.BackColor = System.Drawing.Color.Transparent;
            this.insertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.insertButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.insertButton.Location = new System.Drawing.Point(219, 632);
            this.insertButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(100, 29);
            this.insertButton.TabIndex = 5;
            this.insertButton.Text = "插入行";
            this.insertButton.UseVisualStyleBackColor = false;
            // 
            // cleanButton
            // 
            this.cleanButton.BackColor = System.Drawing.Color.Transparent;
            this.cleanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cleanButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.cleanButton.Location = new System.Drawing.Point(441, 632);
            this.cleanButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(100, 29);
            this.cleanButton.TabIndex = 4;
            this.cleanButton.Text = "清空";
            this.cleanButton.UseVisualStyleBackColor = false;
            // 
            // timeCheckButton
            // 
            this.timeCheckButton.BackColor = System.Drawing.Color.Transparent;
            this.timeCheckButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.timeCheckButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.timeCheckButton.Location = new System.Drawing.Point(331, 632);
            this.timeCheckButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.timeCheckButton.Name = "timeCheckButton";
            this.timeCheckButton.Size = new System.Drawing.Size(100, 29);
            this.timeCheckButton.TabIndex = 6;
            this.timeCheckButton.Text = "时间校正";
            this.timeCheckButton.UseVisualStyleBackColor = false;
            // 
            // delButton
            // 
            this.delButton.BackColor = System.Drawing.Color.Transparent;
            this.delButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.delButton.Location = new System.Drawing.Point(552, 632);
            this.delButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.delButton.Name = "delButton";
            this.delButton.Size = new System.Drawing.Size(100, 29);
            this.delButton.TabIndex = 8;
            this.delButton.Text = "移除";
            this.delButton.UseVisualStyleBackColor = false;
            // 
            // insertCurrentButton
            // 
            this.insertCurrentButton.BackColor = System.Drawing.Color.Transparent;
            this.insertCurrentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.insertCurrentButton.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.insertCurrentButton.Location = new System.Drawing.Point(19, 632);
            this.insertCurrentButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.insertCurrentButton.Name = "insertCurrentButton";
            this.insertCurrentButton.Size = new System.Drawing.Size(203, 29);
            this.insertCurrentButton.TabIndex = 7;
            this.insertCurrentButton.Text = "插入行(当前播放时间)";
            this.insertCurrentButton.UseVisualStyleBackColor = false;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ms_tidy,
            this.tsmi_ms_check,
            this.tsmi_ms_help});
            this.menuStrip.Location = new System.Drawing.Point(9, 2);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(199, 28);
            this.menuStrip.TabIndex = 9;
            // 
            // tsmi_ms_tidy
            // 
            this.tsmi_ms_tidy.Name = "tsmi_ms_tidy";
            this.tsmi_ms_tidy.Size = new System.Drawing.Size(53, 24);
            this.tsmi_ms_tidy.Text = "整理";
            this.tsmi_ms_tidy.Click += new System.EventHandler(this.tsmi_ms_items_Click);
            // 
            // tsmi_ms_check
            // 
            this.tsmi_ms_check.Name = "tsmi_ms_check";
            this.tsmi_ms_check.Size = new System.Drawing.Size(83, 24);
            this.tsmi_ms_check.Text = "检查错误";
            this.tsmi_ms_check.Click += new System.EventHandler(this.tsmi_ms_items_Click);
            // 
            // tsmi_ms_help
            // 
            this.tsmi_ms_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ms_help_about});
            this.tsmi_ms_help.Name = "tsmi_ms_help";
            this.tsmi_ms_help.Size = new System.Drawing.Size(53, 24);
            this.tsmi_ms_help.Text = "帮助";
            // 
            // tsmi_ms_help_about
            // 
            this.tsmi_ms_help_about.Name = "tsmi_ms_help_about";
            this.tsmi_ms_help_about.Size = new System.Drawing.Size(122, 26);
            this.tsmi_ms_help_about.Text = "关于";
            this.tsmi_ms_help_about.Click += new System.EventHandler(this.tsmi_ms_items_Click);
            // 
            // cms_item
            // 
            this.cms_item.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cms_item.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_copy,
            this.tsmi_moveDown,
            this.tsmi_moveUp,
            this.tsmi_clean,
            this.tsmi_del,
            this.tsmi_paste,
            this.tsmi_cut});
            this.cms_item.Name = "cms_item";
            this.cms_item.Size = new System.Drawing.Size(154, 172);
            this.cms_item.Opening += new System.ComponentModel.CancelEventHandler(this.cms_item_Opening);
            // 
            // tsmi_copy
            // 
            this.tsmi_copy.Name = "tsmi_copy";
            this.tsmi_copy.Size = new System.Drawing.Size(153, 24);
            this.tsmi_copy.Text = "复制（&C）";
            this.tsmi_copy.Click += new System.EventHandler(this.cms_item_items_Click);
            // 
            // tsmi_moveDown
            // 
            this.tsmi_moveDown.Name = "tsmi_moveDown";
            this.tsmi_moveDown.Size = new System.Drawing.Size(153, 24);
            this.tsmi_moveDown.Text = "向下移动";
            this.tsmi_moveDown.Click += new System.EventHandler(this.cms_item_items_Click);
            // 
            // tsmi_moveUp
            // 
            this.tsmi_moveUp.Name = "tsmi_moveUp";
            this.tsmi_moveUp.Size = new System.Drawing.Size(153, 24);
            this.tsmi_moveUp.Text = "向上移动";
            this.tsmi_moveUp.Click += new System.EventHandler(this.cms_item_items_Click);
            // 
            // tsmi_clean
            // 
            this.tsmi_clean.Name = "tsmi_clean";
            this.tsmi_clean.Size = new System.Drawing.Size(153, 24);
            this.tsmi_clean.Text = "清空单元格";
            this.tsmi_clean.Click += new System.EventHandler(this.cms_item_items_Click);
            // 
            // tsmi_del
            // 
            this.tsmi_del.Name = "tsmi_del";
            this.tsmi_del.Size = new System.Drawing.Size(153, 24);
            this.tsmi_del.Text = "删除行";
            this.tsmi_del.Click += new System.EventHandler(this.cms_item_items_Click);
            // 
            // tsmi_paste
            // 
            this.tsmi_paste.Name = "tsmi_paste";
            this.tsmi_paste.Size = new System.Drawing.Size(153, 24);
            this.tsmi_paste.Text = "粘贴（&V）";
            this.tsmi_paste.Click += new System.EventHandler(this.cms_item_items_Click);
            // 
            // tsmi_cut
            // 
            this.tsmi_cut.Name = "tsmi_cut";
            this.tsmi_cut.Size = new System.Drawing.Size(153, 24);
            this.tsmi_cut.Text = "剪切（&X）";
            this.tsmi_cut.Click += new System.EventHandler(this.cms_item_items_Click);
            // 
            // LyricMakerF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(1353, 714);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LyricMakerF";
            this.Opacity = 0.9D;
            this.Padding = new System.Windows.Forms.Padding(5, 2, 5, 5);
            this.Text = "歌词编辑器";
            this.Load += new System.EventHandler(this.this_Load);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorDataGridView)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.cms_item.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridView editorDataGridView;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button cleanButton;
        private System.Windows.Forms.Button timeCheckButton;
        private System.Windows.Forms.Button insertCurrentButton;
        private System.Windows.Forms.Button delButton;
        private System.Windows.Forms.ContextMenuStrip cms_item;
        private System.Windows.Forms.ToolStripMenuItem tsmi_copy;
        private System.Windows.Forms.ToolStripMenuItem tsmi_moveDown;
        private System.Windows.Forms.ToolStripMenuItem tsmi_moveUp;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clean;
        private System.Windows.Forms.ToolStripMenuItem tsmi_del;
        private System.Windows.Forms.ToolStripMenuItem tsmi_paste;
        private System.Windows.Forms.ToolStripMenuItem tsmi_cut;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ms_tidy;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ms_check;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ms_help;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ms_help_about;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_lrc;

    }
}