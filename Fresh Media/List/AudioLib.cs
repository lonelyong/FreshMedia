namespace FreshMedia.List
{
    public class AudioLib : NgNet.Collections.SignleCollection<AudioList>, ILibList
    {
        #region public fileds
        public MyLib Lib { get; set; }
        #endregion

        #region attribute
        #region ILib
        /// <summary>
        /// 库名称
        /// </summary>
        public string Name { get; set; } 

        /// <summary>
        /// 库标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Int标签
        /// </summary>
        public int IntTag { get; set; }
        #endregion
        #endregion

        #region constructor destructor 
        public AudioLib() : this(null, null)
        {

        }

        public AudioLib (string name, string title)
        {
            Name = name;
            Title = title;
            IntTag = 0;
        }
        #endregion
    }
}
