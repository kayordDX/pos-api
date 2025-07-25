using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.AdjustmentTypeOutlet.Remove;

public class Endpoint : Endpoint<Request, Entities.AdjustmentTypeOutlet>
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
        Post("/remove");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (!await _userService.IsManager(req.OutletId))
        {
            await Send.ForbiddenAsync();
            return;
        }

        Entities.AdjustmentTypeOutlet? entity = await _dbContext.AdjustmentTypeOutlet.FirstOrDefaultAsync(x => x.AdjustmentTypeId == req.AdjustmentTypeId && x.OutletId == req.OutletId);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        _dbContext.AdjustmentTypeOutlet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
