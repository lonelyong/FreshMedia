using System.Windows.Forms;
using System.Drawing;

namespace FreshMedia.View
{
    class CommControls
    {
        public static ImageList CommImglist { get; }

        public static Pen CommPen { get; }

        public static SolidBrush CommSolidBrush { get; }

        public static PointF CommPointF;

        #region construct function
        static CommControls()
        {
            CommImglist = new ImageList();
            CommPen = new Pen(Color.AliceBlue, 1);
            CommSolidBrush = new SolidBrush(Color.AliceBlue);
            CommControls.initialize();
        }
        #endregion

        #region initializ
        private static void initImgLst()
        {
            CommImglist.Images.Add(Properties.Resources.MUSICPNG);
            CommImglist.Images.Add(Properties.Resources.MUSICPNG_1);
            CommImglist.Images.Add(Properties.Resources.dir_2);
            CommImglist.Images.Add(Properties.Resources.item_playing);
            CommImglist.Images.Add(Properties.Resources.item_paused);
            CommImglist.Images.Add(Properties.Resources.History);
            CommImglist.Images.Add(Properties.Resources.Favorite);//6
            CommImglist.Images.Add(new Bitmap(1, 1));
            CommImglist.Images.Add(Properties.Resources.siyecao);
        }

        private static void initialize()
        {
            CommControls.initImgLst();
        }
        #endregion
    }
}
