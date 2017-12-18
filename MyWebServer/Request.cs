using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;
using System.Web;

namespace MyWebServer
{
    /// <summary>
    /// Represenst a HTTP request
    /// </summary>
    public class Request : IRequest
    {
        private readonly Regex _requestPattern = new Regex(@"^(GET|POST|PUT|DELETE|get|post|put|delete)(\s+)(.*)(\s+)(HTTP\/)(1\.[0-9])$");

        /// <summary>
        /// Builds a HTTP request from a (network) stream
        /// </summary>
        /// <param name="requestStream"></param>
        public Request(Stream requestStream)
        {
            var sr = new StreamReader(requestStream);
            var firstline = sr.ReadLine();
            if (firstline == null)
            {
                return;
            }
            IsValid = _requestPattern.IsMatch(firstline);
            if (!IsValid)
            {
                return;
            }
            var requestLine = firstline.Split(' ');
            Method = requestLine[0].ToUpper();
            Url = new Url(requestLine[1]);

            while (sr.Peek() >= 0)
            {
                var rl = sr.ReadLine();
                var tempSplit = rl.Split(':');
                if (tempSplit.Length >= 2)
                {
                    var tempKey = tempSplit[0].ToLower();
                    var tempVal = tempSplit[1].TrimStart(' ').TrimEnd('\r', '\n');
                    if (tempKey != string.Empty && tempVal != string.Empty)
                    {
                        Headers.Add(tempKey, tempVal);
                    }
                }
                else
                {
                    if (Method != "POST" || !Headers.ContainsKey("content-length")) continue;
                    var buffer = new char[int.Parse(Headers["content-length"])];
                    sr.Read(buffer, 0, buffer.Length);
                    ContentString = WebUtility.UrlDecode( new string(buffer));

                    if (string.IsNullOrEmpty(ContentString)) continue;
                    ContentBytes = Encoding.UTF8.GetBytes(ContentString);
                    ContentStream = new MemoryStream(ContentBytes);
                }
            }
            HeaderCount = Headers.Count();
            if (Headers.ContainsKey("user-agent"))
            {
                UserAgent = Headers["user-agent"];
            }
            if (Headers.ContainsKey("content-type"))
            {
                ContentType = Headers["content-type"];
            }
            if (Headers.ContainsKey("content-length"))
            {
                ContentLength = int.Parse(Headers["content-length"]);
            }
        }

        /// <summary>
        /// Returns true if the request is valid. A request is valid, if method and url could be parsed. A header is not necessary.
        /// </summary>
        public bool IsValid { get; } = false;

        /// <summary>
        /// Returns the request method in UPPERCASE. get -> GET.
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// Returns a URL object of the request. Never returns null.
        /// </summary>
        public IUrl Url { get; } = new Url();

        /// <summary>
        /// Returns the request header. Never returns null. All keys must be lower case.
        /// </summary>
        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Returns the user agent from the request header
        /// </summary>
        public string UserAgent { get; }

        /// <summary>
        /// Returns the number of header or 0, if no header where found.
        /// </summary>
        public int HeaderCount { get; } = 0;

        /// <summary>
        /// Returns the parsed content length request header.
        /// </summary>
        public int ContentLength { get; }

        /// <summary>
        /// Returns the parsed content type request header. Never returns null.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Returns the request content (body) stream or null if there is no content stream.
        /// </summary>
        public Stream ContentStream { get; }

        /// <summary>
        /// Returns the request content (body) as string or null if there is no content.
        /// </summary>
        public string ContentString { get; } = string.Empty;

        /// <summary>
        /// Returns the request content (body) as byte[] or null if there is no content.
        /// </summary>
        public byte[] ContentBytes { get; }
    }
}
