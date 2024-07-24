using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayerTests;

[TestClass]
public class AppDbContextTests
{
    private readonly AppDbContext _context;

    public AppDbContextTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();

        options.UseInMemoryDatabase("TestDbForContext");

        _context = new AppDbContext(options.Options);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    public void AppDbContext_AreNeededTables_DbSet()
    {
        Assert.IsNotNull(_context.Accounts);
        Assert.IsNotNull(_context.Wallets);
        Assert.IsNotNull(_context.FinanceOperationTypes);
        Assert.IsNotNull(_context.FinanceOperations);
    }

    [TestMethod]
    public void AppDbContext_UsedEntityConfiguration_Entities()
    {
        _context.Accounts.AddRange(FillerBbData.Accounts);
        _context.Wallets.AddRange(FillerBbData.Wallets);
        _context.FinanceOperationTypes.AddRange(FillerBbData.FinanceOperationTypes);
        _context.FinanceOperations.AddRange(FillerBbData.FinanceOperations);
        _context.SaveChanges();

        var accounts = _context.Accounts.
                            AsQueryable().
                            Include(a => a.Wallets).
                            ThenInclude(w => w.FinanceOperationTypes).
                            ThenInclude(tt => tt.FinanceOperations).
                            AsNoTracking().
                            ToList();
        var account = accounts.FirstOrDefault();
        var wallet = account.Wallets.FirstOrDefault();
        var t = wallet.GetFinanceOperations();

        Assert.IsNotNull(wallet);
        Assert.IsNotNull(wallet.GetFinanceOperations());
        Assert.IsNotNull(wallet.FinanceOperationTypes);
    }

}
