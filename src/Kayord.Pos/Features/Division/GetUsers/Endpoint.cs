using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Division.GetUsers;

public class Endpoint : Endpoint<Request, List<Response>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/division/users/{divisionId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.Database.SqlQuery<Response>($"""
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
                d.division_id = 17
                AND u.is_active = TRUE
            GROUP BY
                u.user_id
            """).ToListAsync(ct);

        await SendAsync(results);
    }
}