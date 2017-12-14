using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyWebServer
{
    [Serializable]
    public class StatusCodeNotSetException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public StatusCodeNotSetException()
        {
        }

        public StatusCodeNotSetException(string message) : base(message)
        {
        }

        public StatusCodeNotSetException(string message, Exception inner) : base(message, inner)
        {
        }

        protected StatusCodeNotSetException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class ContentNotSetException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ContentNotSetException()
        {
        }

        public ContentNotSetException(string message) : base(message)
        {
        }

        public ContentNotSetException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ContentNotSetException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }   

    [Serializable]
    public class UnknownPluginTypeException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public UnknownPluginTypeException()
        {
        }

        public UnknownPluginTypeException(string message) : base(message)
        {
        }

        public UnknownPluginTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UnknownPluginTypeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
