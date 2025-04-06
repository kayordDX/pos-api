using Kayord.Pos.Data;
using Kayord.Pos.Services;


namespace Kayord.Pos.Features.Stock.Allocate.Create;

public class Endpoint : Endpoint<Request, Entities.StockOrder>
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
        Post("/stock/allocate");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new Entities.StockAllocate
        {
            OutletId = req.OutletId,
            ToOutletId = req.ToOutletId,
            FromDivisionId = req.FromDivisionId,
            ToDivisionId = req.ToDivisionId,
            AssignedUserId = req.AssignedUserId,
            Comment = req.Comment,
            Created = DateTime.UtcNow,
            StockAllocateStatusId = 1,
            FromUserId = _cu.UserId ?? "",
        };

        await _dbContext.StockAllocate.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
