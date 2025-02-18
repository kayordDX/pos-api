using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Supplier.Create;

public class Endpoint : Endpoint<Request, Entities.Supplier>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/supplier");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new Entities.Supplier
        {
            OutletId = req.OutletId,
            Name = req.Name,
            ContactName = req.ContactName,
            ContactNumber = req.ContactNumber,
            Email = req.Email,
        };

        await _dbContext.Supplier.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
