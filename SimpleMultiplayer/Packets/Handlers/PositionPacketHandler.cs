using SimpleMultiplayer;

namespace SimpleMultiplayer.Packets.Handlers;

public class PositionPacketHandler : PacketHandlerBase<Shared.Packets.Position>
{
    protected override void Handle(Shared.Packets.Position packet)
    {
        // This is where the server movement handling logic is written
        
        // Update clients position
        
        // TODO: Client's are in ClientHandler

        // Brodcast position update to all clients
    }
}