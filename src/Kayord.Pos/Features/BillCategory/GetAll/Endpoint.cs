using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.BillCategory.GetAll;

public class Endpoint : Endpoint<Request, List<Entities.BillCategory>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/BillCategory");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.BillCategory.Where(x => x.OutletId == req.OutletId).ToListAsync(ct);
        await Send.OkAsync(results);
    }
}



