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
        int outletId = 0;
        UserRole? uRole = await _dbContext.UserRole.FirstOrDefaultAsync(x => x.UserId == _cu.UserId);
        Entities.Role? role = await _dbContext.Role.FirstOrDefaultAsync(x => x.RoleId == uRole!.RoleId);

        // var roles = await _dbContext.Role.Where(x => x.UserRole!.Any(s => s.UserId == _cu.UserId)).Select(s => s.RoleId).ToListAsync();

        if (role == null)
        {
            await SendNotFoundAsync();
            return;
        }

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
            var divisionIdsNullable = _dbContext.RoleDivision
                .Where(x => x.RoleId == role.RoleId)
                .Select(rd => rd.DivisionId)
                .ToList();

            divisionIds = divisionIdsNullable
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToList();
        }

        var statusIds = _dbContext.OrderItemStatus.Where(x => x.isBackOffice && x.isComplete != true && x.isCancelled != true).Select(rd => rd.OrderItemStatusId).ToList();
        UserOutlet? outlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.isCurrent == true);
        if (outlet == null)
        {
            await SendNotFoundAsync();
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