
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Text;

namespace FreshMedia.Data
{
    class DataManager
    {
        #region public properties
        public DataPaths Paths { get; private set; }

        //用户数据存储位置
        public ApplicationDataModes AppdataMode { get; private set; } = ApplicationDataModes.ApplicationPath;

        #endregion

        #region private fileds
        private string basePath = string.Format("{0}\\base.xml", NgNet.Applications.Current.Directory);
        #endregion

        #region constructor destructor
        public DataManager()
        {
            BaseRead();
            SetAppdataMode(AppdataMode);
        }

        ~DataManager()
        {
            BaseCreate();
        }

        public void Init()
        {

        }
        #endregion

        #region public mehtods
        public void SetAppdataMode(ApplicationDataModes appdataMode, string path = null)
        {
            AppdataMode = appdataMode;
            Paths = new DataPaths(appdataMode, path);
        }
        #region base
        private bool BaseCreate()
        {
            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement("baseConfig", Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            XmlElement xe_0 = xmlDocument.CreateElement(nameof(AppdataMode));
            XmlAttribute xa_0 = xmlDocument.CreateAttribute("value");
            xa_0.Value = ((int)AppdataMode).ToString();
            xe_0.Attributes.Append(xa_0);//////
            xa_0 = xmlDocument.CreateAttribute("type");
            xa_0.Value = AppdataMode.GetType().FullName;
            xe_0.Attributes.Append(xa_0);
            xe.AppendChild(xe_0);
            xmlDocument.Save(basePath);
            return true;
        }

        private bool BaseRead()
        {
            if (File.Exists(basePath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(basePath);

            XmlElement root = xmlDocument.DocumentElement;

            foreach (XmlNode item in root.ChildNodes)
            {
                if (item.Name == nameof(AppdataMode))
                {
                    string attr = ((XmlElement)item).GetAttribute("value");
                    int intAttr = NgNet.ConvertHelper.ToInt(attr, (int)AppdataMode);
                    foreach (int value in Enum.GetValues(typeof(ApplicationDataModes)))
                    {
                        if (value == intAttr)
                        {
                            AppdataMode = (ApplicationDataModes)intAttr;
                            break;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region config
        public bool AppConfigCreat(Controller.MainController _controller, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(AppConfigs), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);
            #region common
            XmlElement xe1 = xmlDocument.CreateElement("common");
            XmlElement xe1_0 = xmlDocument.CreateElement(nameof(_controller.Configs.AutoPlay));
            XmlAttribute xa1_0 = xmlDocument.CreateAttribute("value");
            xa1_0.Value = _controller.Configs.AutoPlay.ToString();
            xe1_0.Attributes.Append(xa1_0);
            //////
            xa1_0 = xmlDocument.CreateAttribute("type");
            xa1_0.Value = _controller.Configs.AutoPlay.GetType().FullName;
            xe1_0.Attributes.Append(xa1_0);
            xe1.AppendChild(xe1_0);

            XmlElement xe1_1 = xmlDocument.CreateElement(nameof(_controller.Configs.ExitStyle));
            XmlAttribute xa1_1 = xmlDocument.CreateAttribute("value");
            xa1_1.Value = ((int)_controller.Configs.ExitStyle).ToString();
            xe1_1.Attributes.Append(xa1_1);
            //////
            xa1_1 = xmlDocument.CreateAttribute("type");
            xa1_1.Value = _controller.Configs.ExitStyle.GetType().FullName;
            xe1_1.Attributes.Append(xa1_1);
            xe1.AppendChild(xe1_1);

            XmlElement xe1_2 = xmlDocument.CreateElement(nameof(_controller.Configs.StartboxEnable));
            XmlAttribute xa1_2 = xmlDocument.CreateAttribute("value");
            xa1_2.Value = _controller.Configs.StartboxEnable.ToString();
            xe1_2.Attributes.Append(xa1_2);
            //////
            xa1_2 = xmlDocument.CreateAttribute("type");
            xa1_2.Value = _controller.Configs.StartboxEnable.GetType().FullName;
            xe1_2.Attributes.Append(xa1_2);
            xe1.AppendChild(xe1_2);

            XmlElement xe1_3 = xmlDocument.CreateElement(nameof(_controller.Configs.StartSize));
            XmlAttribute xa1_3 = xmlDocument.CreateAttribute("value");
            xa1_3.Value = _controller.Configs.StartSize.ToString();
            xe1_3.Attributes.Append(xa1_3);
            //////
            xa1_3 = xmlDocument.CreateAttribute("type");
            xa1_3.Value = _controller.Configs.StartSize.GetType().FullName;
            xe1_3.Attributes.Append(xa1_3);
            xe1.AppendChild(xe1_3);
            xe.AppendChild(xe1);
            #endregion

            #region sleep mode
            XmlElement xe2 = xmlDocument.CreateElement("sleep");
            XmlElement xe2_0 = xmlDocument.CreateElement(nameof(_controller.SleepMode.SleepTime));
            XmlAttribute xa2_0 = xmlDocument.CreateAttribute("value");
            xa2_0.Value = _controller.SleepMode.SleepTime.ToString();
            xe2_0.Attributes.Append(xa2_0);
            //////
            xa2_0 = xmlDocument.CreateAttribute("type");
            xa2_0.Value = _controller.SleepMode.SleepTime.GetType().FullName;
            xe2_0.Attributes.Append(xa2_0);
            xe2.AppendChild(xe2_0);

            XmlElement xe2_1 = xmlDocument.CreateElement(nameof(_controller.SleepMode.ToShutDown));
            XmlAttribute xa2_1 = xmlDocument.CreateAttribute("value");
            xa2_1.Value = _controller.SleepMode.ToShutDown.ToString();
            xe2_1.Attributes.Append(xa2_1);
            //////
            xa2_1 = xmlDocument.CreateAttribute("type");
            xa2_1.Value = _controller.SleepMode.ToShutDown.GetType().FullName;
            xe2_1.Attributes.Append(xa2_1);
            xe2.AppendChild(xe2_1);
            xe.AppendChild(xe2);
            #endregion

            #region player
            XmlElement xe3 = xmlDocument.CreateElement("player");
            XmlElement xe3_0 = xmlDocument.CreateElement(nameof(_controller.PlayController.myPlayer.settings.Volume));
            XmlAttribute xa3_0 = xmlDocument.CreateAttribute("value");
            xa3_0.Value = _controller.PlayController.myPlayer.settings.Volume.ToString();
            xe3_0.Attributes.Append(xa3_0);
            //////
            xa3_0 = xmlDocument.CreateAttribute("type");
            xa3_0.Value = _controller.PlayController.myPlayer.settings.Volume.GetType().FullName;
            xe3_0.Attributes.Append(xa3_0);
            xe3.AppendChild(xe3_0);

            XmlElement xe3_1 = xmlDocument.CreateElement(nameof(_controller.PlayController.myPlayer.settings.CycleMode));
            XmlAttribute xa3_1 = xmlDocument.CreateAttribute("value");
            xa3_1.Value = ((int)_controller.PlayController.myPlayer.settings.CycleMode).ToString();
            xe3_1.Attributes.Append(xa3_1);
            //////
            xa3_1 = xmlDocument.CreateAttribute("type");
            xa3_1.Value = _controller.PlayController.myPlayer.settings.CycleMode.GetType().FullName;
            xe3_1.Attributes.Append(xa3_1);
            xe3.AppendChild(xe3_1);

            XmlElement xe3_2 = xmlDocument.CreateElement(nameof(_controller.PlayController.myPlayer.settings.RewindForwardTime));
            XmlAttribute xa3_2 = xmlDocument.CreateAttribute("value");
            xa3_2.Value = _controller.PlayController.myPlayer.settings.RewindForwardTime.ToString();
            xe3_2.Attributes.Append(xa3_2);
            /////
            xa3_2 = xmlDocument.CreateAttribute("type");
            xa3_2.Value = _controller.PlayController.myPlayer.settings.RewindForwardTime.GetType().FullName;
            xe3_2.Attributes.Append(xa3_2);
            xe3.AppendChild(xe3_2);
            xe.AppendChild(xe3);
            #endregion
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool AppConfigCreate(Controller.MainController _controller)
        {
            return AppConfigCreat(_controller, Paths.FilePath_AppConfig);
        }

        public bool AppConfigRead(Controller.MainController _controller, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;

            foreach (XmlNode item0 in root.ChildNodes)
            {
                switch (item0.Name)
                {
                    case "common":
                        #region common
                        foreach (XmlNode item1 in item0.ChildNodes)
                        {
                            switch (item1.Name)
                            {
                                case nameof(_controller.Configs.AutoPlay):
                                    string attr = ((XmlElement)item1).GetAttribute("value");
                                    _controller.Configs.AutoPlay = NgNet.ConvertHelper.ToBool(attr, _controller.Configs.AutoPlay);
                                    break;
                                case nameof(_controller.Configs.ExitStyle):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    int intAttr = NgNet.ConvertHelper.ToInt(attr, (int)_controller.Configs.ExitStyle);
                                    foreach (int intES in Enum.GetValues(typeof(NgNet.UI.Forms.ExitStyles)))
                                    {
                                        if (intAttr == intES)
                                        {
                                            _controller.Configs.ExitStyle = (NgNet.UI.Forms.ExitStyles)intAttr;
                                            break;
                                        }
                                    }
                                    break;
                                case nameof(_controller.Configs.StartboxEnable):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _controller.Configs.StartboxEnable = NgNet.ConvertHelper.ToBool(attr, _controller.Configs.StartboxEnable);
                                    break;
                                case nameof(_controller.Configs.StartSize):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _controller.Configs.StartSize = NgNet.ConvertHelper.ToSize(attr, _controller.Configs.StartSize);
                                    break;
                                default:
                                    break;
                            }

                        }
                        #endregion
                        break;
                    case "sleep":
                        foreach (XmlNode item1 in item0.ChildNodes)
                        {
                            switch (item1.Name)
                            {
                                case nameof(_controller.SleepMode.SleepTime):
                                    string attr = ((XmlElement)item1).GetAttribute("value");
                                    _controller.SleepMode.SleepTime = NgNet.ConvertHelper.ToUInt(attr, _controller.SleepMode.SleepTime);
                                    break;
                                case nameof(_controller.SleepMode.ToShutDown):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _controller.SleepMode.ToShutDown = NgNet.ConvertHelper.ToBool(attr, _controller.SleepMode.ToShutDown);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "player":
                        foreach (XmlNode item1 in item0.ChildNodes)
                        {
                            switch (item1.Name)
                            {
                                case nameof(_controller.PlayController.myPlayer.settings.Volume):
                                    string attr = ((XmlElement)item1).GetAttribute("value");
                                    byte byteAttr = NgNet.ConvertHelper.ToByte(attr, _controller.PlayController.myPlayer.settings.Volume);
                                    if (byteAttr > 100) byteAttr = 100;
                                    _controller.PlayController.myPlayer.settings.Volume = byteAttr;
                                    break;
                                case nameof(_controller.PlayController.myPlayer.settings.CycleMode):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    int intAttr = NgNet.ConvertHelper.ToInt(attr, (int)_controller.PlayController.myPlayer.settings.CycleMode);
                                    foreach (int intCM in Enum.GetValues(typeof(Player.CycleModes)))
                                    {
                                        if (intAttr == intCM)
                                        {
                                            _controller.PlayController.myPlayer.settings.CycleMode = (Player.CycleModes)intAttr;
                                        }
                                    }
                                    break;
                                case nameof(_controller.PlayController.myPlayer.settings.RewindForwardTime):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    intAttr = NgNet.ConvertHelper.ToInt(attr, _controller.PlayController.myPlayer.settings.RewindForwardTime);
                                    intAttr = Math.Abs(intAttr);
                                    _controller.PlayController.myPlayer.settings.RewindForwardTime = intAttr;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        public bool AppConfigRead(Controller.MainController _controller)
        {
            return AppConfigRead(_controller, Paths.FilePath_AppConfig);
        }
        #endregion

        #region theme
        public bool ThemeCreate(View.ThemeManager _theme, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(View.ThemeManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            XmlElement xe_0 = xmlDocument.CreateElement(nameof(_theme.Current));
            XmlAttribute xa_0 = xmlDocument.CreateAttribute("value");
            xa_0.Value = ((int)_theme.Current).ToString();
            xe_0.Attributes.Append(xa_0);
            xe.AppendChild(xe_0);

            XmlElement xe_1 = xmlDocument.CreateElement(nameof(_theme.Blendable));
            XmlAttribute xa_1 = xmlDocument.CreateAttribute("value");
            xa_1.Value = _theme.Blendable.ToString();
            xe_1.Attributes.Append(xa_1);
            xe.AppendChild(xe_1);

            XmlElement xe_2 = xmlDocument.CreateElement(nameof(_theme.Opacity));
            XmlAttribute xa_2 = xmlDocument.CreateAttribute("value");
            xa_2.Value = _theme.Opacity.ToString();
            xe_2.Attributes.Append(xa_2);
            xe.AppendChild(xe_2);


            Dictionary<string, Color> tmps = new Dictionary<string, Color>()
            {
                {nameof(_theme.BackColor),_theme.BackColor },
                {nameof(_theme.ForeColor),_theme.ForeColor},
                {nameof(_theme.BorderColor),_theme.BorderColor },
                {nameof(_theme.ButtonEnterColor),_theme.ButtonEnterColor },
                {nameof(_theme.ButonDownColor),_theme.ButonDownColor },
                {nameof(_theme.CurrentItemBackColor),_theme.CurrentItemBackColor },
                {nameof(_theme.CurrentItemForeColor),_theme.CurrentItemForeColor },
                {nameof(_theme.ItemUnexistColor),_theme.ItemUnexistColor }};

            foreach (KeyValuePair<string, Color> item in tmps)
            {
                XmlElement xe_x = xmlDocument.CreateElement(item.Key);
                XmlAttribute xa_x = xmlDocument.CreateAttribute("value");
                xa_x.Value = ColorTranslator.ToHtml(item.Value);
                xe_x.Attributes.Append(xa_x);
                xe.AppendChild(xe_x);
            }
            xmlDocument.Save(xmlPath);

            return true;
        }

        public bool ThemeCreate(View.ThemeManager _theme)
        {
            return ThemeCreate(_theme, Paths.FilePath_Theme);
        }

        public bool ThemeRead(View.ThemeManager _theme, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;

            foreach (XmlNode item0 in root.ChildNodes)
            {
                switch (item0.Name)
                {
                    case nameof(_theme.Current):
                        string attr = ((XmlElement)item0).GetAttribute("value");
                        int intAttr = NgNet.ConvertHelper.ToInt(attr, (int)_theme.Current);
                        foreach (int intT in Enum.GetValues(typeof(View.PresetTheme)))
                        {
                            if (intT == intAttr)
                            {
                                _theme.Current = (View.PresetTheme)intAttr;
                                break;
                            }
                        }
                        break;
                    case nameof(_theme.Blendable):
                        attr = ((XmlElement)item0).GetAttribute("value");
                        _theme.Blendable = NgNet.ConvertHelper.ToBool(attr, _theme.Blendable);
                        break;
                    case nameof(_theme.Opacity):
                        attr = ((XmlElement)item0).GetAttribute("value");
                        //_theme.Opacity = NgNet.hConvert.ToDouble(attr, _theme.Opacity);
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        public bool ThemeRead(View.ThemeManager _theme)
        {
            return ThemeRead(_theme, Paths.FilePath_Theme);
        }
        #endregion

        #region lyric
        public bool LyricCreat(Lyric.LyricManager _myLyric, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(Lyric.LyricManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            #region formLyric
            XmlElement xe1 = xmlDocument.CreateElement(nameof(Lyric.FormLyric));
            XmlElement xe1_1 = xmlDocument.CreateElement(nameof(_myLyric.FLyric.Font));
            XmlAttribute xa1_1 = xmlDocument.CreateAttribute(nameof(Font.Name));
            xa1_1.Value = _myLyric.FLyric.Font.Name;
            xe1_1.Attributes.Append(xa1_1);
            xa1_1 = xmlDocument.CreateAttribute(nameof(Font.Size));
            xa1_1.Value = _myLyric.FLyric.Font.Size.ToString();
            xe1_1.Attributes.Append(xa1_1);
            xa1_1 = xmlDocument.CreateAttribute(nameof(Font.Style));
            xa1_1.Value = _myLyric.FLyric.Font.Style.ToString();
            xe1_1.Attributes.Append(xa1_1);
            xe1.AppendChild(xe1_1);

            XmlElement xe1_2 = xmlDocument.CreateElement(nameof(_myLyric.FLyric.Color));
            XmlAttribute xa1_2 = xmlDocument.CreateAttribute("value");
            xa1_2.Value = ColorTranslator.ToHtml(_myLyric.FLyric.Color);
            xe1_2.Attributes.Append(xa1_2);
            xe1.AppendChild(xe1_2);

            XmlElement xe1_3 = xmlDocument.CreateElement(nameof(_myLyric.FLyric.PlayedColor));
            XmlAttribute xa1_3 = xmlDocument.CreateAttribute("value");
            xa1_3.Value = ColorTranslator.ToHtml(_myLyric.FLyric.PlayedColor);
            xe1_3.Attributes.Append(xa1_3);
            xe1.AppendChild(xe1_3);

            XmlElement xe1_4 = xmlDocument.CreateElement(nameof(_myLyric.FLyric.PrepColor));
            XmlAttribute xa1_4 = xmlDocument.CreateAttribute("value");
            xa1_4.Value = ColorTranslator.ToHtml(_myLyric.FLyric.PrepColor);
            xe1_4.Attributes.Append(xa1_4);
            xe1.AppendChild(xe1_4);

            XmlElement xe1_5 = xmlDocument.CreateElement(nameof(_myLyric.FLyric.GradualChangeColor));
            XmlAttribute xa1_5 = xmlDocument.CreateAttribute("value");
            xa1_5.Value = ColorTranslator.ToHtml(_myLyric.FLyric.GradualChangeColor);
            xe1_5.Attributes.Append(xa1_5);
            xe1.AppendChild(xe1_5);

            xe.AppendChild(xe1);
            #endregion

            #region deskLyric
            XmlElement xe2 = xmlDocument.CreateElement(nameof(Lyric.DesktopLyric));
            XmlElement xe2_1 = xmlDocument.CreateElement(nameof(_myLyric.DLyric.Font));
            XmlAttribute xa2_1 = xmlDocument.CreateAttribute(nameof(Font.Name));
            xa2_1.Value = _myLyric.DLyric.Font.Name;
            xe2_1.Attributes.Append(xa2_1);
            xa2_1 = xmlDocument.CreateAttribute(nameof(Font.Size));
            xa2_1.Value = _myLyric.DLyric.Font.Size.ToString();
            xe2_1.Attributes.Append(xa2_1);
            xa2_1 = xmlDocument.CreateAttribute(nameof(Font.Style));
            xa2_1.Value = _myLyric.DLyric.Font.Style.ToString();
            xe2_1.Attributes.Append(xa2_1);
            xe2.AppendChild(xe2_1);

            XmlElement xe2_2 = xmlDocument.CreateElement(nameof(_myLyric.DLyric.Color));
            XmlAttribute xa2_2 = xmlDocument.CreateAttribute("value");
            xa2_2.Value = ColorTranslator.ToHtml(_myLyric.DLyric.Color);
            xe2_2.Attributes.Append(xa2_2);
            xe2.AppendChild(xe2_2);

            XmlElement xe2_3 = xmlDocument.CreateElement(nameof(_myLyric.DLyric.PlayedColor));
            XmlAttribute xa2_3 = xmlDocument.CreateAttribute("value");
            xa2_3.Value = ColorTranslator.ToHtml(_myLyric.DLyric.PlayedColor);
            xe2_3.Attributes.Append(xa2_3);
            xe2.AppendChild(xe2_3);

            XmlElement xe2_4 = xmlDocument.CreateElement(nameof(_myLyric.DLyric.PrepColor));
            XmlAttribute xa2_4 = xmlDocument.CreateAttribute("value");
            xa2_4.Value = ColorTranslator.ToHtml(_myLyric.DLyric.PrepColor);
            xe2_4.Attributes.Append(xa2_4);
            xe2.AppendChild(xe2_4);

            XmlElement xe2_5 = xmlDocument.CreateElement(nameof(_myLyric.DLyric.GradualChangeColor));
            XmlAttribute xa2_5 = xmlDocument.CreateAttribute("value");
            xa2_5.Value = ColorTranslator.ToHtml(_myLyric.DLyric.GradualChangeColor);
            xe2_5.Attributes.Append(xa2_5);
            xe2.AppendChild(xe2_5);

            XmlElement xe2_6 = xmlDocument.CreateElement(nameof(_myLyric.DLyric.Locked));
            XmlAttribute xa2_6 = xmlDocument.CreateAttribute("value");
            xa2_6.Value = _myLyric.DLyric.Locked.ToString();
            xe2_6.Attributes.Append(xa2_6);
            xe2.AppendChild(xe2_6);

            XmlElement xe2_7 = xmlDocument.CreateElement(nameof(_myLyric.DLyric.ClientLocation));
            XmlAttribute xa2_7 = xmlDocument.CreateAttribute("value");
            xa2_7.Value = _myLyric.DLyric.ClientLocation.ToString();
            xe2_7.Attributes.Append(xa2_7);
            xe2.AppendChild(xe2_7);

            XmlElement xe2_8 = xmlDocument.CreateElement(nameof(_myLyric.DLyric.Visible));
            XmlAttribute xa2_8 = xmlDocument.CreateAttribute("value");
            xa2_8.Value = _myLyric.DLyric.Visible.ToString();
            xe2_8.Attributes.Append(xa2_8);
            xe2.AppendChild(xe2_8);

            xe.AppendChild(xe2);
            #endregion

            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool LyricCreate(Lyric.LyricManager _myLyric)
        {
            return LyricCreat(_myLyric, Paths.FilePath_Lyric);
        }

        public bool LyricRead(Lyric.LyricManager _myLyric, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;

            foreach (XmlNode item0 in root.ChildNodes)
            {
                switch (item0.Name)
                {
                    #region formLyric
                    case nameof(Lyric.FormLyric):
                        foreach (XmlNode item1 in item0.ChildNodes)
                        {
                            switch (item1.Name)
                            {
                                case nameof(_myLyric.FLyric.Font):
                                    XmlElement xe = (XmlElement)item1;
                                    string fontName = xe.GetAttribute(nameof(Font.Name));
                                    float fontSize = NgNet.ConvertHelper.ToFloat(xe.GetAttribute(nameof(Font.Size)), _myLyric.FLyric.Font.Size);
                                    if (fontSize < _myLyric.FLyric.MinFontSize || fontSize > _myLyric.FLyric.MaxFontSize)
                                        fontSize = ((_myLyric.FLyric.MaxFontSize + _myLyric.FLyric.MinFontSize) / 2);
                                    FontStyle fontStyle = NgNet.ConvertHelper.ToFontStyle(xe.GetAttribute(nameof(Font.Style)), _myLyric.FLyric.Font.Style);
                                    _myLyric.FLyric.Font = new Font(fontName, fontSize, fontStyle);
                                    break;
                                case nameof(_myLyric.FLyric.Color):
                                    string attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.FLyric.Color = NgNet.ConvertHelper.ToColor(attr, _myLyric.FLyric.Color);
                                    break;
                                case nameof(_myLyric.FLyric.GradualChangeColor):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.FLyric.GradualChangeColor = NgNet.ConvertHelper.ToColor(attr, _myLyric.FLyric.GradualChangeColor);
                                    break;
                                case nameof(_myLyric.FLyric.PlayedColor):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.FLyric.PlayedColor = NgNet.ConvertHelper.ToColor(attr, _myLyric.FLyric.PlayedColor);
                                    break;
                                case nameof(_myLyric.FLyric.PrepColor):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.FLyric.PrepColor = NgNet.ConvertHelper.ToColor(attr, _myLyric.FLyric.PrepColor);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    #endregion

                    #region desktopLyric
                    case nameof(Lyric.DesktopLyric):
                        foreach (XmlNode item1 in item0.ChildNodes)
                        {
                            switch (item1.Name)
                            {
                                case nameof(_myLyric.DLyric.Font):
                                    XmlElement xe = (XmlElement)item1;
                                    string fontName = xe.GetAttribute(nameof(Font.Name));
                                    float fontSize = NgNet.ConvertHelper.ToFloat(xe.GetAttribute(nameof(Font.Size)), _myLyric.DLyric.Font.Size);
                                    if (fontSize < _myLyric.DLyric.MinFontSize || fontSize > _myLyric.DLyric.MaxFontSize)
                                        fontSize = ((_myLyric.DLyric.MaxFontSize + _myLyric.DLyric.MinFontSize) / 2);
                                    FontStyle fontStyle = NgNet.ConvertHelper.ToFontStyle(xe.GetAttribute(nameof(Font.Style)), _myLyric.DLyric.Font.Style);
                                    _myLyric.DLyric.Font = new Font(fontName, fontSize, fontStyle);
                                    break;
                                case nameof(_myLyric.DLyric.Color):
                                    string attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.DLyric.Color = NgNet.ConvertHelper.ToColor(attr, _myLyric.DLyric.Color);
                                    break;
                                case nameof(_myLyric.DLyric.GradualChangeColor):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.DLyric.GradualChangeColor = NgNet.ConvertHelper.ToColor(attr, _myLyric.DLyric.GradualChangeColor);
                                    break;
                                case nameof(_myLyric.DLyric.PlayedColor):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.DLyric.PlayedColor = NgNet.ConvertHelper.ToColor(attr, _myLyric.DLyric.PlayedColor);
                                    break;
                                case nameof(_myLyric.DLyric.PrepColor):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.DLyric.PrepColor = NgNet.ConvertHelper.ToColor(attr, _myLyric.DLyric.PrepColor);
                                    break;
                                case nameof(_myLyric.DLyric.ClientLocation):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.DLyric.ClientLocation = NgNet.ConvertHelper.ToPoint(attr, _myLyric.DLyric.ClientLocation);
                                    break;
                                case nameof(_myLyric.DLyric.Locked):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.DLyric.Locked = NgNet.ConvertHelper.ToBool(attr, _myLyric.DLyric.Locked);
                                    break;
                                case nameof(_myLyric.DLyric.Visible):
                                    attr = ((XmlElement)item1).GetAttribute("value");
                                    _myLyric.DLyric.Visible = NgNet.ConvertHelper.ToBool(attr, _myLyric.DLyric.Visible);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                        #endregion
                }
            }

            return true;
        }

        public bool LyricRead(Lyric.LyricManager _myLyric)
        {
            return LyricRead(_myLyric, Paths.FilePath_Lyric);
        }
        #endregion

        #region openHistory
        public bool OpenHistoryCreat(List.OpenHistory _openHistory, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(List.OpenHistory), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            XmlElement xe1 = xmlDocument.CreateElement(nameof(_openHistory.FolderOpenHistory));
            StringBuilder sb = new StringBuilder();
            foreach (string item in _openHistory.FolderOpenHistory)
            {
                sb.Append("\r\n      *");
                sb.Append(item);
                sb.Append("*");
            }
            sb.Append("\r\n");
            xe1.InnerText = sb.ToString();
            xe.AppendChild(xe1);


            XmlElement xe2 = xmlDocument.CreateElement(nameof(_openHistory.LastOpenFolder));
            xe2.InnerText = _openHistory.LastOpenFolder;
            xe.AppendChild(xe2);
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool OpenHistoryCreate(List.OpenHistory _openHistory)
        {
            return OpenHistoryCreat(_openHistory, Paths.FilePath_FolderHistory);
        }

        public bool OpenHistoryRead(List.OpenHistory _openHistory, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;

            foreach (XmlNode item0 in root.ChildNodes)
            {
                switch (item0.Name)
                {
                    case nameof(_openHistory.FolderOpenHistory):
                        string txt = item0.InnerText;
                        if (txt == null)
                            txt = string.Empty;
                        string[] tmp = txt.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string dir in tmp)
                        {
                            _openHistory.AddFolder(dir);
                        }
                        break;

                    case nameof(_openHistory.LastOpenFolder):
                        if (NgNet.IO.PathHelper.IsPath(item0.InnerText))
                            _openHistory.LastOpenFolder = item0.InnerText;
                        break;
                }
            }
            return true;
        }

        public bool OpenHistoryRead(List.OpenHistory _openHistory)
        {
            return OpenHistoryRead(_openHistory, Paths.FilePath_FolderHistory);
        }
        #endregion

        #region hotkeys
        public bool HotkeysCreat(Controller.HotkeysManager _hotkey, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(Controller.HotkeysManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            foreach (NgNet.Applications.Hotkey item in _hotkey.Hotkeys)
            {
                XmlElement xe_x = xmlDocument.CreateElement(item.Name.Trim());
                XmlAttribute xa_x = xmlDocument.CreateAttribute(nameof(item.Modifiers));
                xa_x.Value = item.Modifiers.ToString();
                xe_x.Attributes.Append(xa_x);
                xa_x = xmlDocument.CreateAttribute(nameof(item.Key));
                xa_x.Value = item.Key.ToString();
                xe_x.Attributes.Append(xa_x);
                xe_x.InnerText = item.Value;
                xe.AppendChild(xe_x);
            }
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool HotkeysCreat(Controller.HotkeysManager _hotkey)
        {
            return HotkeysCreat(_hotkey, Paths.FilePath_Hotkeys);
        }

        public bool HotkeysRead(Controller.HotkeysManager _hotkey, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;

            NgNet.Applications.Hotkey HK;
            foreach (XmlNode item0 in root.ChildNodes)
            {
                XmlElement xe = (XmlElement)item0;
                for (int i = 0; i < _hotkey.Hotkeys.Length; i++)
                {
                    if (_hotkey.Hotkeys[i].Name == xe.Name)
                    {
                        string attrModifiers = xe.GetAttribute(nameof(HK.Modifiers));
                        string attrKey = xe.GetAttribute(nameof(HK.Key));

                        int intAttrModi = NgNet.ConvertHelper.ToInt(attrModifiers, _hotkey.Hotkeys[i].Modifiers);

                    }
                }
            }
            return true;
        }

        public bool HotkeysRead(Controller.HotkeysManager _hotkey)
        {
            return HotkeysRead(_hotkey, Paths.FilePath_Hotkeys);
        }
        #endregion

        #region lib
        #region local
        public bool libLocalCreat(List.ListManager _myLists, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(List.ListManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            StringBuilder sb = new StringBuilder();

            foreach (List.AudioList list in _myLists.Local)
            {
                if (list == null)
                    continue;
                XmlElement xe_x = xmlDocument.CreateElement(list.Name);
                XmlAttribute xa_x = xmlDocument.CreateAttribute("Title");
                xa_x.Value = list.Title;
                xe_x.Attributes.Append(xa_x);

                sb.Clear();
                foreach (string path in list)
                {
                    sb.Append("\r\n      *");
                    sb.Append(path);
                    sb.Append("*");
                }
                sb.Append("\r\n");
                xe_x.InnerText = sb.ToString();
                xe.AppendChild(xe_x);
            }
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool libLocalCreat(List.ListManager _myList)
        {
            return libLocalCreat(_myList, Paths.FilePath_Lib_Local);
        }

        public bool libLocalRead(List.ListManager _myList, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;
            List.AudioList tmp;
            int listIndex = 1;
            _myList.ResetLocal();
            foreach (XmlNode item0 in root.ChildNodes)
            {

                string[] items = item0.InnerText.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                tmp = new FreshMedia.List.AudioList();
                tmp.Name = item0.Name;
                tmp.Title = item0.Name;
                foreach (string item in items)
                {
                    if (tmp.Contains(item) == false//List.Contains 不区分大小写
                        && NgNet.IO.PathHelper.IsPath(item)//判断路径是否合法
                        && Player.Types.SupportedTypes.Contains(Path.GetExtension(item)))//判断文件类型是否被支持
                    {
                        tmp.Add(item);
                    }
                }
                switch (item0.Name)
                {
                    case List.ListManager.NAME_LIST_DEFAULT:
                        _myList.AddList(List.MyLib.Local, tmp, 0);
                        break;
                    default:
                        _myList.AddList(List.MyLib.Local, tmp, listIndex);
                        listIndex++;
                        break;
                }
            }
            return true;
        }

        public bool libLocalRead(List.ListManager _myList)
        {
            return libLocalRead(_myList, Paths.FilePath_Lib_Local);
        }
        #endregion

        #region current
        public bool libCurrentCreat(List.ListManager _myLists, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(List.ListManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            StringBuilder sb = new StringBuilder();

            foreach (List.AudioList list in _myLists.Playing)
            {
                if (list == null)
                    continue;
                XmlElement xe_x = xmlDocument.CreateElement(list.Name);
                XmlAttribute xa_x = xmlDocument.CreateAttribute("value");
                xa_x.Value = list.Title;
                xe_x.Attributes.Append(xa_x);

                sb.Clear();
                foreach (string path in list)
                {
                    sb.Append("\r\n      *");
                    sb.Append(path);
                    sb.Append("*");
                }
                sb.Append("\r\n");
                xe_x.InnerText = sb.ToString();
                xe.AppendChild(xe_x);
            }
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool libCurrentCreat(List.ListManager _myList)
        {
            return libCurrentCreat(_myList, Paths.FilePath_Lib_Current);
        }

        public bool libCurrentRead(List.ListManager _myList, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;
            List.AudioList tmp;

            foreach (XmlNode item0 in root.ChildNodes)
            {
                string[] items = item0.InnerText.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                tmp = new List.AudioList();
                tmp.Clear();
                foreach (string item in items)
                {
                    if (tmp.Contains(item) == false//List.Contains 不区分大小写
                        && NgNet.IO.PathHelper.IsPath(item)//判断路径是否合法
                        && Player.Types.SupportedTypes.Contains(Path.GetExtension(item)))//判断文件类型是否被支持
                    {
                        tmp.Add(item);
                    }
                }
                switch (item0.Name)
                {
                    case List.ListManager.NAME_LIST_CURRENT:
                        _myList.AddMedias(List.MyLib.Playing, item0.Name, tmp);

                        break;
                    default:

                        break;
                }
            }
            return true;
        }

        public bool libCurrentRead(List.ListManager _myList)
        {
            return libCurrentRead(_myList, Paths.FilePath_Lib_Current);
        }
        #endregion

        #region favorite
        public bool libFavoriteCreat(List.ListManager _myLists, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(List.ListManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            StringBuilder sb = new StringBuilder();

            foreach (List.AudioList list in _myLists.Favo)
            {
                if (list == null)
                    continue;
                XmlElement xe_x = xmlDocument.CreateElement(list.Name);
                XmlAttribute xa_x = xmlDocument.CreateAttribute("value");
                xa_x.Value = list.Title;
                xe_x.Attributes.Append(xa_x);

                sb.Clear();
                foreach (string path in list)
                {
                    sb.Append("\r\n      *");
                    sb.Append(path);
                    sb.Append("*");
                }
                sb.Append("\r\n");
                xe_x.InnerText = sb.ToString();
                xe.AppendChild(xe_x);
            }
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool libFavoriteCreat(List.ListManager _myList)
        {
            return libFavoriteCreat(_myList, Paths.FilePath_Lib_Favorite);
        }

        public bool libFavoriteRead(List.ListManager _myList, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;
            List.AudioList tmp;
            foreach (XmlNode item0 in root.ChildNodes)
            {
                string[] items = item0.InnerText.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                tmp = new List.AudioList();
                tmp.Clear();
                foreach (string item in items)
                {
                    if (tmp.Contains(item) == false//List.Contains 不区分大小写
                        && NgNet.IO.PathHelper.IsPath(item)//判断路径是否合法
                        && Player.Types.SupportedTypes.Contains(Path.GetExtension(item)))//判断文件类型是否被支持
                    {
                        tmp.Add(item);
                    }
                }
                switch (item0.Name)
                {
                    case List.ListManager.NAME_LIST_FAVO:
                        _myList.AddMedias(List.MyLib.Favorite, item0.Name, tmp);
                        break;
                    default:

                        break;
                }
            }
            return true;
        }

        public bool libFavoriteRead(List.ListManager _myList)
        {
            return libFavoriteRead(_myList, Paths.FilePath_Lib_Favorite);
        }
        #endregion

        #region history
        public bool libHistoryCreat(List.ListManager _myLists, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(List.ListManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            StringBuilder sb = new StringBuilder();

            foreach (List.AudioList list in _myLists.History)
            {
                if (list == null)
                    continue;
                XmlElement xe_x = xmlDocument.CreateElement(list.Name);
                XmlAttribute xa_x = xmlDocument.CreateAttribute("value");
                xa_x.Value = list.Title;
                xe_x.Attributes.Append(xa_x);

                sb.Clear();
                foreach (string path in list)
                {
                    sb.Append("\r\n      *");
                    sb.Append(path);
                    sb.Append("*");
                }
                sb.Append("\r\n");
                xe_x.InnerText = sb.ToString();
                xe.AppendChild(xe_x);
            }
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool libHistoryCreat(List.ListManager _myList)
        {
            return libHistoryCreat(_myList, Paths.FilePath_Lib_History);
        }

        public bool libHistoryRead(List.ListManager _myList, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;
            List.AudioList tmp;
            foreach (XmlNode item0 in root.ChildNodes)
            {
                string[] items = item0.InnerText.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                tmp = new List.AudioList();
                tmp.Clear();
                foreach (string item in items)
                {
                    if (tmp.Contains(item) == false//List.Contains 不区分大小写
                        && NgNet.IO.PathHelper.IsPath(item)//判断路径是否合法
                        && Player.Types.SupportedTypes.Contains(Path.GetExtension(item)))//判断文件类型是否被支持
                    {
                        tmp.Add(item);
                    }
                }
                switch (item0.Name)
                {
                    case List.ListManager.NAME_LIST_HISTORY:
                        _myList.AddMedias(List.MyLib.History, item0.Name, tmp);
                        break;
                    default:

                        break;
                }
            }
            return true;
        }

        public bool libHistoryRead(List.ListManager _myList)
        {
            return libHistoryRead(_myList, Paths.FilePath_Lib_History);
        }
        #endregion

        #region recentlyAdded
        public bool libRecentlyAddedCreat(List.ListManager _myLists, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(List.ListManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            StringBuilder sb = new StringBuilder();

            foreach (List.AudioList list in _myLists.RecentlyAdded)
            {
                if (list == null)
                    continue;
                XmlElement xe_x = xmlDocument.CreateElement(list.Name);
                XmlAttribute xa_x = xmlDocument.CreateAttribute("value");
                xa_x.Value = list.Title;
                xe_x.Attributes.Append(xa_x);

                sb.Clear();
                foreach (string path in list)
                {
                    sb.Append("\r\n      *");
                    sb.Append(path);
                    sb.Append("*");
                }
                sb.Append("\r\n");
                xe_x.InnerText = sb.ToString();
                xe.AppendChild(xe_x);
            }
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool libRecentlyAddedCreat(List.ListManager _myList)
        {
            return libRecentlyAddedCreat(_myList, Paths.FilePath_Lib_RecentlyAdded);
        }

        public bool libRecentlyAddedRead(List.ListManager _myList, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;
            List.AudioList tmp;
            foreach (XmlNode item0 in root.ChildNodes)
            {
                string[] items = item0.InnerText.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                tmp = new List.AudioList();
                tmp.Clear();
                foreach (string item in items)
                {
                    if (tmp.Contains(item) == false//List.Contains 不区分大小写
                        && NgNet.IO.PathHelper.IsPath(item)//判断路径是否合法
                        && Player.Types.SupportedTypes.Contains(Path.GetExtension(item)))//判断文件类型是否被支持
                    {
                        tmp.Add(item);
                    }
                }
                switch (item0.Name)
                {
                    case List.ListManager.NAME_LIST_RECENTLYADDED:
                        _myList.AddMedias(List.MyLib.RecentlyAdded, item0.Name, tmp);
                        break;
                    default:

                        break;
                }
            }
            return true;
        }

        public bool libRecentlyAddedRead(List.ListManager _myList)
        {
            return libRecentlyAddedRead(_myList, Paths.FilePath_Lib_RecentlyAdded);
        }
        #endregion

        #region mostedPlayed
        public bool libMostlyPlayedCreat(List.ListManager _myLists, string xmlPath)
        {
            string parentFloder = Path.GetDirectoryName(xmlPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            XmlDocument xmlDocument = new XmlDocument();
            //创建Xml声明部分，即<?xml version="1.0" encoding="utf-8" ?>
            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDocument.AppendChild(xmlDeclaration);
            //创建根节点
            XmlElement xe = xmlDocument.CreateElement(nameof(List.ListManager), Program.XmlAppName, Program.WEBSITE);
            xmlDocument.AppendChild(xe);

            XmlElement xe_x = xmlDocument.CreateElement(_myLists.Times.Name);
            XmlAttribute xa_x = xmlDocument.CreateAttribute("value");
            xa_x.Value = _myLists.Times.Title;
            xe_x.Attributes.Append(xa_x);

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            foreach (KeyValuePair<string, string> item in _myLists.Times)
            {
                sb.Append("\r\n      *");
                sb.Append(item.Key);
                sb.Append("*");
            }
            sb.Append("\r\n");
            xe_x.InnerText = sb.ToString();
            xe.AppendChild(xe_x);
            xmlDocument.Save(xmlPath);
            return true;
        }

        public bool libMostlyPlayedCreat(List.ListManager _myList)
        {
            return libMostlyPlayedCreat(_myList, Paths.FilePath_Lib_MostlyPlayed);
        }

        public bool libMostlyPlayedRead(List.ListManager _myList, string xmlPath)
        {
            if (File.Exists(xmlPath) == false)
                return false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            XmlElement root = xmlDocument.DocumentElement;
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            foreach (XmlNode item0 in root.ChildNodes)
            {

                tmp.Clear();
                string[] items = item0.InnerText.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in items)
                {
                    string[] tmp1 = item.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tmp1.Length != 2)
                        continue;
                    if (tmp.Values.Contains(tmp1[1]) == false//List.Contains 不区分大小写
                        && NgNet.IO.PathHelper.IsPath(tmp1[1])//判断路径是否合法
                        && Player.Types.SupportedTypes.Contains(Path.GetExtension(tmp1[1])))//判断文件类型是否被支持
                    {
                        tmp.Add(item, tmp1[1]);
                    }
                }
                switch (item0.Name)
                {
                    case List.ListManager.NAME_LIST_MOSTLYPLAYED:
                        _myList.AddMost(tmp);

                        break;
                    default:

                        break;
                }
            }
            return true;
        }

        public bool libMostlyPlayedRead(List.ListManager _myList)
        {
            return libMostlyPlayedRead(_myList, Paths.FilePath_Lib_MostlyPlayed);
        }
        #endregion

        #region mediaInfoCocah
        public bool InfoCocahCreat(string txtPath)
        {
            string parentFloder = Path.GetDirectoryName(txtPath);
            if (Directory.Exists(parentFloder) == false)
                Directory.CreateDirectory(parentFloder);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < List.ListManager.DataBase[0].Count; i++)
            {
                sb.Append("*");
                sb.Append(List.ListManager.DataBase[0][i]);
                sb.Append("|");
                sb.Append(List.ListManager.DataBase[1][i]);
                sb.Append("|");
                sb.Append(List.ListManager.DataBase[2][i]);
                sb.Append("|");
                sb.Append(List.ListManager.DataBase[3][i]);
                sb.Append("|");
                sb.Append(List.ListManager.DataBase[4][i]);
                sb.Append("|");
                sb.Append(List.ListManager.DataBase[5][i]);
                sb.Append("|");
                sb.Append(List.ListManager.DataBase[6][i]);
                sb.Append("*\r\n");
            }
            System.IO.File.WriteAllText(txtPath, sb.ToString(), Encoding.UTF8);
            return true;
        }

        public bool InfoCocahCreat()
        {
            return InfoCocahCreat(Paths.FilePath_MI_DataBase);
        }

        public bool InfoCocahRead(string txtPath)
        {
            if (!File.Exists(txtPath))
                return false;
            string readTmp = File.ReadAllText(txtPath);
            if (string.IsNullOrWhiteSpace(readTmp))
                return true;
            string[] audios = readTmp.Split(new Char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            string[] items;
            foreach (string item in audios)
            {
                items = item.Split(new char[] { '|' }, StringSplitOptions.None);
                if (items.Length != List.ListManager.DataBase.Length)
                    continue;
                if (List.ListManager.DataBase[0].Contains(items[0]))
                    continue;
                if (NgNet.IO.PathHelper.IsPath(items[0]))
                {
                    List.ListManager.DataBase[0].Add(items[0]);
                    List.ListManager.DataBase[1].Add(items[1]);
                    List.ListManager.DataBase[2].Add(items[2]);
                    List.ListManager.DataBase[3].Add(items[3]);
                    List.ListManager.DataBase[4].Add(items[4]);
                    List.ListManager.DataBase[5].Add(items[5]);
                    List.ListManager.DataBase[6].Add(items[6]);
                }
            }
            return true;
        }

        public bool InfoCocahRead()
        {
            return InfoCocahRead(Paths.FilePath_MI_DataBase);
        }
        #endregion
        #endregion
        #endregion
    }
}
