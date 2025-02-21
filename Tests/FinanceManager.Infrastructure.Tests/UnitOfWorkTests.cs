using Infrastructure;
using Infrastructure.Models;
using Infrastructure.UnitOfWork;
using FinanceManager.Infrastructure.Tests.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Tests;

[TestClass]
public class UnitOfWorkTests
{
    private readonly AppDbContext _context;
    private IUnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();

        options.UseInMemoryDatabase("TestDbForUnitOfWork");

        _context = new AppDbContext(options.Options);

        _unitOfWork = new UnitOfWork(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    public void Constructor_DbContextIsNull_ThrowsException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(null));
    }

    [TestMethod]
    public async Task GetRepository_ReturnedEntitiesFromRepositoryAreExpected_Repository()
    {
        await _context.AddRangeAsync(EntitiesTestDataProvider.Accounts);
        await _context.SaveChangesAsync();
        var expected = EntitiesTestDataProvider.Accounts;
        expected.ForEach(a => a.Wallets = null);

        var result = await _unitOfWork.GetRepository<Account>().GetAllAsync();

        CollectionAssert.AreEqual(expected, result.ToList());
    }

    [TestMethod]
    public async Task SaveChangesAsync_NeededChangesAreSuccessfullySaved_Void()
    {
        var expected = EntitiesTestDataProvider.Accounts.GetRange(0, 2);
        expected.ForEach(a => a.Wallets = null);

        await _context.AddRangeAsync(expected);
        await _unitOfWork.SaveChangesAsync();

        var result = await _unitOfWork.GetRepository<Account>().GetAllAsync();

        CollectionAssert.AreEqual(expected, result.ToList());
    }
}