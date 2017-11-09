using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    class PluginManager : IPluginManager
    {

        public void Add(IPlugin plugin)
        {
            
        }

        public void Add(string plugin)
        {
            
        }

        public void Clear()
        {
            
        }

        public IEnumerable<IPlugin> Plugins { get; } = new List<IPlugin>();
    }
}
