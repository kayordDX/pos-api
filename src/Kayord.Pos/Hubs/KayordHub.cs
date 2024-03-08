using Kayord.Pos.Common.Wrapper;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public interface IKayordHub
{
    Task ReceiveMessage(string message);
    Task PayMessage(Result<Features.Pay.Dto.StatusResultDto> request);
}

public class KayordHub : Hub<IKayordHub>
{

}