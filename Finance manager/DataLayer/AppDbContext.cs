using DataLayer.EntitiesConfiguration;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer;

public class AppDbContext : DbContext
{
    public virtual DbSet<Account> Accounts { get; set; } = default!;
    public virtual DbSet<Wallet> Wallets { get; set; } = default!;
    public virtual DbSet<Transaction> Transactions { get; set; } = default!;
    public virtual DbSet<TransactionType> TransactionsType { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new WalletConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
    }
}
