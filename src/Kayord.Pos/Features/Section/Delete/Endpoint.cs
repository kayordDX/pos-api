using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Section.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/section/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (await _dbContext.Table.Where(x => x.SectionId == req.Id).CountAsync() > 0)
        {
            throw new Exception("Can not delete section with tables");
        }
        var entity = await _dbContext.Section.FirstOrDefaultAsync(x => x.Id == req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        _dbContext.Section.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}