using System.Drawing;
using System.Windows.Forms;

namespace FreshMedia
{
    class AppConfigs
    {
        #region private fields
        private Size _StartSize = new Size(532, 898);
        #endregion

        #region 用户界面
        /// <summary>
        /// 最小窗口大小
        /// </summary>
        public Size MinimumFromSize { get; } = new Size(919, 537);
        /// <summary>
        /// 窗体启动大小
        /// </summary>
        public Size StartSize
        {
            set
            {
                if (value.Height < MinimumFromSize.Height || value.Height >= Screen.PrimaryScreen.WorkingArea.Height)
                    value.Height = MinimumFromSize.Height;
                if (value.Width < MinimumFromSize.Width || value.Width >= Screen.PrimaryScreen.WorkingArea.Width)
                    value.Width = MinimumFromSize.Width;
                _StartSize = value;
            }
            get { return _StartSize; }
        }
        /// <summary>
        /// 是否显示启动画面
        /// </summary>
        public bool StartboxEnable { get; set; } = true;
        /// <summary>
        /// 退出样式 - 是否提示对话框
        /// 0：exit
        /// 1：minsize
        /// </summary>
        public NgNet.UI.Forms.ExitStyles ExitStyle { get; set; } = NgNet.UI.Forms.ExitStyles.MinsizeChoose;
        #endregion

        #region 播放参数
        //是否自动播放
        public bool AutoPlay { get; set; }
        #endregion

    }
}
