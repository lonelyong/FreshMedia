// Copyright(C) 2002-2012 Hugo Rumayor Montemayor, All rights reserved.
using System;
using System.Text;
using System.IO;

namespace libMedia.ID3.Frames
{
    /// <summary>
    /// Manages User defined URL WXXX Frames
    /// </summary>
    /// <remarks>
    /// Text encoding     $xx
    /// Description       text string according to encoding, $00 (00)
    /// URL               text string
    /// </remarks>
    [Frame("WXXX")]
    public class FrameUrlUserDef : FrameBase, IFrameDescription
    {
        #region Fields
        private Utils.TextCode _textEncoding;
        private string _contents;
        private string _url;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a URL frame
        /// </summary>
        /// <param name="frameId">Type of URL frame</param>
        public FrameUrlUserDef(string frameId)
            : base(frameId)
        {
            _textEncoding = Utils.TextCode.Ascii;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Type of text encoding the frame is using
        /// </summary>
        public Utils.TextCode TextCode
        {
            get { return _textEncoding; }
            set { _textEncoding = value; }
        }

        /// <summary>
        /// Description of the frame contents
        /// </summary>
        public string Description
        {
            get { return _contents; }
            set { _contents = value; }
        }

        /// <summary>
        /// The URL page location
        /// </summary>
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parse the binary frame
        /// </summary>
        /// <param name="frame">binary frame</param>
        public override void Parse(byte[] frame)
        {
            //TODO: Handle this invalid tag
            if (frame.Length < 1)
                return;

            int index = 0;
            _textEncoding = (Utils.TextCode)frame[index];
            index++;
            _contents = Utils.TextBuilder.ReadText(frame, ref index, _textEncoding);
            _url = Utils.TextBuilder.ReadTextEnd(frame, index, _textEncoding);
        }

        /// <summary>
        /// Create a binary frame
        /// </summary>
        /// <returns>binary frame</returns>
        public override byte[] Make()
        {
            MemoryStream buffer = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(buffer);
            writer.Write((byte)_textEncoding);
            writer.Write(Utils.TextBuilder.WriteText(_contents, _textEncoding));
            writer.Write(Utils.TextBuilder.WriteTextEnd(_url, _textEncoding));
            return buffer.ToArray();
        }
        /// <summary>
        /// Default frame description
        /// </summary>
        /// <returns>URL text</returns>
        public override string ToString()
        {
            return _url;
        }
        #endregion
    }
}
