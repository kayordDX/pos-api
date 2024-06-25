using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.CashUp.User.Create;

public class Endpoint : Endpoint<Request, CashUpUserItem>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _user;

    public Endpoint(AppDbContext dbContext, CurrentUserService user)
    {
        _dbContext = dbContext;
        _user = user;
    }

    public override void Configure()
    {
        Post("/cashUp/user/create");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        CashUpUserItem ci = new();
        ci.CashUpUserItemTypeId = req.CashUpUserItemTypeId;
        ci.CashUpUserId = req.CashUpUserId;
        ci.UserId = req.UserId;
        ci.OutletId = req.OutletId;
        ci.Value = req.Value;
        _dbContext.CashUpUserItem.Add(ci);
        await _dbContext.SaveChangesAsync();

        CashUpUserItem? result = await _dbContext.CashUpUserItem.FindAsync(ci.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }
        await SendAsync(result);
    }
}
