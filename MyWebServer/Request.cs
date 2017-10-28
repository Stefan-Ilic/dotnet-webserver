using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class Request : IRequest
    {
        public Request(Stream requestStream)
        {
            using (var sr = new StreamReader(requestStream))
            {
                Headers["method"] = sr.ReadLine().Split(' ')[0].ToUpper();
                Method = Headers["method"];
            }
        }

        public bool IsValid { get; }
        public string Method { get; }
        public IUrl Url { get; } = new Url();
        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public string UserAgent { get; }
        public int HeaderCount { get; } = 0;
        public int ContentLength { get; }
        public string ContentType { get; }
        public Stream ContentStream { get; }
        public string ContentString { get; } = String.Empty;
        public byte[] ContentBytes { get; }
    }
}
