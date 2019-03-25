using System;
using System.Diagnostics;
using System.IO;

namespace libMedia.Mp3
{
	/// <summary>
	/// Manage MP3 file data with ID3v2 tags and audio data stream.
	/// </summary>
	public class Mp3File
    {
        // possible return values for update functions
        enum CacheDataState
        {
            eClean, // data is still valid and can be used again
            eDirty  // data is now out of date; delete and recreate it
        };

        #region fields
        /// <summary>
        /// name of source file
        /// </summary>
        private FileInfo _sourceFileInfo;

        /// <summary>
        /// Current MP3Audio object - if re-assigned, owned by assigner.
        /// </summary>
        private IMp3Meta _Mp3Meta;

        /// <summary>
        /// set to true if the audio is replaced
        /// </summary>
        private bool _audioReplaced;

        /// <summary>
        /// offset from start of original stream that the original audio started at
        /// </summary>
        private UInt32 _audioStart;

        /// <summary>
        /// ID3v2 tag model at start of file
        /// </summary>
        private ID3.TagHandler _tagHandler;

        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public Mp3File(string path) : this(new FileInfo(path))
        {

        } 

        /// <summary>
        /// Construct from file info; parse ID3 tags from stream and calculate where the audio must be
        /// </summary>
        /// <param name="fileinfo"></param>
        public Mp3File(FileInfo fileinfo)
        {
            init(fileinfo);
        }

        private void init(FileInfo fileinfo)
        {
            _sourceFileInfo = fileinfo;

            // create an empty frame model, to use if we don't parse anything better
            ID3.TagModel tagModel = new ID3.TagModel();

            // don't know how big the audio is until we've parsed the tags
            UInt32 audioNumBytes;

            using (FileStream sourceStream = fileinfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // all the header calculations use UInt32; 
                // this guarantees all the file offsets we have to deal with fit in a UInt32
                if (sourceStream.Length > UInt32.MaxValue)
                    throw new InvalidFrameException("MP3 file can't be bigger than 4gb");

                // in the absence of any recognised tags,
                // audio starts at the start
                _audioStart = 0;
                // audio is entire file length
                audioNumBytes = (UInt32)sourceStream.Length;

                // try to read an ID3v1 block.
                // If ID3v2 block exists, its values overwrite these
                // Otherwise, if ID3V1 block exists, its values are used
                // The audio is anything that's left after all the tags are excluded.
                try
                {
                    ID3.ID3v1 id3v1 = new ID3.ID3v1();
                    id3v1.Deserialize(sourceStream);

                    // fill in ID3v2 block from the ID3v1 data
                    tagModel = id3v1.FrameModel;

                    // audio is shorter by the length of the id3v1 tag
                    audioNumBytes -= ID3.ID3v1.TagLength;
                }
                catch (ID3.Exceptions.TagNotFoundException)
                {
                    // ignore "no ID3v1 block"
                    // everything else isn't caught here, and throws out to the caller
                }

                try
                {
                    sourceStream.Seek(0, SeekOrigin.Begin);
                    tagModel = ID3.TagManager.Deserialize(sourceStream);

                    // audio starts after the tag
                    _audioStart = (uint)sourceStream.Position;
                    // audio is shorter by the length of the id3v2 tag
                    audioNumBytes -= _audioStart;
                }
                catch (ID3.Exceptions.TagNotFoundException)
                {
                    // ignore "no ID3v2 block"
                    // everything else isn't caught here, and throws out to the caller
                }

                // create a taghandler to hold the tagmodel we've parsed, if any
                _tagHandler = new ID3.TagHandler(tagModel);

            } // closes sourceStream

            // save the location of the audio in the original file
            // passing in audio size and id3 length tag (if any) to help with bitrate calculations
            _Mp3Meta = new mp3Meta(fileinfo, _audioStart, audioNumBytes, _tagHandler.Length);
            _audioReplaced = false;
        }
        #endregion

        #region properties
        /// <summary>
        /// wrapper for the object containing the audio payload
        /// </summary>
        public IMp3Meta Mp3Meta
        {
            get
            {
                return _Mp3Meta;
            }
            private set
            {
                // this destroys the original one
                // but as it only holds names we don't need to dispose it
                _Mp3Meta = value;

                // set a flag so we know we can't update in-situ when they call update
                _audioReplaced = true;
            }
        }

        /// <summary>
        /// ID3v2 tags represented by the Frame Model
        /// </summary>
        public ID3.TagModel TagModel
        {
            get { return _tagHandler.FrameModel; }
            set { _tagHandler.FrameModel = value; }
        }

