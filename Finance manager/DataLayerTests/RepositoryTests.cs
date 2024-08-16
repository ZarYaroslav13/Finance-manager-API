using DataLayer;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayerTests.Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayerTests;

[TestClass]
public class RepositoryTests
{
    private readonly AppDbContext _context;
    private readonly IRepository<Account> _repository;

    public RepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();

        options.UseInMemoryDatabase("TestDbForRepository");

        _context = new AppDbContext(options.Options);

        _repository = new Repository<Account>(_context);
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
        Assert.ThrowsException<ArgumentNullException>(() => new Repository<Account>(null));
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.GetAllIntArgumentsAreLessThenZeroTestData), typeof(RepositoryDataProvider))]
    public void GetAll_IntArgumentsAreLessThenZero_ThrowException(int skip, int take)
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repository.GetAll(take: take, skip: skip));
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.OrderedAccountListForGetAll), typeof(RepositoryDataProvider))]
    public void GetAll_AccountsWithWalletListIsOrderedByLastName_OrderedAccountListWithWallets(List<Account> expectedOrderedAccountsList)
    {
        _context.AddRange(EntitiesTestDataProvider.Accounts);
        _context.AddRange(EntitiesTestDataProvider.Wallets);
        _context.SaveChanges();

        var resultOrderedAccountsList = _repository.GetAll(includeProperties: nameof(Account.Wallets),
                                           orderBy: qa => qa.OrderBy(a => a.LastName))
                                  .ToList();

        CollectionAssert.AreEqual(expectedOrderedAccountsList, resultOrderedAccountsList);
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.AccountsWithIdMoreThen3ListForGetAll), typeof(RepositoryDataProvider))]
    public void GetAll_AccountListIsFilteredById_FilteredAccountList(List<Account> expectedFilteredAccountList)
    {
        Expression<Func<Account, bool>> predicate = (ac) => ac.Id > 3;

        _context.AddRange(EntitiesTestDataProvider.Accounts);
        _context.SaveChanges();

        var resultFilteredAccountList = _repository.GetAll(filter: predicate).ToList();

        CollectionAssert.AreEqual(
            expectedFilteredAccountList
                .OrderBy(a => a.Id).ToList(),
            resultFilteredAccountList
                .OrderBy(a => a.Id).ToList());
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.GetAllWithSkipAndTakeTestData), typeof(RepositoryDataProvider))]
    public void GetAll_WithSkipAndTake_ReceivedExpectedAccountList_AccountList(List<Account> accounts, List<Account> expectedAccountList, int skip, int take)
    {
        _context.AddRange(accounts);
        _context.SaveChanges();

        var result = _repository.GetAll(skip: skip, take: take).ToList();

        CollectionAssert.AreEqual(expectedAccountList, result);
    }

    [TestMethod]
    public void Insert_AddedNewAccountToDatabase_NewAccount()
    {
        var newAccount = new Account()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "Email",
            Password = "Password"
        };

        _repository.Insert(newAccount);
        _context.SaveChanges();

        var accounts = _repository.GetAll();

        Assert.IsTrue(accounts.Any(a => a.Equals(newAccount)));
    }

    [TestMethod]
    public void Update_AccountAftetUpdatingIsChanged_UpdatedAccount()
    {
        var account = new Account()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "Email",
            Password = "Password"
        };

        _repository.Insert(account);
        _context.SaveChanges();

        account.FirstName = "New FirstName";
        _repository.Update(account);
        _context.SaveChanges();

        var updatedAccount = _repository.GetAll().LastOrDefault();

        Assert.AreEqual(account, updatedAccount);
    }

    [TestMethod]
    public void Delete_AccountDontExistsInDatabase_Void()
    {
        _context.AddRange(EntitiesTestDataProvider.Accounts);
        _context.SaveChanges();

        var removedAccount = EntitiesTestDataProvider.Accounts[3];

        _repository.Delete(removedAccount.Id);
        _context.SaveChanges();

        var accounts = _repository.GetAll();

        Assert.IsFalse(accounts.Contains(removedAccount));
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.AccountWithIdEqual2ForGetById), typeof(RepositoryDataProvider))]
    public void GetById_GettedAccountWithNeededId_Account(Account expectedAccount)
    {
        _context.AddRange(EntitiesTestDataProvider.Accounts);
        _context.SaveChanges();

        var foundAccount = _repository.GetById(expectedAccount.Id);

        foundAccount.Wallets = null;

        Assert.AreEqual(expectedAccount, foundAccount);
    }
}
