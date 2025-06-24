using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kayord.Pos.Data.Configuration;

public class CashUpUserItemTypeConfiguration : IEntityTypeConfiguration<CashUpUserItemType>
{
    public void Configure(EntityTypeBuilder<CashUpUserItemType> builder)
    {
        builder.Property(t => t.Id).UseIdentityColumn();
    }
}