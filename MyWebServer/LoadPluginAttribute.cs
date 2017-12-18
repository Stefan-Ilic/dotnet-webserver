using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer
{
    /// <summary>
    /// This attribute is used to mark plugins that shall be loaded into the plugin manager
    /// </summary>
    public class LoadPluginAttribute : Attribute
    {
    }
}
