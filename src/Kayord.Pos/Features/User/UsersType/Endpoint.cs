using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.UsersType;

public class Endpoint : Endpoint<Request, List<UserResponse>>
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
        Get("/user/typeList");
    }

    public override async Task HandleAsync(Request req, CancellationToken c)
    {
        var userOutlet = await _dbContext.UserOutlet.Where(x => x.UserId == _cu.UserId && x.IsCurrent == true).FirstOrDefaultAsync(c);
        if (userOutlet == null)
        {
            await Send.NotFoundAsync(c);
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
                    STRING_AGG(r.name, ',') roles
                FROM user_outlet uo
                JOIN "user" u
                    ON u.user_id = uo.user_id
                JOIN user_role_outlet ur
                    ON uo.outlet_id = ur.outlet_id
                    AND u.user_Id = ur.user_Id
                JOIN role r
                    ON r.role_id = ur.role_id 
                JOIN role_type rt
                    ON r.role_type_id = rt.id
                WHERE 
                    uo.outlet_id = {userOutlet.OutletId}
                    AND rt.is_front_line = {req.IsFrontLine}
                    AND rt.is_back_office = {req.IsBackOffice}
                GROUP BY 
                    uo.is_current,
                    u.user_id,
                    u.email,
                    u.image,
                    u.name
            """
            ).ToListAsync(c);

        await Send.OkAsync(results);
    }
}
