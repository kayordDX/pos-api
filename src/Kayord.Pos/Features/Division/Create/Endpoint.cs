using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Division.Create;

public class Endpoint : Endpoint<Request, Entities.Division>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/division");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var exists = await _dbContext.Division
            .AnyAsync(x => x.DivisionName == req.Name && x.OutletId == req.OutletId && !x.IsDeleted, ct);
        if (exists)
        {
            ThrowError("Division name already exists in this outlet");
        }

        Entities.Division division = new();
        division.DivisionName = req.Name;
        division.DivisionTypeId = req.DivisionTypeId;
        division.OutletId = req.OutletId;

        await _dbContext.Division.AddAsync(division);
        await _dbContext.SaveChangesAsync();
    }
}
