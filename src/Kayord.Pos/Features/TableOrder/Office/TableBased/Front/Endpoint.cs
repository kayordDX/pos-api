using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Features.Role;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.FrontOffice;

public class Endpoint : Endpoint<Request, Response>
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
        Get("/frontOffice/getOrders");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        UserOutlet? userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent == true);
        if (userOutlet == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        List<int> divisionIds = await RoleHelper.GetDivisionsForRoles(req.RoleIds, _dbContext, userOutlet.OutletId, _cu.UserId);

        var statusIds = _dbContext.OrderItemStatus.Where(x => x.IsFrontLine && x.IsComplete != true && x.IsCancelled != true).Select(rd => rd.OrderItemStatusId).ToList();

        var result = await _dbContext.TableBooking
            .Where(x => x.SalesPeriod.OutletId == userOutlet.OutletId && x.CloseDate == null)
            .ProjectToDto()
            .ToListAsync();

        result.ForEach(dto =>
        {
            dto.OrderItems = dto.OrderItems!
            .Where(oi => statusIds.Contains(oi.OrderItemStatusId) &&
                divisionIds.Contains(oi.MenuItem.DivisionId))
            .ToList();
        });

        result = result.Where(x => x.OrderItems!.Any())
            .Where(y => y.User.UserId == _cu.UserId && y.CloseDate == null &&
                y.OrderItems!.Where(x => x.OrderItemStatusId != 1 && x.OrderItemStatusId != 6).Count() > 0)
            .ToList();

        Response response = new()
        {
            LastRefresh = DateTime.Now,
            PendingItems = result.Sum(n => n.OrderItems?.Count) ?? 0,
            PendingTables = result.Count,
            Tables = result
        };
        await Send.OkAsync(response);
    }
}