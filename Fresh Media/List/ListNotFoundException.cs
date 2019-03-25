using System;

namespace FreshMedia.List
{
    public class ListNotFoundException : Exception
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
        #endregion

        #region constructor
        public ListNotFoundException(MyLib lib, string listName) : base(string.Format("未找到列表{0}", listName))
        {
            Lib = lib;
            ListName = listName;
        }
        #endregion
    }
}
