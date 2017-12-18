using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    /// <summary>
    /// This plugin will return the start page
    /// </summary>
    [LoadPlugin]
    public class StartPagePlugin : IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            return url == "/" ? 1f : 0f;
        }

        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The StartPagePlugin is currently Handling the Request");
            var obj = new Response
            {
                StatusCode = 200
            };
            obj.SetContent(Resources.Pages.index);
            return obj;
        }
    }
}
