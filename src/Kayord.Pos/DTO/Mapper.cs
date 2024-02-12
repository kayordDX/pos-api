using Kayord.Pos.Entities;
using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.DTO;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<MenuItemDTO> ProjectToDto(this IQueryable<MenuItem> q);
    public static partial IQueryable<MenuItemDTOBasic> ProjectToBasicDto(this IQueryable<MenuItem> q);    
    public static partial IQueryable<MenuItemOptionGroupDTO> ProjectToDto(this IQueryable<MenuItemOptionGroup> q);
    public static partial IQueryable<OptionDTO> ProjectToDto(this IQueryable<Option> q);
    public static partial IQueryable<OptionGroupDTO> ProjectToDto(this IQueryable<OptionGroup> q);
    public static partial IQueryable<MenuSectionDTO> ProjectToDto(this IQueryable<MenuSection> q);
}