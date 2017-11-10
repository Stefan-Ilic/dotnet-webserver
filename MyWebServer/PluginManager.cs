using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    class PluginManager : IPluginManager
    {
        private List<IPlugin> _plugins = new List<IPlugin>();

        public void Add(IPlugin plugin)
        {
            if (!Plugins.Contains(plugin))
            {
                _plugins.Add(plugin);
            }
        }

        public void Add(string plugin)
        {
        }

        public void Clear()
        {
            
        }

        public IEnumerable<IPlugin> Plugins => _plugins;
    }
}
