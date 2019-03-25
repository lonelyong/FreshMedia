using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshMedia.Player
{
    sealed class Mpeg4Player : PlayerBase
    {
        public override long currentPosition
        {
            get
            {
                return base.currentPosition;
            }
            set
            {
                base.currentPosition = value;
            }
        }

        public override long mediaLength
        {
            get
            {
                return base.mediaLength;
            }
        }

        public override bool Mute
        {
            get
            {
                return base.Mute;
            }

            set
            {
                base.Mute = value;
            }
        }

        public override PlayStates PlayState
        {
            get
            {
                return base.PlayState;
            }
        }

        public override string URL
        {
            get
            {
                return base.URL;
            }

            set
            {
                base.URL = value;
            }
        }

        public override byte Volume
        {
            get
            {
                return base.Volume;
            }

            set
            {
                base.Volume = value;
            }
        }

        public override bool close()
        {
            throw new NotImplementedException();
        }

        public override bool pause()
        {
            throw new NotImplementedException();
        }

        public override bool play()
        {
            return false;
        }

        public override bool stop()
        {
            throw new NotImplementedException();
        }

        public override void rewind(long millisecond)
        {
            throw new NotImplementedException();
        }

        public override void forward(long millisecond)
        {
            throw new NotImplementedException();
        }

        public override event PlayStateChangedEventHandler PlayStateChangedEvent;
    }
}
