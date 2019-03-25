using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.List
{
    public class ItemNotFoundException : Exception
    {

        #region public fileds
        /// <summary>
        /// 未找到的列表所在的库
        /// </summary>
        public MyLib Lib { get; private set; }
        /// <summary>
        /// 未找到列表的名称
        /// </summary>
        public string ListName { get; private set; }
        /// <summary>
        ///  找不到的项名
        /// </summary>
        public string Item { get; }
        #endregion

        #region constructor
        public ItemNotFoundException(MyLib lib, string listName, string item) : base(string.Format("在库{0}的列表{1}中未找到{2}。", lib, listName, item))
        {
            Lib = lib;
            ListName = listName;
            Item = item;
        }
        #endregion
    }
}
