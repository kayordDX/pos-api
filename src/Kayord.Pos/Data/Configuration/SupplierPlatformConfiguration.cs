using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class SupplierPlatformConfiguration : IEntityTypeConfiguration<SupplierPlatform>
{
    public void Configure(EntityTypeBuilder<SupplierPlatform> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}