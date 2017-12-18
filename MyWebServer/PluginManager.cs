using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    /// <summary>
    /// Manages plugins
    /// </summary>
    public class PluginManager : IPluginManager
    {
        private readonly List<IPlugin> _plugins = new List<IPlugin>();

        /// <summary>
        /// Creates a new instance of the plugin manager and fills its Plugins list with all plugins in the directory
        /// </summary>
        public PluginManager()
        {
            var wdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var lst = Directory.GetFiles(wdir)
                .Where(i => new[] { ".dll", ".exe" }.Contains(Path.GetExtension(i)))
                .SelectMany(i => Assembly.LoadFrom(i).GetTypes())
                .Where(myType => myType.IsClass
                                 && !myType.IsAbstract
                                 && myType.GetCustomAttributes().Any(i => i.GetType() == typeof(LoadPluginAttribute))
                                 && myType.GetInterfaces().Any(i => i == typeof(IPlugin)));

            foreach (var type in lst)
            {
                _plugins.Add((IPlugin)Activator.CreateInstance(type));
            }
        }

        /// <summary>
        /// Adds a new plugin. If the plugin was already added, nothing will happen.
        /// </summary>
        /// <param name="plugin"></param>
        public void Add(IPlugin plugin)
        {
            if (!Plugins.Contains(plugin))
            {
                _plugins.Add(plugin);
            }
        }

        /// <summary>
        /// Adds a new plugin by type name. If the plugin was already added, nothing will happen.
        /// Throws an exeption, when the type cannot be resoled or the type does not implement IPlugin.
        /// </summary>
        /// <param name="plugin"></param>
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

        /// <summary>
        /// Clears all plugins
        /// </summary>
        public void Clear()
        {
            _plugins.Clear();
        }

        /// <summary>
        /// Returns the plugin with the highest score according to the accepted request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public IPlugin GetPlugin(Request req)
        {
            return _plugins.Select(i => new { Value = i.CanHandle(req), Plugin = i }).OrderBy(i => i.Value).Last().Plugin;
        }

        /// <summary>
        /// Returns a list of all plugins. Never returns null.
        /// </summary>
        public IEnumerable<IPlugin> Plugins => _plugins;
    }
}
