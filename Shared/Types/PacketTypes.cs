namespace Shared;

public class Packets
{
    public interface IPacket
    {
    }
    
    [Serializable]
    public struct TestPacket : IPacket
    {
        public string Message { get; set; }
    }
}