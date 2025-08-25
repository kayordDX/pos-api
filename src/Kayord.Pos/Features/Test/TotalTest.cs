using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill;
namespace Kayord.Pos.Features.Test;

public class TotalTest : EndpointWithoutRequest<TableTotal?>
{
    private readonly AppDbContext _dbContext;

    public TotalTest(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/test/total");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var billTotal = await BillHelper.GetTotal(7376, _dbContext);
        await Send.OkAsync(billTotal);
    }
}