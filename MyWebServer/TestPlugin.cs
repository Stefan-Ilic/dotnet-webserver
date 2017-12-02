using System;
using System.Collections.Generic;
using System.IO;
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
            var url = req.Url.RawUrl.ToLower();
            if (url == "/")
            {
                return 1;
            }
            return Regex.Matches(url, "test").Count * 0.01f;
        }

        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The TestPlugin is currently Handling the Request\n");
            var obj = new Response
            {
                StatusCode = 200
            };
            const string pathToHtml = @"C:\projects\SWE1\SWE1-CS\MyWebSite\index.html";
            obj.SetContent(File.ReadAllText(pathToHtml));
            return obj;
        }
    }
}
