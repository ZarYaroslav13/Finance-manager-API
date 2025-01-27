using AutoMapper;
using DomainLayer.Mapper.Profiles;
using DomainLayer.Models;
using DomainLayerTests.Data;
using DomainLayerTests.TestHelpers;

namespace DomainLayerTests.Mapper.Profiles;

[TestClass]
public class FinanceOperationTypeProfileTests
{
    private IMapper _mapper;

    public FinanceOperationTypeProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg =>
                    cfg.AddProfile<FinanceOperationTypeProfile>())
            .CreateMapper();
    }

    [TestMethod]
    public void Map_FinanceOperationTypeDataMappedCorrectly_FinanceOperationType()
    {
        var dbFinanceOperationType = DbEntitiesTestDataProvider.FinanceOperationTypes.FirstOrDefault();

        var domainFinanceOperationType = _mapper.Map<FinanceOperationTypeModel>(dbFinanceOperationType);

        Assert.That.AreEqual(dbFinanceOperationType, domainFinanceOperationType);
    }

    [TestMethod]
    public void Map_FinanceOperationTypeDataAreNotLostAfterMapping_FinanceOperationType()
    {
        var dbFinanceOperationType = DbEntitiesTestDataProvider.FinanceOperationTypes.FirstOrDefault();

        var mappedbFinanceOperationType = _mapper
            .Map<Infrastructure.Models.FinanceOperationType>(
                _mapper
                    .Map<FinanceOperationTypeModel>(dbFinanceOperationType));

        Assert.AreEqual(dbFinanceOperationType, mappedbFinanceOperationType);
    }
}
