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
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 8080.
                const int port = 8080;
                var localAddr = IPAddress.Parse("127.0.0.1");

                // Instance TcpListener
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    var client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    // Get a stream object for reading and writing
                    var network = client.GetStream();

                    // Instance a new PluginManager
                    var pluginManager = new PluginManager();

                    // Obtain Request from network stream
                    var req = new Request(network);

                    // Select Plugin and send back to network stream
                    pluginManager.GetPlugin(req).Handle(req).Send(network);

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}