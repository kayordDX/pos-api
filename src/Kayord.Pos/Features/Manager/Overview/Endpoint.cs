using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Features.Role;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Manager.OrderView;

public class Endpoint : Endpoint<Request, List<Response>>
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
        Get("/manager/viewOrders");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        UserOutlet? userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent == true);
        if (userOutlet == null)
        {
            await SendNotFoundAsync();
            return;
        }

        int roleId = 0;
        List<Response> responses = new();
        UserRoleOutlet? userRole = await _dbContext.UserRoleOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.OutletId == userOutlet.OutletId);
        Entities.Role? role = await _dbContext.Role.Include(x => x.RoleType).FirstOrDefaultAsync(x => x.RoleId == userRole!.RoleId);
        if (role == null)
            await SendNotFoundAsync();
        else
            roleId = role.RoleId;

        List<int> divisionIds = await RoleHelper.GetDivisionsForRoles(req.RoleIds, _dbContext, userOutlet.OutletId, _cu.UserId);

        foreach (int divisionId in divisionIds)
        {
            var statusIds = _dbContext.OrderItemStatus.Where(x => x.isBackOffice && x.isComplete != true && x.isCancelled != true).Select(rd => rd.OrderItemStatusId).ToList();
            Entities.Division division = await _dbContext.Division.FirstOrDefaultAsync(x => x.DivisionId == divisionId) ?? new();

            var result = await _dbContext.TableBooking
                .Where(x => x.SalesPeriod.OutletId == userOutlet.OutletId && x.CloseDate == null)
                .ProjectToDto()
                .ToListAsync();

            result.ForEach(dto =>
            {
                dto.OrderItems = dto.OrderItems!
                .Where(oi => statusIds.Contains(oi.OrderItemStatusId) && oi.MenuItem.DivisionId == divisionId)
                .ToList();
            });

            if (role!.RoleType.isBackOffice)
                result = result.Where(x => x.OrderItems!.Any()).Where(x => x.CloseDate == null && x.OrderItems!.Where(y => y.OrderItemStatusId != 1 && y.OrderItemStatusId != 6).Count() > 0).ToList();
            if (role!.RoleType.isFrontLine)
                result = result.Where(x => x.OrderItems!.Any())
                        .Where(y => y.User.UserId == _cu.UserId && y.CloseDate == null
            && y.OrderItems!.Where(x => x.OrderItemStatusId != 1 && x.OrderItemStatusId != 6).Count() > 0).ToList();

            Response response = new()
            {
                LastRefresh = DateTime.Now,
                PendingItems = result.Sum(n => n.OrderItems?.Count) ?? 0,
                PendingTables = result.Count,
                Tables = result,
                Division = division!
            };
            responses.Add(response);
        }
        await SendAsync(responses);
    }
}