using Kayord.Pos.Data;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.TableOrder.UpdateOrderItem
{
    public class Endpoint : Endpoint<Request, Response>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Delete("/order/updateOrderItem");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            OrderItem? entity = await _dbContext.OrderItem.FindAsync(req.OrderItemId);
            if (entity != null)
            {
                entity.OrderItemStatusId = req.OrderItemStatusId;
                if (req.isComplete)
                    entity.OrderCompleted = DateTime.Now;
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                await SendAsync(new Response() { IsSuccess = true });

            }
            else
            {
                await SendAsync(new Response() { IsSuccess = false });
            }
        }
    }
}