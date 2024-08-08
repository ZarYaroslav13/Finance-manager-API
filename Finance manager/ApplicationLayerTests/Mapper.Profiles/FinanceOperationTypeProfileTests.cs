using ApplicationLayer.Mapper.Profiles;
using ApplicationLayer.Models;
using ApplicationLayerTests.Data.Mapper.Profiles;
using ApplicationLayerTests.TestToolExtensions;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayerTests.Mapper.Profiles;

[TestClass]
public class FinanceOperationTypeProfileTests
{
    private readonly IMapper _mapper;

    public FinanceOperationTypeProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg => cfg.AddProfile<FinanceOperationTypeProfile>())
            .CreateMapper();
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationTypeProfileTestDataProvider.DomainFinanceOperationTypeModel), typeof(FinanceOperationTypeProfileTestDataProvider))]
    public void Map_FinanceOperationTypeDataMappedCorrectly_FinanceOperationType(FinanceOperationTypeModel domainType)
    {
        var appFinanceOperationType = _mapper.Map<FinanceOperationTypeDTO>(domainType);

        Assert.That.AreEqual(domainType, appFinanceOperationType);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationTypeProfileTestDataProvider.DomainFinanceOperationTypeModel), typeof(FinanceOperationTypeProfileTestDataProvider))]
    public void Map_FinanceOperationTypeDataAreNotLostAfterMapping_FinanceOperationType(FinanceOperationTypeModel domainType)
    {
        var mappeDomainFinanceOperationType = _mapper
            .Map<FinanceOperationTypeModel>(
                _mapper
                    .Map<FinanceOperationTypeDTO>(domainType));

        Assert.AreEqual(domainType, mappeDomainFinanceOperationType);
    }
}
