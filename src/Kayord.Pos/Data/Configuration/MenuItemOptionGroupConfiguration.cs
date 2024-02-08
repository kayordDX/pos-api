using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class MenuItemOptionGroupConfiguration : IEntityTypeConfiguration<MenuItemOptionGroup>
{
    public void Configure(EntityTypeBuilder<MenuItemOptionGroup> builder)
    {
        builder
         .HasKey(k => new { k.OptionGroupId, k.MenuItemId });

        builder
            .HasOne(s => s.OptionGroup)
            .WithMany(m => m.MenuItemOptionGroups)
            .HasForeignKey(e => e.OptionGroupId);

        builder
           .HasOne(s => s.MenuItem)
           .WithMany(m => m.MenuItemOptionGroups)
           .HasForeignKey(e => e.MenuItemId);
    }
}