        /// <summary>
        /// ID3v2 tags wrapped in an interpreter that gives you real properties for each supported frame 
        /// </summary>
        public ID3.TagHandler TagHandler
        {
            get { return _tagHandler; }
            set { _tagHandler = value; }
        }

        #endregion

        #region public update methods
        
        private CacheDataState _Update()
        {
            CacheDataState _cacheDataState;
            if (!TagModel.IsValid)
            {
                // the standard does not allow an id3v2 tag with no frames in, 
                // so we must remove it completely.
                _cacheDataState = _UpdateNoV2tag();
            }
            else
            {
                TagModel.UpdateSize();
                uint tagSizeComplete = TagModel.Header.TagSizeWithHeaderFooter;

                if (tagSizeComplete <= _audioStart && !_audioReplaced)
                {
                    UpdateInSitu(tagSizeComplete);
                    _cacheDataState = CacheDataState.eClean;
                }
                else
                {
                    // calculate enough padding to round final file to 2k cluster size.
                    uint minLength = tagSizeComplete + _Mp3Meta.NumPayloadBytes + ID3.ID3v1.TagLength;
                    uint newLength = ((minLength + 2047) & 0xFFFFF800);    // round up to whole 2k cluster
                    TagModel.Header.PaddingSize = newLength - minLength;

                    string bakName = Path.ChangeExtension(_sourceFileInfo.FullName, "bak");
                    FileInfo bakFileInfo = new FileInfo(bakName);
                    RewriteFile(bakFileInfo);
                    _cacheDataState = CacheDataState.eDirty;
                }
            }
            return _cacheDataState;
        }

        /// <summary>
        /// Update ID3V2 and V1 tags in-situ if possible, or rewrite the file to add tags if necessary.
        /// </summary>
        /// <returns>bool true if the MP3FileData object is dirty after this</returns>
        public void Update()
        {
            if (_Update() == CacheDataState.eDirty)
                this.init(_sourceFileInfo);
        }

        private CacheDataState _UpdatePacked()
        {
            CacheDataState _cacheDataState;
            if (TagModel.Count == 0)
            {
                // the standard does not allow an id3v2 tag with no frames in, 
                // so we must remove it completely.
                // this removes the padding too, of course.
                _cacheDataState = _UpdateNoV2tag();
            }
            else
            {
                TagModel.UpdateSize();
                TagModel.Header.PaddingSize = 0;

                string bakName = Path.ChangeExtension(_sourceFileInfo.FullName, "bak");
                FileInfo bakFileInfo = new FileInfo(bakName);
                RewriteFile(bakFileInfo);
                _cacheDataState = CacheDataState.eDirty;
            }
            return _cacheDataState;
        }

        /// <summary>
        /// rewrite the file and ID3V2 and V1 tags with no padding.
        /// </summary>
        /// <returns>bool true if the MP3FileData object is dirty after this</returns>
        public void UpdatePacked()
        {          
            if (_UpdatePacked() == CacheDataState.eDirty)
                this.init(_sourceFileInfo);
        }
      
        private CacheDataState _UpdateNoV2tag()
        {
            CacheDataState _cacheDataState;
            if (_audioStart == 0 && !_audioReplaced)
            {
                // no v2 tag to start with; just update v1 tag
                UpdateInSituNoV2tag();
                _cacheDataState = CacheDataState.eClean;
            }
            else
            {
                string bakName = Path.ChangeExtension(_sourceFileInfo.FullName, "bak");
                FileInfo bakFileInfo = new FileInfo(bakName);
                RewriteFileNoV2tag(bakFileInfo);
                _cacheDataState = CacheDataState.eDirty;
            }
            return _cacheDataState;
        }

        /// <summary>
        /// Update file and remove ID3V2 tag (if any); 
        /// update file in-situ if possible, or rewrite the file to remove tag if necessary.
        /// </summary>
        /// <returns>bool true if the MP3FileData object is unusable after this</returns>
        public void UpdateNoV2tag()
        {            
            if (_UpdateNoV2tag() == CacheDataState.eDirty)
                this.init(_sourceFileInfo);
        }
        #endregion

