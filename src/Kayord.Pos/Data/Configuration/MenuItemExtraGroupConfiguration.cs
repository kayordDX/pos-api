using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class MenuItemExtraGroupConfiguration : IEntityTypeConfiguration<MenuItemExtraGroup>
{
    public void Configure(EntityTypeBuilder<MenuItemExtraGroup> builder)
    {
        builder
         .HasKey(k => new { k.ExtraGroupId, k.MenuItemId });

        builder
            .HasOne(s => s.ExtraGroup)
            .WithMany(m => m.MenuItemExtraGroups)
            .HasForeignKey(e => e.ExtraGroupId);

        builder
           .HasOne(s => s.MenuItem)
           .WithMany(m => m.MenuItemExtraGroups)
           .HasForeignKey(e => e.MenuItemId);
    }
}