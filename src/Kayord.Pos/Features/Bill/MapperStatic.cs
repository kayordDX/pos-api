using Kayord.Pos.Entities;
using Riok.Mapperly.Abstractions;
using Kayord.Pos.DTO;


namespace Kayord.Pos.Features.Bill;

[Mapper]
public static partial class MapperStatic
{
    public static partial IQueryable<BillOrderItemDTO> ProjectToDto(this IQueryable<OrderItem> q);

}