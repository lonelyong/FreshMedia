using System;

namespace FreshMedia.View
{
    delegate void BeginLoadingListEventHandler(ListLoadingEventArgs e);

    delegate void StopLoadingListEventHandler();

    delegate void ShowedListChangedEventHandler(ShowedListChangedEventArgs e);

    class ListLoadingEventArgs : EventArgs
    {
        public string Message { get; }

        public string ListName { get; }

        public List.MyLib Lib { get; }

        #region constructor destructor 
        public ListLoadingEventArgs(List.MyLib lib, string listName, string message)
        {
            Message = message;
            Lib = lib;
            ListName = listName;
        }
        #endregion
    }

    class ShowedListChangedEventArgs : EventArgs
    {
        #region properties
        public List.MyLib Lib { get; }

        public string ListName { get; }
        #endregion

        #region constructor
        public ShowedListChangedEventArgs(List.MyLib lib, string listName)
        {
            Lib = lib;
            ListName = listName;
        }
        #endregion
    }
}
