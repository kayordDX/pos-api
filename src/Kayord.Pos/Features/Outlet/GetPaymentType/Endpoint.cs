using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Outlet.GetPaymentType;

public class Endpoint : Endpoint<Request, List<Entities.PaymentType>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/outlet/paymentTypes/{id}");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var response = await _dbContext.OutletPaymentType
            .Where(x => x.OutletId == request.Id)
            .OrderBy(x => x.Position)
            .Select(x => x.PaymentType)
            .ToListAsync();
        await Send.OkAsync(response);
    }
}
