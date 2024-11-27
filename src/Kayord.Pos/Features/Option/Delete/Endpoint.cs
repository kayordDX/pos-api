using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Option.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/option/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Option? option = await _dbContext.Option.FindAsync(req.Id);

        if (option != null)
        {
            _dbContext.Option.Remove(option);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Extra Not Found");
        }
    }

}


