using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Option.Group.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/option/group");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.OptionGroup? optionGroupEntity = await _dbContext.OptionGroup.FindAsync(req.OptionGroupId);
        if (optionGroupEntity == null)
        {
            throw new Exception("Sorry, the princess is in another castle.");
        }

        optionGroupEntity.Name = req.Name;
        optionGroupEntity.MinSelections = req.MinSelections;
        optionGroupEntity.MaxSelections = req.MaxSelections;

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}