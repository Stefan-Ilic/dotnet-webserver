using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class ToLowerPlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            if (url == "/tolower")
            {
                return 1;
            }
            return Regex.Matches(url, "tolower").Count * 0.2f;
        }
        
        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The ToLower plugin is currently Handling the Request\n");
            var resp = new Response();
            var text = req.ContentString.TrimStart("text=".ToCharArray()).ToLower();
            var content = Resources.Pages.tolower.Replace("$$text$$", !string.IsNullOrWhiteSpace(text) ? text : "Bitte geben Sie einen Text ein");
            resp.SetContent(content);
            resp.StatusCode = 200;
            return resp;
        }
    }
}
