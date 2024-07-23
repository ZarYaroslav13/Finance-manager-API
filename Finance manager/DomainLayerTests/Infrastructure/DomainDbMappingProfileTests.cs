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
    public void DomainDbMappingProfileTests_Map_AccountModels()
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

        var mappedDbAccount = _mapper.Map<DataLayer.Models.Account>(domainAccount);

        Assert.AreEqual(dbAccount, mappedDbAccount);
    }

    [TestMethod]
    public void DomainDbMappingProfileTests_Map_WalletModels()
    {
        var dbWallet = FillerBbData.Wallets.FirstOrDefault();

        dbWallet.FinanceOperationTypes = FillerBbData
            .FinanceOperationTypes
            .Where(fot => fot.WalletId == dbWallet.Id)
            .ToList();

        Wallet domainWallet = _mapper.Map<Wallet>(dbWallet);

        Assert.That.Compare(dbWallet, domainWallet);

        var mappedDbWallet = _mapper.Map<DataLayer.Models.Wallet>(domainWallet);

        Assert.AreEqual(dbWallet, mappedDbWallet);
    }

    [TestMethod]
    public void DomainDbMappingProfileTests_Map_FinanceOperationTypeModels()
    {
        var dbFinanceOperationType = FillerBbData.FinanceOperationTypes.FirstOrDefault();

        var domainFinanceOperationType = _mapper.Map<FinanceOperationType>(dbFinanceOperationType);

        Assert.That.Compare(dbFinanceOperationType, domainFinanceOperationType);

        var mappedDbFinanceOperationType = _mapper.Map<DataLayer.Models.FinanceOperationType>(domainFinanceOperationType);

        Assert.AreEqual(dbFinanceOperationType, mappedDbFinanceOperationType);
    }

    [TestMethod]
    public void DomainDbMappingProfileTests_Map_FinanceOperationModels()
    {
        var dbFinanceOperation = FillerBbData.FinanceOperations.FirstOrDefault();
        var typeOfOperation = FillerBbData.FinanceOperationTypes.FirstOrDefault(t => t.Id == dbFinanceOperation.TypeId);

        dbFinanceOperation.Type = typeOfOperation;

        var domainFinanceOperation = _mapper.Map<FinanceOperation>(dbFinanceOperation);

        Assert.That.Compare(dbFinanceOperation, domainFinanceOperation);

        var mappedDbFinanceOperation = _mapper.Map<DataLayer.Models.FinanceOperation>(domainFinanceOperation);

        Assert.AreEqual(dbFinanceOperation, mappedDbFinanceOperation);
    }

    [TestMethod]
    public void DomainDbMappingProfileTests_Map_Exception()
    {
        var dbFinanceOperation = FillerBbData.FinanceOperations.FirstOrDefault();
        var typeOfOperation = FillerBbData.FinanceOperationTypes.FirstOrDefault(t => t.Id == dbFinanceOperation.TypeId);

        Assert.ThrowsException<ArgumentNullException>(() => _mapper.Map<FinanceOperation>(dbFinanceOperation));
    }
}
