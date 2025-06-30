using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class OutletFeatureConfiguration : IEntityTypeConfiguration<OutletFeature>
{
    public void Configure(EntityTypeBuilder<OutletFeature> builder)
    {
        builder.HasKey(k => new { k.FeatureId, k.OutletId });
    }
}