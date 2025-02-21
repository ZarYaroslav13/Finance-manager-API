using FinanceManager.Application.Mapper.Profiles;
using FinanceManager.Application.Models;
using FinanceManager.Application.Tests.Data.Mapper.Profiles;
using FinanceManager.Application.Tests.TestToolExtensions;
using AutoMapper;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Tests.Mapper.Profiles;

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
