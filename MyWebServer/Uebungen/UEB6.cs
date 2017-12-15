using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using MyWebServer;

namespace Uebungen
{
    public class UEB6 : IUEB6
    {
        public void HelloWorld()
        {
        }

        public IPluginManager GetPluginManager()
        {
            return new PluginManager();
        }

        public IRequest GetRequest(System.IO.Stream network)
        {
            return new Request(network);
        }

        public string GetNaviUrl()
        {
            return "/navi";
        }

        public IPlugin GetNavigationPlugin()
        {
            return new NaviPlugin();
        }

        public IPlugin GetTemperaturePlugin()
        {
            return new TempPlugin();
        }

        public string GetTemperatureRestUrl(DateTime from, DateTime until)
        {
            return from.ToString("yyyyMMdd") + "to" + until.ToString("yyyyMMdd");
        }

        public string GetTemperatureUrl(DateTime from, DateTime until)
        {
            return "/temp/" + from.ToString("yyyyMMdd") + "to" + until.ToString("yyyyMMdd");
        }

        public IPlugin GetToLowerPlugin()
        {
            return new ToLowerPlugin();
        }

        public string GetToLowerUrl()
        {
            return "/tolower";
        }
    }
}
