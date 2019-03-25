using System.ComponentModel;
using System.Drawing;
using NgNet.UI;

namespace FreshMedia.View
{
    enum PresetTheme : int
    {
        [Description("经典")]
        Classical = 0,
        [Description("清爽")]
        Cool = 1,
        [Description("沉默灰")]
        SilentGray = 2,
        [Description("活力绿")]
        Green = 3 };

    class ThemeManager : Theme
    {
        #region public theme field
        private PresetTheme _current = PresetTheme.Classical;
        /// <summary>
        /// 获取或设置当前主题
        /// </summary>
        public PresetTheme Current
        {
            set
            {
                _current = value;
                SetTheme(value);
            }
            get { return _current; }
        }
        public Font Font { get; private set; }
        public Image BackImage { get; private set; }                      // 窗体背景图片
        public Color ButtonEnterColor { get; private set; }                    // 全局按钮鼠标进入颜色
        public Color ButonDownColor { get; private set; }                     // 全局按钮鼠标按下颜色
        public Color CurrentItemForeColor { get; private set; }           // 当前播放的音乐项的背景颜色
        public Color CurrentItemBackColor { get; private set; }           // 当前播放的音乐项的前景颜色
        public Color ItemUnexistColor { get; private set; }               // 音乐项当文件不存在是的前景色
        public NgNet.UI.Forms.MenuRender MenuRender { get; private set; }  // 全局菜单的（cms,ms的render）
        #endregion

        #region constructor destructor 
        public ThemeManager()
        {
            BackImage = Properties.Resources.back1;  // 窗体背景图片
            BackColor = Color.LightCyan;             // 窗体背景色
            BorderColor = Color.LightSeaGreen;       // 窗体边框颜色
            ForeColor = Color.Teal;                  // 界面前景色
            ButtonEnterColor = Color.PaleTurquoise;       // 全局按钮鼠标进入颜色
            ButonDownColor = Color.Turquoise;            // 全局按钮鼠标按下颜色
            CurrentItemBackColor = Color.LightCyan;  // 当前播放的音乐项的背景颜色
            CurrentItemForeColor = Color.DeepPink;   // 当前播放的音乐项的前景颜色
            ItemUnexistColor = Color.DarkRed;        // 音乐项当文件不存在是的前景色
            MenuRender = new NgNet.UI.Forms.MenuRender(); //初始化menuRenderColors
        }
        #endregion

        #region private method
        private void SetTheme(PresetTheme theme)
        {
            BeginUpdate();
            switch (theme)
            {
                case PresetTheme.Classical:
                    BackColor = Color.WhiteSmoke;
                    BorderColor = Color.LightSeaGreen;
                    ForeColor = Color.DarkSlateGray;
                    CurrentItemBackColor = Color.LightCyan;
                    CurrentItemForeColor = Color.DeepPink;
                    ButtonEnterColor = Color.LightCyan;
                    ButonDownColor = Color.PaleTurquoise;
                    BackImage = Properties.Resources.back1;
                    break;
                case PresetTheme.Cool:
                    BackColor = Color.LightCyan;
                    BorderColor = Color.LightSeaGreen;
                    ForeColor = Color.DarkSlateGray;
                    CurrentItemBackColor = Color.FromArgb(197, 252, 235);
                    CurrentItemForeColor = Color.Crimson;
                    ButtonEnterColor = Color.FromArgb(189, 252, 201);
                    ButonDownColor = Color.FromArgb(157, 252, 212);
                    BackImage = null;
                    break;
                case PresetTheme.SilentGray:
                    BackColor = Color.DarkGray;
                    BorderColor = Color.FromArgb(52, 52, 52);
                    ForeColor = Color.Black;
                    CurrentItemBackColor = Color.FromArgb(145, 145, 145);
                    CurrentItemForeColor = Color.Green;
                    ButtonEnterColor = Color.Gray;
                    ButonDownColor = Color.DimGray;
                    BackImage = null;
                    break;
                case PresetTheme.Green:
                    BackColor = Color.Honeydew;
                    BorderColor = Color.Green;
                    ForeColor = Color.DarkGreen;
                    CurrentItemBackColor = Color.LightCyan;
                    CurrentItemForeColor = Color.DeepPink;
                    ButtonEnterColor = Color.FromArgb(205, 251, 189);
                    ButonDownColor = Color.FromArgb(178, 249, 168);
                    BackImage = null;
                    break;
                default:
                    SetTheme(PresetTheme.Classical);
                    return;
            }
            //菜单
            MenuRender.Colors.ArrowColor = BorderColor;
            MenuRender.Colors.FontColor = ForeColor;
            MenuRender.Colors.SeparatorColor = ForeColor;

            MenuRender.Colors.DropDownBackStartColor = ButonDownColor;
            MenuRender.Colors.DropDownBackEndColor = Color.WhiteSmoke;
            MenuRender.Colors.DropDownBorderColor = BorderColor;

            MenuRender.Colors.DropDownItemStartColor = ButonDownColor;
            MenuRender.Colors.DropDownItemEndColor = BackColor;
            MenuRender.Colors.DropDownItemBorderColor = BorderColor;

            MenuRender.Colors.MainMenuStartColor = BackColor;
            MenuRender.Colors.MainMenuEndColor = BackColor;
            MenuRender.Colors.MainMenuBorderColor = BorderColor;

            MenuRender.Colors.MenuItemEndColor = ButonDownColor;
            MenuRender.Colors.MenuItemStartColor = BackColor;
            MenuRender.Colors.MenuItemBorderColor = BorderColor;

            MenuRender.Colors.MarginStartColor = BackColor;
            MenuRender.Colors.MarginEndColor = ButonDownColor;
            EndUpdate();
        }
        #endregion

        
    }
}
