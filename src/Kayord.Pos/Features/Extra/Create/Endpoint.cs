using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Extra.Create;

public class Endpoint : Endpoint<Request>
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
        Post("/extra");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (!await _userService.IsManager(req.OutletId))
        {
            await Send.ForbiddenAsync();
            return;
        }

        Entities.ExtraGroup? extraGroup = await _dbContext.ExtraGroup.FirstOrDefaultAsync(x => x.ExtraGroupId == req.ExtraGroupId);

        if (extraGroup == null)
        {
            throw new Exception("Extra Group not found");
        }

        Entities.Extra extra = new()
        {
            Name = req.Name,
            PositionId = req.PositionId,
            Price = req.Price,
            ExtraGroupId = req.ExtraGroupId,
            OutletId = req.OutletId,
        };

        await _dbContext.Extra.AddAsync(extra);
        await _dbContext.SaveChangesAsync();

        await Send.NoContentAsync();
    }
}
