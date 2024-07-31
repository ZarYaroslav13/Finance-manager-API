using AutoMapper;
using DataLayer;
using DomainLayer.Mapper.Profiles;
using DomainLayer.Models;
using DomainLayerTests.Data;
using DomainLayerTests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerTests.Mapper.Profiles;

[TestClass]
public class WalletProfileTests
{
    private IMapper _mapper;

    public WalletProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<WalletProfile>();
                    cfg.AddProfile<FinanceOperationProfile>();
                    cfg.AddProfile<FinanceOperationTypeProfile>();
                })
            .CreateMapper();
    }

    [TestMethod]
    public void Map_WalletDataMappedCorrectly_Wallet()
    {
        var dbWallet = DbEntitiesTestDataProvider.Wallets.FirstOrDefault();

        dbWallet.FinanceOperationTypes = FillerBbData
            .FinanceOperationTypes
            .Where(fot => fot.WalletId == dbWallet.Id)
            .ToList();

        WalletModel domainWallet = _mapper.Map<WalletModel>(dbWallet);

        Assert.That.AreEqual(dbWallet, domainWallet);
    }

    [TestMethod]
    public void Map_WalletDataAreNotLostAfterMapping_Wallet()
    {
        var dbWallet = DbEntitiesTestDataProvider.Wallets.FirstOrDefault();

        dbWallet.FinanceOperationTypes = DbEntitiesTestDataProvider
            .FinanceOperationTypes
            .Where(fot => fot.WalletId == dbWallet.Id)
            .ToList();

        var mappedDbWallet = _mapper
            .Map<DataLayer.Models.Wallet>(
                _mapper
                    .Map<WalletModel>(dbWallet));

        Assert.AreEqual(dbWallet, mappedDbWallet);
    }
}
