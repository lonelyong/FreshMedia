using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libMedia
{
    interface IMpegFrameHeader:IFrameHeader
    {
        string VersionLayer { get;}

        uint Layer { get; set; }
    }
}
