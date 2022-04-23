namespace Shared;

public class Messages
{
    public interface IMessage
    {
    }
    
    [Serializable]
    public struct TestMessage : IMessage
    {
        public string Message { get; set; }
    }
    
    [Serializable]
    public struct SecondMessage : IMessage
    {
        public string Message { get; set; }
    }
}