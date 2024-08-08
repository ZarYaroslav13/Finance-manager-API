using ApplicationLayer.Mapper.Profiles;
using ApplicationLayer.Models;
using ApplicationLayerTests.Data.Mapper.Profiles;
using ApplicationLayerTests.TestToolExtensions;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayerTests.Mapper.Profiles;

[TestClass]
public class FinanceReportProfileTests
{
    private readonly IMapper _mapper;

    public FinanceReportProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<FinanceReportProfile>();
                    cfg.AddProfile<FinanceOperationTypeProfile>();
                    cfg.AddProfile<FinanceOperationProfile>();
                })
            .CreateMapper();
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportProfileTestDataProvider.DomainFinanceReport), typeof(FinanceReportProfileTestDataProvider))]
    public void Map_WalletDataMappedCorrectly_Wallet(FinanceReportModel domainFinanceReport)
    {
        var appFinanceReport = _mapper.Map<FinanceReportDTO>(domainFinanceReport);

        Assert.That.AreEqual(domainFinanceReport, appFinanceReport);
    }
}
