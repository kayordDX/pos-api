using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class OutletExtraGroupConfiguration : IEntityTypeConfiguration<OutletExtraGroup>
{
    public void Configure(EntityTypeBuilder<OutletExtraGroup> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}