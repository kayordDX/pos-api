using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.DTO;

using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Kitchen.GetOrders;

public class Endpoint : EndpointWithoutRequest<Response>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("/kitchen/getOrders");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response response = new();
        int roleId = 0;
        var role = await _dbContext.UserRole.FirstOrDefaultAsync(x => x.UserId == _cu.UserId);
        if (role == null)
            await SendNotFoundAsync();
        else
            roleId = role.RoleId;
        
        var divisionIds =  _dbContext.RoleDivision.Where(x => x.RoleId == roleId).Select(rd => rd.DivisionId).ToList();
        var kitchenStatusIds =  _dbContext.OrderItemStatus.Where(x=>x.isKitchen == true).Select(rd => rd.OrderItemStatusId).ToList();

        response.OrderItems = await _dbContext.OrderItem
        .Where(x => kitchenStatusIds.Contains(x.OrderItemStatusId) && divisionIds.Contains(x.MenuItem.DivisionId)) 
        .ProjectToDto()
        .ToListAsync();

        await SendAsync(response);
    }
}