using System;
using System.Windows.Forms;

namespace FreshMedia.Error
{
    public class ErrorHandle
    {
        #region public method
        public static void MCIErrorEvent(Player.PlayErrorEventArgs e)
        {
            string inf = string.Format(null, "MCIErrorID:{0}\r\nMCIErrorURL:{1}\r\nMCIErrorText:{2}", e.ErrorId, e.ErrorURL, e.ErrorText);
            NgNet.UI.Forms.MessageBox.Show(inf);
        }
        #endregion
    }
}
