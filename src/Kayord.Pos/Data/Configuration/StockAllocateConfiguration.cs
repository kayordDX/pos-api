using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class StockAllocateConfiguration : IEntityTypeConfiguration<StockAllocate>
{
    public void Configure(EntityTypeBuilder<StockAllocate> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();


        // builder
        //     .HasOne(s => s.ToOutlet)
        //     .WithMany()
        //     .HasForeignKey(e => e.OutletId);

        // builder
        //    .HasOne(s => s.PaymentType)
        //    .WithMany()
        //    .HasForeignKey(e => e.PaymentTypeId);
    }
}