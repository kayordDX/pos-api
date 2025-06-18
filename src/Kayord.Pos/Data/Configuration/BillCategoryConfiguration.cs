using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class BillCategoryConfiguration : IEntityTypeConfiguration<BillCategory>
{
    public void Configure(EntityTypeBuilder<BillCategory> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();

    }
}
