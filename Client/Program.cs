using System.Net.Sockets;
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
                    // Send test package
                    Packets.TestPackage test = new Packets.TestPackage
                    {
                        Age = 29,
                        Name = "Mikko"
                    };
                    byte[] data = Shared.Serializer.ObjectToByteArray(test);

                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);

                }
            }
        }
    }
}