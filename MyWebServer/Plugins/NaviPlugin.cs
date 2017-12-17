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
    [LoadPlugin]
    public class NaviPlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            var url = req.Url.RawUrl.ToLower();
            if (url == "/navi")
            {
                return 1;
            }
            return Regex.Matches(url, "tolower").Count * 0.2f;
        }

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
