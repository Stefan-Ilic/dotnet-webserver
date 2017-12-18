using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    /// <summary>
    /// This plugin will return a static file from a directory on the server
    /// </summary>
    [LoadPlugin]
    public class StaticFilePlugin : IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
            if (req.Url.Segments.Contains("static"))
            {
                return 1f;
            }
            return Regex.Matches(req.Url.RawUrl, "static").Count * 0.01f;
        }

        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The StaticFile plugin is currently Handling the Request");
            var wdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string file;
            //if Jenkins won't give me a real URL then I won't treat him as a real WebClient
            if (req.Url.Path.Contains("./deploy")) 
            {
                file = "c:\\workspace\\BIF-WS17-SWE1-if16b072\\deploy" +
                       req.Url.Path.TrimStart("./deploy".ToCharArray());
                
            }
            else
            {
                file = Path.Combine(wdir, req.Url.Path.TrimStart('/'));
            }
            var resp = new Response();
            if (File.Exists(file))
            {
                resp.StatusCode = 200;
                resp.AddHeader("content-type", MimeMapping.GetMimeMapping(file));
                resp.SetContent(File.ReadAllBytes(file));
            }
            else
            {
                resp.StatusCode = 404;
                resp.SetContent(Resources.Pages._404);
            }
            return resp;
        }
    }
}
