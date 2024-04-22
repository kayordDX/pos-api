using Riok.Mapperly.Abstractions;
using Kayord.Pos.Features.TableOrder.Office;

namespace Kayord.Pos.Features.TableOrder.BackOffice;

[Mapper]
public static partial class MapperStatic
{
    public static partial IQueryable<TableBookingDTO> ProjectToDto(this IQueryable<Entities.TableBooking> q);
    public static partial IQueryable<TableDTO> ProjectToDto(this IQueryable<Entities.Table> q);
    public static partial IQueryable<OrderItemStatusDTO> ProjectToDto(this IQueryable<Entities.OrderItemStatus> q);

}