using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Auth;

public class FeatureHandler : AuthorizationHandler<FeatureRequirement>
{
    private readonly UserService _user;
    private readonly AppDbContext _dbContext;

    public FeatureHandler(UserService user, AppDbContext dbContext)
    {
        _user = user;
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, FeatureRequirement requirement)
    {
        int outletId = await _user.GetOutletId();

        var features = await _dbContext.OutletFeature
            .Where(x => x.OutletId == outletId)
            .Select(x => x.Feature.Name.ToLower())
            .ToListAsync();

        if (features.Count == 0) return;

        if (features.Contains(requirement.Feature.ToLower()))
        {
            context.Succeed(requirement);
        }
    }
}