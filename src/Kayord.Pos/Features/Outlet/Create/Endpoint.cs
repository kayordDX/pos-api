using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Outlet.Create;

public class Endpoint : Endpoint<Request, Pos.Entities.Outlet>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/outlet");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Pos.Entities.Outlet entity = new Pos.Entities.Outlet()
        {
            Name = req.Name,
            BusinessId = req.BusinessId
        };
        await _dbContext.Outlet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.Outlet.FindAsync(entity.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(result);
    }
}