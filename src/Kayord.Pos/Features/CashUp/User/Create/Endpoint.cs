using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.CashUp.User.Create;

public class Endpoint : Endpoint<Request, CashUpUserItem>
{
    private readonly AppDbContext _dbContext;
    private readonly UserService _userService;

    public Endpoint(AppDbContext dbContext, UserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public override void Configure()
    {
        Post("/cashUp/user");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (!await _userService.IsManager(req.OutletId))
        {
            await Send.ForbiddenAsync();
            return;
        }

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
            await Send.NotFoundAsync();
            return;
        }
        await Send.OkAsync(result);
    }
}
