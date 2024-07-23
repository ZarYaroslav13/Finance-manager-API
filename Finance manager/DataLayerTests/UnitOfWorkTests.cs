using DataLayer;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DataLayerTests;

[TestClass]
public class UnitOfWorkTests
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private IUnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();

        options.UseInMemoryDatabase("TestDbForUnitOfWork");

        _context = new AppDbContext(options.Options);

        _unitOfWork = new UnitOfWork(_context);
    }

    [TestMethod]
    public void UnitOfWork_Constructor_Exception()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new UnitOfWork(null));
    }

    [TestMethod]
    public void UnitOfWork_GetRepository_Repository()
    {
        _context.AddRange(FillerBbData.Accounts);
        _context.SaveChanges();

        var result = _unitOfWork.GetRepository<Account>().GetAll().ToList();

        var expected = new Repository<Account>(_context).GetAll().ToList();

        Assert.IsTrue(Enumerable.SequenceEqual(expected, result));

        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void UnitOfWork_SaveChanges_Void()
    {
        var expected = FillerBbData.Accounts.GetRange(0,2);

        expected.ForEach(a => a.Wallets = null);

        _context.AddRange(expected);
        _unitOfWork.SaveChanges();

        var result = _unitOfWork.GetRepository<Account>().GetAll().ToList();

        Assert.IsTrue(Enumerable.SequenceEqual(expected, result));

        _context.Database.EnsureDeleted();
    }
}