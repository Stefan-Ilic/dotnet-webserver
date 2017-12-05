﻿using System;
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
    public class Request : IRequest
    {
        private readonly Regex _requestPattern = new Regex(@"^(GET|POST|PUT|DELETE|get|post|put|delete)(\s+)(.*)(\s+)(HTTP\/)(1\.[0-9])$");
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

        public bool IsValid { get; } = false;
        public string Method { get; }
        public IUrl Url { get; } = new Url();
        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public string UserAgent { get; }
        public int HeaderCount { get; } = 0;
        public int ContentLength { get; }
        public string ContentType { get; }
        public Stream ContentStream { get; }
        public string ContentString { get; } = string.Empty;
        public byte[] ContentBytes { get; }
    }
}
