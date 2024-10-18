using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class StockCategoryConfiguration : IEntityTypeConfiguration<StockCategory>
{
    public void Configure(EntityTypeBuilder<StockCategory> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}