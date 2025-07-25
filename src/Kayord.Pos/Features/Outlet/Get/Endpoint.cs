using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Outlet.Get;

public class Endpoint : Endpoint<Request, Entities.Outlet>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/outlet/{id}");
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var entity = await _dbContext.Outlet.FindAsync(request.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        await Send.OkAsync(entity);
    }
}