using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.Features.MenuItem;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<MenuItemAdminDTO> ProjectToAdminDto(this IQueryable<Entities.MenuItem> q);
}