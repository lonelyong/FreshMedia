// Copyright(C) 2002-2012 Hugo Rumayor Montemayor, All rights reserved.
using System;
using System.Runtime.Serialization;

namespace libMedia.ID3.Exceptions
{
	/// <summary>
	/// The exception is thrown when the tag is missing.
	/// </summary>
    [Serializable]
    public class TagNotFoundException : Exception
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TagNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        /// <summary>
        /// 
        /// </summary>
		public TagNotFoundException()
		{
		}
	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
		public TagNotFoundException(string message): base(message)
		{
		}

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
		public TagNotFoundException(string message, Exception inner): base(message, inner)
		{
		}
	}
}