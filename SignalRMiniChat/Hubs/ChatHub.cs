using Microsoft.AspNetCore.SignalR;

namespace SignalRMiniChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(
            string? oldRoomName,
            string newRoomName,
            string user)
        {
            if (!string.IsNullOrEmpty(oldRoomName))
            {
                await Groups.RemoveFromGroupAsync(
                    Context.ConnectionId,
                    oldRoomName);

                await Clients
                    .Group(oldRoomName)
                    .SendAsync(
                        "ReceiveSystemMessage",
                        user + " " + oldRoomName + " odasından ayrıldı.");
            }

            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                newRoomName);

            await Clients
                .Group(newRoomName)
                .SendAsync(
                    "ReceiveSystemMessage",
                    user + " " + newRoomName + " odasına katıldı.");
        }

        public async Task SendMessage(
            string roomName,
            string user,
            string message)
        {
            await Clients
                .Group(roomName)
                .SendAsync(
                    "ReceiveMessage",
                    user,
                    message);
        }
    }
}