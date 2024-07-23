using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class AppDbContext : DbContext
{
    public virtual DbSet<Account> Accounts { get; set; } = default!;
    public virtual DbSet<Wallet> Wallets { get; set; } = default!;
    public virtual DbSet<FinanceOperation> FinanceOperations { get; set; } = default!;
    public virtual DbSet<FinanceOperationType> FinanceOperationTypes { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        //modelBuilder.ApplyConfiguration(new AccountConfiguration());
        //modelBuilder.ApplyConfiguration(new WalletConfiguration());
        //modelBuilder.ApplyConfiguration(new FinanceOperationConfiguration());
        //modelBuilder.ApplyConfiguration(new FinanceOperationTypeConfiguration());
    }
}
