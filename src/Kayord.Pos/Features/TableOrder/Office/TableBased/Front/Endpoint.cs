using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Features.Role;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.FrontOffice;

public class Endpoint(AppDbContext dbContext, CurrentUserService cu) : Endpoint<Request, Response>
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly CurrentUserService _cu = cu;

    public override void Configure()
    {
        Get("/frontOffice/getOrders");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        UserOutlet? userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent == true, ct);
        if (userOutlet == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        List<int> divisionIds = await RoleHelper.GetDivisionsForRoles(req.RoleIds, _dbContext, userOutlet.OutletId, _cu.UserId);

        List<int> statusIds = await _dbContext.OrderItemStatus
            .AsNoTracking()
            .Where(x => x.IsFrontLine && x.IsComplete != true && x.IsCancelled != true)
            .Select(x => x.OrderItemStatusId)
            .ToListAsync(ct);

        var result = await _dbContext.TableBooking
            .AsNoTracking()
            .Where(x =>
                x.SalesPeriod.OutletId == userOutlet.OutletId &&
                x.CloseDate == null &&
                x.UserId == _cu.UserId &&
                x.OrderItems.Any(oi =>
                    statusIds.Contains(oi.OrderItemStatusId) &&
                    divisionIds.Contains(oi.MenuItem.DivisionId) &&
                    oi.OrderItemStatusId != 1 &&
                    oi.OrderItemStatusId != 6))
            .ProjectToDto()
            .ToListAsync(ct);

        result.ForEach(dto =>
        {
            dto.OrderItems = dto.OrderItems!
                .Where(oi =>
                    statusIds.Contains(oi.OrderItemStatusId) &&
                    divisionIds.Contains(oi.MenuItem.DivisionId) &&
                    oi.OrderItemStatusId != 1 &&
                    oi.OrderItemStatusId != 6)
                .ToList();
        });

        Response response = new()
        {
            LastRefresh = DateTime.Now,
            PendingItems = result.Sum(n => n.OrderItems?.Count) ?? 0,
            PendingTables = result.Count,
            Tables = result
        };
        await Send.OkAsync(response, ct);
    }
}
