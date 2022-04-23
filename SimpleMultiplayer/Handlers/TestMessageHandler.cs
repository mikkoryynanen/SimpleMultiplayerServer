using Shared;

namespace SimpleMultiplayer.Handlers;

public class TestMessageHandler : MessageHandler.MessageHandlerBase<Messages.TestMessage>
{
    protected override void Handle(Messages.TestMessage msg)
    {
        Console.WriteLine($"test message: {msg.Message}");
    }
}

public class SecondMessageHandler : MessageHandler.MessageHandlerBase<Messages.SecondMessage>
{
    protected override void Handle(Messages.SecondMessage msg)
    {
        Console.WriteLine($"second message: {msg.Message}");
    }
}