using AutoMapper;
using DataLayer;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Infrastructure;
using DomainLayer.Models;
using DomainLayer.Services;
using DomainLayerTests.Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DomainLayerTests.Services;

[TestClass]
public class CRUDServiceTests
{
    private AppDbContext _context;
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private Repository<DataLayer.Models.Account> _repository;

    private ICRUDService<Account, DataLayer.Models.Account> _service;

    [TestInitialize]
    public void Setup()
    {
        _mapper = new MapperConfiguration(
                cfg =>
                    cfg.AddProfile<DomainDbMappingProfile>())
            .CreateMapper();

        var options = new DbContextOptionsBuilder<AppDbContext>();

        options.UseInMemoryDatabase("TestDbForServises");

        _context = new AppDbContext(options.Options);

        _unitOfWork = new UnitOfWork(_context);

        _repository = new(_context);

        _service = new CRUDService<Account, DataLayer.Models.Account>(_unitOfWork, _mapper);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    [DynamicData(nameof(CRUDServiceTestsDataProvider.ConstructorExceptions), typeof(CRUDServiceTestsDataProvider))]
    public void CRUDServiceTests_Constructor_Exception(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new CRUDService<Account, DataLayer.Models.Account>(unitOfWork, mapper));
    }

    [TestMethod]
    public void CRUDServiceTests_GetAll_AccountsList()
    {
        _context.AddRange(FillerBbData.Accounts.GetRange(0, 4));
        _context.SaveChanges();

        Expression<Func<DataLayer.Models.Account, bool>> expression = a => a.Id > 3;

        var expected = FillerBbData.Accounts.GetRange(0, 4)
                    .Select(_mapper.Map<Account>)
                    .ToList();

        Assert.IsTrue(Enumerable.SequenceEqual(expected, _service.GetAll()));

        expected = FillerBbData.Accounts.GetRange(0, 4)
                    .Where(expression.Compile())
                    .Select(_mapper.Map<Account>)
                    .ToList();

        Assert.IsTrue(Enumerable
                .SequenceEqual(expected
            , _service.GetAll(filter: expression)));
    }

    [TestMethod]
    public void CRUDServiceTests_Add_Account()
    {
        var expected = _mapper
            .Map<Account>(
                FillerBbData
                    .Accounts
                    .FirstOrDefault());

        _service.Add(expected);

        var result = _service
            .GetAll()
            .SingleOrDefault(a => a == expected);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CRUDServiceTests_Update_Account()
    {
        var account = _mapper
            .Map<Account>(
                FillerBbData
                    .Accounts
                    .FirstOrDefault());

        _service.Add(account);

        account.LastName = "New LastName";

        _service.Update(account);

        var result = _service
            .GetAll()
            .SingleOrDefault(a => a.Id == account.Id);

        Assert.AreEqual(account, result);
    }

    [TestMethod]
    public void CRUDServiceTests_Delete_Void()
    {
        _context.AddRange(FillerBbData.Accounts);
        _context.SaveChanges();

        const int idDeletedAccount = 3;

        var expected = FillerBbData.Accounts
                    .Where(a => a.Id != idDeletedAccount)
                    .Select(_mapper.Map<Account>)
                    .ToList();

        var deletedAccount = _mapper.Map<Account>(_repository.GetById(idDeletedAccount));

        _service.Delete(deletedAccount);

        var result = _service.GetAll();

        Assert.IsTrue(Enumerable
                .SequenceEqual(expected
            , result));
    }

    [TestMethod]
    public void CRUDServiceTests_Find_Account()
    {
        _context.AddRange(FillerBbData.Accounts);
        _context.SaveChanges();

        const int idNeededAccount = 3;

        var expected = _mapper
            .Map<Account>(
                FillerBbData.Accounts
                    .FirstOrDefault(a => a.Id == idNeededAccount));

        var result = _service.Find(idNeededAccount);

        Assert.AreEqual(expected, result);
    }
}
