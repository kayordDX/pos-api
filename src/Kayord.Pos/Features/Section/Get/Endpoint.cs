using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Section.Get;

public class Endpoint : Endpoint<Request, Pos.Entities.Section>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/section/{sectionId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Section.FindAsync(req.SectionId);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        await Send.OkAsync(entity);
    }
}