        #region private update methods
        /// <summary>
        /// UpdateInSitu
        /// </summary>
        /// <remarks>
        /// doesn't make a backup as it's only modifying the tags not re-writing the audio
        /// </remarks>
        /// <param name="tagSizeComplete"></param>
        private void UpdateInSitu( uint tagSizeComplete )
        {
            // if tag is before the audio, it shouldn't have a footer
            Debug.Assert(TagModel.Header.Footer == false);

            // calculate enough padding to fill the gap between tag and audio start
            TagModel.Header.PaddingSize = _audioStart - tagSizeComplete;

            // open source file in readwrite mode
            using (FileStream writeStream = _sourceFileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                // write header, tags and padding to start of audio
                ID3.TagManager.Serialize(TagModel, writeStream);

                // verify we've filled the gap exactly
                Debug.Assert(writeStream.Position == _audioStart);

                // now overwrite or append the ID3V1 tag
                WriteID3v1(writeStream);
            }
        }

        /// <summary>
        /// create new output file stream and write the ID3v2 block to it
        /// </summary>
        /// <remarks>
        /// makes a backup as it's modifying the tags and re-writing the audio.
        /// Always need to re-initialise the mp3 file wrapper if you use it after this runs
        /// </remarks>
        /// <param name="bakFileInfo">location of backup file - must be on same drive</param>
        private void RewriteFile( FileInfo bakFileInfo )
        {
            // generate a temp filename in the target's directory
            string tempName = Path.ChangeExtension(_sourceFileInfo.FullName, "$$$");
            FileInfo tempFileInfo = new FileInfo(tempName);

            using (FileStream writeStream = tempFileInfo.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            {
                // write an ID3v2 tag to new file
                ID3.TagManager.Serialize(TagModel, writeStream);

                uint newAudioStart = (uint)writeStream.Position;

                CopyAudioStream(writeStream);

                // if the stream copies without error, update the start of the audio
                _audioStart = newAudioStart;

                // now overwrite or append the ID3v1 tag to new file
                WriteID3v1(writeStream);
            }

            // replace the original file, delete new file if fail
            try
            {
                Utils.FileMover.FileMove(tempFileInfo, _sourceFileInfo, bakFileInfo);
            }
            catch
            {
                tempFileInfo.Delete();
                throw;
            }
        }

        /// <summary>
        /// UpdateInSituNoV2tag
        /// </summary>
        /// <remarks>
        /// doesn't make a backup as it's only modifying the tags not re-writing the audio
        /// </remarks>
        private void UpdateInSituNoV2tag()
        {
            // open source file in readwrite mode
            using (FileStream writeStream = _sourceFileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                // just overwrite or append the ID3V1 tag
                WriteID3v1(writeStream);
            }
        }

        /// <summary>
        /// create new output file stream and don't write the ID3v2 block to it
        /// </summary>
        /// <remarks>
        /// makes a backup as it's re-writing the audio
        /// Always need to re-initialise the mp3 file wrapper if you use it after this runs
        /// </remarks>
        /// <param name="bakFileInfo">location of backup file - must be on same drive</param>
        private void RewriteFileNoV2tag( FileInfo bakFileInfo )
        {
            // generate a temp filename in the target's directory
            string tempName = Path.ChangeExtension(_sourceFileInfo.FullName, "$$$");
            FileInfo tempFileInfo = new FileInfo(tempName);

            using( FileStream writeStream = tempFileInfo.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.Read) )
            {
                CopyAudioStream(writeStream);

                // if the stream copies without error, update the start of the audio
                _audioStart = 0;

                // now overwrite or append the ID3v1 tag to new file
                WriteID3v1(writeStream);
            }

            // replace the original file, delete new file if fail
            try
            {
                Utils.FileMover.FileMove(tempFileInfo, _sourceFileInfo, bakFileInfo);
            }
            catch
            {
                tempFileInfo.Delete();
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writeStream"></param>
        public void CopyAudioStream( FileStream writeStream )
        {
            // open original mp3 file stream and seek to the start of the audio
            // or, if the audio's been replaced, create a memorystream from the buffer.
            using( Stream audio = _Mp3Meta.OpenAudioStream() )
            {
                // Copy mp3 stream
                // this will also copy the original ID3v1 tag if present, 
                // but it's only 128 bytes extra so it won't matter.
                const int size = 4096;
                byte[] bytes = new byte[4096];
                int numBytes;
                while( (numBytes = audio.Read(bytes, 0, size)) > 0 )
                    writeStream.Write(bytes, 0, numBytes);
            }
        }

        /// <summary>
        /// append or overwrite ID3v1 tag at the end of the audio
        /// </summary>
        /// <param name="stream"></param>
        public void WriteID3v1( Stream stream )
        {
            ID3.ID3v1 v1tag = new ID3.ID3v1();
            v1tag.FrameModel = TagModel;

            stream.Seek(_audioStart + _Mp3Meta.NumPayloadBytes, SeekOrigin.Begin);
            v1tag.Write(stream);
        }
        #endregion
    }
}
