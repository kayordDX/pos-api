using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Role.Division.Create;

public class Endpoint : Endpoint<Request, Entities.Division>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/role/division/");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new Entities.RoleDivision
        {
            DivisionId = req.DivisionId,
            RoleId = req.RoleId,

        };

        await _dbContext.RoleDivision.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
