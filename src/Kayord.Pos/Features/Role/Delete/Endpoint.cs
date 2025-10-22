using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Role.Delete;

public class Endpoint : Endpoint<Request, Entities.Role>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/role/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Role.FindAsync(req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        _dbContext.Role.Remove(entity);

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}