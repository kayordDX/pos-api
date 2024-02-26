using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.Features.SalesPeriod.CashUp;

[Mapper]
public static partial class MapperStatic
{
    public static partial IQueryable<BillOrderItemDTO> ProjectToDto(this IQueryable<OrderItem> q);
    public static partial IQueryable<UserDTO> ProjectToDto(this IQueryable<Entities.User> q);



}