using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests;

[TestClass]
public class AppDbContextTests
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public AppDbContextTests()
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDbForContext"));

        _serviceProvider = services.BuildServiceProvider();

        _context = _serviceProvider.GetRequiredService<AppDbContext>();
    }

    [TestMethod]
    public void AppDbContext_AreNeededTables_DbSet()
    {
        Assert.IsNotNull(_context.Accounts);
        Assert.IsNotNull(_context.Wallets);
        Assert.IsNotNull(_context.TransactionsType);
        Assert.IsNotNull(_context.Transactions);
    }

    [TestMethod]
    public void AppDbContext_UsedEntityConfiguration_Entities()
    {
        _context.Accounts.AddRange(FillerBbData.Accounts);
        _context.Wallets.AddRange(FillerBbData.Wallets);
        _context.TransactionsType.AddRange(FillerBbData.TransactionTypes);
        _context.Transactions.AddRange(FillerBbData.Transactions);
        _context.SaveChanges();

        var accounts = _context.Accounts.
                            AsQueryable().
                            Include(a => a.Wallets).
                            ThenInclude(w => w.TransactionTypes).
                            ThenInclude(tt => tt.Transactions).
                            AsNoTracking().
                            ToList();
        var account = accounts.FirstOrDefault();
        var wallet = account.Wallets.FirstOrDefault();
        var t = wallet.Transactions;

        Assert.IsNotNull(wallet);
        Assert.IsNotNull(wallet.Transactions);
        Assert.IsNotNull(wallet.TransactionTypes);
    }

}
