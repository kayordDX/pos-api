using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class ExtraConfiguration : IEntityTypeConfiguration<Extra>
{
    public void Configure(EntityTypeBuilder<Extra> builder)
    {
        builder.Property(t => t.ExtraId).UseIdentityColumn();
    }
}