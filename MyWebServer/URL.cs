using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class Url : IUrl
    {
        private string rawUrl;
        private string path;
        private Dictionary<string, string> parameter;
        private int parameterCount;
        private string[] segments;
        private string fileName;
        private string extension;
        private string fragment;

        public Url()
        {
            rawUrl = string.Empty;
            path = string.Empty;
            parameter = new Dictionary<string, string>();
            parameterCount = 0;
            segments = new string[0];
            fileName = string.Empty;
            extension = string.Empty;
            fragment = string.Empty;
        }

        public Url(string raw)
        {
            rawUrl = raw;
            parameterCount = Utility.CountParams(raw);
        }

        public IDictionary<string, string> Parameter
        {
            get { return parameter; }
        }

        public int ParameterCount
        {
            get { return parameterCount; }
        }

        public string Path
        {
            get { return path; }
        }

        public string RawUrl
        {
            get { return rawUrl; }
        }

        public string Extension
        {
            get { return extension; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public string Fragment
        {
            get { return fragment; }
        }

        public string[] Segments
        {
            get { return segments; }
        }
    }
}
