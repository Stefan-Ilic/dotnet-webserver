using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    [LoadPlugin]
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
            var resp = new Response();
            if (File.Exists(req.Url.Path))
            {
                resp.StatusCode = 200;
                resp.SetContent(File.ReadAllBytes(req.Url.Path));

            }
            else
            {
                resp.StatusCode = 404;
            }
            return resp;
        }
    }
}
