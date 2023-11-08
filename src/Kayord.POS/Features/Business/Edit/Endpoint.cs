using Kayord.POS.Data;

namespace Kayord.Pos.Features.Business.Edit;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/business");
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

        entity.Name = req.Name;
        await _dbContext.SaveChangesAsync();

        await SendNoContentAsync();
    }
}