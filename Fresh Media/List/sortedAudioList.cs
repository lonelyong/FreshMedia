using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.List
{
    class SortedAudioList : SortedList<string, string>, ILibList
    {
        #region attribute
        #region ILibList
        /// <summary>
        /// 列表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列表标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Int标签
        /// </summary>
        public int IntTag { get; set; }
        #endregion
        #endregion

        #region constructor destructor 
        public SortedAudioList() : this("", "")
        {

        }

        public SortedAudioList(string name, string title) : base()
        {
            Name = name;
            Title = title;
            IntTag = 0;
        }
        #endregion
    }
}
