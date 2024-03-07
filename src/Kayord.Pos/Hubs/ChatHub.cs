using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public class ChatHub : Hub
{
    // public async Task ReceiveNotification()
    // {
    //     await Clients.All.ReceiveNotification("The message");
    // }

    public async Task ReceiveNotification(string content)
    {
        await Clients.All.SendAsync("ReceiveNotification", content);
    }


    // public async Task SendStockPrice2()
    // {
    //     await Clients.Client("connectionId").SendAsync("ReceiveStockPrice");
    // }

    // public async Task SendStockPriceUser()
    // {
    //     await Clients.User("connectionId").SendAsync("ReceiveStockPrice");
    // }
}