namespace Kayord.Pos.Features.Pay.PayConfig.TestConfig;

public class Endpoint : Endpoint<Request, bool>
{
    private readonly HaloService _halo;
    public Endpoint(HaloService halo)
    {
        _halo = halo;
    }

    public override void Configure()
    {
        Post("/pay/config/test");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            var result = await _halo.TestConfig(req.Id);
            await SendAsync(result);
        }
        catch (Exception)
        {
            await SendAsync(false);
        }
    }
}
