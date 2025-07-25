using Kayord.Pos.Data;

namespace Kayord.Pos.Features.DivisionType.Edit;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/divisionType");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.DivisionType.FindAsync(req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        entity.DivisionName = req.Name;
        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}