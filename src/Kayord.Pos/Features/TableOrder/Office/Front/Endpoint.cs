using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.Entities;

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
        int outletId = 0;
        var roles = await _dbContext.Role.Where(x => x.UserRole!.Any(s => s.UserId == _cu.UserId)).Select(s => s.RoleId).ToListAsync();

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

        var statusIds = _dbContext.OrderItemStatus.Where(x => x.isFrontLine && x.isComplete != true && x.isCancelled != true).Select(rd => rd.OrderItemStatusId).ToList();
        UserOutlet? outlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.isCurrent == true);
        if (outlet == null)
        {
            await SendNotFoundAsync();
            return;
        }
        else
        {
            outletId = outlet.OutletId;
        }

        var result = await _dbContext.TableBooking
            .Where(x => x.SalesPeriod.OutletId == outletId && x.CloseDate == null)
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

        // result = result.Where(x => x.OrderItems!.Any())
        //     .Where(x => x.CloseDate == null 
        //     && x.OrderItems!.Where(y => y.OrderItemStatusId != 1 && y.OrderItemStatusId != 6).Count() > 0).ToList();

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