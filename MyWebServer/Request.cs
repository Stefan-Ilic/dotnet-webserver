using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class Request : IRequest
    {
        private readonly Regex _requestPattern = new Regex(@"^(GET|POST|PUT|DELETE|get|post|put|delete)(\s+)\/(.*)(\s+)(HTTP\/)([0-9].[0-9]).*");

        public Request(Stream requestStream)
        {
            using (var sr = new StreamReader(requestStream))
            {
                IsValid = _requestPattern.IsMatch(sr.ReadToEnd());
                if (IsValid)
                {
                    requestStream.Seek(0, SeekOrigin.Begin);
                    var requestLine = sr.ReadLine().Split(' ');
                    Method = requestLine[0].ToUpper();
                    Url = new Url(requestLine[1]);

                    while (sr.Peek() >= 0)
                    {
                        var tempSplit = sr.ReadLine().Split(' ');
                        var tempKey = tempSplit.Length == 2 ? tempSplit[0].TrimEnd(':').ToLower() : "";
                        var tempVal = tempSplit.Length == 2 ? tempSplit[1].TrimEnd('\r', '\n') : "";
                        if (tempKey != String.Empty && tempVal != String.Empty)
                        {
                            Headers.Add(tempKey, tempVal);
                        }
                    }
                }
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
