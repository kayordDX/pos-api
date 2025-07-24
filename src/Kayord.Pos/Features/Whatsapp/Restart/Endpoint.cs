using Kayord.Pos.Services.Whatsapp;

namespace Kayord.Pos.Features.Whatsapp.Restart;

public class Endpoint : EndpointWithoutRequest<List<Response>>
{
    private readonly WhatsappService _whatsappService;

    public Endpoint(WhatsappService whatsappService)
    {
        _whatsappService = whatsappService;
    }

    public override void Configure()
    {
        Get("/whatsapp/restart");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var terminateResponse = await _whatsappService.Terminate();
        var startResponse = await _whatsappService.Start();
        List<Response> response = new();
        response.Add(terminateResponse);
        response.Add(startResponse);
        await SendAsync(response);
    }
}