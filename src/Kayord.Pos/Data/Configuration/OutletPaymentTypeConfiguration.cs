using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class OutletPaymentTypeConfiguration : IEntityTypeConfiguration<OutletPaymentType>
{
    public void Configure(EntityTypeBuilder<OutletPaymentType> builder)
    {
        builder
         .HasKey(k => new { k.OutletId, k.PaymentTypeId });

        builder
            .HasOne(s => s.Outlet)
            .WithMany(m => m.OutletPaymentTypes)
            .HasForeignKey(e => e.OutletId);

        builder
           .HasOne(s => s.PaymentType)
           .WithMany(m => m.OutletPaymentTypes)
           .HasForeignKey(e => e.PaymentTypeId);
    }
}