using Kayord.Pos.Services.Whatsapp;

namespace Kayord.Pos.Features.Whatsapp.Connect;

public class Endpoint : EndpointWithoutRequest<WResponse<SessionConnectResponse>>
{
    private readonly WhatsappService _whatsappService;

    public Endpoint(WhatsappService whatsappService)
    {
        _whatsappService = whatsappService;
    }

    public override void Configure()
    {
        Post("/whatsapp/connect");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _whatsappService.Connect();
        await Send.OkAsync(response);
    }
}