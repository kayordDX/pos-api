using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Role.Update;

public class Endpoint : Endpoint<Request, Entities.Role>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/role/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Role.FindAsync(req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        entity.Name = req.Name;
        entity.Description = req.Description;
        entity.OutletId = req.OutletId;
        entity.RoleTypeId = req.RoleTypeId;


        await _dbContext.SaveChangesAsync();
        await Send.OkAsync(entity);
    }
}