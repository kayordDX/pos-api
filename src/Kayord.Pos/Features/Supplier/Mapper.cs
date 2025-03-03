using Kayord.Pos.DTO;
using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.Features.Supplier;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<SupplierDTO> ProjectToDto(this IQueryable<Entities.Supplier> q);
}