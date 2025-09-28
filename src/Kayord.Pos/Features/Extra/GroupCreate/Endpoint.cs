using Kayord.Pos.Data;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Extra.GroupCreate;

public class Endpoint : Endpoint<Request>
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
        Post("/extraGroup");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        int outletId = await Helper.GetUserOutlet(_dbContext, _user.UserId ?? "");

        if (outletId == 0)
        {
            await Send.NotFoundAsync();
            return;
        }

        Entities.ExtraGroup? extraGroup = new()
        {
            Name = req.Name,
            OutletId = outletId,
        };

        await _dbContext.Database.BeginTransactionAsync(ct);

        await _dbContext.ExtraGroup.AddAsync(extraGroup);
        await _dbContext.SaveChangesAsync();

        if (req.IsGlobal)
        {
            Entities.OutletExtraGroup outletExtraGroup = new()
            {
                OutletId = extraGroup.OutletId,
                ExtraGroupId = extraGroup.ExtraGroupId,
                ExtraGroup = extraGroup
            };
            await _dbContext.OutletExtraGroup.AddAsync(outletExtraGroup);
            await _dbContext.SaveChangesAsync();
        }

        await _dbContext.Database.CommitTransactionAsync(ct);

        await Send.NoContentAsync();
    }
}