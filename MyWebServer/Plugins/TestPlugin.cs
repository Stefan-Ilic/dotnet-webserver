﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    [LoadPlugin]
    public class TestPlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            if (url == "/")
            {
                return 0.9f;
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
            obj.SetContent("Testitest");
            return obj;
        }
    }
}