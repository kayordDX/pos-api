using Kayord.POS.Data;

namespace Kayord.Pos.Features.Business.Create;

public class Endpoint : Endpoint<Request, POS.Entities.Business>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/business");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        POS.Entities.Business entity = new POS.Entities.Business()
        {
            Name = req.Name
        };
        await _dbContext.Business.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.Business.FindAsync(entity.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(result);
    }
}