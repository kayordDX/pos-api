using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class InventoryOrderConfiguration : IEntityTypeConfiguration<InventoryOrder>
{
    public void Configure(EntityTypeBuilder<InventoryOrder> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}