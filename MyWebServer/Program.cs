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

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("GET / HTTP/1.1\r\nHost: localhost\r\nConnection: keep-alive\r\nAccept: text/html,application/xhtml+xml\r\nUser-Agent: Unit-Test-Agent/1.0 (The OS)\r\nAccept-Encoding: gzip,deflate,sdch\r\nAccept-Language: de-AT,de;q=0.8,en-US;q=0.6,en;q=0.4\r\n\r\n");
            writer.Flush();
            stream.Position = 0;

            var test = new Request(stream);

            var stream2 = new MemoryStream();
            var writer2 = new StreamWriter(stream);
            writer.Write("Connection: keep-alive\r\nAccept: text/html,application/xhtml+xml\r\nUser-Agent: Unit-Test-Agent/1.0 (The OS)\r\nAccept-Encoding: gzip,deflate,sdch\r\nAccept-Language: de-AT,de;q=0.8,en-US;q=0.6,en;q=0.4\r\n\r\n");
            writer.Flush();
            stream.Position = 0;

            var test2 = new Request(stream);



            Console.WriteLine("\nHit the any key to exit...");
            Console.ReadKey();
        }
    }
}
