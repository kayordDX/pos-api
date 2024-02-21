
using Kayord.Pos.Data;
using Kayord.Pos.Services;


using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.GetNotifications;

public class Endpoint : EndpointWithoutRequest<Response>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("/user/getNotifications");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response response = new();
        response.Notifications = await _dbContext.UserNotification.Where(x => x.UserId == _cu.UserId && x.DateRead == null && (x.DateExpires == null || x.DateExpires.Value > DateTime.Now)).ToListAsync();



        await SendAsync(response);
    }
}