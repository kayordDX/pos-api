using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Table.Update;

public class Endpoint : Endpoint<Request, Pos.Entities.Table>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/table/{tableId:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Table.FindAsync(req.TableId);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        entity.Name = req.Name;
        entity.SectionId = req.SectionId;
        entity.Capacity = req.Capacity;
        entity.Position = req.Position;

        await _dbContext.SaveChangesAsync();
        await SendAsync(entity);
    }
}