using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    /// <summary>
    /// Represents an Url
    /// </summary>
    public class Url : IUrl
    {
        /// <summary>
        /// Instances an empty Url object
        /// </summary>
        public Url()
        {

        }

        /// <summary>
        /// Instances a URL object from a string
        /// </summary>
        /// <param name="raw"></param>
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

        /// <summary>
        /// Returns a dictionary with the parameter of the url. Never returns null.
        /// </summary>
        public IDictionary<string, string> Parameter { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Returns the number of parameter of the url. Returns 0 if there are no parameter.
        /// </summary>
        public int ParameterCount { get; } = 0;

        /// <summary>
        /// Returns the path of the url, without parameter.
        /// </summary>
        public string Path { get; } = "";

        /// <summary>
        /// Returns the raw url.
        /// </summary>
        public string RawUrl { get; } = "";

        /// <summary>
        /// Returns the extension of the url filename, including the leading dot. If the url contains no filename, a empty string is returned. Never returns null.
        /// </summary>
        public string Extension { get; } = "";

        /// <summary>
        /// Returns the filename (with extension) of the url path. If the url contains no filename, a empty string is returned. Never returns null. A filename is present in the url, if the last segment contains a name with at least one dot.
        /// </summary>
        public string FileName { get; } = "";

        /// <summary>
        /// Returns the url fragment. A fragment is the part after a '#' char at the end of the url. If the url contains no fragment, a empty string is returned. Never returns null.
        /// </summary>
        public string Fragment { get; } = "";

        /// <summary>
        /// Returns the segments of the url path. A segment is divided by '/' chars. Never returns null.
        /// </summary>
        public string[] Segments { get; } = new string[0];
    }
}
