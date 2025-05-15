using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Option.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/option");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.Option? optionEntity = await _dbContext.Option.FindAsync(req.OptionId);
        if (optionEntity == null)
        {
            throw new Exception("Sorry, the princess is in another castle.");
        }

        optionEntity.Name = req.Name;
        optionEntity.Price = req.Price;
        optionEntity.PositionId = req.PositionId;
        optionEntity.OptionGroupId = req.OptionGroupId;
        optionEntity.IsAvailable = req.IsAvailable;

        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}