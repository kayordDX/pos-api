using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Division.Edit;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/division");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Division? division = await _dbContext.Division.FindAsync(req.Id);
        if (division == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        division.DivisionName = req.Name;
        division.DivisionTypeId = req.DivisionTypeId;
        division.OutletId = req.OutletId;



        await _dbContext.SaveChangesAsync();

        await Send.NoContentAsync();
    }
}