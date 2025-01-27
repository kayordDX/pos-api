using Kayord.Pos.DTO;
using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.Features.Stock;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<StockDTO> ProjectToDto(this IQueryable<Entities.Stock> q);
}