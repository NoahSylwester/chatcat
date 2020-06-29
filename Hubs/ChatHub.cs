using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string[] userIDs)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            foreach (string userID in userIDs)
            {
                await Clients.Client(userID).SendAsync("ReceiveMessage", user, message);
            }
        }
        public override async Task OnConnectedAsync()
        {
            ConnectedUser.Ids.Add(Context.ConnectionId);
            await Clients.Caller.SendAsync("InitUser", ConnectedUser.Ids);
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