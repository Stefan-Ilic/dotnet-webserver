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
        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public int ContentLength { get; } = 0;
        public string ContentType { get; set; }
        public int StatusCode { get; set; }
        public string Status { get; }
        public string ServerHeader { get; set; } = "BIF-SWE1-Server";

        public void AddHeader(string header, string value)
        {
            
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
    }
}
