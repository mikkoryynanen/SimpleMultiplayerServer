using System.Reflection;
using Shared;

namespace SimpleMultiplayer;

public class PacketProcessor
{
    private readonly Dictionary<Type, PacketHandler.IPacketHandler> _handlers = new();

    public PacketProcessor()
    {
        // var types = Assembly.GetExecutingAssembly().GetTypes();
        // foreach (var type in types)
        // {
        //     if(type.Namespace == "")
        //         AddHandler((PacketHandler.IPacketHandler)type);
        // }
    }

    public void AddHandler(PacketHandler.IPacketHandler handler)
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

    public void ProcessMessage(Packets.IPacket msg)
    {
        if (_handlers.TryGetValue(msg.GetType(), out var handler))
        {
            handler.HandlePacket(msg);
        }
        else
        {
        }
    }
}