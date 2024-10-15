using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.Entities;

using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.BackOffice;

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
        Get("/backOffice/getOrders");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        UserOutlet? userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent == true);
        if (userOutlet == null)
        {
            await SendNotFoundAsync();
            return;
        }

        var roles = await _dbContext.UserRoleOutlet
            .Where(x => x.OutletId == userOutlet.OutletId && x.UserId == _cu.UserId)
            .Select(x => x.RoleId)
            .ToListAsync();

        List<int> divisionIds = req.DivisionIds?.Split(",")
            .Select(item =>
            {
                int value;
                bool parsed = int.TryParse(item, out value);
                return new
                {
                    parsed = parsed,
                    value = value
                };
            })
            .Where(item => item.parsed)
            .Select(item => item.value)
            .ToList() ?? new List<int>();

        if (divisionIds.Count == 0)
        {
            divisionIds = await _dbContext.RoleDivision
                .Where(x => roles.Contains(x.RoleId))
                .Select(x => x.DivisionId)
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToListAsync();
        }

        var statusIds = _dbContext.OrderItemStatus
            .Where(x => x.isBackOffice && x.isComplete != true && x.isCancelled != true)
            .Select(rd => rd.OrderItemStatusId)
            .ToList();

        var result = await _dbContext.TableBooking
            .Where(x => x.SalesPeriod.OutletId == userOutlet.Id && x.CloseDate == null)
            .ProjectToDto()
            .ToListAsync();

        result.ForEach(dto =>
        {
            dto.OrderItems = dto.OrderItems!
            .Where(oi => statusIds.Contains(oi.OrderItemStatusId) &&
                divisionIds.Contains(oi.MenuItem.DivisionId))
            .ToList();
        });

        result = result.Where(x => x.OrderItems!.Any()).Where(x => x.CloseDate == null && x.OrderItems!.Where(y => y.OrderItemStatusId != 1 && y.OrderItemStatusId != 6).Count() > 0).ToList();

        Response response = new()
        {
            LastRefresh = DateTime.Now,
            PendingItems = result.Sum(n => n.OrderItems?.Count) ?? 0,
            PendingTables = result.Count,
            Tables = result
        };
        await SendAsync(response);
    }
}