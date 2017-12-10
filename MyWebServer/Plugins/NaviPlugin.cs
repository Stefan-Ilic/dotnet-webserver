using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;
using MyWebServer.Helper;

namespace MyWebServer
{
    [LoadPlugin]
    public class NaviPlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            if (url == "/navi")
            {
                return 1;
            }
            return Regex.Matches(url, "tolower").Count * 0.2f;
        }

        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The Navi Plugin is currently handling the request");
            var sax = new SaxParser();
            sax.Update();




            var resp = new Response();
            resp.StatusCode = 404;
            return resp;
        }
    }
}
