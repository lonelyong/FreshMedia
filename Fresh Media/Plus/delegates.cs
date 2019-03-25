using System;

namespace FreshMedia.Plus
{
    delegate void OnSleepingEventHandler(OnSleepingEventArgs e);

    class OnSleepingEventArgs : EventArgs
    {
        public uint LeftTime { get; }

        public uint SleepTime { get; }

        #region constructor destructor 
        public OnSleepingEventArgs(uint leftTime, uint sleepTime)
        {
            this.LeftTime = leftTime;
            this.SleepTime = sleepTime;
        }
        #endregion
    }
}
