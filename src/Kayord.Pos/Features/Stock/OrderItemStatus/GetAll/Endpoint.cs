using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItemStatus.GetAll;

public class Endpoint : EndpointWithoutRequest<List<StockOrderItemStatusDTO>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/stockorderitemstatus");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.StockOrderItemStatus.ProjectToDto().ToListAsync();
        await SendAsync(results);
    }
}