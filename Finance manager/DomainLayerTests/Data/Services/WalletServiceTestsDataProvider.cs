using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayerTests.Data.Services;

public static class WalletServiceTestsDataProvider
{
    public static IEnumerable<object[]> GetAllWalletsOfAccountTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new List<Wallet>
            {
                new Wallet { Id = 1, Balance = 100, AccountId = 1, Name = "Wallet1" },
                new Wallet { Id = 2, Balance = 200, AccountId = 1, Name = "Wallet2" }
            },
            1
        }
    };

    public static IEnumerable<object[]> AddOrUpdateWalletTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new WalletModel()
            {
                Id = 1,
                AccountId = 1,
                Balance = 1000,
                Name = "Test"
            },
            new Wallet()
            {
                Id = 1,
                AccountId = 1,
                Balance = 1000,
                Name = "Test"
            }
        }
    };

    public static IEnumerable<object[]> UpdateAccountTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new WalletModel()
            {
                Id = 1,
                AccountId = 1,
                Balance = 1000,
                Name = "Test"
            },
            new Wallet()
            {
                Id = 1,
                AccountId = 1,
                Balance = 1000,
                Name = "Test"
            }
        }
    };
}
