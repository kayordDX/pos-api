using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class AdjustmentTypeOutletConfiguration : IEntityTypeConfiguration<AdjustmentTypeOutlet>
{
    public void Configure(EntityTypeBuilder<AdjustmentTypeOutlet> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}