using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration;

public class FinanceOperationConfiguration : IEntityTypeConfiguration<FinanceOperation>
{
    public void Configure(EntityTypeBuilder<FinanceOperation> builder)
    {
        builder.HasData(FillerBbData.Transactions);

        builder.HasOne(t => t.Type).
            WithMany(t => t.Transactions).
            HasForeignKey(t => t.TypeId);
    }
}
