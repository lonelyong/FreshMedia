// Copyright(C) 2002-2012 Hugo Rumayor Montemayor, All rights reserved.
using System;
using System.Runtime.Serialization;

namespace libMedia.ID3.Exceptions
{
	/// <summary>
	/// The exception is thrown when a frame is corrupt.
	/// </summary>
    [Serializable]
    public class InvalidFrameException : InvalidStructureException
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected InvalidFrameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
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
		public InvalidFrameException(string message, Exception inner): base(message, inner)
		{
		}
	}
}