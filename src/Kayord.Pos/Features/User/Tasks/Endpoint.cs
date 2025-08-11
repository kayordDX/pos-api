using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.Tasks;

public class Endpoint : Endpoint<Request, PaginatedList<Response>>
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
        Get("/user/tasks");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var notification = await _dbContext.StockAllocateItem
            .Where(x => x.StockAllocateItemStatusId == 2 && x.AssignedUserId == _cu.UserId)
            .Select(x => new Response()
            {
                Id = x.Id,
                OutletId = x.StockAllocate.OutletId,
                Outlet = new DTO.OutletDTOBasic() { Id = x.StockAllocate.ToOutletId, Name = x.StockAllocate.ToOutlet.Name },
                AssignedUserId = x.AssignedUserId,
                AssignedUser = new DTO.UserDTO()
                {
                    UserId = x.AssignedUser!.UserId,
                    Name = x.AssignedUser.Name,
                    Email = x.AssignedUser.Email,
                    Image = x.AssignedUser.Image,
                    IsActive = x.AssignedUser.IsActive
                },
                Name = x.Stock.Name + " " + "(" + x.Actual + " " + x.Stock.Unit.Name + ")",
                Status = x.StockAllocateItemStatus.Name,
                Type = "Stock Allocation",
                ToDivisionId = x.StockAllocate.ToDivisionId,
                Description = ((x.StockAllocate.ToOutletId == x.StockAllocate.OutletId) ? "" : x.StockAllocate.Outlet.Name + " -> " + x.StockAllocate.ToOutlet.Name + "-> ") + x.StockAllocate.FromDivision.DivisionName + " to " + x.StockAllocate.ToDivision.DivisionName,
                LastModified = x.LastModified ?? x.Created
            })
            .OrderBy(x => x.LastModified)
            .AsNoTracking()
            .GetPagedAsync(r, ct);

        await Send.OkAsync(notification);
    }
}
