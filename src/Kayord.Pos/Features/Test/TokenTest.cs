using System.Text.Json;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Kayord.Pos.Data;
using Kayord.Pos.Services;


namespace Kayord.Pos.Features.Test;

public class TokenResult
{
    public string Token { get; set; } = string.Empty;
}

public class TokenTest : EndpointWithoutRequest<TokenResult>
{
    private readonly UserService _userService;

    public TokenTest(UserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Get("/test/token");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var uid = "92jlIC3p9uUavQOw5Pf5bX61ck13";
        var token = await _userService.GetIdToken(uid);

        TokenResult result = new()
        {
            Token = token.IdToken
        };
        await Send.OkAsync(result);
    }
}