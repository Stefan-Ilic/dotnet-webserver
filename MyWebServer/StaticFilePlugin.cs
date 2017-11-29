﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class StaticFilePlugin : IPlugin
    {
        public StaticFilePlugin()
        {
            
        }

        public float CanHandle(IRequest req)
        {
            return 0.1f;
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