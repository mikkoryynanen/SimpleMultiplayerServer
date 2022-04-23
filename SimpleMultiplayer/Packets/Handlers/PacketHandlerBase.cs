/*
 * Helpful links:
 * https://stackoverflow.com/questions/1477471/design-pattern-for-handling-multiple-message-types
*/
using Shared;

namespace SimpleMultiplayer;

public class PacketHandler
{
    public interface IPacketHandler
    {
        Type ConcreteType { get; }
        bool HandlePacket(Packets.IPacket packet);
    }
    
    
}

public abstract class PacketHandlerBase<T> : PacketHandler.IPacketHandler where T : Packets.IPacket
{
    public Type ConcreteType => typeof(T);

    public bool HandlePacket(Packets.IPacket packet)
    {
        if (!(packet is T concretePacket)) 
            return false;
            
        Handle(concretePacket);
        return true;
    }

    protected abstract void Handle(T packet);
}