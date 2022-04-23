using Shared;

namespace SimpleMultiplayer.Handlers;

public class TestPacketHandler : PacketHandlerBase<Packets.TestPacket>
{
    protected override void Handle(Packets.TestPacket msg)
    {
        throw new NotImplementedException();
    }
}