using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class AdjustmentTypeConfiguration : IEntityTypeConfiguration<AdjustmentType>
{
    public void Configure(EntityTypeBuilder<AdjustmentType> builder)
    {
        builder.Property(t => t.AdjustmentTypeId).UseIdentityColumn();
    }
}