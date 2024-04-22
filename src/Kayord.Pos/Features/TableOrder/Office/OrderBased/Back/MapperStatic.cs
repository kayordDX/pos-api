using Riok.Mapperly.Abstractions;


namespace Kayord.Pos.Features.Order.BackOffice;

[Mapper]
public static partial class MapperStatic
{
    //  public static partial IQueryable<TableBookingDTO> ProjectToDto(this IQueryable<Entities.TableBooking> q);
    public static partial IQueryable<TableDTO> ProjectToDto(this IQueryable<Entities.Table> q);
    public static partial IQueryable<OrderItemStatusDTO> ProjectToDto(this IQueryable<Entities.OrderItemStatus> q);
    public static partial IQueryable<OrderItemDTO> ProjectToDto(this IQueryable<OrderItem> q);
    public static partial IQueryable<OrderGroupDTO> ProjectToDto(this IQueryable<Entities.OrderGroup> q);



}