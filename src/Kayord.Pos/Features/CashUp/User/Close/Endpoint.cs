
using Kayord.Pos.Data;
using Kayord.Pos.Services;
namespace Kayord.Pos.Features.CashUp.User.Close;

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
        Post("/cashUp/zaber");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (_user.UserId == null)
        {
            await SendForbiddenAsync();
            return;
        }

        Response response = await Detail.CashUp.CashUpProcess(req, _dbContext, _user, true);
        await SendAsync(response);
    }
}