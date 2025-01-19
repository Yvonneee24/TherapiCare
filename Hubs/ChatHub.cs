using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message, long timestamp)
    {
        // Broadcast message to all clients
        await Clients.All.SendAsync("ReceiveMessage", user, message, timestamp);
    }

    // Method to notify clients when Customer Service is online
    public async Task CustomerServiceOnline()
    {
        await Clients.All.SendAsync("CustomerServiceOnline");
    }

    // Method to notify clients when Customer Service goes offline
    public async Task CustomerServiceOffline()
    {
        await Clients.All.SendAsync("CustomerServiceOffline");
    }
}