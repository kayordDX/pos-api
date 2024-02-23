using Kayord.Pos.Entities;
using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.DTO;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<MenuItemDTO> ProjectToDto(this IQueryable<MenuItem> q);
    public static partial IQueryable<ExtraDTO> ProjectToDto(this IQueryable<Extra> q);
    public static partial IQueryable<OptionDTO> ProjectToDto(this IQueryable<Option> q);
    public static partial IQueryable<OrderItemOptionDTO> ProjectToDto(this IQueryable<OrderItemOption> q);
    public static partial IQueryable<OrderItemExtraDTO> ProjectToDto(this IQueryable<OrderItemExtra> q);
    public static partial IQueryable<OrderItemDTO> ProjectToDto(this IQueryable<OrderItem> q);
    public static partial IQueryable<MenuItemDTOBasic> ProjectToBasicDto(this IQueryable<MenuItem> q);
    public static partial IQueryable<MenuItemOptionGroupDTO> ProjectToDto(this IQueryable<MenuItemOptionGroup> q);
    public static partial IQueryable<OptionGroupDTO> ProjectToDto(this IQueryable<OptionGroup> q);
    public static partial IQueryable<OptionGroupBasicDTO> ProjectToBasicDto(this IQueryable<OptionGroup> q);
    public static partial IQueryable<MenuItemExtraGroupDTO> ProjectToDto(this IQueryable<MenuItemExtraGroup> q);
    public static partial IQueryable<ExtraGroupDTO> ProjectToDto(this IQueryable<ExtraGroup> q);
    public static partial IQueryable<ExtraGroupBasicDTO> ProjectToBasicDto(this IQueryable<ExtraGroup> q);
    public static partial IQueryable<MenuSectionDTO> ProjectToDto(this IQueryable<MenuSection> q);
    public static partial IQueryable<TableBookingDTO> ProjectToDto(this IQueryable<TableBooking> q);
    public static partial IQueryable<UserDTO> ProjectToDto(this IQueryable<User> q);
}