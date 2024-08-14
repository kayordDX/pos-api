using Kayord.Pos.Services.Whatsapp;

namespace Kayord.Pos.Features.Whatsapp.QrCode
{
    public class Endpoint : EndpointWithoutRequest<QrResponse>
    {
        private readonly WhatsappService _whatsappService;

        public Endpoint(WhatsappService whatsappService)
        {
            _whatsappService = whatsappService;
        }

        public override void Configure()
        {
            Get("/whatsapp/qr");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var status = await _whatsappService.QrCode();
            await SendAsync(status);
        }
    }
}