using DomainLayer.Models;

namespace DomainLayerTests.Data;

public class AccountDataProvider
{
    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password", Wallets = new()},
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password", Wallets = new()},
            true
        },
        new object[]
        {
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            true
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"},
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"},
            true
        },
        new object[]
        {
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            true
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 2, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            false
        },
        new object[]
        {
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            false
        },
        new object[]
        {
            new AccountModel(),
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            false
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(),
            false
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", },
            false
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            null,
            false
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new WalletModel(),
            false
        }
    };
}
