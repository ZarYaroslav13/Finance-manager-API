﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration;

public class FinanceOperationConfiguration : IEntityTypeConfiguration<FinanceOperation>
{
    public void Configure(EntityTypeBuilder<FinanceOperation> builder)
    {
        builder.HasData(FillerBbData.FinanceOperations);

        builder.HasOne(t => t.Type).
            WithMany(t => t.FinanceOperations).
            HasForeignKey(t => t.TypeId);
    }
}
