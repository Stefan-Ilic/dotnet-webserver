﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebServer
{
    [Serializable()]
    public class StatusCodeNotSetException : System.Exception
    {
        public StatusCodeNotSetException() : base() { }
        public StatusCodeNotSetException(string message) : base(message) { }
        public StatusCodeNotSetException(string message, System.Exception inner) : base(message, inner) { }
        protected StatusCodeNotSetException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
}