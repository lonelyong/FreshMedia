using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FreshMedia.List
{
    class ListManager
    {
        #region private filed
        #endregion

        #region event
        public event ListItemsChangedEventHandler ListItemsChangedEvent;
        public event ListNameChangedEventHandler ListNameChangedEvent;
        public event ListAddedEventHandler ListAddedEvent;
        public event ListRemovedEventHandler ListRemovedEvent;
        public event ListCleanedEventHandler ListCleanedEvent;
        public event LibResetedEventHandler LibResetedEvent;
        #endregion

        #region 列表参数
        /// <summary>
        /// 受保护的列表名称
        /// </summary>
        public static string[] ProtectedListName;
        public static List<string>[] DataBase;
        #region list
        public AudioLib Local { get; }

        public AudioLib History { get; }

        public AudioLib Favo { get; }

        public AudioLib RecentlyAdded { get; }

        public AudioLib Playing { get; }

        public SortedAudioList Times { get; }
        #endregion

        #region 列表及库名称
        public const string NAME_LIB_CURRENT = "Current";

        public const string NAME_LIB_LOCAL = "Local";

        public const string NAME_LIB_FAVO = "Favorite";

        public const string NAME_LIB_HISTORY = "History";

        public const string NAME_LIB_RECENTLYADDED = "RecentlyAdded";

        public const string NAME_LIB_MOSTPLAYED = "MostlyPlayed";

        public const string TITLE_LIB_CURRENT = "正在播放";

        public const string TITLE_LIB_LOCAL = "本地列表";

        public const string TITLE_LIB_FAVO = "我的收藏";

        public const string TITLE_LIB_HISTORY = "历史播放";

        public const string TITLE_LIB_RECENTLYADDED = "最近添加";

        public const string TITLE_LIB_MOSTPLAYED = "最多播放";

        public const string NAME_LIST_DEFAULT = "默认列表";

        public const string NAME_LIST_CURRENT = "正在播放";

        public const string NAME_LIST_FAVO = "我的收藏";

        public const string NAME_LIST_HISTORY = "历史播放";

        public const string NAME_LIST_RECENTLYADDED = "最近添加";

        public const string NAME_LIST_MOSTLYPLAYED = "最多播放";
        #endregion

        #region 限制参数
        public const int MAX_COUNT_LOCAL = 18;//允许的最大本地列表数

        public const int MAX_COUNT_RENCENTLYADDED = 100;//最近添加的最大音乐数

        public const int MAX_COUNT_HISTORY = 100;//历史播放的最大音乐数

        public const int MAX_COUNT_MOSTLYPLAYED = 100;//最多播放的最大音乐数

        public const int MAX_LENGTH_LISTNAME = 18;//列表名的最大长度
        #endregion
        #endregion

        #region constructor destructor 
        public ListManager()
        {
            Favo = new AudioLib(NAME_LIB_FAVO, TITLE_LIB_FAVO);
            History = new AudioLib(NAME_LIB_HISTORY, TITLE_LIB_HISTORY);
            Local = new AudioLib(NAME_LIB_LOCAL, TITLE_LIB_LOCAL);
            Playing = new AudioLib(NAME_LIB_CURRENT, TITLE_LIB_CURRENT);
            RecentlyAdded = new AudioLib(NAME_LIB_RECENTLYADDED, TITLE_LIB_RECENTLYADDED);
            Times = new SortedAudioList(NAME_LIB_MOSTPLAYED, TITLE_LIB_MOSTPLAYED);
            ResetFavo();
            ResetHistory();
            ResetLocal();
            ResetPlaying();
            ResetRecentlyAdded();
            ResetMostedPlayed();
        }

        static ListManager()
        {
            ProtectedListName = new string[]
           {
                "默认列表",  "最近播放",
                "历史播放",  "搜索结果",
                "我的收藏",  "播放记录",
                "最近添加",  "本地列表",
                "最多播放",  "最近添加",
                "searchlist","我的最爱",
                "alllist",   "audiolist",
                "null",      "current",
                "local",     "favorite",
                "mostlyplayed","历史",
                "列表","recentlyadded"
           };

            DataBase = new List<string>[]
            {
                new List <string>(),
                new List <string>(),
                new List <string>(),
                new List <string>(),
                new List <string>(),
                new List <string>(),
                new List <string>()
            };
        }
        #endregion

        #region private method
        private void init()
        {

        }
        #endregion

        #region 列表基本操作
        #region 列表名检测
        /// <summary>
        /// 判断列表名是不是合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool NameTest(string name)
        {
            string msg = null;
            if (NgNet.IO.PathHelper.IsNameInvalidCharContained(name))
            {
                msg = "不合法的列表名，请勿输入  / | \\ ? \" : * 等 ";
            }
            if (name.Contains(" "))
            {
                msg = "列表名不能包含空格，请重新输入！";
            }
            if (ProtectedListName.Contains(name.ToLower()))
            {
                msg = "您输入了程序保留的名称，请重新输入！";
            }
            if (name.Length > MAX_LENGTH_LISTNAME)
            {
                msg = string.Format("您输入的列表名称过长，请确保您输入的字符数不超过{0}个，您需要重新输入！", MAX_LENGTH_LISTNAME);
            }
            if(msg == null) return true;
            else
            {
                NgNet.UI.Forms.MessageBox.Show(msg, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.DialogResult.OK, 6);
                return false;
            }
        }
        #endregion

        #region 音乐信息库(static)
        //记录音乐信息数据库
        private Shell32.ShellClass shellClass = new Shell32.ShellClass();
        private Shell32.Folder shell32Folder;
        private Shell32.FolderItem shell32FolderItem;
        public void AddDataBase(string path)
        {
            if (DataBase[0].Contains(path))
                return;
            shell32Folder = shellClass.NameSpace(Path.GetDirectoryName(path));
            shell32FolderItem = shell32Folder.ParseName(Path.GetFileName(path));
            DataBase[0].Add(path);
            DataBase[1].Add(shell32Folder.GetDetailsOf(shell32FolderItem, 13));
            DataBase[2].Add(shell32Folder.GetDetailsOf(shell32FolderItem, 21));
            DataBase[3].Add(shell32Folder.GetDetailsOf(shell32FolderItem, 14));
            DataBase[4].Add(shell32Folder.GetDetailsOf(shell32FolderItem, 15));
            DataBase[5].Add(shell32Folder.GetDetailsOf(shell32FolderItem, 27));
            DataBase[6].Add(shell32Folder.GetDetailsOf(shell32FolderItem, 0));
        }
        #endregion

        #region 列表初始化
        public void ResetLocal()
        {
            Local.Clear();
            AudioList al = new AudioList();
            al.Name = NAME_LIST_DEFAULT;
            al.Title = NAME_LIST_DEFAULT;
            Local.Add(al);
            LibResetedEvent?.Invoke(new LibResetedEventArgs(MyLib.Local));
        }

        public void ResetHistory()
        {
            History.Clear();
            AudioList alist = new AudioList();
            alist.Name = NAME_LIST_HISTORY;
            alist.Title = NAME_LIST_HISTORY;
            History.Add(alist);
            LibResetedEvent?.Invoke(new LibResetedEventArgs(MyLib.History));
        }

        public void ResetFavo()
        {
            Favo.Clear();
            AudioList alist = new AudioList();
            alist.Name = NAME_LIST_FAVO;
            alist.Title = NAME_LIST_FAVO;
            Favo.Add(alist);
            LibResetedEvent?.Invoke(new LibResetedEventArgs(MyLib.Favorite));
        }

        public void ResetRecentlyAdded()
        {
            RecentlyAdded.Clear();
            AudioList alist = new AudioList();
            alist.Name = NAME_LIST_RECENTLYADDED;
            alist.Title = NAME_LIST_RECENTLYADDED;
            RecentlyAdded.Add(alist);
            LibResetedEvent?.Invoke(new LibResetedEventArgs(MyLib.RecentlyAdded));
        }

        public void ResetPlaying()
        {
            Playing.Clear();
            AudioList alist = new AudioList();
            alist.Name = NAME_LIST_CURRENT;
            alist.Title = NAME_LIST_CURRENT;
            Playing.Add(alist);
            LibResetedEvent?.Invoke(new LibResetedEventArgs(MyLib.Playing));
        }

        public void ResetMostedPlayed()
        {
            Times.Clear();
            Times.Name = NAME_LIST_MOSTLYPLAYED;
            Times.Title = NAME_LIST_MOSTLYPLAYED;
            LibResetedEvent?.Invoke(new LibResetedEventArgs(MyLib.MostlyPlayed));
        }
        #endregion

        #region  获取列表信息
        /// <summary>
        /// 获取指定列表的音乐数
        /// </summary>
        /// <param name="lib">库名</param>
        /// <param name="listName">列表名</param>
        /// <returns></returns>
        public int GetMediaCount(MyLib lib, string listName)
        {
            if (string.IsNullOrWhiteSpace(listName))
                return GetListCount(lib);
            AudioLib _lib = new AudioLib();
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    _lib = Playing;
                    break;
                case MyLib.Local:
                    _lib = Local;
                    break;
                case MyLib.History:
                    _lib = History;
                    break;
                case MyLib.Favorite:
                    _lib = Favo;
                    break;
                case MyLib.RecentlyAdded:
                    _lib = RecentlyAdded;
                    break;
                case MyLib.MostlyPlayed:
                    return Times.Count;
                default:
                    break;
            }

            int listIndex = GetListIndex(lib, listName);
            if (listIndex == -1)
                return -1;
            return _lib[listIndex].Count;
        }

        public int GetListCount(MyLib lib)
        {
            int _count = 0;
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    _count = Playing.Count;
                    break;
                case MyLib.Local:
                    for (int i = 0; i < Local.Count; i++)
                    {
                        if (Local[i] != null)
                            _count++;
                    }
                    break;
                case MyLib.History:
                    _count = History.Count;
                    break;
                case MyLib.Favorite:
                    _count = Favo.Count;
                    break;
                case MyLib.RecentlyAdded:
                    _count = RecentlyAdded.Count;
                    break;
                case MyLib.MostlyPlayed:
                    _count = Times.Count;
                    break;
                default:
                    break;
            }
            return _count;
        }

        /// <summary>
        /// 获取本地列表的索引
        /// </summary>
        /// <param name="listName">列表名</param>
        /// <returns></returns>
        public int GetListIndex(MyLib lib, string listName)
        {
            int index = -1;
            AudioLib _lib = new AudioLib();
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    _lib = Playing;
                    break;
                case MyLib.Local:
                    _lib = Local;
                    break;
                case MyLib.History:
                    _lib = History;
                    break;
                case MyLib.Favorite:
                    _lib = Favo;
                    break;
                case MyLib.RecentlyAdded:
                    _lib = RecentlyAdded;
                    break;
                case MyLib.MostlyPlayed:

                    return 0;
                default:
                    break;
            }
            for (int i = 0; i < _lib.Count; i++)
            {

                if (_lib[i] == null)
                    continue;
                if (_lib[i].Name == listName)
                {
                    return i;
                }
            }
            return index;
        }

        /// <summary>
        /// 获取库名称
        /// </summary>
        /// <param name="lib"></param>
        /// <returns></returns>
        public string GetLibName(MyLib lib)
        {
            string name = null;
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    name = NAME_LIB_CURRENT;
                    break;
                case MyLib.Local:
                    name = NAME_LIB_LOCAL;
                    break;
                case MyLib.History:
                    name = NAME_LIB_HISTORY;
                    break;
                case MyLib.Favorite:
                    name = NAME_LIB_FAVO;
                    break;
                case MyLib.RecentlyAdded:
                    name = NAME_LIB_RECENTLYADDED;
                    break;
                case MyLib.MostlyPlayed:
                    name = NAME_LIB_MOSTPLAYED;
                    break;
                default:
                    break;
            }
            return name;
        }

        /// <summary>
        /// 获取库的显示标题
        /// </summary>
        /// <param name="lib"></param>
        /// <returns></returns>
        public string GetLibTitle(MyLib lib)
        {
            string title = null;
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    title = TITLE_LIB_CURRENT;
                    break;
                case MyLib.Local:
                    title = TITLE_LIB_LOCAL;
                    break;
                case MyLib.History:
                    title = TITLE_LIB_HISTORY;
                    break;
                case MyLib.Favorite:
                    title = TITLE_LIB_FAVO;
                    break;
                case MyLib.RecentlyAdded:
                    title = TITLE_LIB_RECENTLYADDED;
                    break;
                case MyLib.MostlyPlayed:
                    title = TITLE_LIB_MOSTPLAYED;
                    break;
                default:
                    break;
            }
            return title;
        }
        #endregion

        #region 添加音乐 列表
        public void AddList(MyLib lib, AudioList list, int listIndex)
        {
            if (listIndex < 0 || listIndex > GetListCount(lib))
                return;
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    break;
                case MyLib.Local:
                    if (listIndex == 0)
                    {
                        string[] tmp = Local[0].ToArray<string>();
                        Local[0] = list;
                        ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(lib, NAME_LIST_DEFAULT, list, tmp));
                        return;
                    }   
                    else
                        Local.Insert(listIndex, list);
                    break;
                case MyLib.History:
                    break;
                case MyLib.Favorite:
                    break;
                case MyLib.RecentlyAdded:
                    break;
                case MyLib.MostlyPlayed:
                    break;
                default:
                    break;
            }
            ListAddedEvent?.Invoke(new ListAddedEventArgs(lib, list.Name, listIndex));
        }

        /// <summary>
        /// 刷新播放次数
        /// </summary>
        /// <param name="path">音乐路径</param>
        public void AddMost(IEnumerable<KeyValuePair<string,string>> timepPths)
        {
            if (timepPths == null || timepPths.Count<KeyValuePair<string, string>>() == 0)
                return;
            int itemIndex = -1;
            foreach (KeyValuePair<string,string> item in timepPths)
            {
                itemIndex = Times.IndexOfValue(item.Value);
                if (itemIndex != -1)
                    Times.RemoveAt(itemIndex);
                Times.Add(item.Key, item.Value);
            }     
        }

        public int AddMedias(MyLib lib, string listName, IEnumerable<string> paths)
        {
            if (paths == null || paths.Count<string>() == 0)
                return 0;

            int listIndex = GetListIndex(lib, listName);
            int itemIndex = -1;
            if (listIndex == -1)
                throw new List.ListNotFoundException(lib, listName);
            StringBuilder sb = new StringBuilder(null);
            IEnumerable<string> addItems;
            IEnumerable<string> existedItems;
            AudioList al = null;
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    al = Playing[listIndex];
                    break;
                case MyLib.Local:
                    al = Local[listIndex];
                    break;
                case MyLib.History:
                    #region
                    al = History[listIndex];
                    foreach (string item in paths)
                    {
                        itemIndex = al.IndexOf(item);
                        if (itemIndex != -1)
                        {
                            al.Remove(item);
                        }
                        al.Insert(0, item);
                    }
                    //最近添加数据库记录是否超出上限
                    if (al.Count > MAX_COUNT_HISTORY)
                    {
                        al.RemoveRange(MAX_COUNT_HISTORY, al.Count - MAX_COUNT_HISTORY);
                    }
                    ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(lib, listName, "", null));
                    return paths.Count<string>();
                    #endregion
                case MyLib.Favorite:
                    al = Favo[listIndex];
                    break;
                case MyLib.RecentlyAdded:
                    #region
                    al = RecentlyAdded[listIndex];
                    foreach (string item in paths)
                    {
                        itemIndex = al.IndexOf(item);
                        if (itemIndex != -1)
                        {
                            al.Remove(item);
                        }
                        al.Insert(0, item);
                    }
                    //最近添加数据库记录是否超出上限
                    if (al.Count > MAX_COUNT_RENCENTLYADDED)
                    {
                        al.RemoveRange(MAX_COUNT_RENCENTLYADDED, al.Count - ListManager.MAX_COUNT_RENCENTLYADDED);
                    }
                    if (paths.Count<string>() > 0)
                        ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(lib, listName, "", null));
                    return paths.Count<string>();
                #endregion
                case MyLib.MostlyPlayed:
                    #region 
                    foreach (string path in paths)
                    {
                        itemIndex = Times.IndexOfValue(path);
                        if (itemIndex != -1)
                        {                           
                            int time = Convert.ToInt32(Times.Keys[itemIndex].Substring(0, Times.Keys[itemIndex].IndexOf("|")));
                            Times.RemoveAt(itemIndex);
                            try
                            {
                                Times.Add(string.Format("{0:D3}|{1}", time + 1, path), path);
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            Times.Add($"001|{path}", path);
                        }
                    }
                    ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(lib, listName, "", null));
                    return paths.Count<string>();
                    #endregion
                default:
                    break;
            }
            if (al == null)
                return -1;
            al.AddRange(paths, out existedItems, out addItems); 
            ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(lib, listName, addItems, null)); 
            foreach (string item in existedItems)
            {
                sb.Append("\r\n  ");
                sb.Append(item);
            }
            if (addItems.Count<string>() < paths.Count<string>())//表示有已经存在的音乐
            {
                string inf = string.Format("列表 <{0}> 中已经存在以下文件：{1}", listName, sb);
                NgNet.UI.Forms.MessageBox.Show(inf);
            }
            return addItems.Count<string>();
        }
        #endregion

        #region 移除音乐 列表
        /// <summary>
        /// 移除指定库指定列表指定路径的音乐
        /// </summary>
        /// <param name="lib">音乐所在库</param>
        /// <param name="listName">音乐所在列表</param>
        /// <param name="paths">音乐的文件路径</param>
        public void RemoveMedias(MyLib lib, string listName, IEnumerable<string> paths)
        {
            if (paths == null)
                return;
            int listindex = GetListIndex(lib, listName);
            if (listindex == -1)
                throw new ListNotFoundException(lib, listName);
            List<string> removed = new List<string>();
            int itemIndex;
            IList<string> scrList = null;
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    scrList = Playing[listindex];
                    //在数据库及列表删除指定项         
                    break;
                case MyLib.Local:
                    scrList = Local[listindex];
                    break;
                case MyLib.History:
                    scrList = History[listindex];
                    break;
                case MyLib.Favorite:
                    scrList = Favo[listindex];
                    break;
                case MyLib.RecentlyAdded:
                    scrList = RecentlyAdded[listindex];
                    break;
                case MyLib.MostlyPlayed:
                    scrList = Times.Values;
                    break;
                default:
                    break;
            }
            if(lib == MyLib.MostlyPlayed)
                foreach (string item in paths)
                {
                    itemIndex = scrList.IndexOf(item);
                    if (itemIndex != -1)
                    {
                        Times.RemoveAt(itemIndex);
                        removed.Add(item);
                    }
                }
            else
                foreach (string item in paths)
                {
                    itemIndex = scrList.IndexOf(item);
                    if (itemIndex != -1)
                    {
                        scrList.RemoveAt(itemIndex);
                        removed.Add(item);
                    }
                }
            if (removed.Count > 0)
                ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(lib, listName, null, removed));
        }

        /// <summary>
        /// 在所有列表中移除音乐
        /// </summary>
        /// <param name="paths"></param>
        public void RemoveAudioInAllList(IEnumerable<string> paths)
        {
            //正在播放列表
            RemoveMedias(List.MyLib.Playing, NAME_LIST_CURRENT, paths);
            //我的收藏
            RemoveMedias(List.MyLib.Favorite, NAME_LIST_FAVO, paths);
            //历史播放
            RemoveMedias(List.MyLib.History, NAME_LIST_HISTORY, paths);
            //最近添加
            RemoveMedias(List.MyLib.RecentlyAdded, NAME_LIST_RECENTLYADDED, paths);
            //最多播放
            RemoveMedias(List.MyLib.MostlyPlayed, NAME_LIST_MOSTLYPLAYED, paths);
            //本地列表
            foreach (AudioList locallist in Local)
            {
                if (locallist != null)
                    RemoveMedias(MyLib.Local, locallist.Name, paths);
            }
        }

        /// <summary>
        /// 清空指定库指定名称的列表
        /// </summary>
        /// <param name="lib">库名</param>
        /// <param name="listName">列表名</param>
        public void CleanList(MyLib lib, string listName)
        {
            int listindex = GetListIndex(lib, listName);
            if (listindex == -1)
                throw new ListNotFoundException(lib, listName);

            switch (lib)
            {
                case List.MyLib.None:
                    break;
                case List.MyLib.Playing:
                    Playing[listindex].Clear();//清空正在播放列表数据库
                    break;
                case List.MyLib.Local:
                    Local[listindex].Clear();
                    break;
                case List.MyLib.History:
                    History[listindex].Clear();
                    break;
                case List.MyLib.Favorite:
                    Favo[listindex].Clear();
                    break;
                case List.MyLib.RecentlyAdded:
                    RecentlyAdded[listindex].Clear();
                    break;
                case List.MyLib.MostlyPlayed:
                    Times.Clear();
                    break;
            }
            ListCleanedEvent?.Invoke(new ListCleanedEventArgs(lib, listName));
        }
        /// <summary>
        /// 清空库
        /// </summary>
        /// <param name="lib"></param>
        public void CleanLib(MyLib lib)
        {
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    break;
                case MyLib.Local:
                    ResetLocal();
                    break;
                case MyLib.History:
                    break;
                case MyLib.Favorite:
                    break;
                case MyLib.RecentlyAdded:
                    break;
                case MyLib.MostlyPlayed:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 移除列表
        /// </summary>
        /// <param name="lib"></param>
        /// <param name="listName"></param>
        public void RemoveList(List.MyLib lib, string listName)
        {
            int listIndex = GetListIndex(lib, listName);
            if (listIndex == -1)
                throw new List.ListNotFoundException(lib, listName);
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    break;
                case MyLib.Local:
                    if (listIndex == GetListIndex(MyLib.Local, NAME_LIST_DEFAULT))
                        break;
                    else if(listIndex == -1)
                        throw new ListNotFoundException(lib, listName);
                    Local.RemoveAt(listIndex);
                    ListRemovedEvent?.Invoke(new ListRemovedEventArgs(lib, listName));
                    break;
                case MyLib.History:
                    break;
                case MyLib.Favorite:
                    break;
                case MyLib.RecentlyAdded:
                    break;
                case MyLib.MostlyPlayed:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 重命名
        public void RenameList(MyLib lib, string oldListName, string newListName)
        {
            if (string.IsNullOrWhiteSpace(oldListName + newListName))
                throw new Exception($"{nameof(oldListName)},{nameof(newListName)}不能为空");
            if (string.Compare(newListName, oldListName, true) == 0)
                return;
            int listIndex = GetListIndex(lib, oldListName);
            if(listIndex == -1)
                throw new ListNotFoundException(lib, oldListName);
            ILibList targetLibList = null;
            switch (lib)
            {
                case MyLib.None:
                    break;
                case MyLib.Playing:
                    targetLibList = Playing[listIndex];
                    break;
                case MyLib.Local:
                    targetLibList = Local[listIndex];
                    break;
                case MyLib.History:
                    targetLibList = History[listIndex];
                    break;
                case MyLib.Favorite:
                    targetLibList = Favo[listIndex];
                    break;
                case MyLib.RecentlyAdded:
                    targetLibList = RecentlyAdded[listIndex];
                    break;
                case MyLib.MostlyPlayed:
                    targetLibList = Times;
                    break;
                default:
                    break;
            }
            targetLibList.Name = newListName;
            targetLibList.Title = newListName;
            ListNameChangedEvent?.Invoke(new ListNameChangedEventArgs(lib, oldListName, newListName));
        }
        #endregion
        #endregion
    }
}
