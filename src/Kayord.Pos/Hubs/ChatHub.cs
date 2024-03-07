using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(string message);
}

public class ChatHub : Hub<IChatHub>
{
    // public async Task ReceiveNotification()
    // {
    //     await Clients.All.ReceiveNotification("The message");
    // }

    // public async Task ReceiveNotification(string content)
    // {
    //     await Clients.All.ReceiveMessage();
    // }


    // public async Task SendStockPrice2()
    // {
    //     await Clients.Client("connectionId").SendAsync("ReceiveStockPrice");
    // }

    // public async Task SendStockPriceUser()
    // {
    //     await Clients.User("connectionId").SendAsync("ReceiveStockPrice");
    // }
}