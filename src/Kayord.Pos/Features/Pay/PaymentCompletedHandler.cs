using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay;

public class PaymentCompletedHandler : IEventHandler<PaymentCompletedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;

    public PaymentCompletedHandler(IServiceScopeFactory scopeFactory, ILogger<PaymentCompletedHandler> logger)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(PaymentCompletedEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var _dbContext = scope.Resolve<AppDbContext>();

        if (_dbContext == null)
        {
            throw new Exception("Dependency injection failed");
        }

        Payment? p = await _dbContext.Payment.FirstOrDefaultAsync(x => x.PaymentReference == eventModel.PaymentReference);
        if (p == null)
        {
            HaloReference? hRef = await _dbContext.HaloReference.FirstOrDefaultAsync(x => x.Id.ToString() == eventModel.PaymentReference);
            if (hRef != null)
            {
                p = new()
                {
                    Amount = eventModel.Amount,
                    PaymentReference = eventModel.PaymentReference,
                    DateReceived = DateTime.UtcNow,
                    UserId = eventModel.UserId,
                    TableBookingId = hRef.TableBookingId,
                    PaymentTypeId = 1

                };
                await _dbContext.Payment.AddAsync(p);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}