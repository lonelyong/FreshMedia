// Copyright(C) 2002-2012 Hugo Rumayor Montemayor, All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;

namespace libMedia.ID3
{
    /// <summary>
    /// Helper class to get a description of the frame identifiers.
    /// </summary>
    public static class FrameDescription 
    {
        #region Fields
        /// <summary>
        /// Keep a relation between frame Frames and descriptions of them
        /// </summary>
        private static Dictionary<string, string> _descriptions;
        #endregion

        #region constructor
        static FrameDescription()
        {
            intitalize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtain a human description of a frame
        /// </summary>
        /// <param name="frameId">the four character frame id</param>
        /// <returns>description of the tag</returns>
        public static string GetDescription(string frameId)
        {
            string description;
            if (_descriptions.TryGetValue(frameId, out description))
                return description;
            return "Unknown tag";

        }

        /// <summary>
        /// Fill the hash with the frame descriptors
        /// </summary>
        private static void intitalize()
        {
            _descriptions = new Dictionary<string, string>();
            // ID3v2.3
            _descriptions.Add("TYER", "Recording Year");
            // ID3v2.4
            _descriptions.Add("AENC", "Audio encryption");
            _descriptions.Add("APIC", "Attached picture");
            _descriptions.Add("ASPI", "Audio seek point index");

            _descriptions.Add("COMM", "Comments");
            _descriptions.Add("COMR", "Commercial frame");

            _descriptions.Add("ENCR", "Encryption method registration");
            _descriptions.Add("EQU2", "Equalisation");
            _descriptions.Add("ETCO", "Event timing codes");

            _descriptions.Add("GEOB", "General encapsulated object");
            _descriptions.Add("GRID", "Group identification registration");

            _descriptions.Add("LINK", "Linked information");

            _descriptions.Add("MCDI", "Music CD identifier");
            _descriptions.Add("MLLT", "MPEG location lookup table");

            _descriptions.Add("OWNE", "Ownership frame");

            _descriptions.Add("PRIV", "Private frame");
            _descriptions.Add("PCNT", "Play counter");
            _descriptions.Add("POPM", "Popularimeter");
            _descriptions.Add("POSS", "Position synchronisation frame");

            _descriptions.Add("RBUF", "Recommended buffer size");
            _descriptions.Add("RVA2", "Relative volume adjustment");
            _descriptions.Add("RVRB", "Reverb");

            _descriptions.Add("SEEK", "Seek frame");
            _descriptions.Add("SIGN", "Signature frame");
            _descriptions.Add("SYLT", "Synchronised lyric/text");
            _descriptions.Add("SYTC", "Synchronised tempo codes");

            _descriptions.Add("TALB", "Album/Movie/Show title");
            _descriptions.Add("TBPM", "Beats per minute)");
            _descriptions.Add("TCOM", "Composer");
            _descriptions.Add("TCON", "Content type");
            _descriptions.Add("TCOP", "Copyright message");
            _descriptions.Add("TDEN", "Encoding time");
            _descriptions.Add("TDLY", "Playlist delay");
            _descriptions.Add("TDOR", "Original release time");
            _descriptions.Add("TDRC", "Recording time");
            _descriptions.Add("TDRL", "Release time");
            _descriptions.Add("TDTG", "Tagging time");
            _descriptions.Add("TENC", "Encoded by");
            _descriptions.Add("TEXT", "Lyricist/Text writer");
            _descriptions.Add("TFLT", "File type");
            _descriptions.Add("TIPL", "Involved people list");
            _descriptions.Add("TIT1", "Content group description");
            _descriptions.Add("TIT2", "Title/song name/content description");
            _descriptions.Add("TIT3", "Subtitle/Description refinement");
            _descriptions.Add("TKEY", "Initial key");
            _descriptions.Add("TLAN", "Language(s)");
            _descriptions.Add("TLEN", "Length");
            _descriptions.Add("TMCL", "Musician credits list");
            _descriptions.Add("TMED", "Media type");
            _descriptions.Add("TMOO", "Mood");
            _descriptions.Add("TOAL", "Original album/movie/show title");
            _descriptions.Add("TOFN", "Original filename");
            _descriptions.Add("TOLY", "Original lyricist(s)/text writer(s)");
            _descriptions.Add("TOPE", "Original artist(s)/performer(s)");
            _descriptions.Add("TOWN", "File owner/licensee");
            _descriptions.Add("TPE1", "Lead performer(s)/Soloist(s)");
            _descriptions.Add("TPE2", "Band/orchestra/accompaniment");
            _descriptions.Add("TPE3", "Conductor/performer refinement");
            _descriptions.Add("TPE4", "Interpreted, remixed, or otherwise modified by");
            _descriptions.Add("TPOS", "Part of a set");
            _descriptions.Add("TPRO", "Produced notice");
            _descriptions.Add("TPUB", "Publisher");
            _descriptions.Add("TRCK", "Track number/Position in set");
            _descriptions.Add("TRSN", "Internet radio station name");
            _descriptions.Add("TRSO", "Internet radio station owner");
            _descriptions.Add("TSOA", "Album sort order");
            _descriptions.Add("TSOP", "Performer sort order");
            _descriptions.Add("TSOT", "Title sort order");
            _descriptions.Add("TSRC", "ISRC (international standard recording code)");
            _descriptions.Add("TSSE", "Software/Hardware and settings used for encoding");
            _descriptions.Add("TSST", "Set subtitle");
            _descriptions.Add("TXXX", "User defined text information frame");

            _descriptions.Add("UFID", "Unique file identifier");
            _descriptions.Add("USER", "Terms of use");
            _descriptions.Add("USLT", "Unsynchronised lyric/text transcription");

            _descriptions.Add("WCOM", "Commercial information");
            _descriptions.Add("WCOP", "Copyright/Legal information");
            _descriptions.Add("WOAF", "Official audio file webpage");
            _descriptions.Add("WOAR", "Official artist/performer webpage");
            _descriptions.Add("WOAS", "Official audio source webpage");
            _descriptions.Add("WORS", "Official Internet radio station homepage");
            _descriptions.Add("WPAY", "Payment");
            _descriptions.Add("WPUB", "Publishers official webpage");
            _descriptions.Add("WXXX", "User defined URL link frame");
        } 
        #endregion
    } 
}
