using Server.Mapper.Profiles;
using Server.Models;
using ApplicationLayerTests.Data.Mapper.Profiles;
using ApplicationLayerTests.TestToolExtensions;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayerTests.Mapper.Profiles;

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
