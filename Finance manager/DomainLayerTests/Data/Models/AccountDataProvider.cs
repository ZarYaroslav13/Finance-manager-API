using DomainLayer.Models;

namespace DomainLayerTests.Data.Models;

public class AccountDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password", Wallets = new()},
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password", Wallets = new()}
        },
        new object[]
        {
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"},
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"}
        },
        new object[]
        {
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 2, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new AccountModel(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new AccountModel(),
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel()
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", }
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            null
        },
        new object[]
        {
            new AccountModel(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new WalletModel()
        }
    };
}
