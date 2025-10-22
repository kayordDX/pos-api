using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Section.Create;

public class Endpoint : Endpoint<Request, Pos.Entities.Section>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/section");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new Entities.Section
        {
            Name = req.Name,
            OutletId = req.OutletId
        };

        await _dbContext.Section.AddAsync(entity);
        await _dbContext.SaveChangesAsync(ct);

        await Send.OkAsync(entity);
    }
}
