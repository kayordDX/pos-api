using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Business.GetAll;

public class Endpoint : EndpointWithoutRequest<List<Pos.Entities.Business>>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _currentUserService;

    public Endpoint(AppDbContext dbContext, CurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public override void Configure()
    {
        Get("/business");
        // AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var test = _currentUserService;
        var results = await _dbContext.Business.ToListAsync();
        await SendAsync(results);
    }
}