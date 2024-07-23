using AutoMapper;
using DataLayer;
using DomainLayer.Infrastructure;
using DomainLayer.Models;
using DomainLayerTests.TestHelpers;

namespace DomainLayerTests.Infrastructure;

[TestClass]
public class DomainDbMappingProfileTests
{
    private readonly IMapper _mapper;

    public DomainDbMappingProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg => 
                    cfg.AddProfile<DomainDbMappingProfile>())
            .CreateMapper();
    }

    [TestMethod]
    public void DomainDbMappingProfileTests_MapDbAccount_DomainAccount()
    {
        var dbAccount = new DataLayer.Models.Account() 
        {
            Id = 2,
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "email@gmail.com",
            Password = "password",
            Wallets = FillerBbData.Wallets.Where(w => w.AccountId == 2).ToList()
        };
        Account domainAccount = _mapper.Map<Account>(dbAccount);

        Assert.IsTrue(DataLayerDomainModelComparer.AreEqual(dbAccount, domainAccount));
    }
}
