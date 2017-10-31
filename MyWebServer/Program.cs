using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var obj = new Response();
            obj.StatusCode = 200;
            Console.WriteLine(obj.Status);

            Console.WriteLine("\nHit the any key to exit...");
            Console.ReadKey();
        }
    }
}
