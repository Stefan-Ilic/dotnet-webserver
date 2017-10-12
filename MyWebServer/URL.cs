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
            /*rawUrl = "";
            path = "";
            parameter = new Dictionary<string, string>();
            parameterCount = 0;
            segments = new string[0];
            fileName = "";
            extension = "";
            fragment = "";
            return;*/
        }

        public Url(string raw)
        {
            rawUrl = raw;
            parameterCount = Utility.CountParams(raw);
            string[] splitUrl = raw.Split('?');
            string allParams;
            //splitUrl.Length != 2 ? allParams = "" : allParams = splitUrl[1];
            if (splitUrl.Length != 2)
            {
                allParams = "";
            }
            else
            {
                allParams = splitUrl[1];
            }
            path = splitUrl[0];
            var allPairs = allParams.Split('&').Select(x => x.Split('='));
            foreach (var item in allPairs)
            {
                parameter.Add(item[0], item[1]);
            }
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
