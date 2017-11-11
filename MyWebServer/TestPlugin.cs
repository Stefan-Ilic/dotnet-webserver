using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class TestPlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower(); // is this cheating?
            if (url == "/")
            {
                return 1;
            }
            return Regex.Matches(url, "test").Count * 0.01f;
        }

        public IResponse Handle(IRequest req)
        {
            var obj = new Response
            {
                StatusCode = 200
            };
            obj.SetContent("I wear a jacket indoors because man's not hot");
            return obj;
        }
    }
}
