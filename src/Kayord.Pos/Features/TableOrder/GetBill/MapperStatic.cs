using Kayord.Pos.Entities;
using Riok.Mapperly.Abstractions;
using Kayord.Pos.DTO;


namespace Kayord.Pos.Features.TableOrder.GetBill;

[Mapper]
public static partial class MapperStatic
{
    public static partial IQueryable<BillOrderItemDTO> ProjectToDto(this IQueryable<OrderItem> q);
    public static partial IQueryable<OptionDTO> ProjectToDto(this IQueryable<Option> q);
    public static partial IQueryable<ExtraDTO> ProjectToDto(this IQueryable<Extra> q);

}