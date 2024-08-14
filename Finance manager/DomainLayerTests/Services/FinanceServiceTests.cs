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
    private readonly IRepository<FinanceOperationType> _repository;
    private readonly IRepository<FinanceOperation> _financeOperationsRepository;
    private readonly IMapper _mapper;

    public FinanceServiceTests()
    {
        _repository = A.Fake<IRepository<FinanceOperationType>>();
        _financeOperationsRepository = A.Fake<IRepository<FinanceOperation>>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<IMapper>();

        A.CallTo(() => _unitOfWork.GetRepository<FinanceOperationType>()).Returns(_repository);
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
        A.CallTo(() => _repository.GetAll(
            A<Func<IQueryable<FinanceOperationType>,
            IOrderedQueryable<FinanceOperationType>>>._,
            A<Expression<Func<FinanceOperationType, bool>>>.That.Matches(filter =>
                filter != null && filter.Compile()(new FinanceOperationType { WalletId = walletId })),
        A<string[]>._))
            .Returns(financeOperationTypes);


        var result = _service.GetAllFinanceOperationTypesOfWallet(walletId);


        A.CallTo(() => _repository.GetAll(
           A<Func<IQueryable<FinanceOperationType>,
           IOrderedQueryable<FinanceOperationType>>>._,
           A<Expression<Func<FinanceOperationType, bool>>>.That.Matches(filter =>
               filter != null && filter.Compile()(new FinanceOperationType { WalletId = walletId })),
       A<string[]>._))
           .MustHaveHappenedOnceExactly();

        A.CallTo(() => _mapper.Map<FinanceOperationTypeModel>(A<FinanceOperationType>._))
            .MustHaveHappened(financeOperationTypes.Count, Times.Exactly);

        Assert.AreEqual(financeOperationTypes.Count, result.Count);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.AddFinanceOperationTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public void AddFinanceOperationType_ServiceInvokeMethodInsertByRepository_FinanceOperationTypeModel(FinanceOperationTypeModel modelForAdding, FinanceOperationType typeForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperationType>(modelForAdding)).Returns(typeForRepository);
        A.CallTo(() => _repository.Insert(typeForRepository)).Returns(typeForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeModel>(typeForRepository)).Returns(modelForAdding);

        var result = _service.AddFinanceOperationType(modelForAdding);

        A.CallTo(() => _repository.Insert(typeForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.UpdateFinanceOperationTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public void UpdateFinanceOperationType_ServiceInvokeMethodUpdateByRepository_FinanceOperationTypeModel(FinanceOperationTypeModel modelForAdding, FinanceOperationType typeForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperationType>(modelForAdding)).Returns(typeForRepository);
        A.CallTo(() => _repository.Update(typeForRepository)).Returns(typeForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeModel>(typeForRepository)).Returns(modelForAdding);

        var result = _service.UpdateFinanceOperationType(modelForAdding);

        A.CallTo(() => _repository.Update(typeForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    public void DeleteFinanceOperationType_ServiceInvokeMethodDeleteTypeByRepository_Void()
    {
        const int idFinanceOperationTypeForDelete = 2;

        _service.DeleteFinanceOperationType(idFinanceOperationTypeForDelete);

        A.CallTo(() => _repository.Delete(idFinanceOperationTypeForDelete)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();
    }
    #endregion

    #region FinanceOperationTests

    [TestMethod]

    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationsOfWalletTestData), typeof(FinanceServiceTestsDataProvider))]
    public void GetAllFinanceOperationOfWallet_ReceivedExpectedNumberFinanceOperations_FinanceOperationsList(
        List<FinanceOperation> financeOperations,
        List<FinanceOperationType> financeOperationTypes,
        int walletId)
    {
        int numberFinanceOperationForOneType = financeOperations.Count / financeOperationTypes.Count;
        int startofFinanceOperations = 0 - numberFinanceOperationForOneType;

        A.CallTo(() => _repository.GetAll(A<Func<IQueryable<FinanceOperationType>, IOrderedQueryable<FinanceOperationType>>>._,
                                            A<Expression<Func<FinanceOperationType, bool>>>._,
                                            A<string[]>._))
        .Returns(financeOperationTypes);

        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                             A<Expression<Func<FinanceOperation, bool>>>._,
                                                             A<string[]>._))
          .ReturnsLazily(call =>
          {
              startofFinanceOperations += numberFinanceOperationForOneType;
              return financeOperations.GetRange(startofFinanceOperations, numberFinanceOperationForOneType);
          });


        var result = _service.GetAllFinanceOperationOfWallet(walletId);

        A.CallTo(() => _mapper.Map<FinanceOperationModel>(A<FinanceOperation>._)).MustHaveHappened(financeOperations.Count, Times.Exactly);

        Assert.AreEqual(financeOperations.Count, result.Count);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.GetAllFinanceOperationsOfTypeTestData), typeof(FinanceServiceTestsDataProvider))]
    public void GetAllFinanceOperationOfType_ReceivedExpectedNumberFinanceOperations_FinanceOperationsList(List<FinanceOperation> financeOperations, int typeId)
    {
        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { TypeId = typeId })),
                                                            A<string[]>._))
            .Returns(financeOperations);


        var result = _service.GetAllFinanceOperationOfType(typeId);


        A.CallTo(() => _financeOperationsRepository.GetAll(A<Func<IQueryable<FinanceOperation>, IOrderedQueryable<FinanceOperation>>>._,
                                                            A<Expression<Func<FinanceOperation, bool>>>.That.Matches(filter =>
                                                                filter != null && filter.Compile()(new FinanceOperation { TypeId = typeId })),
                                                            A<string[]>._))
           .MustHaveHappenedOnceExactly();

        A.CallTo(() => _mapper.Map<FinanceOperationModel>(A<FinanceOperation>._))
            .MustHaveHappened(financeOperations.Count, Times.Exactly);

        Assert.AreEqual(financeOperations.Count, result.Count);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.AddFinanceOperationTestData), typeof(FinanceServiceTestsDataProvider))]
    public void AddFinanceOperation_ServiceInvokeMethodInsertByRepository_FinanceOperationModel(FinanceOperationModel modelForAdding, FinanceOperation financeOperationForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperation>(modelForAdding)).Returns(financeOperationForRepository);
        A.CallTo(() => _financeOperationsRepository.Insert(financeOperationForRepository)).Returns(financeOperationForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationModel>(financeOperationForRepository)).Returns(modelForAdding);

        var result = _service.AddFinanceOperationType(modelForAdding);

        A.CallTo(() => _financeOperationsRepository.Insert(financeOperationForRepository)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChanges()).MustHaveHappenedOnceExactly();

        Assert.AreEqual(modelForAdding, result);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceServiceTestsDataProvider.UpdateFinanceOperationTestData), typeof(FinanceServiceTestsDataProvider))]
    public void UpdateFinanceOperation_ServiceInvokeMethodUpdateByRepository_FinanceOperationModel(FinanceOperationModel modelForAdding, FinanceOperation financeOperationForRepository)
    {
        A.CallTo(() => _mapper.Map<FinanceOperation>(modelForAdding)).Returns(financeOperationForRepository);
        A.CallTo(() => _financeOperationsRepository.Update(financeOperationForRepository)).Returns(financeOperationForRepository);
        A.CallTo(() => _mapper.Map<FinanceOperationModel>(financeOperationForRepository)).Returns(modelForAdding);

        var result = _service.UpdateFinanceOperationType(modelForAdding);

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
