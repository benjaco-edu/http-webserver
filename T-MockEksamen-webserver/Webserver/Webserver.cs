using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace T_MockEksamen_webserver.Webserver
{
    public class Webserver
    {
        private readonly TcpListener _serverSocket;


        /// <summary>
        /// Starter en http server
        /// </summary>
        /// <param name="port">Port serveren skal køre på, default 80</param>
        /// <param name="ip">Locationen serveren skal køre på, default 127.0.0.1</param>
        public Webserver(int port = 80, string ip = "127.0.0.1")
        {
            Console.WriteLine("Listen on port "+port+", localhost");
            _serverSocket = new TcpListener(IPAddress.Parse(ip), port);
            _serverSocket.Start();

            Thread thread = new Thread(ListenForClientConnections);
            thread.Start();
        }
        private void ListenForClientConnections()
        {
            while (true)
            {
                TcpClient connectionSocket = _serverSocket.AcceptTcpClient();

                Console.WriteLine("Client connected");
                
                new ClientHandler(connectionSocket);
            }
        }
    }
}