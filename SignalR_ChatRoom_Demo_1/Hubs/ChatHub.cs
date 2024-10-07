using Microsoft.AspNetCore.SignalR;
using SignalR_ChatRoom_Demo_1.DataService;
using SignalR_ChatRoom_Demo_1.Models;

namespace SignalR_ChatRoom_Demo_1.Hubs
{
    public class ChatHub : Hub
    {

        private readonly ShareDB _shared;

        public ChatHub(ShareDB shared) => _shared = shared;
        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined");

        }

        public async Task SpecificChatRoom(UserConnection conn)
        {
            //add vào group theo connectionId
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            //lưu thông tin kết nối -connection
            _shared.connections[Context.ConnectionId] = conn;
            
            //gửi message lên client
            await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{conn.Username} has joined {conn.ChatRoom}");


        }


        public async Task SendMessage(string msg)
        {
            if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                await Clients.Group(conn.ChatRoom)
                    .SendAsync("RecivedSpecificMessage", conn.Username, msg);
            }
        }

    }
}
