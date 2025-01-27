using Infrastructure.Models;
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

    public static IEnumerable<object[]> AddWalletTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new WalletModel()
            {
                Id = 0,
                AccountId = 1,
                Balance = 1000,
                Name = "Test"
            },
            new Wallet()
            {
                Id = 0,
                AccountId = 1,
                Balance = 1000,
                Name = "Test"
            }
        }
    };

    public static IEnumerable<object[]> UpdateWalletTestData { get; } = new List<object[]>
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

    public static IEnumerable<object[]> IsAccountOwnerWalletAsyncInvalidAccountIdOrWalletIdThrowsArgumentOutOfRangeExceptionTestData { get; } = new List<object[]>
    {
        new object[] { -1, -1},
        new object[] { -1, 0},
        new object[] { 0, -1},
        new object[] { 1, 0},
        new object[] { 0, 1},
        new object[] { 0, 0},
    };
}
