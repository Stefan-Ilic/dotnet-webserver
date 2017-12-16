using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using BIF.SWE1.Interfaces;
using MyWebServer.Helper;

namespace MyWebServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var thread = new Thread(EntryWriter.Write);
            thread.Start();

            var server = new WebServer();
            server.Listen();
        }
    }
}