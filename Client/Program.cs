using System.Net.Sockets;
using Shared.Packats;
using SimpleMultiplayer.Packets.Handlers;

namespace SimpleMultiplayer
{
    class Client
    {
        private static PacketProcessor _packetProcessor = null;
        
        static void Main(string[] args)
        {
            // TODO: Move these to somewhere else
            _packetProcessor = new();
            //TODO: Can we figure out a better way of adding new handlers?
            _packetProcessor.AddHandler(new PositionPacketHandler());
            
            using (TcpClient client = new TcpClient())
            {
                client.Connect("localhost", 13000);

                using (NetworkStream stream = client.GetStream())
                {
                    Shared.Packets.Position position = new()
                    {
                        X = 6,
                        Y = 9,
                        ClientId = new Guid("c83b069a-be1b-4c58-815d-b90bef964307")
                    };
                    byte[] data = Shared.Serializer.ObjectToByteArray(position);

                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);
                    
                    Thread.Sleep(3000);
                    
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
                    }
                }
            }
        }
    }
}