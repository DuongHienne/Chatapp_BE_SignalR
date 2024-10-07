using SignalR_ChatRoom_Demo_1.Models;
using System.Collections.Concurrent;

namespace SignalR_ChatRoom_Demo_1.DataService
{
    public class ShareDB
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();

        public ConcurrentDictionary<string, UserConnection> connections => _connections;

    }
}
