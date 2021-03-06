using System.Net;
using System.Net.Sockets;
using Shared.Packats;
using SimpleMultiplayer.Clients;
using SimpleMultiplayer.Packets.Handlers;

namespace SimpleMultiplayer;

class Server
{
    private static bool IsRunning { get; set; } = false;
        
    private static PacketProcessor _packetProcessor = null;
        
        public static void Start()
        {
            // TODO: Move these to somewhere else
            _packetProcessor = new();
            //TODO: Can we figure out a better way of adding new handlers?
            _packetProcessor.AddHandler(new PositionPacketHandler());
            
            Console.WriteLine("Starting server...");
            TcpListener listener = new TcpListener(IPAddress.Any, 13000);
            listener.Start();

            IsRunning = true;

            while (IsRunning)
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
            
            ClientHandler.AddClient(new ClientHandler.Client
            {
                Id = new Guid("c83b069a-be1b-4c58-815d-b90bef964307"),
                Position = new(),
                Username = "Test user"
            });

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

                    var msg = Shared.Serializer.ByteArrayToObject<Shared.Packets.IPacket>(receivedData);
                    _packetProcessor.ProcessMessage(msg);
                    
                    Thread.Sleep(500);
                    
                    Shared.Packets.Position position = new()
                    {
                        X = 0,
                        Y = 0,
                        ClientId = new Guid("c83b069a-be1b-4c58-815d-b90bef964307")
                    };
                    byte[] data = Shared.Serializer.ObjectToByteArray(position);

                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);
                }
            }
            
            stream.Dispose();
        }        
}