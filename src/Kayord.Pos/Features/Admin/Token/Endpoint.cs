using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Admin.Token;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly UserService _userService;
    private readonly CurrentUserService _currentUserService;

    public Endpoint(UserService userService, CurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public override void Configure()
    {
        Post("/admin/token");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        if (_currentUserService.UserId == "92jlIC3p9uUavQOw5Pf5bX61ck13")
        {
            var token = await _userService.GetCustomToken(r.UserId);
            Response result = new()
            {
                Token = token
            };
            await Send.OkAsync(result);
        }
        else
        {
            await Send.ForbiddenAsync(ct);
        }
    }
}