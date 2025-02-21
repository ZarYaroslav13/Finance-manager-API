using FinanceManager.Application.Mapper.Profiles;
using FinanceManager.Application.Models;
using FinanceManager.Application.Tests.Data.Mapper.Profiles;
using FinanceManager.Application.Tests.TestToolExtensions;
using AutoMapper;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Tests.Mapper.Profiles;

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
