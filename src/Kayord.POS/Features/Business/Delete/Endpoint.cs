using Kayord.POS.Data;

namespace Kayord.Pos.Features.Business.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/business");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Business.FindAsync(req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        _dbContext.Business.Remove(entity);
        await _dbContext.SaveChangesAsync();

        await SendNoContentAsync();
    }
}