using Kayord.Pos.Features.Business.Create;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Test;

public class Endpoint : EndpointWithoutRequest<bool>
{
    private readonly IEmailSender _emailSender;

    public Endpoint(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _emailSender.SendEmailAsync("kokjaco2@gmail.com", "Invoice 2024-03-27", """
        Hi Jaco,
        Please find the attached invoice for today.

        Thank you for your business!
        """);
        await SendAsync(true);
    }
}