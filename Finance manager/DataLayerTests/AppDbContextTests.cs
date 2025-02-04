using Infrastructure;
using InfractructureTests.Data;
using Microsoft.EntityFrameworkCore;

namespace InfractructureTests;

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
    public void TableInitialization_DbSetsNotNull_DbSet()
    {
        Assert.IsNotNull(_context.Accounts);
        Assert.IsNotNull(_context.Wallets);
        Assert.IsNotNull(_context.FinanceOperationTypes);
        Assert.IsNotNull(_context.FinanceOperations);
    }

    [TestMethod]
    public void UsedEntityConfiguration_RelatedEntitiesNotNull_RelatedEntities()
    {
        _context.Accounts.AddRange(EntitiesTestDataProvider.Accounts);
        _context.Wallets.AddRange(EntitiesTestDataProvider.Wallets);
        _context.FinanceOperationTypes.AddRange(EntitiesTestDataProvider.FinanceOperationTypes);
        _context.FinanceOperations.AddRange(EntitiesTestDataProvider.FinanceOperations);
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
