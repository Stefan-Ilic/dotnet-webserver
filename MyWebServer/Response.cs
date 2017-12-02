using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class Response : IResponse
    {
        private int _statusCode;
        private string _serverHeader = "BIF-SWE1-Server";
        private byte[] _content = new byte[0];

        public Response()
        {

        }

        public void AddHeader(string header, string value)
        {
            if (Headers.ContainsKey(header))
            {
                Headers[header] = value;
            }
            else
            {
                Headers.Add(header, value);
            }
        }

        public void SetContent(string content)
        {
            _content = Encoding.UTF8.GetBytes(content);
        }

        public void SetContent(byte[] content)
        {
            _content = content;
        }

        public void SetContent(Stream stream)
        {
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            _content = ms.ToArray();
        }

        public void Send(Stream network)
        {
            Console.WriteLine("SENDING");
            var writer = new BinaryWriter(network);
            writer.Write(Encoding.ASCII.GetBytes("HTTP/1.1 " + Status + "\r\n"));

            foreach (var entry in Headers)
            {
                writer.Write(Encoding.ASCII.GetBytes(entry.Key + ": " + entry.Value + "\r\n"));
            }
            writer.Write(Encoding.ASCII.GetBytes("\r\n"));

            if (ContentLength == 0 && !string.IsNullOrEmpty(ContentType))
            {
                throw new ContentNotSetException();
            }

            writer.Write(_content);

        }

        public IDictionary<string, string> Headers { get; } =
            new Dictionary<string, string> {{"Server", "BIF-SWE1-Server"}};
        public int ContentLength => _content.Length;
        public string ContentType { get; set; }

        public int StatusCode
        {
            get
            {
                if (_statusCode == 0)
                {
                    throw new StatusCodeNotSetException();
                }
                return _statusCode;
            }
            set => _statusCode = value;
        }

        public string Status
        {
            get
            {
                switch (StatusCode)
                {
                    case 200:
                        return "200 OK";
                    case 404:
                        return "404 Not Found";
                    case 500:
                        return "500 Internal Server Error";
                    default:
                        return "2 + 2 is 4 minus 1 that's 3 QUICK MAFS";
                }
            }
        }

        public string ServerHeader
        {
            get => _serverHeader;

            set
            {
                _serverHeader = value;
                AddHeader("Server", _serverHeader);
            }
        }
    }
}
