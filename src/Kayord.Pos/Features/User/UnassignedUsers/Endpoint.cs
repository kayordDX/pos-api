using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.UnassignedUsers;

public class Endpoint : Endpoint<Request, PaginatedList<UserResponse>>
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
        Get("/user/unassigned");
    }

    public override async Task HandleAsync(Request req, CancellationToken c)
    {
        var userOutlet = await _dbContext.UserOutlet.Where(x => x.UserId == _cu.UserId && x.IsCurrent == true).FirstOrDefaultAsync(c);
        if (userOutlet == null)
        {
            await SendNotFoundAsync(c);
            return;
        }
        var results = await _dbContext.Database
            .SqlQuery<UserResponse>(
            $"""
            SELECT
                uo.is_current,
                u.user_id,
                u.email,
                u.image,
                u.name,
                '' roles
            FROM user_outlet uo
            JOIN "user" u
                ON u.user_id = uo.user_id
            LEFT JOIN user_role_outlet ur
                ON uo.outlet_id = ur.outlet_id
            AND u.user_id = ur.user_id
            WHERE uo.outlet_id = {userOutlet.OutletId}
            AND ur.id IS NULL
            """
            ).GetPagedAsync(req, c);

        await SendAsync(results);
    }
}
