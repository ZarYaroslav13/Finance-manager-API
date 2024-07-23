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

        Assert.That.Compare(dbAccount, domainAccount);
    }

    [TestMethod]
    public void DomainDbMappingProfileTests_MapDbWallet_DomainWallet()
    {
        var dbWallet = FillerBbData.Wallets.FirstOrDefault();

        dbWallet.FinanceOperationTypes = FillerBbData
            .FinanceOperationTypes
            .Where(fot => fot.WalletId == dbWallet.Id)
            .ToList();

        Wallet domainWallet = _mapper.Map<Wallet>(dbWallet);

        Assert.That.Compare(dbWallet, domainWallet);
    }

    [TestMethod]
    public void DomainDbMappingProfileTests_MapDbFinanceOperationType_DomainFinanceOperationType()
    {
        var dbFinanceOperationType = FillerBbData.FinanceOperationTypes.FirstOrDefault();

        FinanceOperationType domainFinanceOperationType = _mapper.Map<FinanceOperationType>(dbFinanceOperationType);

        Assert.That.Compare(dbFinanceOperationType, domainFinanceOperationType);
    }

    [TestMethod]
    public void DomainDbMappingProfileTests_MapDbFinanceOperation_DomainFinanceOperation()
    {
        var dbFinanceOperation = FillerBbData.FinanceOperations.FirstOrDefault();
        var typeOfOperation = FillerBbData.FinanceOperationTypes.FirstOrDefault(t => t.Id == dbFinanceOperation.TypeId);

        Assert.ThrowsException<ArgumentNullException>(() => _mapper.Map<FinanceOperation>(dbFinanceOperation));

        dbFinanceOperation.Type = typeOfOperation;

        FinanceOperation domainFinanceOperationType = _mapper.Map<FinanceOperation>(dbFinanceOperation);

        Assert.That.Compare(dbFinanceOperation, domainFinanceOperationType);
    }


}
