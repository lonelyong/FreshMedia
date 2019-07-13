using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreshMedia.View
{
    class FormEx : Form
    {
        /// <summary>
        /// 在Alt + Tab视图中显示
        /// </summary>
        public bool ShowInAltTab { get; set; } = false;


        private const int WS_EX_APPWINDOW = 0x40000;
        private const int WS_EX_TOOLWINDOW = 0x80;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!ShowInTaskbar)
                    cp.ExStyle &= (~WS_EX_APPWINDOW);    // 不显示在TaskBar
                if (!ShowInAltTab)
                    cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt-Tab
                return cp;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormEx
            // 
            this.ClientSize = new System.Drawing.Size(295, 256);
            this.Name = "FormEx";
            this.ResumeLayout(false);

        }
    }
}
