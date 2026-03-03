using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class StockPeriodSnapshotConfiguration : IEntityTypeConfiguration<StockPeriodSnapshot>
{
    public void Configure(EntityTypeBuilder<StockPeriodSnapshot> builder)
    {
        builder.HasKey(k => k.StockItemId);
        builder.Property(t => t.StockItemId).ValueGeneratedNever();
    }
}
