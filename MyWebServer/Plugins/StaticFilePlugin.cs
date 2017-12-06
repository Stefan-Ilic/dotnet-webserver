using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    [LoadPlugin]
    public class StaticFilePlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            if (req.Url.Segments.Contains("static"))
            {
                return 1f;
            }
            return Regex.Matches(req.Url.RawUrl, "static").Count * 0.01f;
        }

        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The StaticFile plugin is currently Handling the Request");
            var wdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var resp = new Response();
            var file = Path.Combine(wdir, req.Url.Path.TrimStart('/'));
            if (File.Exists(file))
            {
                resp.StatusCode = 200;
                resp.SetContent(File.ReadAllBytes(file));

            }
            else
            {
                resp.StatusCode = 404;
            }
            return resp;
        }
    }
}
