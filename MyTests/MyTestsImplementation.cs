using BIF.SWE1.Interfaces;
using MyWebServer;

namespace MyTests
{
    public class MyTestsImplementation : IMyTests
    {
        public void HelloWorld()
        {
            // I'm fine
        }

        public IRequest GetRequest(System.IO.Stream network)
        {
            return new Request(network);
        }

        public IPlugin GetToLowerPlugin()
        {
            return new ToLowerPlugin();
        }

        public string GetToLowerUrl()
        {
            return "/tolower";
        }

        public IPlugin GetStaticFilePlugin()
        {
            return new StaticFilePlugin();
        }
    }
}
