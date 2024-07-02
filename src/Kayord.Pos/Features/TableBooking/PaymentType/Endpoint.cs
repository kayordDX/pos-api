using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.PaymentType;

public class Endpoint : Endpoint<Request, CashUpUserItem>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/tableBooking/paymentType");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Payment.Include(x => x.TableBooking).Where(x => x.Id == req.PaymentId).FirstOrDefaultAsync();
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        if (entity.TableBooking.CashUpUserId != null)
        {
            throw new Exception("Cannot update payment type after cash up is done.");
        }

        entity.PaymentTypeId = req.PaymentTypeId;
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}
