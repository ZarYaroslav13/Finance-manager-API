using FinanceManager.Application.Mapper.Profiles;
using FinanceManager.Application.Models;
using FinanceManager.Application.Tests.Data.Mapper.Profiles;
using FinanceManager.Application.Tests.TestToolExtensions;
using AutoMapper;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Tests.Mapper.Profiles;

[TestClass]
public class WalletProfileTests
{
    private readonly IMapper _mapper;

    public WalletProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<WalletProfile>();
                    cfg.AddProfile<FinanceOperationTypeProfile>();
                    cfg.AddProfile<FinanceOperationProfile>();
                })
            .CreateMapper();
    }

    [TestMethod]
    [DynamicData(nameof(WalletProfileTestDataProvider.DomainWallet), typeof(WalletProfileTestDataProvider))]
    public void Map_WalletDataMappedCorrectly_Wallet(WalletModel domainWallet)
    {
        WalletDTO appWallet = _mapper.Map<WalletDTO>(domainWallet);

        Assert.That.AreEqual(domainWallet, appWallet);
    }

    [TestMethod]
    [DynamicData(nameof(WalletProfileTestDataProvider.DomainWallet), typeof(WalletProfileTestDataProvider))]
    public void Map_WalletDataAreNotLostAfterMapping_Wallet(WalletModel domainWallet)
    {
        var mappedomainWallet = _mapper
            .Map<WalletModel>(
                _mapper
                    .Map<WalletDTO>(domainWallet));

        Assert.AreEqual(domainWallet, mappedomainWallet);
    }
}
