using System;
using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Shared;
using SimpleMultiplayer.Handlers;

namespace SimpleMultiplayer
{
    class Server
    {
        private static MessageProcessor messageProcessor = null;
        
        static void Main(string[] args)
        {
            messageProcessor = new();
            messageProcessor.AddHandler(new TestMessageHandler());
            
            Console.WriteLine("Starting server...");
            TcpListener listener = new TcpListener(IPAddress.Any, 13000);
            listener.Start();

            while (true)
            {
                Console.WriteLine("Server listening on port {0}", listener.LocalEndpoint);
                Console.WriteLine("Server waiting for connection");
                
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Client connection established");

                if (client != null)
                {
                    Thread thread = new Thread(ProcessData);
                    thread.Start(client);
                }
            }
        }

        private static void ProcessData(object? obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            client.SendBufferSize = 4096;
            client.ReceiveBufferSize = 4096;

            while (true)
            {
                byte[] receivedData = new byte[4096];
                int packSize = 0;
                if (stream.DataAvailable)
                {
                    do
                    {
                        // Reading the data sent from the client
                        // assign it to received data byte array
                        packSize = stream.Read(receivedData, 0, receivedData.Length);
                    } while (stream.DataAvailable);
                }

                if (packSize > 0)
                {
                    // Reading of the data from the byte array
                    // Convert it correctly

                    var msg = Shared.Serializer.ByteArrayToObject<Shared.Messages.IMessage>(receivedData);
                    messageProcessor.ProcessMessage(msg);
                }
            }
            
            stream.Dispose();
        }
    }
}