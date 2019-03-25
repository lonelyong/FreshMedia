using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace FreshMedia.Controller
{
    class PlayController
    {
        #region private filed
        private MainController _mc;

        #endregion

        #region public filed
        public Player.MediaPlayer myPlayer { get; }
        #endregion

        #region constructor destructor 
        public PlayController(MainController _controller)
        {
            myPlayer = new Player.MediaPlayer();
            this._mc = _controller;
            this._mc.MyLists.ListItemsChangedEvent += new List.ListItemsChangedEventHandler(PlayingListItemsCountChangedEvent);
            this._mc.MyLists.ListCleanedEvent += new FreshMedia.List.ListCleanedEventHandler(PlayingListCleanedEvent);
        }
        #endregion

        #region  play control
        public static bool FileTest(string path)
        {
            if (File.Exists(path))
                return true;
            NgNet.UI.Forms.MessageBox.Show("文件不存在！ \r\n\r\npath = " + path);
            return false;
        }
        /// <summary>
        /// 暂停或播放
        /// </summary>
        public void PlayOrPause()
        {
            //当前播放状态为正在播放
            if (myPlayer.settings.PlayState == Player.PlayStates.playing)
            {
                myPlayer.ctControls.pause();
                return;
            }
            //当前播放状态为暂停
            else if (myPlayer.settings.PlayState == Player.PlayStates.paused)
            {
                myPlayer.ctControls.play();
                return;
            }
            else if (myPlayer.settings.PlayState == Player.PlayStates.stoped || myPlayer.settings.PlayState == Player.PlayStates.mediaEnd)
            {
                myPlayer.ctControls.play();
                return;
            }
            List.MyLib lib = _mc.ListViewManager.MyListView.ShowedLib;
            string listName = _mc.ListViewManager.MyListView.ShowedList;
            int listIndex = _mc.MyLists.GetListIndex(lib, listName);
            if (listIndex == -1)
                throw new List.ListNotFoundException(lib, listName);
            IEnumerable<string> paths = null;
            switch (lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    paths = _mc.MyLists.Playing[listIndex];
                    break;
                case List.MyLib.Local:
                    paths = _mc.MyLists.Local[listIndex];
                    break;
                case List.MyLib.History:
                    paths = _mc.MyLists.History[listIndex];
                    break;
                case List.MyLib.Favorite:
                    paths = _mc.MyLists.Favo[listIndex];
                    break;
                case List.MyLib.RecentlyAdded:
                    paths = _mc.MyLists.RecentlyAdded[listIndex];
                    break;
                case List.MyLib.MostlyPlayed:
                    paths = _mc.MyLists.Times.Values.Reverse();
                    break;
                default:
                    break;
            }
            if (paths == null || paths.Count<string>() == 0)
            {
                for (int i = 0; i < _mc.MyLists.Local.Count; i++)
                {
                    if (_mc.MyLists.Local[i].Count == 0)
                        continue;
                    else
                        ListPlay(List.MyLib.Local, _mc.MyLists.Local[i].Name, _mc.MyLists.Local[i][0]);
                    break;
                }
            }
            else
            {
                ListPlay(lib, listName, paths.FirstOrDefault<string>());
            }

        }
        /// <summary>
        ///播放指定库的指定列表
        /// </summary>
        /// <param name="lib">库名</param>
        /// <param name="listName">列表名</param>
        /// <param name="path">文件路径</param>
        public void ListPlay(List.MyLib lib, string listName, string path)
        {
            if (FileTest(path) == false)
                return;
            int currentListIndex = _mc.MyLists.GetListIndex(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT);
            if (currentListIndex == -1)
                throw new List.ListNotFoundException(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT);
            int listIndex = _mc.MyLists.GetListIndex(lib, listName);
            if (listIndex == -1)
                throw new List.ListNotFoundException(lib, listName);
            //                  
            //创建正在播放列表
            IEnumerable<string> paths = null;
            switch (lib)
            {
                case List.MyLib.None:
                case List.MyLib.Playing:
                    goto PlayPath;
                case List.MyLib.Local:
                    paths = _mc.MyLists.Local[listIndex];
                    break;
                case List.MyLib.History:
                    paths = _mc.MyLists.History[listIndex];
                    break;
                case List.MyLib.Favorite:
                    paths = _mc.MyLists.Favo[listIndex];
                    break;
                case List.MyLib.RecentlyAdded:
                    paths = _mc.MyLists.RecentlyAdded[listIndex];
                    break;
                case List.MyLib.MostlyPlayed:
                    paths = _mc.MyLists.Times.Values.Reverse();
                    break;
                default:
                    throw new Exception(string.Format("类型为{0}的字段{1}引发异常", typeof(List.MyLib).Namespace, "lib"));
            }
            _mc.MyLists.CleanList(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT);
            _mc.MyLists.AddMedias(List.MyLib.Playing, List.ListManager.NAME_LIST_CURRENT, paths);
        PlayPath:
            myPlayer.ctControls.listPlay(path);
        }
        /// <summary>
        /// 临时播放指定路径的音乐
        /// </summary>
        /// <param name="path"></param>
        public void InterimPlay(string path)
        {
            myPlayer.ctControls.interimPlay(path);
        }
        #endregion

        #region private method
        private void PlayingListItemsCountChangedEvent(List.ListItemsChangedEventArgs e)
        {
            if (e.Lib == FreshMedia.List.MyLib.Playing)
            {
                myPlayer.playList.AddMeidas(e.NewItems);
                myPlayer.playList.RemoveMedias(e.RemovedItems);
            }
        }

        private void PlayingListCleanedEvent(List.ListCleanedEventArgs e)
        {
            if (e.Lib == FreshMedia.List.MyLib.Playing)
            {
                myPlayer.playList.Clean();
            }
        }
        #endregion
    }
}
