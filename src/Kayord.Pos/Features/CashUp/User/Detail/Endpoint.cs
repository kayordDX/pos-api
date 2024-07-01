using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.Features.CashUp.User.Close;

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
            await SendForbiddenAsync();
            return;
        }

        Response response = await CashUp.CashUpProcess(req, _dbContext, _user, false);
        await SendAsync(response);
    }
}