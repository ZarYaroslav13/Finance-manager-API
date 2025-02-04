using Server.Mapper.Profiles;
using Server.Models;
using ApplicationLayerTests.Data.Mapper.Profiles;
using ApplicationLayerTests.TestToolExtensions;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayerTests.Mapper.Profiles;

[TestClass]
public class AccountProfileTests
{
    private readonly IMapper _mapper;

    public AccountProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<AccountProfile>();
                    cfg.AddProfile<WalletProfile>();
                    cfg.AddProfile<FinanceOperationTypeProfile>();
                    cfg.AddProfile<FinanceOperationProfile>();
                })
            .CreateMapper();
    }

    [TestMethod]
    [DynamicData(nameof(AccountProfileTestDataProvider.DomainAccount), typeof(AccountProfileTestDataProvider))]
    public void Map_AccountDataMappedCorrectly_AccountModels(AccountModel domainAccount)
    {
        var appAccount = _mapper.Map<AccountDTO>(domainAccount);

        Assert.That.AreEqual(domainAccount, appAccount);
    }

    [TestMethod]
    [DynamicData(nameof(AccountProfileTestDataProvider.DomainAccount), typeof(AccountProfileTestDataProvider))]
    public void Map_AccountDataAreNotLostAfterMapping_AccountModels(AccountModel domainAccount)
    {
        var mappeddomainAccount = _mapper
            .Map<AccountModel>(
                _mapper
                    .Map<AccountDTO>(domainAccount));

        Assert.AreEqual(domainAccount, mappeddomainAccount);
    }
}
