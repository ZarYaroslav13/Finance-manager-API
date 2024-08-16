using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services.Finances;
using DomainLayerTests.Data.Services;
using DomainLayerTests.TestHelpers;
using FakeItEasy;
using System;
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
    public void GetAllFinanceOperationTypesOfWallet_ReceivedExpectedNumberFinanceOperationTypes_FinanceOperationTypesList(List<FinanceOperationType> financeOperationTypes, int walletId)
    {
        A.CallTo(() => _financeOperationTypesRepository.GetAll(
            A<Func<IQueryable<FinanceOperationType>,
            IOrderedQueryable<FinanceOperationType>>>._,
            A<Expression<Func<FinanceOperationType, bool>>>.That.Matches(filter =>
                filter != null && filter.Compile()(new FinanceOperationType { WalletId = walletId })),
            A<int>._, A<int>._,
        A<string[]>._))
            .Returns(financeOperationTypes);


        var result = _service.GetAllFinanceOperationTypesOfWallet(walletId);


        A.CallTo(() => _financeOperationTypesRepository.GetAll(
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
    public void AddFinanceOperationType_InstanceIdNotEqualZero_ThrowsExeption()
    {
        FinanceOperationTypeModel type = new() { Id = 1 };

        Assert.ThrowsException<ArgumentException>(() => _service.AddFinanceOperationType(type));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.AddFinanceOperationTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public void AddFinanceOperationType_ServiceInvokeMethodInsertByRepository_FinanceOperationTypeModel(FinanceOperationTypeModel modelForAdding, FinanceOperationType typeForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperationType>(modelForAdding)).Returns(typeForRepository);
        A.CallTo(() => _financeOperationTypesRepository.Insert(typeForRepository)).Returns(typeForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeModel>(typeForRepository)).Returns(modelForAdding);

        var result = _service.AddFinanceOperationType(modelForAdding);

        A.CallTo(() => _financeOperationTypesRepository.Insert(typeForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public void UpdateFinanceOperationType_InstanceIdEqualZero_ThrowsExeption()
    {
        FinanceOperationTypeModel type = new() { Id = 0 };

        Assert.ThrowsException<ArgumentException>(() => _service.UpdateFinanceOperationType(type));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.UpdateFinanceOperationTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public void UpdateFinanceOperationType_ServiceInvokeMethodUpdateByRepository_FinanceOperationTypeModel(FinanceOperationTypeModel modelForAdding, FinanceOperationType typeForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperationType>(modelForAdding)).Returns(typeForRepository);
        A.CallTo(() => _financeOperationTypesRepository.Update(typeForRepository)).Returns(typeForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeModel>(typeForRepository)).Returns(modelForAdding);

        var result = _service.UpdateFinanceOperationType(modelForAdding);

        A.CallTo(() => _financeOperationTypesRepository.Update(typeForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.DeleteFinanceOperationTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public void DeleteFinanceOperationType_SomeFinanceOperationsHaveReferenceToType_ThrowsException(int typeId, List<FinanceOperation> financeOperations)
    {
        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                           A<Expression<Func<FinanceOperation, bool>>>._,
            A<int>._, A<int>._,
                                           A<string[]>._))
       .Returns(financeOperations);

        Assert.ThrowsException<InvalidOperationException>(() => _service.DeleteFinanceOperationType(typeId));
    }

    [TestMethod]
    public void DeleteFinanceOperationType_ServiceInvokeMethodDeleteTypeByRepository_Void()
    {
        const int idFinanceOperationTypeForDelete = 2;

        _service.DeleteFinanceOperationType(idFinanceOperationTypeForDelete);

        A.CallTo(() => _financeOperationTypesRepository.Delete(idFinanceOperationTypeForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();
    }
    #endregion

    #region FinanceOperationTests

    #region Tests for GetAllFinanceOperationOfWallet(int walletId, int count = 0)

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationOfWalletArgumentsAreLessThenZeroTestData), typeof(FinanceServiceTestsDataProvider))]
    public void GetAllFinanceOperationOfWallet_ArgumentsAreLessThenZero_ThrowsArgumentException(int walletId, int count,int index)
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _service.GetAllFinanceOperationOfWallet(walletId, count, index));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationOfWalletWithCountAndIndexTestData), typeof(FinanceServiceTestsDataProvider))]
    public void GetAllFinanceOperationOfWallet_WithCountAndIndex_GetAllWasInvokedAndReceivedExpectedList_ListFinanceOperationModels(List<FinanceOperation> operations, int walletId, int index, int count)
    {
        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { Type = new() { WalletId = walletId } })),
                                                             index, count,
                                                            A<string[]>._)).Returns(operations);

        var result = _service.GetAllFinanceOperationOfWallet(walletId, count, index);

        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { Type = new() { WalletId = walletId } })),
                                                             index, count,
                                                            A<string[]>._)).MustHaveHappenedOnceExactly();

        Assert.AreEqual(operations.Count, result.Count);
    }

    #endregion

    #region Tests for GetAllFinanceOperationOfWallet(int walletId, DateTime startDate, DateTime endDate)

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetAllFinanceOperationOfWallet_DateRange_WalletIdIsLessThanOrEqualToZero_ThrowsArgumentException()
    {
        _service.GetAllFinanceOperationOfWallet(0, DateTime.Now.AddDays(-1), DateTime.Now);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetAllFinanceOperationOfWallet_DateRange_StartDateIsGreaterThanEndDate_ThrowsArgumentOutOfRangeException()
    {
        _service.GetAllFinanceOperationOfWallet(1, DateTime.Now, DateTime.Now.AddDays(-1));
    }

    [TestMethod]
    public void GetAllFinanceOperationOfWallet_DateRange_ReturnsOperationsWithinRange()
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

        A.CallTo(() => _financeOperationTypesRepository.GetAll(A<Func<IQueryable<FinanceOperationType>, IOrderedQueryable<FinanceOperationType>>>._,
                                            A<Expression<Func<FinanceOperationType, bool>>>._,
            A<int>._, A<int>._,
                                            A<string[]>._))
        .Returns(financeOperationTypes);

        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                             A<Expression<Func<FinanceOperation, bool>>>._,
            A<int>._, A<int>._,
                                                             A<string[]>._))
          .Returns(financeOperations);

        A.CallTo(() => _mapper.Map<FinanceOperationModel>(A<FinanceOperation>._)).Returns(new IncomeModel(new() { EntryType = EntryType.Income }));

        var result = _service.GetAllFinanceOperationOfWallet(walletId, startDate, endDate);

        Assert.AreEqual(financeOperations.Count, result.Count);
    }

    #endregion

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationsOfTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public void GetAllFinanceOperationOfType_ReceivedExpectedNumberFinanceOperations_FinanceOperationsList(List<FinanceOperation> financeOperations, int typeId)
    {
        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { TypeId = typeId })),
                                                             A<int>._, A<int>._,
                                                            A<string[]>._))
            .Returns(financeOperations);


        var result = _service.GetAllFinanceOperationOfType(typeId);


        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
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
    public void AddFinanceOperation_InstanceIdNotEqualZero_ThrowsExeption()
    {
        IncomeModel operation = new(new()) { Id = 1 };

        Assert.ThrowsException<ArgumentException>(() => _service.AddFinanceOperation(operation));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.AddFinanceOperationTestData), typeof(FinanceServiceTestsDataProvider))]
    public void AddFinanceOperation_ServiceInvokeMethodInsertByRepository_FinanceOperationModel(FinanceOperationModel modelForAdding, FinanceOperation financeOperationForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperation>(modelForAdding)).Returns(financeOperationForRepository);
        A.CallTo(() => _financeOperationsRepository.Insert(financeOperationForRepository)).Returns(financeOperationForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationModel>(financeOperationForRepository)).Returns(modelForAdding);

        var result = _service.AddFinanceOperation(modelForAdding);

        A.CallTo(() => _financeOperationsRepository.Insert(financeOperationForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public void UpdateFinanceOperation_InstanceIdEqualZero_ThrowsExeption()
    {
        IncomeModel operation = new(new()) { Id = 0 };

        Assert.ThrowsException<ArgumentException>(() => _service.UpdateFinanceOperation(operation));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.UpdateFinanceOperationTestData), typeof(FinanceServiceTestsDataProvider))]
    public void UpdateFinanceOperation_ServiceInvokeMethodUpdateByRepository_FinanceOperationModel(FinanceOperationModel modelForAdding, FinanceOperation financeOperationForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperation>(modelForAdding)).Returns(financeOperationForRepository);
        A.CallTo(() => _financeOperationsRepository.Update(financeOperationForRepository)).Returns(financeOperationForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationModel>(financeOperationForRepository)).Returns(modelForAdding);

        var result = _service.UpdateFinanceOperation(modelForAdding);

        A.CallTo(() => _financeOperationsRepository.Update(financeOperationForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public void DeleteFinanceOperation_ServiceInvokeMethodDeleteByRepository_Void()
    {
        const int idFinanceOperationTypeForDelete = 2;

        _service.DeleteFinanceOperation(idFinanceOperationTypeForDelete);

        A.CallTo(() => _financeOperationsRepository.Delete(idFinanceOperationTypeForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();
    }
    #endregion
}
