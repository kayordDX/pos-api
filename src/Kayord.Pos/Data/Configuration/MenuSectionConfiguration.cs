using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class MenuSectionConfiguration : IEntityTypeConfiguration<MenuSection>
{
    public void Configure(EntityTypeBuilder<MenuSection> builder)
    {
        builder.Property(t => t.MenuSectionId).UseIdentityColumn();
        builder
            .HasOne(s => s.Parent)
            .WithMany(m => m.SubMenuSections)
            .HasForeignKey(e => e.ParentId);
    }
}