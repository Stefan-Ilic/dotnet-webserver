using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var wdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            const string jenkins = "c:\\workspace\\BIF - WS17 - SWE1 - if16b072\\deploy";
            if (wdir != jenkins)
            {
                var database = new Database();
                database.AddEntry(1f, DateTime.Now);
            }
            var server = new WebServer();
            server.Listen();
        }
    }
}