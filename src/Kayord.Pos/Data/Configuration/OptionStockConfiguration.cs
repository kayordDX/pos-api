using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class OptionStockConfiguration : IEntityTypeConfiguration<OptionStock>
{
    public void Configure(EntityTypeBuilder<OptionStock> builder)
    {
        builder
            .HasKey(k => new { k.OptionId, k.StockId });
    }
}