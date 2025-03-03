using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class StockOrderConfiguration : IEntityTypeConfiguration<StockOrder>
{
    public void Configure(EntityTypeBuilder<StockOrder> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}