using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class OrderItemStatusAuditConfiguration : IEntityTypeConfiguration<OrderItemStatusAudit>
{
    public void Configure(EntityTypeBuilder<OrderItemStatusAudit> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}