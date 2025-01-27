using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Repository;
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
    public async Task Cleanup()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.DisposeAsync();
    }

    [TestMethod]
    public void Constructor_DbContextIsNull_ThrowsException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new Repository<Account>(null));
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.GetAllIntArgumentsAreLessThenZeroTestData), typeof(RepositoryDataProvider))]
    public async Task GetAllAsync_IntArgumentsAreLessThenZero_ThrowException(int skip, int take)
    {
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _repository.GetAllAsync(take: take, skip: skip));
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.OrderedAccountListForGetAll), typeof(RepositoryDataProvider))]
    public async Task GetAllAsync_AccountsWithWalletListIsOrderedByLastName_OrderedAccountListWithWallets(List<Account> expectedOrderedAccountsList)
    {
        await _context.AddRangeAsync(EntitiesTestDataProvider.Accounts);
        await _context.AddRangeAsync(EntitiesTestDataProvider.Wallets);
        await _context.SaveChangesAsync();

        var resultOrderedAccountsList = await _repository.GetAllAsync(includeProperties: nameof(Account.Wallets),
                                           orderBy: qa => qa.OrderBy(a => a.LastName));

        CollectionAssert.AreEqual(expectedOrderedAccountsList, resultOrderedAccountsList.ToList());
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.AccountsWithIdMoreThen3ListForGetAll), typeof(RepositoryDataProvider))]
    public async Task GetAllAsync_AccountListIsFilteredById_FilteredAccountList(List<Account> expectedFilteredAccountList)
    {
        Expression<Func<Account, bool>> predicate = (ac) => ac.Id > 3;

        await _context.AddRangeAsync(EntitiesTestDataProvider.Accounts);
        await _context.SaveChangesAsync();

        var resultFilteredAccountList = await _repository.GetAllAsync(filter: predicate);

        CollectionAssert.AreEqual(
            expectedFilteredAccountList
                .OrderBy(a => a.Id).ToList(),
            resultFilteredAccountList
                .OrderBy(a => a.Id).ToList());
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.GetAllWithSkipAndTakeTestData), typeof(RepositoryDataProvider))]
    public async Task GetAllAsync_WithSkipAndTake_ReceivedExpectedAccountList_AccountList(List<Account> accounts, List<Account> expectedAccountList, int skip, int take)
    {
        await _context.AddRangeAsync(accounts);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync(skip: skip, take: take);

        CollectionAssert.AreEqual(expectedAccountList, result.ToList());
    }

    [TestMethod]
    public async Task Insert_AddedNewAccountToDatabase_NewAccount()
    {
        var newAccount = new Account()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "Email",
            Password = "Password"
        };

        _repository.Insert(newAccount);
        await _context.SaveChangesAsync();

        var accounts = await _repository.GetAllAsync();

        Assert.IsTrue(accounts.Any(a => a.Equals(newAccount)));
    }

    [TestMethod]
    public async Task UpdateAsync_AccountAfterUpdatingIsChanged_UpdatedAccount()
    {
        var account = new Account()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "Email",
            Password = "Password"
        };

        _repository.Insert(account);
        await _context.SaveChangesAsync();

        account.FirstName = "New FirstName";
        _repository.Update(account);
        await _context.SaveChangesAsync();

        var updatedAccount = (await _repository.GetAllAsync()).LastOrDefault();

        Assert.AreEqual(account, updatedAccount);
    }

    [TestMethod]
    public async Task Delete_AccountDoesNotExistInDatabase_Void()
    {
        await _context.AddRangeAsync(EntitiesTestDataProvider.Accounts);
        await _context.SaveChangesAsync();

        var removedAccount = EntitiesTestDataProvider.Accounts[3];

        _repository.Delete(removedAccount.Id);
        await _context.SaveChangesAsync();

        var accounts = await _repository.GetAllAsync();

        Assert.IsFalse(accounts.Contains(removedAccount));
    }

    [TestMethod]
    [DynamicData(nameof(RepositoryDataProvider.AccountWithIdEqual2ForGetById), typeof(RepositoryDataProvider))]
    public async Task GetByIdAsync_GettedAccountWithNeededId_Account(Account expectedAccount)
    {
        await _context.AddRangeAsync(EntitiesTestDataProvider.Accounts);
        await _context.SaveChangesAsync();

        var foundAccount = await _repository.GetByIdAsync(expectedAccount.Id);

        foundAccount.Wallets = null;

        Assert.AreEqual(expectedAccount, foundAccount);
    }

    [TestMethod]
    public void GetByIdAsync_AccountWithIdDontExist_ThrowsArgumentExceptin()
    {
        int id = 0;

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _repository.GetByIdAsync(id));
    }
}
