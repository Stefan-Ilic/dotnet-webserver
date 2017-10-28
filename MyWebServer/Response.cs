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
            
        }

        public void SetContent(byte[] content)
        {
            
        }

        public void SetContent(Stream stream)
        {
            
        }

        public void Send(Stream network)
        {
            
        }

        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public int ContentLength { get; } = 0;
        public string ContentType { get; set; }
        public int StatusCode { get; set; }

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
                        return "chandler > ross pre monica but ross > chandler post monica";
                }
            }
        }

        public string ServerHeader { get; set; } = "BIF-SWE1-Server";
    }
}
