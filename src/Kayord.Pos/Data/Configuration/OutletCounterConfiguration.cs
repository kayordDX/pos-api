using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class OutletCounterConfiguration : IEntityTypeConfiguration<OutletCounter>
{
    public void Configure(EntityTypeBuilder<OutletCounter> builder)
    {
        builder.HasKey(k => new { k.Id });
        builder.HasIndex(k => new { k.OutletId });
    }
}