using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    [LoadPlugin]
    public class TempPlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            if (req.Url.Segments.Contains("temp"))
            {
                return 1f;
            }
            return Regex.Matches(req.Url.RawUrl, "temp").Count * 0.02f;
        }

        public IResponse Handle(IRequest req)
        {
            var resp = new Response();
            if (!File.Exists(req.Url.Path))
            {
                resp.StatusCode = 404;
            }
            return resp;
        }
    }
}
