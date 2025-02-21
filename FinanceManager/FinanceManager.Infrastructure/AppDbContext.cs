using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public virtual DbSet<Account> Accounts { get; set; } = default!;
    public virtual DbSet<Wallet> Wallets { get; set; } = default!;
    public virtual DbSet<FinanceOperation> FinanceOperations { get; set; } = default!;
    public virtual DbSet<FinanceOperationType> FinanceOperationTypes { get; set; } = default!;
    public virtual DbSet<Admin> Admins { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
