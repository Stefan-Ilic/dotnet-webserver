using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    /// <summary>
    /// This plugin will return 
    /// </summary>
    [LoadPlugin]
    public class ToLowerPlugin : IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            if (url == "/tolower")
            {
                return 1;
            }
            return Regex.Matches(url, "tolower").Count * 0.2f;
        }

        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The ToLower plugin is currently Handling the Request");
            var resp = new Response();
            var text = req.ContentString.TrimStart("text=".ToCharArray()).ToLower();
            var content = Resources.Pages.tolower.Replace("$$text$$", !string.IsNullOrWhiteSpace(text) ? text : "Bitte geben Sie einen Text ein");
            resp.SetContent(content);
            resp.StatusCode = 200;
            return resp;
        }
    }
}
