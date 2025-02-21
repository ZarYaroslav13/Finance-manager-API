﻿using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfiguration;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder
            .HasData(FillerBbData.Wallets);

        builder
            .HasOne(w => w.Account)
            .WithMany(a => a.Wallets)
            .HasForeignKey(w => w.AccountId);
    }
}
