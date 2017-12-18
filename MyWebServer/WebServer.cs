using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebServer
{
    /// <summary>
    /// Does everything a webserver does
    /// </summary>
    public class WebServer
    {
        /// <summary>
        /// Instances the webserver
        /// </summary>
        public WebServer()
        {
            Server = new TcpListener(Ip, Port);
            Server.Start();
            IsRunning = true;
        }

        /// <summary>
        /// Starts an infinite loop, accepts WebClients and calls HandleClient on a new thread
        /// </summary>
        public void Listen()
        {
            while (IsRunning)
            {
                var client = Server.AcceptTcpClient();
                var thread = new Thread(() => HandleClient(ref client));
                thread.Start();
            }
        }

        private static void HandleClient(ref TcpClient client)
        {
            var network = client.GetStream();
            var pluginManager = new PluginManager();
            var req = new Request(network);
            if (req.IsValid && req.Url.RawUrl != "/favicon.ico")
            {
                pluginManager.GetPlugin(req).Handle(req).Send(network);
            }
            client.Close();
        }

        /// <summary>
        /// An Instance of TcpListener that represents the Server
        /// </summary>
        public TcpListener Server { get; set; }

        /// <summary>
        /// A boolean flag indicating if the server is running
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// The port where the server is operating. By default, it's 8080
        /// </summary>
        public int Port { get; set; } = 8080;

        /// <summary>
        /// The IP address of the server. By default, it's localhost
        /// </summary>
        public IPAddress Ip { get; set; } = IPAddress.Parse("127.0.0.1");
    }
}
