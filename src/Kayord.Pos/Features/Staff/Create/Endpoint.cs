using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Staff.Create;

public class Endpoint : Endpoint<Request, Pos.Entities.Staff>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/staff");
        AllowAnonymous();  
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Pos.Entities.Staff entity = new Pos.Entities.Staff()
        {
            Name = req.Name,
            StaffType = req.StaffType,
            OutletId = req.OutletId
        };
        await _dbContext.Staff.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.Staff.FindAsync(entity.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(result);
    }
}