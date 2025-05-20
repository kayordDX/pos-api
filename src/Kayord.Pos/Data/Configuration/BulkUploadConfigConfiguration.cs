using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class BulkUploadConfigConfiguration : IEntityTypeConfiguration<BulkUploadConfig>
{
    public void Configure(EntityTypeBuilder<BulkUploadConfig> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}