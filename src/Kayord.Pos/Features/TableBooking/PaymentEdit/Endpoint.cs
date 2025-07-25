using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.PaymentEdit;

public class Endpoint : Endpoint<Request, CashUpUserItem>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/tableBooking/payment");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Payment.Include(x => x.TableBooking).Where(x => x.Id == req.PaymentId).FirstOrDefaultAsync();
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        if (entity.TableBooking.CashUpUserId != null)
        {
            throw new Exception("Cannot update payment after cash up is done.");
        }

        entity.PaymentTypeId = req.PaymentTypeId;
        entity.Amount = req.Amount;

        await _dbContext.SaveChangesAsync();
        await TableBooking.SaveTotal(entity.TableBooking.Id, _dbContext);
        await Send.NoContentAsync();
    }
}
