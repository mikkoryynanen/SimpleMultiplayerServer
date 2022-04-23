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
                    Messages.TestMessage test = new()
                    {
                        Message = "Hello from the client"
                    };
                    byte[] data = Shared.Serializer.ObjectToByteArray(test);

                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);
                }
            }
            
            Thread.Sleep(10000);
            
            using (TcpClient client = new TcpClient())
            {
                client.Connect("localhost", 13000);

                using (NetworkStream stream = client.GetStream())
                {
                    Messages.TestMessage test = new()
                    {
                        Message = "This is second message from the client"
                    };
                    byte[] data = Shared.Serializer.ObjectToByteArray(test);

                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);
                }
            }
        }
    }
}