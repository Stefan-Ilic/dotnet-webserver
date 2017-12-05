using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    [LoadPlugin]
    public class StartPagePlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            return url == "/" ? 1f : 0f;
        }

        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The StartPagePlugin is currently Handling the Request\n");
            var obj = new Response
            {
                StatusCode = 200
            };
            obj.SetContent(Resources.Pages.index);
            return obj;
        }
    }
}
