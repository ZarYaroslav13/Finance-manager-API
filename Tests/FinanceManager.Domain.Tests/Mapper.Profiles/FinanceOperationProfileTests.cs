using AutoMapper;
using Infrastructure.Models;
using FinanceManager.Domain.Mapper.Profiles;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Tests.Data;
using FinanceManager.Domain.Tests.TestHelpers;

namespace FinanceManager.Domain.Tests.Mapper.Profiles;

[TestClass]
public class FinanceOperationProfileTests
{
    private IMapper _mapper;

    public FinanceOperationProfileTests()
    {
        _mapper = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.AddProfile<FinanceOperationProfile>();
                     cfg.AddProfile<FinanceOperationTypeProfile>();
                 })
             .CreateMapper();
    }

    [TestMethod]
    public void Map_FinanceOperationDataMappedCorrectly_FinanceOperation()
    {
        var dbFinanceOperation = DbEntitiesTestDataProvider
            .FinanceOperations
            .Select(fo => new FinanceOperation()
            {
                Id = fo.Id,
                Amount = fo.Amount,
                Date = fo.Date,
                Type = fo.Type,
                TypeId = fo.TypeId,
            })
            .FirstOrDefault();
        var typeOfOperation = DbEntitiesTestDataProvider.FinanceOperationTypes.FirstOrDefault(t => t.Id == dbFinanceOperation.TypeId);

        dbFinanceOperation.Type = typeOfOperation;

        var domainFinanceOperation = _mapper.Map<FinanceOperationModel>(dbFinanceOperation);

        Assert.That.AreEqual(dbFinanceOperation, domainFinanceOperation);
    }

    [TestMethod]
    public void Map_FinanceOperationDataAreNotLostAfterMapping_FinanceOperation()
    {
        var dbFinanceOperation = DbEntitiesTestDataProvider
            .FinanceOperations
            .Select(fo => new FinanceOperation()
            {
                Id = fo.Id,
                Amount = fo.Amount,
                Date = fo.Date,
                Type = fo.Type,
                TypeId = fo.TypeId,
            })
            .FirstOrDefault();
        var typeOfOperation = DbEntitiesTestDataProvider.FinanceOperationTypes.FirstOrDefault(t => t.Id == dbFinanceOperation.TypeId);

        dbFinanceOperation.Type = typeOfOperation;

        var mappedbFinanceOperation = _mapper
            .Map<FinanceOperation>(
                _mapper
                    .Map<FinanceOperationModel>(dbFinanceOperation));

        Assert.AreEqual(dbFinanceOperation, mappedbFinanceOperation);
    }

    [TestMethod]
    public void Map_NullValueFinancialOperationType_ThowsException()
    {
        var dbFinanceOperation = DbEntitiesTestDataProvider.FinanceOperations.FirstOrDefault();

        Assert.ThrowsException<ArgumentNullException>(() => _mapper.Map<FinanceOperationModel>(dbFinanceOperation));
    }
}
