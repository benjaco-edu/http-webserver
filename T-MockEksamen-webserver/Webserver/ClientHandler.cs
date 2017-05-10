using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace T_MockEksamen_webserver.Webserver
{
    internal class ClientHandler
    {
        private readonly StreamReader _sr;
        private readonly StreamWriter streamWriter;

        private readonly TcpClient _connectionSocket;

        private readonly Task ListenForRequest;
        private readonly Task ListenForDisconnections;

        private bool _connected = true;

        public ClientHandler(TcpClient connectionSocket)
        {
            _connectionSocket = connectionSocket;

            Stream ns = connectionSocket.GetStream();
            _sr = new StreamReader(ns);
            streamWriter = new StreamWriter(ns) { AutoFlush = true };
            // enable automatic flushing

            ListenForRequest = new Task(ListenForRequests);
            ListenForRequest.Start();


            ListenForDisconnections = new Task(ListenForDisconnect);
            ListenForDisconnections.Start();
        }




        private void ListenForRequests()
        {
            List<string> headerBuffer = new List<string>();

            Console.WriteLine("Listen for req");

            try
            {
                while (_connected)
                {

                    string message = _sr.ReadLine();

                    if (message == "")
                    {
                        Execute(headerBuffer);
                        headerBuffer.Clear();
                    }
                    else
                    {
                        headerBuffer.Add(message);
                    }


                }
            }
            catch (SocketException e)
            {
                // clienten lukkede
            }
            catch (IOException e)
            {
                // clienten lukkede
            }
            catch (Exception e) { }
        }

        private void Execute(List<string> headerBuffer)
        {
            try
            {
                var firstLine = headerBuffer[0].Split(' ');
                var url = firstLine[1];

                if (url == "/")
                {
                    url = "/index.html";
                }

                switch (firstLine[0])
                {
                    case "GET":
                        Methodes.Get.Execute(headerBuffer, url, streamWriter);
                        break;
                }
            }
            catch (Exception)
            { }

        }

        private async void ListenForDisconnect()
        {
            Console.WriteLine("Listen for disc");

            while (true)
            {
                await Task.Delay(1000);

                if (_connectionSocket.Connected) continue;

                _connected = false;
                _sr.Close();
                streamWriter.Close();
                _connectionSocket.Close();

                ListenForRequest.Dispose();
                ListenForDisconnections.Dispose();
                break;
            }
        }
    }
}