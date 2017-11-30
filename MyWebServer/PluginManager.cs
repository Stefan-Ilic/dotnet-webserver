using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class PluginManager : IPluginManager
    {
        private readonly List<IPlugin> _plugins = new List<IPlugin>();

        public PluginManager()
        {
            var wdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var lst = Directory.GetFiles(wdir)
                .Where(i => new[] { ".dll", ".exe" }.Contains(Path.GetExtension(i)))
                .SelectMany(i => Assembly.LoadFrom(i).GetTypes())
                .Where(myType => myType.IsClass
                                 && !myType.IsAbstract
                                 && myType.GetInterfaces().Any(i => i == typeof(IPlugin)));

            foreach (var type in lst)
            {
                _plugins.Add((IPlugin)Activator.CreateInstance(type));
            }
        }

        public void Add(IPlugin plugin)
        {
            if (!Plugins.Contains(plugin))
            {
                _plugins.Add(plugin);
            }
        }

        public void Add(string plugin)
        {
            {
                if (Type.GetType(plugin) != null)
                {
                    var addThis = (IPlugin)Activator.CreateInstance(Type.GetType(plugin, throwOnError: true));

                    if (_plugins.All(x => x != addThis))
                    {
                        _plugins.Add(addThis);
                    }
                }
                else
                {
                    throw new UnknownPluginTypeException();
                }
            }
        }

        public void Clear()
        {
            _plugins.Clear();
        }

        public IEnumerable<IPlugin> Plugins => _plugins;
    }
}
