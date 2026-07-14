using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class RoleDivisionConfiguration : IEntityTypeConfiguration<RoleDivision>
{
    public void Configure(EntityTypeBuilder<RoleDivision> builder)
    {
        builder.HasIndex(e => new { e.RoleId, e.DivisionId }).IsUnique();
    }
}
