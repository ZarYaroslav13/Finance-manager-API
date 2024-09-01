using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using DomainLayerTests.Data.Services;
using FakeItEasy;          
using System.Linq.Expressions;

namespace DomainLayerTests.Services;

[TestClass]
public class AccountServiceTests
{
    private readonly IAccountService _service;
    public readonly IAdminService _adminService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Account> _repository;
    private readonly IMapper _mapper;

    public AccountServiceTests()
    {
        _adminService = A.Fake<IAdminService>();
        _repository = A.Fake<IRepository<Account>>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<IMapper>();

        A.CallTo(() => _unitOfWork.GetRepository<Account>()).Returns(_repository);

        _service = new AccountService(_adminService, _unitOfWork, _mapper);
    }

    [TestMethod]
    public void Constructor_AdminServiceNull_ThrowsException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AccountService(null, _unitOfWork, _mapper));
    }

    [TestMethod]
    public void AddAccountAsync_InstanceIdNotEqualZero_Throwexception()
    {
        AccountModel account = new() { Id = 1 };

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.AddAccountAsync(account));
    }

    [TestMethod]
    public void GetAccountsAsync_IncorrectAdminEmail_ThrowsUnauthorizedAccessException()
    {
        string email = "incorrectEmail";

        A.CallTo(() => _adminService.IsItAdmin(email)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _service.GetAccountsAsync(email));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.GetAccountsSkipOrTakeNegativeTestData), typeof(AccountServiceTestsDataProvider))]
    public void GetAccountsAsync_WithNegativeSkipOrTake_ThrowsArgumentException(int skip, int take)
    {
        string adminEmail = "email";

        A.CallTo(() => _adminService.IsItAdmin(adminEmail)).Returns(true);

        Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.GetAccountsAsync(adminEmail, skip, take));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.GetAccountsValidInputsTestData), typeof(AccountServiceTestsDataProvider))]
    public async Task GetAccountsAsync_ValidInputs_ReturnsExpectedNumberMappedAccountModels(List<Account> accounts, int skip, int take)
    {
        string adminEmail = "email";

        A.CallTo(() => _adminService.IsItAdmin(adminEmail)).Returns(true);

        A.CallTo(() => _repository.GetAllAsync(
                A<Func<IQueryable<Account>,
                 IOrderedQueryable<Account>>>._,
                 A<Expression<Func<Account, bool>>>._,
                 skip, take,
                 A<string[]>._))
            .Returns(accounts);

        A.CallTo(() => _mapper.Map<AccountModel>(A<Account>.Ignored))
            .Returns(new());


        var result = await _service.GetAccountsAsync(adminEmail, skip, take);


        Assert.AreEqual(accounts.Count, result.Count);

        A.CallTo(() => _repository.GetAllAsync(
                A<Func<IQueryable<Account>,
                 IOrderedQueryable<Account>>>._,
                 A<Expression<Func<Account, bool>>>._,
                 skip, take,
                 A<string[]>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mapper.Map<AccountModel>(A<Account>.Ignored)).MustHaveHappened();
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.GetAccountsNoSkipOrTakeTestData), typeof(AccountServiceTestsDataProvider))]
    public async Task GetAccountsAsync_NoSkipOrTake_ReturnsAllMappedAccountModels(List<Account> accounts)
    {
        string adminEmail = "email";

        A.CallTo(() => _adminService.IsItAdmin(adminEmail)).Returns(true);

        A.CallTo(() => _repository.GetAllAsync(
                A<Func<IQueryable<Account>,
                 IOrderedQueryable<Account>>>._,
                 A<Expression<Func<Account, bool>>>._,
                 0, 0,
                 A<string[]>._))
            .Returns(accounts);


        var result = await _service.GetAccountsAsync(adminEmail);


        Assert.AreEqual(accounts.Count, result.Count);

        A.CallTo(() => _repository.GetAllAsync(
                A<Func<IQueryable<Account>,
                 IOrderedQueryable<Account>>>._,
                 A<Expression<Func<Account, bool>>>._,
                 0, 0,
                 A<string[]>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mapper.Map<AccountModel>(A<Account>.Ignored)).MustHaveHappened(accounts.Count, Times.Exactly);
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.AddAccountTestData), typeof(AccountServiceTestsDataProvider))]
    public async Task AddAccount_ServiceInvokeMethodInsertByRepository_AccountModel(AccountModel modelForAdding, Account accountForRepository)
    {
        A.CallTo(() => _mapper.Map<Account>(modelForAdding)).Returns(accountForRepository);
        A.CallTo(() => _repository.Insert(accountForRepository)).Returns(accountForRepository);
        A.CallTo(() => _mapper.Map<AccountModel>(accountForRepository)).Returns(modelForAdding);

        var result = await _service.AddAccountAsync(modelForAdding);

        A.CallTo(() => _repository.Insert(accountForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public async Task UpdateAccountAsync_InstanceIdEqualZero_ThrowsException()
    {
        AccountModel account = new() { Id = 0 };

        await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _service.UpdateAccountAsync(account));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.UpdateAccountTestData), typeof(AccountServiceTestsDataProvider))]
    public async Task UpdateAccountAsync_ServiceInvokeMethodUpdateByRepository_AccountModel(AccountModel modelForUpdate, Account accountForRepository)
    {
        A.CallTo(() => _mapper.Map<Account>(modelForUpdate)).Returns(accountForRepository);
        A.CallTo(() => _repository.UpdateAsync(accountForRepository)).Returns(accountForRepository);
        A.CallTo(() => _mapper.Map<AccountModel>(accountForRepository)).Returns(modelForUpdate);

        var result = await _service.UpdateAccountAsync(modelForUpdate);

        A.CallTo(() => _repository.UpdateAsync(accountForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForUpdate, result);
    }

    [TestMethod]
    public void DeleteAccountWithId_ServiceInvokeMethodDeleteByRepository_Void()
    {
        const int idAccountForDelete = 2;

        _service.DeleteAccountWithId(idAccountForDelete);

        A.CallTo(() => _repository.Delete(idAccountForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.TryLogInThrowsNullExceptionTestData), typeof(AccountServiceTestsDataProvider))]
    public void TryLogIn_ArgumentsAreNull_ThrowNullException(string email, string password)
    {
        Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service.TryLogInAsync(email, password));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.TryLogInThrowsExceptionTestData), typeof(AccountServiceTestsDataProvider))]
    public void TryLogIn_ArgumentsAreWhiteSpace_ThrowException(string email, string password)
    {
        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.TryLogInAsync(email, password));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.TryLogInAccountWithEmailAndPasswordExistTestData), typeof(AccountServiceTestsDataProvider))]
    public async Task TryLogInAsync_AccountWithEmailAndPasswordExistInDatabase_AccountModel(List<Account> dbAccounts, Account expectedAccountFromDb, string password)
    {
        AccountModel accountModel = new()
        {
            Id = expectedAccountFromDb.Id,
            LastName = expectedAccountFromDb.LastName,
            FirstName = expectedAccountFromDb.FirstName,
            Email = expectedAccountFromDb.Email,
            Password = expectedAccountFromDb.Password
        };

        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Account>, IOrderedQueryable<Account>>>._,
            A<Expression<Func<Account, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(dbAccounts);
        A.CallTo(() => _mapper.Map<AccountModel>(expectedAccountFromDb)).Returns(accountModel);

        var result = await _service.TryLogInAsync(expectedAccountFromDb.Email, password);

        Assert.AreEqual(accountModel, result);
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.TryLogInAccountWithEmailAndPasswordNotExistTestData), typeof(AccountServiceTestsDataProvider))]
    public async Task TryLogIn_AccountWithEmailAndPasswordNotExistInDatabase_AccountModel(List<Account> dbAccounts, string email, string password)
    {
        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Account>, IOrderedQueryable<Account>>>._,
            A<Expression<Func<Account, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(dbAccounts);
        A.CallTo(() => _mapper.Map<AccountModel>(null)).Returns(null);

        var result = await _service.TryLogInAsync(email, password);

        Assert.IsNull(result);
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.IsItEmailReturnTrueTestData), typeof(AccountServiceTestsDataProvider))]
    public void IsItEmail_EnteredStringIsEmail_True(string str)
    {
        Assert.IsTrue(_service.IsItEmail(str));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.IsItEmailReturnFalseTestData), typeof(AccountServiceTestsDataProvider))]
    public void IsItEmail_EnteredStringIsNotEmail_False(string str)
    {
        Assert.IsFalse(_service.IsItEmail(str));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.CanTakeThisEmailReturnTrueTestData), typeof(AccountServiceTestsDataProvider))]
    public async Task CanTakeThisEmailAsync_EmailIsValidAndNotExistInDatabase_True(List<Account> accounts, string email)
    {
        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Account>, IOrderedQueryable<Account>>>._,
            A<Expression<Func<Account, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(accounts);

        Assert.IsTrue(await _service.CanTakeThisEmailAsync(email));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.IsItEmailReturnFalseTestData), typeof(AccountServiceTestsDataProvider))]
    public void CanTakeThisEmailAsync_EmailIsNotValid_ThrowsFormatException(string email)
    {
        Assert.ThrowsExceptionAsync<FormatException>(() => _service.CanTakeThisEmailAsync(email));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.CanTakeThisEmailThrowsArgumentExceptionTestData), typeof(AccountServiceTestsDataProvider))]
    public void CanTakeThisEmailAsync_EmailExistInDatabase_ThrowsArgumentException(List<Account> accounts, string email)
    {
        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Account>, IOrderedQueryable<Account>>>._,
            A<Expression<Func<Account, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(accounts);

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.CanTakeThisEmailAsync(email));
    }
}
