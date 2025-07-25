
using Kayord.Pos.Data;
using Kayord.Pos.Services;
namespace Kayord.Pos.Features.CashUp.User.Close;

public class Endpoint : Endpoint<Request, Detail.Response>
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
        Post("/cashUp/close");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (!await _userService.IsManager(req.OutletId))
        {
            await Send.ForbiddenAsync();
            return;
        }

        if (_userService.GetCurrentUserService().UserId == null)
        {
            await Send.ForbiddenAsync();
            return;
        }

        var response = await Detail.CashUp.CashUpProcess(req.OutletId, req.UserId, _dbContext, _userService.GetCurrentUserService(), true);

        await Send.OkAsync(response);
    }
}