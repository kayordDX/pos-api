using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class MenuItemStockConfiguration : IEntityTypeConfiguration<MenuItemStock>
{
    public void Configure(EntityTypeBuilder<MenuItemStock> builder)
    {
        builder
            .HasKey(k => new { k.MenuItemId, k.StockItemId });
    }
}