using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (req.Url.Segments.Contains("temp"))
            {
                return 1f;
            }
            return Regex.Matches(req.Url.RawUrl, "temp").Count * 0.02f;
        }

        public IResponse Handle(IRequest req)
        {
            Console.WriteLine("The Temp Plugin is currently handling the request");
            var database = new Database();
            var temps = database.GetAllTemps();
            var dateTimes = database.GetAllDateTimes();
            var content = "";

            for (var i = 0; i < temps.Count; i++)
            {
                content += 
                    "<tr>"+
                        "<td>"+
                            $"{temps[i]}°C" +
                        "</td>" +
                        "<td>" +
                            $"{dateTimes[i].Day.ToString().PadLeft(2, '0')}" + 
                            $".{dateTimes[i].Month.ToString().PadLeft(2, '0')}." + 
                            $"{dateTimes[i].Year}" +
                        "</td>" +
                        "<td>"+ 
                            $"{dateTimes[i].TimeOfDay}" +
                        "</td>" +
                    "</tr>";
            }

            var resp = new Response();
            resp.SetContent(Resources.Pages.temp.Replace("$$data$$", content));
            resp.StatusCode = 200;
            return resp;
        }
    }
}
