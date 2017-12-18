using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    /// <summary>
    /// This plugin is used for testing plugins
    /// </summary>
    [LoadPlugin]
    public class TestPlugin : IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            if (url == "/")
            {
                return 0.9f;
            }
            return Regex.Matches(url, "test").Count * 0.01f;
        }

        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The TestPlugin is currently Handling the Request\n");
            var obj = new Response
            {
                StatusCode = 200
            };
            obj.SetContent("Testitest");
            return obj;
        }
    }
}
