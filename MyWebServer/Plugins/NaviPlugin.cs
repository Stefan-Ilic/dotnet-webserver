using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;
using MyWebServer.Helper;

namespace MyWebServer
{
    /// <summary>
    /// This plugin can will return cities where a certain street can be found
    /// </summary>
    [LoadPlugin]
    public class NaviPlugin : IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            if (url == "/navi")
            {
                return 1;
            }
            return Regex.Matches(url, "tolower").Count * 0.2f;
        }

        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The Navi Plugin is currently handling the request");
            var street = req.ContentString.TrimStart("street=".ToCharArray()).ToLower();
            var resp = new Response { StatusCode = 200 };
            var content = "";
            var message = "";
            if (SaxParser.IsLocked())
            {
                message = "Die Karte wird gerade neu aufbereitet";
            }
            else if (string.IsNullOrEmpty(street))
            {
                message = "Bitte geben Sie eine Anfrage ein";
            }
            else if (street.Contains("update=straßenkarte neu aufbereiten"))
            {
                message = "Die interne Karte wurde aktualisiert";
                SaxParser.Update();
            }
            else
            {
                var cities = SaxParser.GetCities(street);
                message = cities.Count + " Orte gefunden";
                content = cities.Aggregate(content, (current, city) => current + "<tr><td>" + city + "</td></tr>");
            }
            resp.SetContent(Resources.Pages.navi.Replace("$$message$$", message).Replace("$$cities$$", content));
            return resp;
        }
    }
}
