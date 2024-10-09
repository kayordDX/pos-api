using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.Users;

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
        Get("/user/list");
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
                    uo."IsCurrent",
                    u."UserId",
                u."Email",
                u."Image",
                u."Name",
                STRING_AGG(r."Name", ',') "Roles"
                FROM "UserOutlet" uo
                JOIN "User" u
                    ON u."UserId" = uo."UserId"
                JOIN "UserRoleOutlet" ur
                    ON uo."OutletId" = ur."OutletId"
                AND u."UserId" = ur."UserId"
                JOIN "Role" r
                    ON r."RoleId" = ur."RoleId" 
                WHERE uo."OutletId" = {userOutlet.OutletId}
                GROUP BY 
                uo."IsCurrent",
                    u."UserId",
                u."Email",
                u."Image",
                u."Name"
            """
            ).GetPagedAsync(req, c);

        await SendAsync(results);
    }
}
