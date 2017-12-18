using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyWebServer
{
    /// <summary>
    /// Is thrown when the status code is not set
    /// </summary>
    [Serializable]
    public class StatusCodeNotSetException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        /// <summary>
        /// Throws the exception
        /// </summary>
        public StatusCodeNotSetException()
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="message"></param>
        public StatusCodeNotSetException(string message) : base(message)
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public StatusCodeNotSetException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected StatusCodeNotSetException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Is thrown when the content is not set
    /// </summary>
    [Serializable]
    public class ContentNotSetException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        /// <summary>
        /// Throws the exception
        /// </summary>
        public ContentNotSetException()
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="message"></param>
        public ContentNotSetException(string message) : base(message)
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ContentNotSetException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ContentNotSetException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }   

    /// <summary>
    /// is thrown when the plugin type is not known
    /// </summary>
    [Serializable]
    public class UnknownPluginTypeException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        /// <summary>
        /// Throws the exception
        /// </summary>
        public UnknownPluginTypeException()
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="message"></param>
        public UnknownPluginTypeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public UnknownPluginTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Throws the exception
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected UnknownPluginTypeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
