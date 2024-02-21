
using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;


using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.GetNotifications;

public class Endpoint : EndpointWithoutRequest<List<UserNotification>>
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
        List<UserNotification> response = new();
        response = await _dbContext.UserNotification.Where(x => x.UserId == _cu.UserId && x.DateRead == null).ToListAsync();
        await SendAsync(response);
    }
}