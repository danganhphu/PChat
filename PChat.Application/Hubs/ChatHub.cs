using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace PChat.Application.Hubs;

public class ChatHub: Hub
{
    private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

    public string GetConnectionId()
    {
        return Context.ConnectionId;
    }

    public override async Task OnConnectedAsync()
    {
        Users.TryAdd(Context.ConnectionId, Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Users.TryRemove(Context.ConnectionId, out _);
        await base.OnDisconnectedAsync(exception);
    }

    // Test hub
    /*
    public async Task AskServer(string message)
    {
        string response = message == "hello" ? "Message was: xin chao..." : "Message was: tam biet";
        await Clients.Client(this.Context.ConnectionId).SendAsync("askServerResponse", response);
    }
    */
}