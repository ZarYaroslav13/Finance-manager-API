using DataLayer.Models;
using DataLayer.Security;
using DomainLayer.Models;

namespace DomainLayerTests.Data.Services;

public static class AccountServiceTestsDataProvider
{
    public static IEnumerable<object[]> AddAccountTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new AccountModel()
            {
                Id = 0,
                LastName = "LastName",
                FirstName = "FirstName",
                Email = "Email@gmail.com",
                Password = "Password"
            },
            new Account()
            {
                Id = 0,
                LastName = "LastName",
                FirstName = "FirstName",
                Email = "Email@gmail.com",
                Password = "Password"
            }
        }
    };

    public static IEnumerable<object[]> UpdateAccountTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new AccountModel()
            {
                Id = 1,
                LastName = "LastName",
                FirstName = "FirstName",
                Email = "Email@gmail.com",
                Password = "Password"
            },
            new Account()
            {
                Id = 1,
                LastName = "LastName",
                FirstName = "FirstName",
                Email = "Email@gmail.com",
                Password = "Password"
            }
        }
    };

    public static IEnumerable<object[]> TryLogInThrowsNullExceptionTestData { get; } = new List<object[]>
    {
        new object[] { "Fake", null },
        new object[] { null, "Fake" },
        new object[] { null, null }
    };

    public static IEnumerable<object[]> TryLogInThrowsExceptionTestData { get; } = new List<object[]>
    {
        new object[] { "", "Fake" },
        new object[] { "Fake", "" },
        new object[] { "", "" }
    };

    private static List<Account> accounts = new()
    {
        new Account()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = new PasswordCoder().ComputeSHA256Hash("password123"),
        },
        new Account()
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = new PasswordCoder().ComputeSHA256Hash("password456"),
        },
        new Account()
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Johnson",
            Email = "michael.johnson@example.com",
            Password = new PasswordCoder().ComputeSHA256Hash("password789"),
        }
    };

    public static IEnumerable<object[]> TryLogInAccountWithEmailAndPasswordExistTestData { get; } = new List<object[]>
    {
        new object[]
        {
            accounts, accounts[0], "password123"
        },
        new object[]
        {
            accounts, accounts[1], "password456"
        },
        new object[]
        {
            accounts, accounts[2], "password789"
        },
    };

    public static IEnumerable<object[]> TryLogInAccountWithEmailAndPasswordNotExistTestData { get; } = new List<object[]>
    {
        new object[]
        {
            accounts, accounts[0].Email, "notExistedPassword"
        },
        new object[]
        {
            accounts, "notExistedEmail", "password456"
        },
        new object[]
        {
            accounts, "notExistedEmail", "notExistedPassword"
        },
    };

    public static IEnumerable<object[]> IsItEmailReturnTrueTestData { get; } = new List<object[]>
    {
        new object[] { "test.email@example.com" },
        new object[] { "user.name+tag+sorting@example.com" },
        new object[] { "user_name@example.co.uk" },
        new object[] { "user-name@sub.example.org" },
        new object[] { "firstname.lastname@example.travel" },
    };

    public static IEnumerable<object[]> IsItEmailReturnFalseTestData { get; } = new List<object[]>
    {
        new object[] { "plainaddress" },
        new object[] { "@missingusername.com" },
        new object[] { "username@.com" },
        new object[] { "usernam@e@com" },
        new object[] { "username@- example.com" },
    };

    public static IEnumerable<object[]> CanTakeThisEmailReturnTrueTestData { get; } = new List<object[]>
    {
        new object[]{ accounts, "NewEmail@gmail.com"},
        new object[]{ accounts, "NewEmail1@gmail.com"}
    };

    public static IEnumerable<object[]> CanTakeThisEmailThrowsArgumentExceptionTestData { get; } = new List<object[]>
    {
        new object[]{ accounts, accounts[0].Email },
        new object[]{ accounts, accounts[1].Email },
        new object[]{ accounts, accounts[2].Email },
    };
}
