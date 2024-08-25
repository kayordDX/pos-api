using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;
using YamlDotNet.Core.Tokens;

namespace Kayord.Pos.Features.TableOrder.SendToKitchen
{
    public class Endpoint : Endpoint<Request, Response>
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
            Post("/order/sendKitchen");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var tableBooking = await _dbContext.TableBooking
                .Include(x => x.SalesPeriod)
                .Where(x => x.Id == req.TableBookingId).FirstOrDefaultAsync();

            var orderItemsToUpdate = await _dbContext.OrderItem
                .Include(x => x.MenuItem)
                .Where(oi => oi.OrderItemStatusId == 1 && oi.TableBookingId == req.TableBookingId)
                .ToListAsync();

            OrderGroup order = new();
            await _dbContext.OrderGroup.AddAsync(order);

            List<int>? divisions = new();
            int outletId = tableBooking?.SalesPeriod.OutletId ?? 0;

            foreach (var orderItem in orderItemsToUpdate)
            {
                orderItem.OrderItemStatusId = 2;
                orderItem.OrderUpdated = DateTime.UtcNow;
                orderItem.OrderGroup = order;
                var divisionId = orderItem.MenuItem.DivisionId ?? 0;
                if (!divisions.Contains(divisionId))
                {
                    divisions.Add(divisionId);
                }
            }

            await _dbContext.SaveChangesAsync();

            await PublishAsync(new SoundEvent() { OutletId = outletId, Divisions = divisions });

            await SendAsync(new Response { IsSuccess = true });
        }
    }
}