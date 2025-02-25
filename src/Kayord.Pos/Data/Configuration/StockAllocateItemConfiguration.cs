using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class StockAllocateItemConfiguration : IEntityTypeConfiguration<StockAllocateItem>
{
    public void Configure(EntityTypeBuilder<StockAllocateItem> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}