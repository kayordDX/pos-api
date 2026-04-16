using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class TableBookingConfiguration : IEntityTypeConfiguration<TableBooking>
{
    public void Configure(EntityTypeBuilder<TableBooking> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
        builder.HasIndex(i => new { i.UserId, i.CloseDate });
    }
}
