using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using YamlDotNet.Core.Tokens;

namespace Kayord.Pos.Features.Order.UpdateOrderItem
{
    public class Endpoint : Endpoint<Request, Pos.Entities.OrderItem>
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
                await SendOkAsync();
            }
            else
            {
                await SendNotFoundAsync();
            }
        }
    }
}