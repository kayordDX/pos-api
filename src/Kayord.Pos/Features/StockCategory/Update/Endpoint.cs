using Kayord.Pos.Data;

namespace Kayord.Pos.Features.StockCategory.Update;

public class Endpoint : Endpoint<Request, Pos.Entities.StockCategory>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/stockCategory/{id:int}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockCategory.FindAsync(req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        if (req.IsDeleted == true)
        {
            var dependant = _dbContext.StockCategory.FirstOrDefault(x => x.ParentId == req.Id && x.IsDeleted == false);
            if (dependant != null)
            {
                ValidationContext.Instance.ThrowError("Can not delete category with dependant");
            }

            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            await Send.OkAsync(entity);
            return;
        }
        entity.Name = req.Name;
        entity.ParentId = req.ParentId;
        entity.IsDeleted = false;

        await _dbContext.SaveChangesAsync();
        await Send.OkAsync(entity);
    }

}