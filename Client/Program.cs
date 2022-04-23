﻿using System.Net.Sockets;
using Shared;

namespace SimpleMultiplayer
{
    class Client
    {
        static void Main(string[] args)
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect("localhost", 13000);

                using (NetworkStream stream = client.GetStream())
                {
                    Packets.TestPacket test = new()
                    {
                        Message = "Hello from the client"
                    };
                    byte[] data = Shared.Serializer.ObjectToByteArray(test);

                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);
                }
            }
        }
    }
}