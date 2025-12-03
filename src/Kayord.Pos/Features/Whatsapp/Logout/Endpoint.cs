using Kayord.Pos.Services.Whatsapp;

namespace Kayord.Pos.Features.Whatsapp.Logout;

public class Endpoint : EndpointWithoutRequest<WResponse<SessionLogout>>
{
    private readonly WhatsappService _whatsappService;

    public Endpoint(WhatsappService whatsappService)
    {
        _whatsappService = whatsappService;
    }

    public override void Configure()
    {
        Post("/whatsapp/logout");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var terminateResponse = await _whatsappService.Logout();
        await Send.OkAsync(terminateResponse);
    }
}