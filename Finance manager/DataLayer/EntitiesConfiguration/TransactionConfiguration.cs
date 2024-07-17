using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasData(FillerBbData.Transactions);

        builder.HasOne(t => t.Type).
            WithMany(t => t.Transactions).
            HasForeignKey(t => t.TypeId);
    }
}
