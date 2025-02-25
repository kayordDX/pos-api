using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class StockItemAuditConfiguration : IEntityTypeConfiguration<StockItemAudit>
{
    public void Configure(EntityTypeBuilder<StockItemAudit> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}