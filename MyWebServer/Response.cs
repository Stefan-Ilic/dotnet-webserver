using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    /// <summary>
    /// Represents a HTTP response
    /// </summary>
    public class Response : IResponse
    {
        private int _statusCode;
        private string _serverHeader = "BIF-SWE1-Server";
        private byte[] _content = new byte[0];

        /// <summary>
        /// Instances an empty reponse
        /// </summary>
        public Response()
        {

        }

        /// <summary>
        /// Adds or replaces a response header in the headers dictionary.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
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

        /// <summary>
        /// Sets a string content. The content will be encoded in UTF-8.
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(string content)
        {
            _content = Encoding.UTF8.GetBytes(content);
        }

        /// <summary>
        /// Sets a byte[] as content.
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(byte[] content)
        {
            _content = content;
        }

        /// <summary>
        /// Sets the stream as content.
        /// </summary>
        /// <param name="stream"></param>
        public void SetContent(Stream stream)
        {
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            _content = ms.ToArray();
        }

        /// <summary>
        /// Sends the response to the network stream.
        /// </summary>
        /// <param name="network"></param>
        public void Send(Stream network)
        {
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

        /// <summary>
        /// Returns a writable dictionary of the response headers. Never returns null.
        /// </summary>
        public IDictionary<string, string> Headers { get; } =
            new Dictionary<string, string> {{"Server", "BIF-SWE1-Server"}};

        /// <summary>
        /// Returns the content length or 0 if no content is set yet.
        /// </summary>
        public int ContentLength => _content.Length;

        /// <summary>
        /// Gets or sets the content type of the response.
        /// </summary>
        /// <exception cref="InvalidOperationException">A specialized implementation may throw a InvalidOperationException when the content type is set by the implementation.</exception>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the current status code. An Exceptions is thrown, if no status code was set.
        /// </summary>
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

        /// <summary>
        /// Returns the status code as string. (200 OK)
        /// </summary>
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

        /// <summary>
        /// Gets or sets the Server response header. Defaults to "BIF-SWE1-Server".
        /// </summary>
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
