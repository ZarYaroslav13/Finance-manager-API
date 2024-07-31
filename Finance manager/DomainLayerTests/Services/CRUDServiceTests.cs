using AutoMapper;
using DataLayer;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Mapper.Profiles;
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
    private Repository<Account> _repository;

    private ICRUDService<AccountModel, Account> _service;

    public CRUDServiceTests()
    {
        _mapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.AddProfile<AccountProfile>();
                   cfg.AddProfile<WalletProfile>();
                   cfg.AddProfile<FinanceOperationProfile>();
                   cfg.AddProfile<FinanceOperationTypeProfile>();
               })
           .CreateMapper();

        var options = new DbContextOptionsBuilder<AppDbContext>();

        options.UseInMemoryDatabase("TestDbForServises");

        _context = new AppDbContext(options.Options);

        _unitOfWork = new UnitOfWork(_context);

        _repository = new(_context);

        _service = new CRUDService<AccountModel, Account>(_unitOfWork, _mapper);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    [DynamicData(nameof(CRUDServiceTestsDataProvider.ConstructorExceptions), typeof(CRUDServiceTestsDataProvider))]
    public void Constructor_NullValueOfConstructorArguments_ThrowsException(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new CRUDService<AccountModel, Account>(unitOfWork, mapper));
    }

    [TestMethod]
    public void GetAll_GettedExpectedAccounModelstList_AccountModelsList()
    {
        _context.AddRange(DbEntitiesTestDataProvider.Accounts);
        _context.SaveChanges();
        
        var expected = DbEntitiesTestDataProvider.Accounts
                    .Select(_mapper.Map<AccountModel>)
                    .ToList();

        CollectionAssert.AreEqual(expected, _service.GetAll());
    }

    [TestMethod]
    public void GetAllWithFilter_GettedExpectedAccounModelstList_FilteredAccountModelsList()
    {
        _context.AddRange(DbEntitiesTestDataProvider.Accounts.GetRange(0, 4));
        _context.SaveChanges();

        Expression<Func<Account, bool>> expression = a => a.Id > 3;

        var expected = DbEntitiesTestDataProvider.Accounts.GetRange(0, 4)
                    .Where(expression.Compile())
                    .Select(_mapper.Map<AccountModel>)
                    .ToList();

        CollectionAssert.AreEqual(expected, _service.GetAll(filter: expression));
    }

    [TestMethod]
    public void GetAllWithOrder_GettedExpectedOrderedByLastNameAccounModelstList_OrderedByLastNameAccountModelsList()
    {
        _context.AddRange(DbEntitiesTestDataProvider.Accounts);
        _context.SaveChanges();

        Expression<Func<Account, string>> orderby = (Account a) => a.LastName;

        var expected = DbEntitiesTestDataProvider.Accounts
                    .OrderBy(orderby.Compile())
                    .Select(_mapper.Map<AccountModel>)
                    .ToList();

        CollectionAssert.AreEqual(expected, _service.GetAll(orderBy: qa => qa.OrderBy(orderby)));
    }

    [TestMethod]
    public void Add_AddedExpectedNewAccount_Account()
    {
        var expected = _mapper
            .Map<AccountModel>(
                DbEntitiesTestDataProvider
                    .Accounts
                    .FirstOrDefault());

        _service.Add(expected);

        var result = _service
            .GetAll()
            .SingleOrDefault(a => a == expected);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Update_UpdatedNeededAccount_Account()
    {
        var account = _mapper
            .Map<AccountModel>(
                DbEntitiesTestDataProvider
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
    public void Delete_DeletedAccountIsNotExistInDatabase_Void()
    {
        _context.AddRange(DbEntitiesTestDataProvider.Accounts);
        _context.SaveChanges();

        const int idDeletedAccount = 3;

        var expected = DbEntitiesTestDataProvider.Accounts
                    .Where(a => a.Id != idDeletedAccount)
                    .Select(_mapper.Map<AccountModel>)
                    .ToList();

        var deletedAccount = _mapper.Map<AccountModel>(_repository.GetById(idDeletedAccount));

        _service.Delete(deletedAccount);

        var result = _service.GetAll();

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(CRUDServiceTestsDataProvider.DataForFindTests), typeof(CRUDServiceTestsDataProvider))]
    public void Find_WantedAndReceivedAccountsAreEqual_Account(List<Account> dbAccounts, AccountModel wantedAccount)
    {
        int idNeededAccount = wantedAccount.Id;

        _context.AddRange(dbAccounts);
        _context.SaveChanges();

        var result = _service.Find(idNeededAccount);

        Assert.AreEqual(wantedAccount, result);
    }
}
