using Kayord.Pos.Data;

namespace Kayord.Pos.Features.BillCategory.Create;

public class Endpoint : Endpoint<Request, Entities.BillCategory>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/billCategory");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Pos.Entities.BillCategory entity = new Pos.Entities.BillCategory()
        {
            Name = req.Name,
            OutletId = req.OutletId
        };
        await _dbContext.BillCategory.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}