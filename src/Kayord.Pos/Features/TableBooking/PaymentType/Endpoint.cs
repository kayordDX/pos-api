using Kayord.Pos.Data;
using Kayord.Pos.Entities;

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
        var entity = await _dbContext.Payment.FindAsync(req.PaymentId);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        entity.PaymentTypeId = req.PaymentTypeId;
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}
