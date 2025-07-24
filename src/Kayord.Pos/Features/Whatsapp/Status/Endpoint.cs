using Kayord.Pos.Services.Whatsapp;

namespace Kayord.Pos.Features.Whatsapp.Status;

public class Endpoint : EndpointWithoutRequest<Services.Whatsapp.Status>
{
    private readonly WhatsappService _whatsappService;

    public Endpoint(WhatsappService whatsappService)
    {
        _whatsappService = whatsappService;
    }

    public override void Configure()
    {
        Get("/whatsapp/status");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var status = await _whatsappService.GetStatus();
        await SendAsync(status);
    }
}