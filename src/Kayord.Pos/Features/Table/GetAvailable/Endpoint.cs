using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Table.GetAvailable;

public class Endpoint : Endpoint<Request, List<Response>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/table/available");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var bookedTableIds = await _dbContext.TableBooking
              .Where(booking => booking.Table.Section.OutletId == req.OutletId && booking.CloseDate == null)
              .Select(booking => booking.TableId)
              .ToListAsync();

        var results = await _dbContext.Table
            .Where(table => table.Section.OutletId == req.OutletId && !bookedTableIds.Contains(table.TableId) && table.isDeleted != true)
            .OrderBy(x => x.Position)
            .ProjectToDto()
            .ToListAsync();

        await SendAsync(results);
    }
}