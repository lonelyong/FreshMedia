using System;
namespace libMedia
{

	/// <summary>
	/// The exception is thrown when an audio frame is corrupt.
	/// </summary>
    [Serializable]
    public class InvalidFrameException : ID3.Exceptions.InvalidStructureException
	{
        /// <summary>
        /// 
        /// </summary>
		public InvalidFrameException()
		{
		}
	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
		public InvalidFrameException(string message): base(message)
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public InvalidFrameException(string message, Exception inner)
            : base(message, inner)
		{
		}
	}
}