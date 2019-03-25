using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.List
{
    public class AudioList :NgNet.Collections.SignleCollection<string>, ILibList
    {
        #region private fileds
        private string _Title = string.Empty;
        #endregion

        #region public fileds
        public MyLib ParentLib { get; set; }
        #endregion

        #region ILibList
        /// <summary>
        /// 列表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列表标题
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (value == _Title)
                    return;
                string oldName = _Title;
                _Title = value;
                if (ListNameChangedEvent != null)
                    ListNameChangedEvent(new ListNameChangedEventArgs(ParentLib, oldName, value));
            }
        }

        /// <summary>
        /// Int标签
        /// </summary>
        public int IntTag { get; set; }

        #endregion

        #region envets
        public event ListNameChangedEventHandler ListNameChangedEvent;
        public event ListItemsChangedEventHandler ListItemsChangedEvent;
        public event ListCleanedEventHandler ListCleanedEvent;
        #endregion

        #region constructor destructor 
        public AudioList()
        {
            Name = string.Empty;
            Title = string.Empty;
            IntTag = 0;
        }

        public AudioList(AudioList audioList) : base(audioList)
        {
            Name = audioList.Name;
            Title = audioList.Title;
            IntTag = 0;
        }
        #endregion

        #region override
        public override string this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }

        public override void Add(string item)
        {
            bool existed;
            base.Add(item, out existed);
            if (existed == false)
                ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(ParentLib, Name, item, null));
        }

        public override void Insert(int index, string item)
        {
            base.Insert(index, item);
            ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(ParentLib, Name, item, null));
        }

        public override void Clear()
        {
            base.Clear();
            ListCleanedEvent?.Invoke(new ListCleanedEventArgs(ParentLib, Name));
        }

        public override bool Remove(string item)
        {
            bool exist = base.Remove(item);
            if(exist)
                ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(ParentLib, Name, null, item));
            return exist;
        }

        public override void RemoveAt(int index)
        {
            string rmItem = base[index];
            base.RemoveAt(index);
            ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(ParentLib, Name, null, rmItem));
        }

        public override void RemoveRange(int index, int count)
        {
            string[] rmItems = new string[count];
            for (int i = index; i < index + count; i++)
            {
                rmItems[i - index] = base[i];
            }
            base.RemoveRange(index, count);
            ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(ParentLib, Name, null, rmItems));
        }

        public override void AddRange(IEnumerable<string> collection)
        {
            base.AddRange(collection);
        }

        public override void AddRange(IEnumerable<string> collection, out IEnumerable<string> existedItems, out IEnumerable<string> addedItems)
        {
            base.AddRange(collection, out existedItems, out addedItems);
            ListItemsChangedEvent?.Invoke(new ListItemsChangedEventArgs(ParentLib, Name, addedItems, existedItems));
        }
        #endregion
    }
}
