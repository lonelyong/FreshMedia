using System.ComponentModel;

namespace FreshMedia.Player
{
    /// <summary>
    /// 播放状态
    /// </summary>
    public enum PlayStates : int
    {
        /// <summary>
        /// 播放
        /// </summary>
        playing = 0,
        /// <summary>
        /// 暂停
        /// </summary>
        paused = 1,
        /// <summary>
        /// 停止
        /// </summary>
        stoped = 2,
        /// <summary>
        /// 播放结束
        /// </summary>
        mediaEnd = 3,
        /// <summary>
        /// 切换歌曲
        /// </summary>
        transitioning = 4,
        /// <summary>
        /// 播放器关闭 当前Url为空
        /// </summary>
        closed = 5
    };

    /// <summary>
    /// 循环模式
    /// </summary>
    public enum CycleModes : int
    {
        /// <summary>
        /// 单曲播放
        /// </summary>
        [Description("单曲播放")]
        OneOnce = 0,
        /// <summary>
        /// 单曲循环
        /// </summary>
        [Description("单曲循环")]
        OneCycle = 1,
        /// <summary>
        /// 顺序播放
        /// </summary>
        [Description("顺序播放")]
        AllOnce = 2,
        /// <summary>
        /// 全部循环
        /// </summary>
        [Description("列表循环")]
        AllCycle = 4,
        /// <summary>
        /// 随机播放
        /// </summary>
        [Description("随机播放")]
        Random = 8
    }

    /// <summary>
    /// 播放位置改变模式
    /// </summary>
    public enum PositionChangeModes
    {
        General = 0,
        UserChange = 1
    }
}
