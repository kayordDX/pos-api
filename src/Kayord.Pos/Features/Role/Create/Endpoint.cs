using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Role.Create;

public class Endpoint : Endpoint<Request, Entities.Role>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/role");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var newRole = new Entities.Role
        {
            Name = req.Name,
            Description = req.Description,
            RoleTypeId = req.RoleTypeId,
            OutletId = req.OutletId
        };
        await _dbContext.Role.AddAsync(newRole);
        await _dbContext.SaveChangesAsync();
        await Send.OkAsync(newRole);
    }
}