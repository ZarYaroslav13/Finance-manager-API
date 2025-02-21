using FinanceManager.Application.Mapper.Profiles;
using FinanceManager.Application.Models;
using FinanceManager.Application.Tests.Data.Mapper.Profiles;
using FinanceManager.Application.Tests.TestToolExtensions;
using AutoMapper;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Tests.Mapper.Profiles;

[TestClass]
public class FinanceOperationProfileTests
{
    private readonly IMapper _mapper;

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
    [DynamicData(nameof(FinanceOperationProfileTestDataProvider.FinanceOperation), typeof(FinanceOperationProfileTestDataProvider))]
    public void Map_FinanceOperationDataMappedCorrectly_FinanceOperation(FinanceOperationDTO appFinanceOperation)
    {
        var domainFinanceOperation = _mapper.Map<FinanceOperationModel>(appFinanceOperation);

        Assert.That.AreEqual(domainFinanceOperation, appFinanceOperation);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationProfileTestDataProvider.FinanceOperation), typeof(FinanceOperationProfileTestDataProvider))]
    public void Map_FinanceOperationDataAreNotLostAfterMapping_FinanceOperation(FinanceOperationDTO appFinanceOperation)
    {
        var mappeAppFinanceOperation = _mapper
            .Map<FinanceOperationDTO>(
                _mapper
                    .Map<FinanceOperationModel>(appFinanceOperation));

        Assert.AreEqual(mappeAppFinanceOperation, appFinanceOperation);
    }
}
