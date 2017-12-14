using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class Url : IUrl
    {
        public Url()
        {

        }

        public Url(string raw)
        {
            if (raw == null)
            {
                return;
            }

            RawUrl = raw;
            var splitUrl = raw.Split('?');
            var allParams = splitUrl.Length != 2 ? "" : splitUrl[1];
            var pathSplitFragment = splitUrl[0].Split('#');
            Path = pathSplitFragment[0];
            Fragment = pathSplitFragment.Length == 2 ? pathSplitFragment[1] : "";
            var allPairs = allParams.Split('&').Select(x => x.Split('='));
            if (allParams != "")
            {
                foreach (var item in allPairs)
                {
                    Parameter.Add(item[0], item[1]);
                }
            }
            ParameterCount = Parameter.Count();
            Segments = Path.Split('/').Skip(1).ToArray();
            if (Segments.Length != 0 && Segments[Segments.Length - 1].Contains('.'))
            {
                Extension = Segments[Segments.Length - 1].Split('.')[1].ToLower();
            }
        }

        public IDictionary<string, string> Parameter { get; } = new Dictionary<string, string>();
        public int ParameterCount { get; } = 0;
        public string Path { get; } = "";
        public string RawUrl { get; } = "";
        public string Extension { get; } = "";
        public string FileName { get; } = "";
        public string Fragment { get; } = "";
        public string[] Segments { get; } = new string[0];
    }
}
