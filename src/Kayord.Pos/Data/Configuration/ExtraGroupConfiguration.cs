using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class ExtraGroupConfiguration : IEntityTypeConfiguration<ExtraGroup>
{
    public void Configure(EntityTypeBuilder<ExtraGroup> builder)
    {
        builder.Property(t => t.ExtraGroupId).UseIdentityColumn();
    }
}