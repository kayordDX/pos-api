
using Kayord.Pos.Data;
using Kayord.Pos.Services;
namespace Kayord.Pos.Features.CashUp.User.Close;

public class Endpoint : Endpoint<Request, Detail.Response>
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
        Post("/cashUp/close");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (_user.UserId == null)
        {
            await SendForbiddenAsync();
            return;
        }

        var response = await Detail.CashUp.CashUpProcess(req.OutletId, req.UserId, _dbContext, _user, true);
        await SendAsync(response);
    }
}