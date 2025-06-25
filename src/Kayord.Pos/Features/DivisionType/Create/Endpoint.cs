using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.DivisionType.Create;

public class Endpoint : Endpoint<Request, Pos.Entities.DivisionType>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/divisionType");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.DivisionType entity = new Entities.DivisionType()
        {
            DivisionName = req.Name
        };
        await _dbContext.DivisionType.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        await SendAsync(entity);
    }
}
