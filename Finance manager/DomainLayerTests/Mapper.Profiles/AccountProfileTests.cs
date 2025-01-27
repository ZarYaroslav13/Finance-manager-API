using AutoMapper;
using Infrastructure.Models;
using DomainLayer.Mapper.Profiles;
using DomainLayer.Models;
using DomainLayerTests.Data;
using DomainLayerTests.TestHelpers;

namespace DomainLayerTests.Mapper.Profiles;

[TestClass]
public class AccountProfileTests
{
    private IMapper _mapper;

    public AccountProfileTests()
    {
        _mapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.AddProfile<AccountProfile>();
                   cfg.AddProfile<WalletProfile>();
                   cfg.AddProfile<FinanceOperationProfile>();
                   cfg.AddProfile<FinanceOperationTypeProfile>();
               })
           .CreateMapper();
    }

    [TestMethod]
    public void Map_AccountDataMappedCorrectly_AccountModels()
    {
        var dbAccount = new Account()
        {
            Id = 2,
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "email@gmail.com",
            Password = "password",
            Wallets = DbEntitiesTestDataProvider.Wallets.Where(w => w.AccountId == 2).ToList()
        };

        AccountModel domainAccount = _mapper.Map<AccountModel>(dbAccount);

        Assert.That.AreEqual(dbAccount, domainAccount);
    }

    [TestMethod]
    public void Map_AccountDataAreNotLostAfterMapping_Account()
    {
        var dbAccount = new Account()
        {
            Id = 2,
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "email@gmail.com",
            Password = "password",
            Wallets = DbEntitiesTestDataProvider.Wallets.Where(w => w.AccountId == 2).ToList()
        };

        var mappedDbAccount = _mapper
            .Map<Account>(
                _mapper
                    .Map<AccountModel>(dbAccount));

        Assert.AreEqual(dbAccount, mappedDbAccount);
    }
}
