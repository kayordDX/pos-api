using Kayord.Pos.Data;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.CashUp.User.Delete;

public class Endpoint : Endpoint<Request, CashUpUserItem>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/cashUp/user");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        CashUpUserItem? entity = await _dbContext.CashUpUserItem.FindAsync(req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        _dbContext.CashUpUserItem.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await SendOkAsync();
    }
}
