using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class MenuItemBulkStockConfiguration : IEntityTypeConfiguration<MenuItemBulkStock>
{
    public void Configure(EntityTypeBuilder<MenuItemBulkStock> builder)
    {
        builder
            .HasKey(k => new { k.MenuItemId, k.StockId });
    }
}