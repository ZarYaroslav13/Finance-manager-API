using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services.Wallets;
using DomainLayerTests.Data.Services;
using FakeItEasy;
using System.Linq.Expressions;

namespace DomainLayerTests.Services;

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
    public void GetAllWalletsOfAccount_ReturnWalletOfChoosenAccount_WalletModelList(List<Wallet> wallets, int accountId)
    {
        A.CallTo(() => _repository.GetAll(
            A<Func<IQueryable<Wallet>, IOrderedQueryable<Wallet>>>._,
            A<Expression<Func<Wallet, bool>>>.That.Matches(filter =>
                filter != null && filter.Compile()(new Wallet { AccountId = accountId })),
            A<string[]>._))
            .Returns(wallets);

        var result = _service.GetAllWalletsOfAccount(accountId);

        A.CallTo(() => _repository.GetAll(
            A<Func<IQueryable<Wallet>, IOrderedQueryable<Wallet>>>._,
            A<Expression<Func<Wallet, bool>>>.That.Matches(filter =>
                filter != null && filter.Compile()(new Wallet { AccountId = accountId })),
            A<string[]>._))
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => _mapper.Map<WalletModel>(A<Wallet>._))
            .MustHaveHappened(wallets.Count, Times.Exactly);
    }

    [TestMethod]
    [DynamicData(nameof(WalletServiceTestsDataProvider.AddOrUpdateWalletTestData), typeof(WalletServiceTestsDataProvider))]
    public void AddNewWallet_ServiceInvokeMethodInsertByRepository_WalletModel(WalletModel modelForAdding, Wallet walletForRepository)
    {
        A.CallTo(() => _mapper.Map<Wallet>(modelForAdding)).Returns(walletForRepository);
        A.CallTo(() => _repository.Insert(walletForRepository)).Returns(walletForRepository);
        A.CallTo(() => _mapper.Map<WalletModel>(walletForRepository)).Returns(modelForAdding);

        var result = _service.AddNewWallet(modelForAdding);

        A.CallTo(() => _repository.Insert(walletForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    [DynamicData(nameof(WalletServiceTestsDataProvider.AddOrUpdateWalletTestData), typeof(WalletServiceTestsDataProvider))]
    public void UpdateWallet_ServiceInvokeMethodUpdateByRepository_WalletModel(WalletModel modelForUpdate, Wallet walletForRepository)
    {
        A.CallTo(() => _mapper.Map<Wallet>(modelForUpdate)).Returns(walletForRepository);
        A.CallTo(() => _repository.Update(walletForRepository)).Returns(walletForRepository);
        A.CallTo(() => _mapper.Map<WalletModel>(walletForRepository)).Returns(modelForUpdate);

        var result = _service.UpdateWallet(modelForUpdate);

        A.CallTo(() => _repository.Update(walletForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForUpdate, result);
    }

    [TestMethod]
    public void DeleteWalletWithId_ServiceInvokeMethodDeleteByRepository_Void()
    {
        const int idWalletForDelete = 2;

        _service.DeleteWalletById(idWalletForDelete);

        A.CallTo(() => _repository.Delete(idWalletForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    public void FindWallet_ServiceInvokeMethodGetByIdByRepository_WalletModel()
    {
        const int idWalletForSearch = 2;

        Wallet wallet = new();

        A.CallTo(() => _repository.GetById(idWalletForSearch)).Returns(wallet);

        _service.FindWallet(idWalletForSearch);

        A.CallTo(() => _repository.GetById(idWalletForSearch)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mapper.Map<WalletModel>(wallet)).MustHaveHappenedOnceExactly();
    }
}
