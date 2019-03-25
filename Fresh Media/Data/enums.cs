namespace FreshMedia.Data
{
    /// <summary>
    /// 配置信息及用户数据存储位置
    /// </summary>
    enum ApplicationDataModes : int
    {
        /// <summary>
        /// 系统应用程序目录
        /// </summary>
        AppdataPath = 0,
        /// <summary>
        /// 应用程序目录
        /// </summary>
        ApplicationPath = 1,
        /// <summary>
        /// 用户自定义
        /// </summary>
        UserSet = 2
    }
}
