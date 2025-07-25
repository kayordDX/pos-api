using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem.Status;

public class Endpoint : EndpointWithoutRequest<List<StockOrderItemStatusDTO>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/stock/orderItem/status");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.StockOrderItemStatus.ProjectToDto().ToListAsync();
        await Send.OkAsync(results);
    }
}