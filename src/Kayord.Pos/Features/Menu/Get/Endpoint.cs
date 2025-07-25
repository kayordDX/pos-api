using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Menu.Get;

public class Endpoint : Endpoint<Request, Pos.Entities.Menu>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/menu/{menuId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var menu = await _dbContext.Menu.FindAsync(req.MenuId);
        if (menu == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        await Send.OkAsync(menu);
    }
}