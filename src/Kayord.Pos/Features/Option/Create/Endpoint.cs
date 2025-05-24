using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Option.Create;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/option");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.OptionGroup? optionGroup = await _dbContext.OptionGroup.FirstOrDefaultAsync(x => x.OptionGroupId == req.OptionGroupId);

        if (optionGroup == null)
        {
            throw new Exception("Option Group not found");
        }

        Entities.Option option = new()
        {
            Name = req.Name,
            PositionId = req.PositionId,
            Price = req.Price,
            OptionGroupId = req.OptionGroupId,
            OutletId = req.OutletId,
        };

        await _dbContext.Option.AddAsync(option);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}
