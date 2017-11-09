using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    class Plugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            return 0;
        }

        public IResponse Handle(IRequest req)
        {
            return new Response();
        }
    }
}
