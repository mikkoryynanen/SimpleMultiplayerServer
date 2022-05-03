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
    
    [Serializable]
    public struct Position : IPacket
    {
        public Guid ClientId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}