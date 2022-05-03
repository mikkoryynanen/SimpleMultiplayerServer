using SimpleMultiplayer;

namespace SimpleMultiplayer.Packets.Handlers;

public class PositionPacketHandler : PacketHandlerBase<Shared.Packets.Position>
{
    protected override void Handle(Shared.Packets.Position packet)
    {
        // This is where client movement logic is written
    }
}