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


            var testurl = new Url();

           Console.WriteLine("Number or params: {0}", testurl.ParameterCount);


            Console.WriteLine("\nHit the any key to exit...");
            Console.ReadKey();
        }
    }
}
