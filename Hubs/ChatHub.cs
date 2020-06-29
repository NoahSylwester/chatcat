using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, string groupName)
        {
            if (groupName != "")
            {
                await Clients.Group(groupName).SendAsync("ReceiveMessage", message, Context.ConnectionId);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message, Context.ConnectionId);
            }
        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group '{groupName}'.", "chatcat");
        }
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group '{groupName}'.", "chatcat");
        }
        public async Task SendGroupInvite(string connectionID, string groupName)
        {
            await Clients.Client(connectionID).SendAsync("ReceiveInvite", Context.ConnectionId, groupName);
        }
        public override async Task OnConnectedAsync()
        {
            ConnectedUser.Ids.Add(Context.ConnectionId);
            await Clients.Caller.SendAsync("InitUser", Context.ConnectionId);
            await Clients.Caller.SendAsync("UsersOnline", ConnectedUser.Ids);
            await Clients.Caller.SendAsync("ReceiveMessage", "Hi! Welcome to chatcat! To get started you can either create a room in the top right or join an existing one from invite!", "chatcat");
            await Clients.Caller.SendAsync("ReceiveMessage", "Once you've created a room, you can invite others either by clicking their connection ID, or by entering it directly with 'invite by ID'", "chatcat");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception) { 
            ConnectedUser.Ids.Remove(Context.ConnectionId);  
            await base.OnDisconnectedAsync(exception); 
         }
        public async Task QueryUsersOnline()
        {
            await Clients.Caller.SendAsync("UsersOnline", ConnectedUser.Ids);
        }
    }
    public static class ConnectedUser
    {
        // init list of all connected users
        public static List<string> Ids = new List<string>();  
    }
}