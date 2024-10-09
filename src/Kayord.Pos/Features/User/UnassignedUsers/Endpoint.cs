using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.UnassignedUsers;

public class Endpoint : Endpoint<Request, PaginatedList<Response>>
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
            .SqlQuery<Response>(
            $"""
                SELECT
                    uo."IsCurrent",
                    u."UserId",
                u."Email",
                u."Image",
                u."Name"
                FROM "UserOutlet" uo
                JOIN "User" u
                    ON u."UserId" = uo."UserId"
                LEFT JOIN "UserRoleOutlet" ur
                    ON uo."OutletId" = ur."OutletId"
                AND u."UserId" = ur."UserId"
                WHERE uo."OutletId" = {userOutlet.OutletId}
                AND ur."Id" IS NULL
            """
            ).GetPagedAsync(req, c);

        await SendAsync(results);
    }
}
