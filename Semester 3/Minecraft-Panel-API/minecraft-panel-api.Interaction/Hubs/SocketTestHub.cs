using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace minecraft_panel_api.Interaction.Hubs
{
    [Authorize]
    public class SocketTestHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task PlayerJoinEvent(string user)
        {
            Console.WriteLine(user + ": has joined the server.");
            await Clients.Others.SendAsync("PlayerJoinEvent", user, " has joined the server.");
        }

        public async Task ChatMessageEvent(string user)
        {
            Console.WriteLine(user + ": has sent a message.");
            await Clients.Others.SendAsync("ChatMessageEvent", user, " has sent a message.");
        }
        
        public async Task PlayerQuitEvent(string user)
        {
            Console.WriteLine(user + ": has left the server.");
            await Clients.Others.SendAsync("PlayerQuitEvent", user, " has left the server.");
        }

        //[HubMethodName("ReceiveMessage")]
        public async Task ReceiveMessage(string user, string message)
        {
            Console.WriteLine(user + ": " + message);
            await Clients.Others.SendAsync("ReceiveMessage", user, message);
        }
        
        public async Task Send(string message)
        {
            await Clients
                .Caller
                .SendAsync("OnMessageRecieved", message);
        }

        //public async Task SendMessage(string message)
        //{
        //    var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
        //    Console.WriteLine("To: " + routeOb.To.ToString());
        //    Console.WriteLine("Message Recieved on: " + Context.ConnectionId );
        //    if(routeOb.To.ToString() == string.Empty)
        //    {
        //        Console.WriteLine("Broadcast");
        //        await Clients.All.SendAsync("ReceiveMessage", message);
        //    }
        //    else
        //    {
        //        string toClient = routeOb.To;
        //        Console.WriteLine("Targeted on: " + toClient);
        //        
        //        await Clients.Client(toClient).SendAsync("ReceiveMessage", message);
        //    }
        //}
        
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}