using Kayord.Pos.Data;
using Kayord.Pos.Services;
namespace Kayord.Pos.Features.TableOrder.GetBill;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("/order/getBill");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Response response = await Bill.Get(req, _dbContext);
        await Send.OkAsync(response);
    }
}