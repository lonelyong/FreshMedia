
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FreshMedia.View.VList
{
    class ViewManager : IFreshMedia 
    {
        #region public properties    
        public LibView MyLibView { get; private set; }

        public ListView MyListView { get; private set; }

        public SearchBox MySearchBox { get; private set; }
        #endregion

        #region  IFreshMedia

        Controller.MainController IFreshMedia.Controller
        {
            get
            {
                return _mc;
            }
        }
        #endregion

        #region attributes
        private Controller.MainController _mc;
        #endregion

        #region constructor destructor 
        public ViewManager(Controller.MainController _controller)
        {
            this._mc = _controller;
        }

        public void Init()
        {
            MyListView = new ListView(this);
            MyListView.Init();
            MyLibView = new LibView(this);
            MyLibView.Init();
            MySearchBox = new SearchBox(_mc.MainForm, MyLibView.LibTreeView, this);
            MySearchBox.Init();

            MyListView.LoadAudioList(List.MyLib.Favorite, List.ListManager.NAME_LIST_FAVO, true);
        }
        #endregion

        #region public method

        #endregion
    }
}
