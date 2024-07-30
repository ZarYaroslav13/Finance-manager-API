using DataLayer.Models;

namespace DataLayerTests.Data.Models;

public class AccountDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"},
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"}
        },
        new object[]
        {
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 2, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new Account(),
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account()
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", }
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            null
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Wallet()
        }
    };
}
