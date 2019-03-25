using System.Windows.Forms;
using System.Drawing;
namespace FreshMedia.View
{
    class TreeViewEx : TreeView
    {
        #region private filed
        //treeNode back
        SolidBrush backSB = new SolidBrush(Color.Azure);
        //treeNode fore
        SolidBrush foreSB = new SolidBrush(Color.DarkGreen);
        //记录字符串的大小
        SizeF sizeString = new SizeF();
        //treeNode 的image
        int imageIndex = 0;
        #endregion

        #region public filed
        /// <summary>
        /// 选中项（treeNode）的背景色
        /// </summary>
        public Color NodeSelectedBackColor { get; set; }
        /// <summary>
        /// 选中项（treeNode）的前景色
        /// </summary>
        public Color NodeSelectedForeColor { get; set; }
        #endregion

        #region override
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {

            sizeString = e.Graphics.MeasureString(e.Node.Text, Font);
            if (e.Node.IsSelected)
            {
                if (Focused || !HideSelection)
                {
                    backSB.Color = NodeSelectedBackColor;
                    foreSB.Color = NodeSelectedForeColor;
                }
                else
                {
                    backSB.Color = e.Node.BackColor;
                    foreSB.Color = e.Node.ForeColor;
                }
            }
            else
            {
                backSB.Color = e.Node.BackColor;
                foreSB.Color = e.Node.ForeColor;
            }
            e.Graphics.FillRectangle(backSB, e.Bounds);
            // 绘制图标
            if (ImageList == null || ImageList.Images.Count == 0)
            {
                imageIndex = -1;
            }
            else
            {
                if (e.Node.IsSelected)
                {
                    imageIndex = e.Node.SelectedImageIndex == -1 ? SelectedImageIndex == -1 ? 0 : SelectedImageIndex : e.Node.SelectedImageIndex;
                }
                else
                {
                    imageIndex = e.Node.ImageIndex == -1 ? ImageIndex == -1 ? 0 : ImageIndex : e.Node.ImageIndex;
                }
                e.Graphics.DrawImage(ImageList.Images[imageIndex], e.Node.Level * Indent, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
            }
            imageIndex = imageIndex == -1 ? 0 : 1;
            e.Graphics.DrawString(e.Node.Text, Font, foreSB, Indent * (e.Node.Level + imageIndex), e.Bounds.Top + (int)((e.Bounds.Height - sizeString.Height) / 2));
        }
        #endregion

        #region constructor destructor 
        public TreeViewEx() : base()
        {
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
            NodeSelectedBackColor = SystemColors.Highlight;
            NodeSelectedForeColor = Color.White;
        }
        #endregion
    }
}
