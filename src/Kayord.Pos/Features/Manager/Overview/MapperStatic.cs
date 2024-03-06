using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.Features.Manager.OrderView;

[Mapper]
public static partial class MapperStatic
{
    public static partial IQueryable<TableBookingDTO> ProjectToDto(this IQueryable<Entities.TableBooking> q);
    public static partial IQueryable<TableDTO> ProjectToDto(this IQueryable<Entities.Table> q);
    public static partial IQueryable<OrderItemStatusDTO> ProjectToDto(this IQueryable<Entities.OrderItemStatus> q);
    public static partial IQueryable<DivisionDTO> ProjectToDto(this IQueryable<Entities.Division> q);


}