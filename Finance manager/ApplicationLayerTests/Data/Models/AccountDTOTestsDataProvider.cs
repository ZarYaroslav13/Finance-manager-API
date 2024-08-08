using Finance_manager_API.Models;

namespace ApplicationLayerTests.Data.Models;

public static class AccountDTOTestDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password", Wallets = new()},
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password", Wallets = new()}
        },
        new object[]
        {
            new AccountDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"},
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"}
        },
        new object[]
        {
            new AccountDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountDTO(){ Id = 2, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new AccountDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new AccountDTO(),
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountDTO()
        },
        new object[]
        {
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", }
        },
        new object[]
        {
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            null
        },
        new object[]
        {
            new AccountDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new WalletDTO()
        }
    };
}
