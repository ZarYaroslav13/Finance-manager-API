using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using DomainLayerTests.Data.Services;
using FakeItEasy;
using System.Linq.Expressions;

namespace DomainLayerTests.Services;

[TestClass]
public class AccountServiceTests
{
    private readonly IAccountService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Account> _repository;
    private readonly IMapper _mapper;

    public AccountServiceTests()
    {
        _repository = A.Fake<IRepository<Account>>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<IMapper>();

        A.CallTo(() => _unitOfWork.GetRepository<Account>()).Returns(_repository);

        _service = new AccountService(_unitOfWork, _mapper);
    }

    [TestMethod]
    public void AddAccount_InstanceIdNotEqualZero_Throwexception()
    {
        AccountModel account = new() { Id = 1 };

        Assert.ThrowsException<ArgumentException>(() => _service.AddAccount(account));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.AddAccountTestData), typeof(AccountServiceTestsDataProvider))]
    public void AddAccount_ServiceInvokeMethodInsertByRepository_AccountModel(AccountModel modelForAdding, Account accountForRepository)
    {
        A.CallTo(() => _mapper.Map<Account>(modelForAdding)).Returns(accountForRepository);
        A.CallTo(() => _repository.Insert(accountForRepository)).Returns(accountForRepository);
        A.CallTo(() => _mapper.Map<AccountModel>(accountForRepository)).Returns(modelForAdding);

        var result = _service.AddAccount(modelForAdding);

        A.CallTo(() => _repository.Insert(accountForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public void UpdateAccount_InstanceIdEqualZero_Throwexception()
    {
        AccountModel account = new() { Id = 0 };

        Assert.ThrowsException<ArgumentException>(() => _service.UpdateAccount(account));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.UpdateAccountTestData), typeof(AccountServiceTestsDataProvider))]
    public void UpdateAccount_ServiceInvokeMethodUpdateByRepository_AccountModel(AccountModel modelForUpdate, Account accountForRepository)
    {
        A.CallTo(() => _mapper.Map<Account>(modelForUpdate)).Returns(accountForRepository);
        A.CallTo(() => _repository.Update(accountForRepository)).Returns(accountForRepository);
        A.CallTo(() => _mapper.Map<AccountModel>(accountForRepository)).Returns(modelForUpdate);

        var result = _service.UpdateAccount(modelForUpdate);

        A.CallTo(() => _repository.Update(accountForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForUpdate, result);
    }

    [TestMethod]
    public void DeleteAccountWithId_ServiceInvokeMethodDeleteByRepository_Void()
    {
        const int idAccountForDelete = 2;

        _service.DeleteAccountWithId(idAccountForDelete);

        A.CallTo(() => _repository.Delete(idAccountForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.TryLogInThrowsNullExceptionTestData), typeof(AccountServiceTestsDataProvider))]
    public void TryLogIn_ArgumentsAreNull_ThrowNullException(string email, string password)
    {
        Assert.ThrowsException<ArgumentNullException>(() => _service.TryLogIn(email, password));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.TryLogInThrowsExceptionTestData), typeof(AccountServiceTestsDataProvider))]
    public void TryLogIn_ArgumentsAreWhiteSpace_ThrowException(string email, string password)
    {
        Assert.ThrowsException<ArgumentException>(() => _service.TryLogIn(email, password));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.TryLogInAccountWithEmailAndPasswordExistTestData), typeof(AccountServiceTestsDataProvider))]
    public void TryLogIn_AccountWithEmailAndPasswordExistInDatabase_AccountModel(List<Account> dbAccounts, Account expectedAccountFromDb, string password)
    {
        AccountModel accountModel = new()
        {
            Id = expectedAccountFromDb.Id,
            LastName = expectedAccountFromDb.LastName,
            FirstName = expectedAccountFromDb.FirstName,
            Email = expectedAccountFromDb.Email,
            Password = expectedAccountFromDb.Password
        };

        A.CallTo(() => _repository.GetAll(
            A<Func<IQueryable<Account>, IOrderedQueryable<Account>>>._,
            A<Expression<Func<Account, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(dbAccounts);
        A.CallTo(() => _mapper.Map<AccountModel>(expectedAccountFromDb)).Returns(accountModel);

        var result = _service.TryLogIn(expectedAccountFromDb.Email, password);

        Assert.AreEqual(accountModel, result);
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.TryLogInAccountWithEmailAndPasswordNotExistTestData), typeof(AccountServiceTestsDataProvider))]
    public void TryLogIn_AccountWithEmailAndPasswordNotExistInDatabase_AccountModel(List<Account> dbAccounts, string email, string password)
    {
        A.CallTo(() => _repository.GetAll(
            A<Func<IQueryable<Account>, IOrderedQueryable<Account>>>._,
            A<Expression<Func<Account, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(dbAccounts);
        A.CallTo(() => _mapper.Map<AccountModel>(null)).Returns(null);

        var result = _service.TryLogIn(email, password);

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
    public void CanTakeThisEmail_EmailIsValidAndNotExistInDatabase_True(List<Account> accounts, string email)
    {
        A.CallTo(() => _repository.GetAll(
            A<Func<IQueryable<Account>, IOrderedQueryable<Account>>>._,
            A<Expression<Func<Account, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(accounts);

        Assert.IsTrue(_service.CanTakeThisEmail(email));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.IsItEmailReturnFalseTestData), typeof(AccountServiceTestsDataProvider))]
    public void CanTakeThisEmail_EmailIsNotValid_ThrowsFormatException(string email)
    {
        Assert.ThrowsException<FormatException>(() => _service.CanTakeThisEmail(email));
    }

    [TestMethod]
    [DynamicData(nameof(AccountServiceTestsDataProvider.CanTakeThisEmailThrowsArgumentExceptionTestData), typeof(AccountServiceTestsDataProvider))]
    public void CanTakeThisEmail_EmailExistInDatabase_ThrowsArgumentException(List<Account> accounts, string email)
    {
        A.CallTo(() => _repository.GetAll(
            A<Func<IQueryable<Account>, IOrderedQueryable<Account>>>._,
            A<Expression<Func<Account, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(accounts);

        Assert.ThrowsException<ArgumentException>(() => _service.CanTakeThisEmail(email));
    }
}
