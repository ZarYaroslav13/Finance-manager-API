using AutoMapper;
using Infrastructure.Models;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services.Wallets;
using FinanceManager.Domain.Tests.Data.Services;
using FakeItEasy;
using System.Linq.Expressions;

namespace FinanceManager.Domain.Tests.Services;

[TestClass]
public class WalletServiceTests
{
    private readonly IWalletService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Wallet> _repository;
    private readonly IMapper _mapper;

    public WalletServiceTests()
    {
        _repository = A.Fake<IRepository<Wallet>>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<IMapper>();

        A.CallTo(() => _unitOfWork.GetRepository<Wallet>()).Returns(_repository);

        _service = new WalletService(_unitOfWork, _mapper);
    }

    [TestMethod]
    [DynamicData(nameof(WalletServiceTestsDataProvider.GetAllWalletsOfAccountTestData), typeof(WalletServiceTestsDataProvider))]
    public async Task GetAllWalletsOfAccountAsync_ReceivedExpectedNumberWallets_WalletModelList(List<Wallet> wallets, int accountId)
    {
        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Wallet>, IOrderedQueryable<Wallet>>>._,
            A<Expression<Func<Wallet, bool>>>.That.Matches(filter =>
                filter != null && filter.Compile()(new Wallet { AccountId = accountId })),
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(wallets);

        var result = await _service.GetAllWalletsOfAccountAsync(accountId);

        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Wallet>, IOrderedQueryable<Wallet>>>._,
            A<Expression<Func<Wallet, bool>>>.That.Matches(filter =>
                filter != null && filter.Compile()(new Wallet { AccountId = accountId })),
            A<int>._, A<int>._,
            A<string[]>._))
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => _mapper.Map<WalletModel>(A<Wallet>._))
            .MustHaveHappened(wallets.Count, Times.Exactly);

        Assert.AreEqual(wallets.Count, result.Count);
    }

    [TestMethod]
    public void AddWalletAsync_InstanceIdNotEqualZero_ThrowsException()
    {
        WalletModel wallet = new() { Id = 1 };

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.AddWalletAsync(wallet));
    }

    [TestMethod]
    [DynamicData(nameof(WalletServiceTestsDataProvider.AddWalletTestData), typeof(WalletServiceTestsDataProvider))]
    public async Task AddWalletAsync_ServiceInvokeMethodInsertByRepository_WalletModel(WalletModel modelForAdding, Wallet walletForRepository)
    {
        A.CallTo(() => _mapper.Map<Wallet>(modelForAdding)).Returns(walletForRepository);
        A.CallTo(() => _repository.Insert(walletForRepository)).Returns(walletForRepository);
        A.CallTo(() => _mapper.Map<WalletModel>(walletForRepository)).Returns(modelForAdding);

        var result = await _service.AddWalletAsync(modelForAdding);

        A.CallTo(() => _repository.Insert(walletForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public void UpdateWalletAsync_InstanceIdEqualZero_ThrowsException()
    {
        WalletModel wallet = new() { Id = 0 };

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.UpdateWalletAsync(wallet));
    }

    [TestMethod]
    [DynamicData(nameof(WalletServiceTestsDataProvider.UpdateWalletTestData), typeof(WalletServiceTestsDataProvider))]
    public async Task UpdateWalletAsync_ServiceInvokeMethodUpdateByRepository_WalletModel(WalletModel modelForUpdate, Wallet walletForRepository)
    {
        A.CallTo(() => _mapper.Map<Wallet>(modelForUpdate)).Returns(walletForRepository);
        A.CallTo(() => _repository.Update(walletForRepository)).Returns(walletForRepository);
        A.CallTo(() => _mapper.Map<WalletModel>(walletForRepository)).Returns(modelForUpdate);

        var result = await _service.UpdateWalletAsync(modelForUpdate);

        A.CallTo(() => _repository.Update(walletForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForUpdate, result);
    }

    [TestMethod]
    public async Task DeleteWalletWithIdAsync_ServiceInvokeMethodDeleteByRepository_Void()
    {
        const int idWalletForDelete = 2;

        await _service.DeleteWalletByIdAsync(idWalletForDelete);

        A.CallTo(() => _repository.Delete(idWalletForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    public async Task FindWalletAsync_ServiceInvokeMethodGetByIdByRepository_WalletModel()
    {
        const int idWalletForSearch = 2;

        Wallet wallet = new();

        A.CallTo(() => _repository.GetByIdAsync(idWalletForSearch)).Returns(wallet);

        await _service.FindWalletAsync(idWalletForSearch);

        A.CallTo(() => _repository.GetByIdAsync(idWalletForSearch)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mapper.Map<WalletModel>(wallet)).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    [DynamicData(
        nameof(WalletServiceTestsDataProvider.IsAccountOwnerWalletAsyncInvalidAccountIdOrWalletIdThrowsArgumentOutOfRangeExceptionTestData),
        typeof(WalletServiceTestsDataProvider))]
    public void IsAccountOwnerWalletAsync_InvalidAccountIdOrWalletId_ThrowsArgumentOutOfRangeException(int accountId, int walletId)
    {
        Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _service.IsAccountOwnerWalletAsync(accountId, walletId));
    }

    [TestMethod]
    public async Task IsAccountOwnerWalletAsync_ValidAccountIdAndWalletId_ReturnsTrue()
    {
        Wallet wallet = new() { Id = 1, AccountId = 2 };

        A.CallTo(() => _repository.GetByIdAsync(wallet.Id))
            .Returns(wallet);

        var result = await _service.IsAccountOwnerWalletAsync(wallet.AccountId, wallet.Id);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsAccountOwnerWalletAsync_WalletNotOwnedByAccount_ReturnsFalse()
    {
        Wallet wallet = new() { Id = 1, AccountId = 2 };

        A.CallTo(() => _repository.GetByIdAsync(wallet.Id))
            .Returns(wallet);

        var result = await _service.IsAccountOwnerWalletAsync(wallet.AccountId + 1, wallet.Id);

        Assert.IsFalse(result);
    }
}
