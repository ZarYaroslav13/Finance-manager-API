using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration;

public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
{
    public void Configure(EntityTypeBuilder<TransactionType> builder)
    {
        builder
            .HasData(FillerBbData.TransactionTypes);

        builder
            .HasOne(tt => tt.Wallet)
            .WithMany(w => w.TransactionTypes)
            .HasForeignKey(tt => tt.WalletId);
    }
}
