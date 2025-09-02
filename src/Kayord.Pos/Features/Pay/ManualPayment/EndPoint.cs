using Kayord.Pos.Data;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Pay.ManualPayment;

public class Endpoint : Endpoint<Request, Entities.Payment>
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
        Post("/pay/manual");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Payment entity = new()
        {
            Amount = req.Amount,
            PaymentReference = Guid.NewGuid().ToString(),
            DateReceived = DateTime.UtcNow,
            UserId = _cu.UserId ?? "",
            TableBookingId = req.TableBookingId,
            PaymentTypeId = req.PaymentTypeId
        };

        await _dbContext.Payment.AddAsync(entity);

        await _dbContext.SaveChangesAsync();
        await Send.OkAsync(entity);
    }
}