using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services.Finances;
using DomainLayerTests.Data.Services;
using FakeItEasy;
using System.Linq.Expressions;

namespace DomainLayerTests.Services;

[TestClass]
public class FinanceServiceTests
{
    private readonly IFinanceService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<FinanceOperationType> _financeOperationTypesRepository;
    private readonly IRepository<FinanceOperation> _financeOperationsRepository;
    private readonly IMapper _mapper;

    public FinanceServiceTests()
    {
        _financeOperationTypesRepository = A.Fake<IRepository<FinanceOperationType>>();
        _financeOperationsRepository = A.Fake<IRepository<FinanceOperation>>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<IMapper>();

        A.CallTo(() => _unitOfWork.GetRepository<FinanceOperationType>()).Returns(_financeOperationTypesRepository);
        A.CallTo(() => _unitOfWork.GetRepository<FinanceOperation>()).Returns(_financeOperationsRepository);

        _service = new FinanceService(_unitOfWork, _mapper);
    }

    [TestMethod]
    public void Constructor_CreatedFinanceOperationRepository_Repository()
    {
        A.CallTo(() => _unitOfWork.GetRepository<FinanceOperation>()).MustHaveHappenedOnceExactly();
    }

    #region FinanceOperationTypeTests
    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationTypesOfWalletTestData), typeof(FinanceServiceTestsDataProvider))]
    public async Task GetAllFinanceOperationTypesOfWalletAsync_ReceivedExpectedNumberFinanceOperationTypes_FinanceOperationTypesList(List<FinanceOperationType> financeOperationTypes, int walletId)
    {
        A.CallTo(() => _financeOperationTypesRepository.GetAllAsync(
            A<Func<IQueryable<FinanceOperationType>,
            IOrderedQueryable<FinanceOperationType>>>._,
            A<Expression<Func<FinanceOperationType, bool>>>.That.Matches(filter =>
                filter != null && filter.Compile()(new FinanceOperationType { WalletId = walletId })),
            A<int>._, A<int>._,
        A<string[]>._))
            .Returns(financeOperationTypes);


        var result = await _service.GetAllFinanceOperationTypesOfWalletAsync(walletId);


        A.CallTo(() => _financeOperationTypesRepository.GetAllAsync(
           A<Func<IQueryable<FinanceOperationType>,
           IOrderedQueryable<FinanceOperationType>>>._,
           A<Expression<Func<FinanceOperationType, bool>>>.That.Matches(filter =>
               filter != null && filter.Compile()(new FinanceOperationType { WalletId = walletId })),
            A<int>._, A<int>._,
       A<string[]>._))
           .MustHaveHappenedOnceExactly();

        A.CallTo(() => _mapper.Map<FinanceOperationTypeModel>(A<FinanceOperationType>._))
            .MustHaveHappened(financeOperationTypes.Count, Times.Exactly);

        Assert.AreEqual(financeOperationTypes.Count, result.Count);
    }

    [TestMethod]
    public void AddFinanceOperationTypeAsync_InstanceIdNotEqualZero_ThrowsExeption()
    {
        FinanceOperationTypeModel type = new() { Id = 1 };

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.AddFinanceOperationTypeAsync(type));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.AddFinanceOperationTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public async Task AddFinanceOperationTypeAsync_ServiceInvokeMethodInsertByRepository_FinanceOperationTypeModel(FinanceOperationTypeModel modelForAdding, FinanceOperationType typeForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperationType>(modelForAdding)).Returns(typeForRepository);
        A.CallTo(() => _financeOperationTypesRepository.Insert(typeForRepository)).Returns(typeForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeModel>(typeForRepository)).Returns(modelForAdding);

        var result = await _service.AddFinanceOperationTypeAsync(modelForAdding);

        A.CallTo(() => _financeOperationTypesRepository.Insert(typeForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public void UpdateFinanceOperationTypeAsync_InstanceIdEqualZero_ThrowsExeption()
    {
        FinanceOperationTypeModel type = new() { Id = 0 };

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.UpdateFinanceOperationTypeAsync(type));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.UpdateFinanceOperationTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public async Task UpdateFinanceOperationTypeAsync_ServiceInvokeMethodUpdateByRepository_FinanceOperationTypeModel(FinanceOperationTypeModel modelForAdding, FinanceOperationType typeForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperationType>(modelForAdding)).Returns(typeForRepository);
        A.CallTo(() => _financeOperationTypesRepository.UpdateAsync(typeForRepository)).Returns(typeForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeModel>(typeForRepository)).Returns(modelForAdding);

        var result = await _service.UpdateFinanceOperationTypeAsync(modelForAdding);

        A.CallTo(() => _financeOperationTypesRepository.UpdateAsync(typeForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.DeleteFinanceOperationTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public void DeleteFinanceOperationTypeAsync_SomeFinanceOperationsHaveReferenceToType_ThrowsException(int typeId, List<FinanceOperation> financeOperations)
    {
        A.CallTo(() => _financeOperationsRepository.GetAllAsync(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                           A<Expression<Func<FinanceOperation, bool>>>._,
            A<int>._, A<int>._,
                                           A<string[]>._))
       .Returns(financeOperations);

        Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _service.DeleteFinanceOperationTypeAsync(typeId));
    }

    [TestMethod]
    public async Task DeleteFinanceOperationTypeAsync_ServiceInvokeMethodDeleteTypeByRepository_Void()
    {
        const int idFinanceOperationTypeForDelete = 2;

        await _service.DeleteFinanceOperationTypeAsync(idFinanceOperationTypeForDelete);

        A.CallTo(() => _financeOperationTypesRepository.Delete(idFinanceOperationTypeForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.IsAccountOwnerOfFinanceOperationTypeAsyncArgumentsAreLessOrEqualZeroThrowsArgumentOutOfRangeExceptionTestData), typeof(FinanceServiceTestsDataProvider))]
    public void IsAccountOwnerOfFinanceOperationTypeAsync_ArgumentsAreLessOrEqualZero_ThrowsArgumentOutOfRangeException(int accountid, int typeId)
    {
        Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _service.IsAccountOwnerOfFinanceOperationTypeAsync(accountid, typeId));
    }

    [TestMethod]
    public async Task IsAccountOwnerOfFinanceOperationTypeAsync_AccountIsOwner_ReturnsTrue()
    {
        var walletRepository = A.Fake<IRepository<Wallet>>();
        var wallet = new Wallet() { Id = 1, AccountId = 2 };
        var type = new FinanceOperationType() { Id = 3, WalletId = wallet.Id };

        A.CallTo(() => _unitOfWork.GetRepository<Wallet>())
           .Returns(walletRepository);
        A.CallTo(() => _financeOperationTypesRepository.GetByIdAsync(type.Id))
            .Returns(type);
        A.CallTo(() => walletRepository.GetByIdAsync(wallet.Id))
            .Returns(wallet);

        bool result = await _service.IsAccountOwnerOfFinanceOperationTypeAsync(wallet.AccountId, type.Id);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsAccountOwnerOfFinanceOperationTypeAsync_AccountIsNotOwner_ReturnsFalse()
    {
        var walletRepository = A.Fake<IRepository<Wallet>>();
        var wallet = new Wallet() { Id = 1, AccountId = 2 };
        var type = new FinanceOperationType() { Id = 3, WalletId = wallet.Id };

        A.CallTo(() => _unitOfWork.GetRepository<Wallet>())
           .Returns(walletRepository);
        A.CallTo(() => _financeOperationTypesRepository.GetByIdAsync(type.Id))
            .Returns(type);
        A.CallTo(() => walletRepository.GetByIdAsync(wallet.Id))
            .Returns(wallet);

        bool result = await _service.IsAccountOwnerOfFinanceOperationTypeAsync(wallet.AccountId + 1, type.Id);

        Assert.IsFalse(result);
    }
    #endregion

    #region FinanceOperationTests

    #region Tests for GetAllFinanceOperationOfWallet(int walletId, int count = 0)

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationOfWalletArgumentsAreLessThenZeroTestData), typeof(FinanceServiceTestsDataProvider))]
    public void GetAllFinanceOperationOfWalletAsync_ArgumentsAreLessThenZero_ThrowsArgumentException(int walletId, int count, int index)
    {
        Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _service.GetAllFinanceOperationOfWalletAsync(walletId, count, index));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationOfWalletWithCountAndIndexTestData), typeof(FinanceServiceTestsDataProvider))]
    public async Task GetAllFinanceOperationOfWalletAsync_WithCountAndIndex_GetAllWasInvokedAndReceivedExpectedList_ListFinanceOperationModels(List<FinanceOperation> operations, int walletId, int index, int count)
    {
        A.CallTo(() => _financeOperationsRepository.GetAllAsync(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { Type = new() { WalletId = walletId } })),
                                                             index, count,
                                                            A<string[]>._)).Returns(operations);

        var result = await _service.GetAllFinanceOperationOfWalletAsync(walletId, count, index);

        A.CallTo(() => _financeOperationsRepository.GetAllAsync(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { Type = new() { WalletId = walletId } })),
                                                             index, count,
                                                            A<string[]>._)).MustHaveHappenedOnceExactly();

        Assert.AreEqual(operations.Count, result.Count);
    }

    #endregion

    #region Tests for GetAllFinanceOperationOfWallet(int walletId, DateTime startDate, DateTime endDate)

    [TestMethod]
    public void GetAllFinanceOperationOfWalletAsync_DateRange_WalletIdIsLessThanOrEqualToZero_ThrowsArgumentException()
    {
        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.GetAllFinanceOperationOfWalletAsync(0, DateTime.Now.AddDays(-1), DateTime.Now));
    }

    [TestMethod]
    public void GetAllFinanceOperationOfWalletAsync_DateRange_StartDateIsGreaterThanEndDate_ThrowsArgumentOutOfRangeException()
    {
        Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _service.GetAllFinanceOperationOfWalletAsync(1, DateTime.Now, DateTime.Now.AddDays(-1)));
    }

    [TestMethod]
    public async Task GetAllFinanceOperationOfWalletAsync_DateRange_ReturnsOperationsWithinRange()
    {
        var financeOperations = new List<FinanceOperation>
        {
            new FinanceOperation { Type = new() { EntryType = EntryType.Income}, Date = DateTime.Now.AddDays(-3) },
            new FinanceOperation { Type = new() { EntryType = EntryType.Income},  Date = DateTime.Now.AddDays(-2) },
            new FinanceOperation { Type = new() { EntryType = EntryType.Income},  Date = DateTime.Now.AddDays(-1) }
        };
        var financeOperationTypes = new List<FinanceOperationType> { new FinanceOperationType() };
        int walletId = 1;
        DateTime startDate = DateTime.Now.AddDays(-4);
        DateTime endDate = DateTime.Now.AddDays(-1);

        A.CallTo(() => _financeOperationTypesRepository.GetAllAsync(A<Func<IQueryable<FinanceOperationType>, IOrderedQueryable<FinanceOperationType>>>._,
                                            A<Expression<Func<FinanceOperationType, bool>>>._,
            A<int>._, A<int>._,
                                            A<string[]>._))
        .Returns(financeOperationTypes);

        A.CallTo(() => _financeOperationsRepository.GetAllAsync(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                             A<Expression<Func<FinanceOperation, bool>>>._,
            A<int>._, A<int>._,
                                                             A<string[]>._))
          .Returns(financeOperations);

        A.CallTo(() => _mapper.Map<FinanceOperationModel>(A<FinanceOperation>._)).Returns(new IncomeModel(new() { EntryType = EntryType.Income }));

        var result = await _service.GetAllFinanceOperationOfWalletAsync(walletId, startDate, endDate);

        Assert.AreEqual(financeOperations.Count, result.Count);
    }

    #endregion

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationsOfTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public async Task GetAllFinanceOperationOfTypeAsync_ReceivedExpectedNumberFinanceOperations_FinanceOperationsList(List<FinanceOperation> financeOperations, int typeId)
    {
        A.CallTo(() => _financeOperationsRepository.GetAllAsync(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { TypeId = typeId })),
                                                             A<int>._, A<int>._,
                                                            A<string[]>._))
            .Returns(financeOperations);


        var result = await _service.GetAllFinanceOperationOfTypeAsync(typeId);


        A.CallTo(() => _financeOperationsRepository.GetAllAsync(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { TypeId = typeId })),
                                                                A<int>._, A<int>._,
                                                            A<string[]>._))
           .MustHaveHappenedOnceExactly();

        A.CallTo(() => _mapper.Map<FinanceOperationModel>(A<FinanceOperation>._))
            .MustHaveHappened(financeOperations.Count, Times.Exactly);

        Assert.AreEqual(financeOperations.Count, result.Count);
    }

    [TestMethod]
    public void AddFinanceOperationAsync_InstanceIdNotEqualZero_ThrowsExeption()
    {
        IncomeModel operation = new(new()) { Id = 1 };

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.AddFinanceOperationAsync(operation));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.AddFinanceOperationTestData), typeof(FinanceServiceTestsDataProvider))]
    public async Task AddFinanceOperationAsync_ServiceInvokeMethodInsertByRepository_FinanceOperationModel(FinanceOperationModel modelForAdding, FinanceOperation financeOperationForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperation>(modelForAdding)).Returns(financeOperationForRepository);
        A.CallTo(() => _financeOperationsRepository.Insert(financeOperationForRepository)).Returns(financeOperationForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationModel>(financeOperationForRepository)).Returns(modelForAdding);

        var result = await _service.AddFinanceOperationAsync(modelForAdding);

        A.CallTo(() => _financeOperationsRepository.Insert(financeOperationForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public void UpdateFinanceOperationAsync_InstanceIdEqualZero_ThrowsExeption()
    {
        IncomeModel operation = new(new()) { Id = 0 };

        Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.UpdateFinanceOperationAsync(operation));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.UpdateFinanceOperationTestData), typeof(FinanceServiceTestsDataProvider))]
    public async Task UpdateFinanceOperationAsync_ServiceInvokeMethodUpdateByRepository_FinanceOperationModel(FinanceOperationModel modelForAdding, FinanceOperation financeOperationForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperation>(modelForAdding)).Returns(financeOperationForRepository);
        A.CallTo(() => _financeOperationsRepository.UpdateAsync(financeOperationForRepository)).Returns(financeOperationForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationModel>(financeOperationForRepository)).Returns(modelForAdding);

        var result = await _service.UpdateFinanceOperationAsync(modelForAdding);

        A.CallTo(() => _financeOperationsRepository.UpdateAsync(financeOperationForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public async Task DeleteFinanceOperationAsync_ServiceInvokeMethodDeleteByRepository_Void()
    {
        const int idFinanceOperationTypeForDelete = 2;

        await _service.DeleteFinanceOperationAsync(idFinanceOperationTypeForDelete);

        A.CallTo(() => _financeOperationsRepository.Delete(idFinanceOperationTypeForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    public async Task IsAccountOwnerOfFinanceOperationAsync_AccountIsOwner_ReturnsTrue()
    {
        var walletRepository = A.Fake<IRepository<Wallet>>();
        var wallet = new Wallet() { Id = 1, AccountId = 2 };
        var type = new FinanceOperationType() { Id = 3, WalletId = wallet.Id };
        var operation = new FinanceOperation() { Id = 4, TypeId = type.Id };

        A.CallTo(() => _unitOfWork.GetRepository<Wallet>())
           .Returns(walletRepository);
        A.CallTo(() => _financeOperationsRepository.GetByIdAsync(operation.Id))
            .Returns(operation);
        A.CallTo(() => _financeOperationTypesRepository.GetByIdAsync(type.Id))
            .Returns(type);
        A.CallTo(() => walletRepository.GetByIdAsync(wallet.Id))
            .Returns(wallet);

        bool result = await _service.IsAccountOwnerOfFinanceOperationAsync(wallet.AccountId, operation.Id);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsAccountOwnerOfFinanceOperationAsync_AccountIsNotOwner_ReturnsFalse()
    {
        var walletRepository = A.Fake<IRepository<Wallet>>();
        var wallet = new Wallet() { Id = 1, AccountId = 2 };
        var type = new FinanceOperationType() { Id = 3, WalletId = wallet.Id };
        var operation = new FinanceOperation() { Id = 4, TypeId = type.Id };

        A.CallTo(() => _unitOfWork.GetRepository<Wallet>())
           .Returns(walletRepository);
        A.CallTo(() => _financeOperationsRepository.GetByIdAsync(operation.Id))
            .Returns(operation);
        A.CallTo(() => _financeOperationTypesRepository.GetByIdAsync(operation.TypeId))
            .Returns(type);
        A.CallTo(() => walletRepository.GetByIdAsync(type.WalletId)).Returns(wallet);

        bool result = await _service.IsAccountOwnerOfFinanceOperationAsync(wallet.AccountId + 1, operation.Id);

        Assert.IsFalse(result);
    }
    #endregion
}
