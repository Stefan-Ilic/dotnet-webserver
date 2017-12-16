﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    [LoadPlugin]
    public class TempPlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            if (req.Url.Segments.Contains("temp") || req.Url.Segments.Contains("GetTemperature"))
            {
                return 1f;
            }
            return Regex.Matches(req.Url.RawUrl, "temp").Count * 0.02f;
        }

        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The Temp Plugin is currently handling the request");

            var database = new Database();
            var temps = new List<float>();
            var dateTimes = new List<DateTime>();
            var previous = "";
            var next = "";
            var content = "";
            var resp = new Response {StatusCode = 200};

            if (req.Url.Segments.Contains("GetTemperature"))
            {
                return HandleRest(req);
            }

            var page = req.Url.Parameter.ContainsKey("page") ? int.Parse(req.Url.Parameter["page"]) : 1;
            page = page < 1 ? 1 : page;

            if (req.Url.Parameter.ContainsKey("search") && !string.IsNullOrWhiteSpace(req.Url.Parameter["search"]))
            {
                var dateTime = Convert.ToDateTime(WebUtility.UrlDecode(req.Url.Parameter["search"])); //TODO error handling here
                temps = database.GetTemps(page, dateTime);
                dateTimes = database.GetDateTimes(page, dateTime);
                previous = $"/temp?search={dateTime}&page=" + (page == 1 ? 1 : page - 1);
                next = $"/temp?search={dateTime}&page=" + (page + 1);
            }
            else
            {
                temps = database.GetTemps(page);
                dateTimes = database.GetDateTimes(page);
                previous = "/temp?page=" + (page == 1 ? 1 : page - 1);
                next = "/temp?page=" + (page + 1);
            }

            content = BuildContent(temps, dateTimes);

            resp.SetContent(Resources.Pages.temp.Replace("$$data$$", content).Replace("$$previous$$", previous).Replace("$$next$$", next));
            resp.ContentType = "text/html";
            return resp;
        }

        private static IResponse HandleRest(IRequest req)
        {
            var day = req.Url.Segments[req.Url.Segments.Length - 1];
            var month = req.Url.Segments[req.Url.Segments.Length - 2];
            var year = req.Url.Segments[req.Url.Segments.Length - 3];

            var database = new Database();

            var dateToCheck = Convert.ToDateTime($"{day}/{month}/{year}");
            var temps = database.GetTemps(dateToCheck);
            var dateTimes = database.GetDateTimes(dateToCheck);

            var xml = BuildXml(temps, dateTimes);

            var resp = new Response { StatusCode = 200 };
            resp.SetContent(xml);
            resp.AddHeader("content-type", "text/xml");
            resp.ContentType = "text/xml";
            return resp;
        }

        private static string BuildXml(IReadOnlyList<float> temps, IReadOnlyList<DateTime> dateTimes)
        {
            var xml = "<Entries>\n";
            for (var i = 0; i < temps.Count; i++)
            {
                xml +=
                    "<Entry>\n" +
                    "<Date>\n" +
                    $"{dateTimes[i].Year}" +
                    $"/{dateTimes[i].Month.ToString().PadLeft(2, '0')}/" +
                    $"{dateTimes[i].Day.ToString().PadLeft(2, '0')}\n" +
                    "</Date>\n" +
                    "<Time>\n" +
                    $"{dateTimes[i].TimeOfDay}\n" +
                    "</Time>\n" +
                    "<Temperature>\n" +
                    $"{temps[i]}\n" +
                    "</Temperature>\n" +
                    "</Entry>\n";
            }
            xml += "</Entries>";
            return xml;
        }

        private static string BuildContent(IReadOnlyList<float> temps, IReadOnlyList<DateTime> dateTimes)
        {
            var content = "";

            for (var i = 0; i < temps.Count; i++)
            {
                content +=
                    "<tr>" +
                    "<td>" +
                    $"{temps[i]}°C" +
                    "</td>" +
                    "<td>" +
                    $"{dateTimes[i].Day.ToString().PadLeft(2, '0')}" +
                    $".{dateTimes[i].Month.ToString().PadLeft(2, '0')}." +
                    $"{dateTimes[i].Year}" +
                    "</td>" +
                    "<td>" +
                    $"{dateTimes[i].TimeOfDay}" +
                    "</td>" +
                    "</tr>";
            }
            return content;
        }
    }
}
