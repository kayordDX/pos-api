using Kayord.Pos.Data;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.CashUp.User.Detail;

public class Endpoint : Endpoint<Request, Response>
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
        Get("/cashUp/user/detail/{userId}/{outletId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (_user.UserId == null)
        {
            await Send.ForbiddenAsync();
            return;
        }

        Response response = await CashUp.CashUpProcess(req.OutletId, req.UserId, _dbContext, _user, false, req.CashUpUserId);
        await Send.OkAsync(response);
    }
}