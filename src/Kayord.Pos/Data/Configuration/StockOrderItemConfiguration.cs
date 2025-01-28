using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class StockOrderItemConfiguration : IEntityTypeConfiguration<StockOrderItem>
{
    public void Configure(EntityTypeBuilder<StockOrderItem> builder)
    {
        builder.HasKey(k => new { k.StockOrderId, k.StockItemId });
    }
}