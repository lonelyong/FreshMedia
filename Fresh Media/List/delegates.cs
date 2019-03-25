using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.List
{
    public delegate void ListItemsChangedEventHandler(ListItemsChangedEventArgs e);
    public delegate void ListNameChangedEventHandler(ListNameChangedEventArgs e);
    public delegate void ListAddedEventHandler(ListAddedEventArgs e);
    public delegate void ListRemovedEventHandler(ListRemovedEventArgs e);
    public delegate void ListCleanedEventHandler(ListCleanedEventArgs e);
    public delegate void LibResetedEventHandler(LibResetedEventArgs e);

    public class ListItemsChangedEventArgs : EventArgs
    {
        public List.MyLib Lib { get; }

        public string List { get; }

        public IEnumerable<string> NewItems { get; }

        public IEnumerable<string> RemovedItems { get; }

        #region constructor destructor 
        public ListItemsChangedEventArgs(List.MyLib lib, string listName, IEnumerable<string> newItems, IEnumerable<string> removedItems)
        {
            this.Lib = lib;
            this.List = listName;
            NewItems = newItems == null ? new string[] { } : newItems;
            RemovedItems = removedItems == null ? new string[] { } : removedItems;
        }

        public ListItemsChangedEventArgs(List.MyLib lib, string listName, string newItem, string removedItem)
            : this(
                  lib, 
                  listName, 
                  newItem == null ? new string[] { } : new string[] { newItem }, 
                  removedItem == null ? new string[] { } : new string[] { removedItem })
        {
           
        }
        #endregion
    }

    public class ListNameChangedEventArgs : EventArgs
    {
        public string OldName { get; }

        public string NewName { get; }

        public MyLib Lib { get; }

        #region constructor destructor 
        public ListNameChangedEventArgs(MyLib lib, string oldName, string newName)
        {
            this.OldName = oldName;
            this.NewName = newName;
            Lib = lib;
        }
        #endregion
    }

    public class ListAddedEventArgs : EventArgs
    {
        public MyLib Lib { get; }

        public string ListName { get; }

        public int ListIndex { get; }

        #region constructor destructor 
        public ListAddedEventArgs(MyLib lib, string listName, int listIndex)
        {
            Lib = lib;
            ListName = listName;
            ListIndex = listIndex;
        }
        #endregion
    }

    public class ListRemovedEventArgs : EventArgs
    {
        public List.MyLib Lib { get; }
        public string ListName { get; }

        #region constructor destructor 
        public ListRemovedEventArgs(List.MyLib lib, string listName)
        {
            this.Lib = lib;
            this.ListName = listName;
        }
        #endregion
    }

    public class ListCleanedEventArgs
    {
        public MyLib Lib { get; }
        public string ListName { get; }

        #region constructor destructor 
        public ListCleanedEventArgs(MyLib lib, string listName)
        {
            Lib = lib;
            ListName = listName;
        }
        #endregion
    }

    public class LibResetedEventArgs
    {
        public MyLib Lib { get; }

        #region constructor destructor 
        public LibResetedEventArgs(MyLib lib)
        {
            this.Lib = lib;
        }
        #endregion
    }
}
