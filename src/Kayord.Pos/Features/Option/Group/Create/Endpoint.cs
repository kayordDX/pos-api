using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Option.Group.Create;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/option/group");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.OptionGroup optionGroup = new()
        {
            Name = req.Name,
            MinSelections = req.MinSelections,
            MaxSelections = req.MaxSelections,
            OutletId = req.OutletId
        };

        await _dbContext.OptionGroup.AddAsync(optionGroup);
        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
