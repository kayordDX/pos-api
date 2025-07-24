using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Outlet.Update;

public class Endpoint : Endpoint<Request, Pos.Entities.Outlet>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/outlet/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Outlet.FindAsync(req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        entity.Name = req.Name;
        entity.BusinessId = req.BusinessId;

        await _dbContext.SaveChangesAsync();
        await SendAsync(entity);
    }
}