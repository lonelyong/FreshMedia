using System;
using System.Drawing;
using System.IO;
using NgNet.UI;
using NgNet.UI.Forms;
using System.Windows.Forms;
namespace FreshMedia.View
{
    partial class InfoForm : NgNet.UI.Forms.TitleableForm
    {
        #region private fields
        private List.MyLib scrlib;// 音乐的来源库
        private string scrlist;   // 音乐的来源列表
        private string audioPath = null;
        private string lyricPath = null;

        private Controller.MainController _mc;
        #endregion

        #region constructor destructor 
        public InfoForm(string path, List.MyLib scrlib, string scrlist, Controller.MainController _controller)
        {
            InitializeComponent();
            this.audioPath = path;
            this.scrlib = scrlib;
            this.scrlist = scrlist;
            this._mc = _controller;
            TitleBar.Style = TitleBarStyles.None;
            FormHelper.SetFormRoundRgn(3, 3);
            //
            //获取歌词文件路径
            this.lyricPath = Lyric.LyricApi.getLyricPathByAudio(path, true);
            //获取歌词文件路径
            this.getInfo();
            TitleBar.IconVisible = false;
        }
        #endregion

        #region IThemeBase
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                foreach (Control control in this.ContentPanel.Controls)
                {
                    control.BackColor = value;
                }
                foreach (Control control in this.otherInfoGbox.Controls)
                {
                    control.BackColor = value;//
                }
                foreach (Control control in this.fileInfoGbox.Controls)
                {
                    control.BackColor = value;//
                }
                foreach (Control control in this.lyricGbox.Controls)
                {
                    control.BackColor = value;//
                }
            }
        }
        #endregion

        #region this
        private void themeChangedEvent(ThemeChangedEventArgs e)
        {
            this.BorderColor = e.ThemeClass.BorderColor;
            this.BackColor = e.ThemeClass.BackColor;
        }

        private void this_Load(object sender, EventArgs e)
        {
            //自动应用主题
            _mc.Theme.ThemeChanged += new ThemeChangedEventHandler(themeChangedEvent);
            FormClosed += new FormClosedEventHandler((object sender1, FormClosedEventArgs e1) =>
            {
                _mc.Theme.ThemeChanged -= new ThemeChangedEventHandler(themeChangedEvent);
            });
        }

        private void this_SizeChanged(object sender, EventArgs e)
        {
            fileInfoGbox.Left = 12;
            otherInfoGbox.Left = otherInfoGbox.Left;
            lyricGbox.Left = otherInfoGbox.Left;

            fileInfoGbox.Top = 6;
            otherInfoGbox.Top = fileInfoGbox.Bottom + 4;
            lyricGbox.Top = otherInfoGbox.Bottom + 4;

            fileInfoGbox.Width = ContentPanel.Width - fileInfoGbox.Left * 2;
            otherInfoGbox.Width = fileInfoGbox.Width;
            lyricGbox.Width = fileInfoGbox.Width;

            backButton.Left = fileInfoGbox.Right - backButton.Width - (fileInfoGbox.Width - copyButton.Right);
            playButton.Left = backButton.Left - playButton.Width + 1;
            backButton.Top = ContentPanel.Height - 4 - backButton.Height;
            playButton.Top = backButton.Top;
        }
        #endregion

        #region private methods
        private void btns_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.backButton))
            {
                this.Close();
            }
            else if (sender.Equals(this.playButton))
            {
                if (!Controller.PlayController.FileTest(this.audioPath))
                    return;
                //来源是正在播放列表
                if (scrlib == List.MyLib.Playing)
                    // 当前音乐正在播放
                    if (string.Compare(audioPath, _mc.PlayController.myPlayer.settings.URL, true) == 0)
                        if (_mc.PlayController.myPlayer.settings.PlayState == Player.PlayStates.playing)
                            if (NgNet.UI.Forms.MessageBox.Show(this, "此音乐正在播放，是否重新开始？", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                _mc.PlayController.ListPlay(scrlib, scrlist, audioPath);
                            else
                                return;
                        else
                            _mc.PlayController.myPlayer.ctControls.play();
                    else
                        _mc.PlayController.ListPlay(scrlib, scrlist, audioPath);
                else
                    _mc.PlayController.ListPlay(scrlib, scrlist, audioPath);
            }
            else if (sender.Equals(this.copyButton))//复制音乐路径
            {
                if (string.IsNullOrEmpty(audioPath)) return;
                Clipboard.SetText(audioPath);
            }
            else if (sender.Equals(this.lyricLocButton))
            {
                NgNet.IO.PathHelper.ShowInExplorer(this.lyricPath, true);
            }
            else if (sender.Equals(this.openlyricButton))
            {
                _mc.LyricManager.OpenLyricByAudioPath(audioPath);
            }
            else if (sender.Equals(this.locButton))
            {
                NgNet.IO.PathHelper.ShowInExplorer(this.audioPath, true);
            }
        }

        private void getInfo()
        {
            //
            //文件信息
            try
            {
                libMedia.Mp3.Mp3File mp3doc = new libMedia.Mp3.Mp3File(audioPath);

                Player.AudioInfo ai = new Player.AudioInfo(audioPath);
                pathTxtBox.Text = audioPath;
                tBox_name.Text = ai.Name;
                tBox_type.Text = System.IO.Path.GetExtension(audioPath);
                albumTxtBox.Text = mp3doc.TagHandler.Album;
                timeTxtBox.Text = ai.Length;
                singerTxtBox.Text = ai.Artist;
                channelsTxtBox.Text = mp3doc.Mp3Meta.Header.ChannelMode.ToString();
                tracksTxtBox.Text = mp3doc.TagHandler.Track;

                khzTxtBox.Text = string.Format("{0}kHz", mp3doc.Mp3Meta.Header.SamplesPerSecond);
                versonTxtBox.Text = mp3doc.Mp3Meta.Header.VersionLayer;
                copyRightTxtBox.Text = mp3doc.Mp3Meta.Header.Copyright.ToString();
                kbpsTxtBox.Text = ai.BitRate;
                lengthTxtBox.Text = NgNet.IO.FileHelper.LengthFormat(new FileInfo(audioPath).Length);
                creationTimeTxtBox.Text = new FileInfo(audioPath).CreationTime.ToString();
                hashTxtBox.Text = NgNet.Security.HashHelper.GetFileMD5(audioPath);
            }
            catch (Exception ex)
            {
                NgNet.UI.Forms.MessageBox.Show(ex.Message);
            }

            if (_mc.MyLists.Times.ContainsValue(audioPath))
            {
                string value = _mc.MyLists.Times.Keys[_mc.MyLists.Times.IndexOfValue(audioPath)];
                playTimesTxtBox.Text = value.Substring(0, value.IndexOf("|"));
            }
            else
            {
                playTimesTxtBox.Text = "0";
            }
            //文件信息
            //
            //其他信息

            //其他信息
            //
            //歌词信息
            lyricTxtBox.Text = lyricPath;
            //歌词信息
            //
            this.Text = string.Format("音乐信息 - {0}", Application.ProductName);
        }
        #endregion
    }
}