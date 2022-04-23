using System.Reflection;
using Shared;

namespace SimpleMultiplayer;

public class MessageProcessor
{
    private readonly Dictionary<Type, MessageHandler.IMessageHandler> _handlers = new();

    public MessageProcessor()
    {
        // var types = Assembly.GetExecutingAssembly().GetTypes();
        // foreach (var type in types)
        // {
        //     if(type.Namespace == "")
        //         AddHandler((MessageHandler.IMessageHandler)type);
        // }
    }

    public void AddHandler(MessageHandler.IMessageHandler handler)
    {
        var concreteType = handler.ConcreteType;
        if (_handlers.ContainsKey((concreteType)))
        {
            throw new Exception($"Handler for type {concreteType} already exists");

            // If multiple messages of the same type are supported
            // Use list in dictionary
        }

        _handlers[concreteType] = handler;
    }

    public void ProcessMessage(Messages.IMessage msg)
    {
        if (_handlers.TryGetValue(msg.GetType(), out var handler))
        {
            handler.HandleMessage(msg);
        }
        else
        {
        }
    }
}