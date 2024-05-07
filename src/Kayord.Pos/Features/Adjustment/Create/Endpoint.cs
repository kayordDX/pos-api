using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Adjustment.Create;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Post("/adjustment");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var tableBooking = await _dbContext.TableBooking
            .Include(x => x.Adjustments)
            .Where(x => x.Id == req.TableBookingId)
            .FirstOrDefaultAsync();

        if (tableBooking == null)
        {
            await SendNotFoundAsync();
            return;
        }

        if (tableBooking.Adjustments == null)
        {
            tableBooking.Adjustments = new List<Entities.Adjustment>();
        }

        tableBooking.Adjustments.Add(new Entities.Adjustment()
        {
            AdjustmentTypeId = req.AdjustmentTypeId,
            Amount = req.Amount,
            Note = req.Note,
            UserId = _cu.UserId ?? ""
        });
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}
