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


            //StreamReader s = new StreamReader(Stream network);
            //string blah = s.ReadToEnd();

            //var stream2 = new MemoryStream(new byte[1234]);

            //StreamReader sr = new StreamReader(stream2);
            //string blah = sr.ReadToEnd();
            //Console.WriteLine(blah);

            //var test = new Request(stream2);

            //var test2 = new Response();

            //if (test2 is IResponse)
            //{
            //    Console.WriteLine("Yeah it implements the interface\n");   
            //}



            Console.WriteLine("\nHit the any key to exit...");
            Console.ReadKey();
        }
    }
}
