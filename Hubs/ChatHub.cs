using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, Context.ConnectionId);
            // foreach (string userID in userIDs)
            // {
            //     await Clients.Client(userID).SendAsync("ReceiveMessage", user, message);
            // }
        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
        public override async Task OnConnectedAsync()
        {
            ConnectedUser.Ids.Add(Context.ConnectionId);
            await Clients.Caller.SendAsync("InitUser", Context.ConnectionId);
            await Clients.Caller.SendAsync("UsersOnline", ConnectedUser.Ids);
            await base.OnConnectedAsync(); 
        }
        public override async Task OnDisconnectedAsync(Exception exception) { 
            ConnectedUser.Ids.Remove(Context.ConnectionId);  
            await base.OnDisconnectedAsync(exception); 
         }
    }
    public static class ConnectedUser
    {
        // init list of all connected users
        public static List<string> Ids = new List<string>();  
    }
}