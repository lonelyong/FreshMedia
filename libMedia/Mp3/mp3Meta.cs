using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace libMedia.Mp3
{
    class mp3Meta : IMp3Meta
    {
        #region Fields

        /// <summary>
        /// first audio frame; could be could be xing or vbri header
        /// </summary>
        private mp3Frame _firstFrame;

        /// <summary>
        /// duration of the audio block, as parsed from the optional ID3v2 "TLEN" tag
        /// the Xing/VBRI header is more authoritative for bitrate calculations, if present.
        /// </summary>
        private TimeSpan? _id3DurationTag;

        /// <summary>
        /// number of frames and audio bytes - obtained by counting the entire file - slow!
        /// </summary>
        Mp3Stats _audioStats;

        /// <summary>
        /// flag that is set on parse error.
        /// This might turn into an enum for different errors at some point
        /// </summary>
        private bool _hasInconsistencies;

        /// <summary>
        /// holds audio stream filename; opened afresh when we need the data
        /// </summary>
        private FileInfo _sourceFileInfo;
        /// <summary>
        /// offset from start of stream that the audio starts at
        /// </summary>
        private UInt32 _payloadStart;
        /// <summary>
        /// total length (bytes) of mp3 audio frames in the file,
        /// could be different from what's declared in the header if the file is corrupt.
        /// </summary>
        private UInt32 _payloadNumBytes;

        #endregion

        #region Construction
        /// <summary>
        /// construct audio file
        /// passing in audio size and id3 length tag (if any) to help with bitrate calculations
        /// </summary>
        /// <param name="sourceFileInfo"></param>
        /// <param name="audioStart"></param>
        /// <param name="payloadNumBytes"></param>
        /// <param name="id3DurationTag"></param>
        public mp3Meta(FileInfo sourceFileInfo, UInt32 audioStart, UInt32 payloadNumBytes, TimeSpan? id3DurationTag)
        {
            _firstFrame = ReadFirstFrame(sourceFileInfo, audioStart, payloadNumBytes);
            _id3DurationTag = id3DurationTag;
            if (_firstFrame == null)
            {
                throw new InvalidFrameException("MPEG Audio Frame not found");
            }
 
            /*_hasInconsistencies = false;*/
            _sourceFileInfo = sourceFileInfo;
            _payloadStart = audioStart;
            _payloadNumBytes = payloadNumBytes;

            CheckConsistency();
        }

        private static mp3Frame ReadFirstFrame(FileInfo sourceFileInfo, UInt32 audioStart, UInt32 audioNumBytes)
        {
            using (FileStream stream = sourceFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                stream.Seek(audioStart, SeekOrigin.Begin);

                // read a base level mp3 frame header
                // if it can't even do that right, just fail the call.
                return mp3FrameFactory.CreateFrame(stream, audioNumBytes);
            }
        }

        private void CheckConsistency()
        {
            Exception ex = ParsingError;
            if (ex != null)
            {
                _hasInconsistencies = true;
                Trace.WriteLine(ex.Message);
            }
        }
        #endregion

        #region IMp3Meta Functions
        /// <summary>
        /// the stream containing the audio data, wound to the start
        /// </summary>
        /// <remarks>
        /// it is the caller's responsibility to dispose of the returned stream
        /// and to call NumPayloadBytes to know how many bytes to read.
        /// </remarks>
        public Stream OpenAudioStream()
        {
            FileStream stream = _sourceFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Seek(_payloadStart, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// calculate sha-1 of the audio data
        /// </summary>
        public byte[] CalculateAudioSHA1()
        {
            using (Stream stream = OpenAudioStream())
            {
                // This is one implementation of the abstract class SHA1.
                System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();

                uint numLeft = _payloadNumBytes;

                const int size = 4096;
                byte[] bytes = new byte[4096];
                int numBytes;

                while (numLeft > 0)
                {
                    // read a whole block, or to the end of the file
                    numBytes = stream.Read(bytes, 0, size);

                    // audio ends on or before end of this read; exit loop and checksum what we have.
                    if (numLeft <= numBytes)
                        break;

                    sha.TransformBlock(bytes, 0, size, bytes, 0);
                    numLeft -= (uint)numBytes;
                }

                sha.TransformFinalBlock(bytes, 0, (int)numLeft);

                byte[] result = sha.Hash;
                return result;
            }
        }

        /// <summary>
        /// Count frames and bytes of file to see who's telling porkies
        /// </summary>
        public void ScanWholeFile()
        {
            _audioStats._numFrames = 0;
            _audioStats._numBytes = 0;

            using (Stream stream = OpenAudioStream())
            {
                uint payloadStart = (uint)stream.Position;
                try
                {
                    while (true)
                    {
                        uint pos = (uint)stream.Position;
                        uint used = pos - payloadStart;
                        uint remainingBytes = NumPayloadBytes - used;

                        mp3Frame frame = mp3FrameFactory.CreateFrame(stream, remainingBytes);
                        if (frame == null)
                            break;

                        ++_audioStats._numFrames;
                        _audioStats._numBytes += frame.FrameLengthInBytes;
                        //Trace.WriteLine(string.Format("frame {0} ({1} bytes) found at {2}",
                        //                              _audioStats._numFrames,
                        //                              frame.Header.FrameLengthInBytes,
                        //                              stream.Position - frame.Header.FrameLengthInBytes));
                    }
                }
                catch (Exception e)
                {
                    _hasInconsistencies = true;
                    Trace.WriteLine(e.Message);
                }
            }
        }
        #endregion

        #region IMp3Meta Properties

        /// <summary>
        /// text info, e.g. the encoding standard of audio data in AudioStream
        /// /// </summary>
        public string DebugString
        {
            get
            {
                //----AudioFrame----
                //  Payload: 10336 frames in 4750766 bytes
                //  Length: 270 seconds
                //  140 kbit
                //  Counted: 10567 frames in 4750766 bytes
                string retval = string.Format("{0}\n----Audio----\n  Payload: {1} frames in {2} bytes\n  Length: {3:N3} seconds\n  {4:N4} kbit",
                                     _firstFrame.DebugString,
                                     NumPayloadFrames,
                                     NumPayloadBytes,
                                     Duration,
                                     BitRate);

                if (_audioStats._numFrames > 0)
                {
                    retval += string.Format("\n  Counted: {0} frames, {1} bytes",
                                            _audioStats._numFrames,
                                            _audioStats._numBytes);
                }
                //----AudioFile----
                //  Header starts: 12288 bytes
                //  FileSize: 4750766 bytes
                retval = string.Format("{0}\n----AudioFile----\n  Header starts: {1} bytes\n  Payload: {2} bytes",
                                              retval,
                                              _payloadStart,
                                              _payloadNumBytes);
                return retval;
            }
        }

        /// <summary>
        /// the number of bytes of data in AudioStream, always the real size of the file
        /// </summary>
        public uint NumPayloadBytes
        {
            get
            {
                return _payloadNumBytes;
            }
        }

        /// <summary>
        /// the mp3 frame header number of bytes of audio data in AudioStream
        /// </summary>
        public mp3FrameHeader Header
        {
            get
            {
                return _firstFrame.Header;
            }
        }

        /// <summary>
        /// is it a VBR file? i.e. is it better encoding quality than cbr at the same bitrate?
        /// first we make a guess based on the audio header found in the first frame.
        /// </summary>
        /// <remarks>
        /// if the frame didn't have any strong opinions,
        /// we don't check if the mp3 audio header bitrate is the same as the calculated bitrate
        /// because a truncated file shows up as vbr (because the bitrates don't match)
        /// and we just return false.
        /// </remarks>
        public virtual bool IsVbr
        {
            get
            {
                bool? frameVbr = _firstFrame.IsVbr;
                if (frameVbr != null)
                    return frameVbr.Value;

                return false;
            }
        }

        /// <summary>
        /// does it have a VBR (VBRI, XING, INFO, LAME) header?
        /// </summary>
        public bool HasVbrHeader
        {
            get
            {
                // return true if it's no longer the base class 'AudioFrame'
                return _firstFrame.GetType() != typeof(mp3Frame);
            }
        }

        /// <summary>
        /// Number of bytes playable audio, VBR header priority, best for calculating bitrates
        /// </summary>
        /// <remarks>
        /// if there is no xing/vbri header, it's the same as NumPayloadBytes
        /// if the xing header doesn't have the audio bytes filled in, 
        /// it can still return 'don't know, but you need to take one header off the file length'
        /// </remarks>
        public uint NumAudioBytes
        {
            get
            {
                uint? numAudioBytes = _firstFrame.NumAudioBytes;
                if (numAudioBytes != null)
                    return numAudioBytes.Value;
                else if (HasVbrHeader)
                {
                    // vbr header will never be free bitrate, because they're vbr instead
                    uint? frameLengthInBytes = _firstFrame.Header.FrameLengthInBytes;
                    if (frameLengthInBytes == null)
                        throw new InvalidFrameException("VBR files cannot be 'free' bitrate");

                    return NumPayloadBytes - (uint)_firstFrame.Header.FrameLengthInBytes;
                }
                else
                    return NumPayloadBytes;
            }
        }

        /// <summary>
        /// Number of Frames in file (including the header frame)
        /// VBR header priority, best for calculating bitrates
        /// or if not present, calculated from the number of bytes in the audio block, as reported by the caller
        /// This will be correct for CBR files, at least.
        /// </summary>
        public uint NumPayloadFrames
        {
            get
            {
                uint? numPayloadFrames = _firstFrame.NumPayloadFrames;
                if (numPayloadFrames.HasValue)
                    return numPayloadFrames.Value;
                else
                {
                    double? framelength = Header.IdealisedFrameLengthInBytes;
                    if (framelength.HasValue)
                        return (uint)Math.Round(NumPayloadBytes / framelength.Value);
                    else
                        return NumPayloadBytes / _firstFrame.FrameLengthInBytes;
                }
            }
        }

        /// <summary>
        /// Number of Frames of playable audio
        /// </summary>
        /// <remarks>
        /// if there is no xing/vbri header, it's the same as NumPayloadFrames
        /// if the xing header doesn't have the audio frames filled in, 
        /// it can still return 'don't know, but you need to take one header off the file length'
        /// </remarks>
        public uint NumAudioFrames
        {
            get
            {
                if (HasVbrHeader)
                    return NumPayloadFrames - 1;
                else
                    return NumPayloadFrames;
            }
        }

        /// <summary>
        /// Number of seconds for bitrate calculations.
        /// first get it from the xing/vbri headers,
        /// then from the id3 TLEN tag,
        /// then from the file size and initial frame bitrate.
        /// </summary>
        public double Duration
        {
            get
            {
                double? headerDuration = _firstFrame.Duration;
                if (headerDuration != null)
                    return headerDuration.Value;
                else if (_id3DurationTag != null)
                    return _id3DurationTag.Value.TotalSeconds;
                else
                    return NumAudioFrames * Header.SecondsPerFrame;
            }
        }

        /// <summary>
        /// bitrate calculated from the id3 length tag, and the length of the audio
        /// </summary>
        public double? BitRateCalc
        {
            get
            {
                if (_id3DurationTag == null)
                    return null;
                else
                    return NumPayloadBytes * 8 / _id3DurationTag.Value.TotalSeconds;
            }
        }

        /// <summary>
        /// bitrate published in the standard mp3 header
        /// </summary>
        public uint? BitRateMp3
        {
            get
            {
                return Header.BitRate;
            }
        }

        /// <summary>
        /// vbr bitrate from xing or vbri header frame
        /// audio without xing or vbri header returns null
        /// </summary>
        public double? BitRateVbr
        {
            get
            {
                return _firstFrame.BitRateVbr;
            }
        }

        /// <summary>
        /// overall best guess of bitrate; there's always a way of guessing it
        /// </summary>
        public double BitRate
        {
            get
            {
                // get best guess at duration from derived classes, or id3 TLEN tag, or first frame bitrate
                double duration = Duration;

                // get best guess at numbytes from derived classes, or audio length
                uint numBytes = NumAudioBytes;

                // bitrate is size / time
                return numBytes / duration * 8;
            }
        }

        /// <summary>
        /// did it parse without any errors?
        /// </summary>
        public bool HasInconsistencies
        {
            get
            {
                return _hasInconsistencies;
            }
        }

        /// <summary>
        /// get the error from the parse operation, if any
        /// </summary>
        /// <remarks>
        /// should the parse operation save all thrown exceptions here,
        /// and not generate it on demand?
        /// </remarks>
        public Exception ParsingError
        {
            get
            {
                uint? vbrPayloadBytes = _firstFrame.NumPayloadBytes;
                if (vbrPayloadBytes != null && vbrPayloadBytes != NumPayloadBytes)
                {
                    return new InvalidVbrSizeException(NumPayloadBytes, vbrPayloadBytes.Value);
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region nested types

        struct Mp3Stats
        {
            public uint _numFrames;
            public uint _numBytes;
        }

        #endregion
    }
}
