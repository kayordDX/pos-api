using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Division.GetUsers;

public class Endpoint : Endpoint<Request, List<Response>>
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
        Get("/division/users/{divisionId}");
    }
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        var results = (await _dbContext.Database.SqlQuery<Response>($"""
                SELECT
                    u.user_id,
                    u.name,
                    u.email
                FROM
                    user_role_outlet uro
                    JOIN role_division rd ON uro.role_id = rd.role_id
                    JOIN division d ON d.division_id = rd.division_id
                    JOIN "user" u ON u.user_id = uro.user_id
                WHERE
                    d.division_id = {req.DivisionId} 
                    AND u.is_active = TRUE
                GROUP BY
                    u.user_id 
                """).ToListAsync(ct));
        if (req.excludeSelf)
        {
            results = results.Where(x => x.UserId != _cu.UserId).ToList();
        }
        if (results.Count > 0)
        {
            await SendAsync(results);
        }
        else
        {
            await SendNotFoundAsync();
        }
    }
}