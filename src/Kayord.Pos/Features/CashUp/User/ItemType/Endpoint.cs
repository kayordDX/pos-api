using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.CashUp.User.ItemType;

public class Endpoint : EndpointWithoutRequest<List<Entities.CashUpUserItemType>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/cashUp/user/itemType");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _dbContext.CashUpUserItemType.Where(x => x.IsAuto == false).ToListAsync();
        await SendAsync(result);
    }
}