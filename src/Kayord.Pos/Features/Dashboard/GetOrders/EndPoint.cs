using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Kayord.Pos.DTO;

using Microsoft.EntityFrameworkCore;
using SqlKata;
using Kayord.Pos.Common.Wrapper;

namespace Kayord.Pos.Features.Dashboard.GetOrders;

public class Endpoint : EndpointWithoutRequest<List<OrderItemDTO>>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _user;
    public Endpoint(AppDbContext dbContext, CurrentUserService user)
    {
        _dbContext = dbContext;
        _user = user;
    }

    public override void Configure()
    {
        Get("/dashboard/getOrders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int roleId = 0;
        var role = await _dbContext.UserRole.FirstOrDefaultAsync(x => x.UserId == _user.UserId);
        if (role == null)
            await SendNotFoundAsync();
        else
            roleId = role.RoleId;

        List<RoleDivision> RoleDivisions = _dbContext.RoleDivision.Where(x => x.RoleId == roleId).ToList();

        var result = await _dbContext.OrderItem
       .ProjectToDto()
       .ToListAsync();
        await SendAsync(result);
    }
}