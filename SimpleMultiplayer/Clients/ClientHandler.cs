namespace SimpleMultiplayer.Clients;

public class ClientHandler
{
    // private Dictionary<Client>
    public class Client
    {
        public Guid Id { get; set; } = new Guid("c83b069a-be1b-4c58-815d-b90bef964307");

        // TODO: Extract these to user/player struct
        public string Username { get; set; }
        public Shared.Packets.Position Position { get; set; } // TODO: Use domain class for position

        public void SetPosition(float x, float y)
        {
            Position = new Shared.Packets.Position
            {
                X = x,
                Y = y
            };
        }
    }

    private static Dictionary<Guid, Client> _clients = new();

    public static void BrodcastAll()
    {
        foreach (var client in _clients.Values) 
        {
            
        }
    }
    
    public static void AddClient(Client client)
    {
        _clients[client.Id] = client;

        // if (GetClient(client.Id).HasValue)
        // {
        //     throw new Exception($"Client with id ${clientId} already exists");
        // }
        // else
        // {
        //     _clients[clientId] = client;
        // }
    }

    public static Client? GetClient(Guid clientId)
    {
        if (_clients.TryGetValue(clientId, out var client))
            return client;
        return null;
    }
}