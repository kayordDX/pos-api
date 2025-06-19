using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Role.Division.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/role/division/{roleId}/{divisionId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.RoleDivision.FirstOrDefaultAsync(x => x.DivisionId == req.DivisionId && x.RoleId == req.RoleId);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        _dbContext.RoleDivision.Remove(entity);

        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}