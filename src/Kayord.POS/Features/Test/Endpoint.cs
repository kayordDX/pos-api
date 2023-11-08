namespace Kayord.POS.Features.Test;

public class Endpoint : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await SendAsync(new Response
        {
            FirstName = "Steff Bosch"
        });
    }
}