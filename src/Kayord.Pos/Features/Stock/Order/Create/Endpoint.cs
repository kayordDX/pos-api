using Kayord.Pos.Data;
using Kayord.Pos.Services;


namespace Kayord.Pos.Features.Stock.Order.Create;

public class Endpoint : Endpoint<Request, Entities.StockOrder>
{
    private readonly AppDbContext _dbContext;
    private readonly UserService _userService;

    public Endpoint(AppDbContext dbContext, UserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public override void Configure()
    {
        Post("/stock/order");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (!await _userService.IsManager(req.OutletId))
        {
            await SendForbiddenAsync();
            return;
        }

        var entity = new Entities.StockOrder
        {
            OutletId = req.OutletId,
            OrderNumber = req.OrderNumber,
            DivisionId = req.DivisionId,
            SupplierId = req.SupplierId,
            StockOrderStatusId = 1
        };

        await _dbContext.StockOrder.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
