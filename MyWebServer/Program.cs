using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var server = new WebServer();
            server.Listen();
        }
    }
}