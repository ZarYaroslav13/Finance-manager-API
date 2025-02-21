using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfiguration;

public class FinanceOperationTypeConfiguration : IEntityTypeConfiguration<FinanceOperationType>
{
    public void Configure(EntityTypeBuilder<FinanceOperationType> builder)
    {
        builder
            .HasData(FillerBbData.FinanceOperationTypes);

        builder
            .HasOne(tt => tt.Wallet)
            .WithMany(w => w.FinanceOperationTypes)
            .HasForeignKey(tt => tt.WalletId);
    }
}
