using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Kayord.Pos.Features.Stock;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.UpdateOrderItem;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/order/updateOrderItem");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        bool isSuccess = true;
        string message = "";

        var notification = "";
        var tableName = "";
        var nC = 0;
        string tUserId = "";
        var status = "";
        OrderItemStatus? oIS = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);
        if (oIS == null)
        {
            throw new Exception("Status not found");
        }
        OrderGroup order = new();
        if (oIS.AssignGroup)
        {
            await _dbContext.OrderGroup.AddAsync(order);
        }

        List<int>? divisions = new();
        int outletId = 0;
        bool soundNotify = false;

        foreach (int r in req.OrderItemIds)
        {
            OrderItem? entity = await _dbContext.OrderItem
                .Include(x => x.TableBooking)
                    .ThenInclude(x => x.SalesPeriod)
                .Include(x => x.TableBooking)
                    .ThenInclude(x => x.Table)
                .Include(x => x.MenuItem)
                .Include(x => x.OrderItemExtras)!
                    .ThenInclude(x => x.Extra)
                .Include(x => x.OrderItemOptions)!
                    .ThenInclude(x => x.Option)
                .FirstOrDefaultAsync(x => x.OrderItemId == r);

            if (entity != null)
            {

                // If send to kitchen add extra validation
                if (req.OrderItemStatusId == 2)
                {
                    if (entity.TableBooking.CloseDate != null)
                    {
                        throw new Exception("Table is closed");
                    }
                }

                if (req.OrderItemStatusId != 2)
                {
                    entity.OrderItemStatusId = req.OrderItemStatusId;
                    if (oIS?.AssignGroup ?? false)
                    {
                        entity.OrderGroup = order;
                    }
                    entity.OrderUpdated = DateTime.UtcNow;
                    if (oIS?.IsComplete ?? false)
                        entity.OrderCompleted = DateTime.Now;
                }

                status = oIS?.Status;


                if (req.OrderItemStatusId == 2)
                {
                    // Check if item has stock
                    bool isMenuItemAvailable = await StockManager.IsMenuItemAvailable(entity.MenuItemId, _dbContext, ct);
                    bool isExtrasAvailable = await StockManager.IsExtrasAvailable(entity.OrderItemId, entity.MenuItem.DivisionId, _dbContext, ct);
                    bool isOptionsAvailable = await StockManager.IsOptionsAvailable(entity.OrderItemId, entity.MenuItem.DivisionId, _dbContext, ct);

                    if (isMenuItemAvailable && isExtrasAvailable && isOptionsAvailable)
                    {
                        entity.OrderItemStatusId = req.OrderItemStatusId;
                        if (oIS?.AssignGroup ?? false)
                        {
                            entity.OrderGroup = order;
                        }
                        entity.OrderUpdated = DateTime.UtcNow;
                        if (oIS?.IsComplete ?? false)
                            entity.OrderCompleted = DateTime.Now;
                        await _dbContext.SaveChangesAsync();
                        await PublishAsync(new StockEvent() { OrderItemIds = [entity.OrderItemId], IsReverse = false }, Mode.WaitForAll);
                    }
                    else
                    {
                        List<string> unavailableComponents = new();
                        if (!isMenuItemAvailable)
                        {
                            unavailableComponents.Add("Menu Item");
                        }
                        if (!isExtrasAvailable)
                        {
                            unavailableComponents.Add("Extras");
                        }
                        if (!isOptionsAvailable)
                        {
                            unavailableComponents.Add("Options");
                        }

                        isSuccess = false;
                        message = $"Selected items(s) out of stock - {string.Join(", ", unavailableComponents)}";
                        continue;
                    }
                }


                if (oIS?.IsNotify ?? false)
                {
                    Entities.MenuItem? i = await _dbContext.MenuItem.FirstOrDefaultAsync(x => x.MenuItemId == entity.MenuItemId);
                    if (i != null)
                    {
                        nC++;
                        tableName = entity.TableBooking.Table.Name;
                        notification = notification == "" ? i.Name : notification + ", " + i.Name;
                        tUserId = entity.TableBooking.UserId;
                    }
                }

                if (oIS?.IsBackOffice ?? false)
                {
                    outletId = entity.TableBooking.SalesPeriod.OutletId;
                    var divisionId = entity.MenuItem.DivisionId ?? 0;
                    if (!divisions.Contains(divisionId))
                    {
                        divisions.Add(divisionId);
                    }
                    soundNotify = true;
                }
            }
            else
            {
                await Send.OkAsync(new Response() { IsSuccess = false, Message = "Order not found" });
            }
        }

        // Stock
        if ((oIS?.IsUpdateStock ?? false) && req.OrderItemStatusId != 2)
        {
            // stock event publish
            await PublishAsync(new StockEvent() { OrderItemIds = req.OrderItemIds, IsReverse = oIS?.IsUpdateStockReverse ?? false }, Mode.WaitForNone);
        }


        if (notification != "")
        {
            await PublishAsync(new NotificationEvent()
            {
                UserId = tUserId,
                Title = "Item Status",
                Body = tableName + " - " + notification + " - " + status,
            }, Mode.WaitForNone);
        }

        if (soundNotify)
        {
            // var roleIds = _dbContext.RoleDivision.Where(x => divisions.Contains(x.DivisionId)).Select(x => x.RoleId).ToList();
            await PublishAsync(new SoundEvent() { OutletId = outletId, DivisionIds = divisions });
        }

        await _dbContext.SaveChangesAsync();
        await Send.OkAsync(new Response() { IsSuccess = isSuccess, Message = message });
    }
}