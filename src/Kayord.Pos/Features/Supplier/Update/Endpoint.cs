using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Supplier.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/supplier");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Supplier.FindAsync(req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        entity.Name = req.Name;
        entity.ContactName = req.ContactName;
        entity.ContactNumber = req.ContactNumber;
        entity.Email = req.Email;

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
