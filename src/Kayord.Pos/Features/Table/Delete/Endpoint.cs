using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Table.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/table/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (await _dbContext.TableBooking.Where(x => x.TableId == req.Id && x.CloseDate == null).CountAsync() > 0)
        {
            throw new Exception("Can not delete table with open booking");
        }
        var entity = await _dbContext.Table.FirstOrDefaultAsync(x => x.TableId == req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }
        entity.isDeleted = true;

        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}