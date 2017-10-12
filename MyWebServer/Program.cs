using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var url = new Url("www.hallo.com?a=b&c=d");


            Console.WriteLine("\nHit the any key to exit...");
            Console.ReadKey();
        }
    }
}
