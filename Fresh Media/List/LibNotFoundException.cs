using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.List
{


    public class LibNotFoundException : Exception
    {
        /// <summary>
        /// 未找到的列表所在的库
        /// </summary>
        public MyLib Lib { get; private set; }

        #region constructor
        public LibNotFoundException(MyLib lib) : base(string.Format("未找到库{0}"))
        {
            Lib = lib;
        }
        #endregion

    }
}
