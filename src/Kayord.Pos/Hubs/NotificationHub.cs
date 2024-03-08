using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public interface INotificationHub
{
    Task ReceiveMessage(string message);
}

public class NotificationHub : Hub<INotificationHub>
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