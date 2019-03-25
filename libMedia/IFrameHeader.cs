namespace libMedia
{
    interface IFrameHeader
    {
        uint? BitRate { get;  }

        uint SamplesPerSecond { get; }
    }
}
