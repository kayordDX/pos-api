using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class UserOutletConfiguration : IEntityTypeConfiguration<UserOutlet>
{
    public void Configure(EntityTypeBuilder<UserOutlet> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
        builder.HasIndex(i => new { i.UserId, i.IsCurrent });
    }
}
