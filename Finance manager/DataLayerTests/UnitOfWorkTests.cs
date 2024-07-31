using DataLayer;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using DataLayerTests.Data;
using Microsoft.EntityFrameworkCore;

namespace DataLayerTests;

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
    public void GetRepository_ReturnedEntitiesFromRepositoryAreExpected_Repository()
    {
        _context.AddRange(EntitiesTestDataProvider.Accounts);
        _context.SaveChanges();
        var expected = EntitiesTestDataProvider.Accounts;
        expected.ForEach(a => a.Wallets = null);

        var result = _unitOfWork.GetRepository<Account>().GetAll().ToList();

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SaveChanges_NeededChangesAreSuccessfullySaved_Void()
    {
        var expected = EntitiesTestDataProvider.Accounts.GetRange(0, 2);
        expected.ForEach(a => a.Wallets = null);

        _context.AddRange(expected);
        _unitOfWork.SaveChanges();

        var result = _unitOfWork.GetRepository<Account>().GetAll().ToList();

        CollectionAssert.AreEqual(expected, result);
    }
}