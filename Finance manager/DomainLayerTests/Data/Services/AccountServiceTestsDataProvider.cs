﻿using DataLayer.Models;
using DataLayer.Security;
using DomainLayer.Models;
using DomainLayer.Services.Admins;
using FakeItEasy;

namespace DomainLayerTests.Data.Services;

public static class AccountServiceTestsDataProvider
{
    private static List<Account> accountsUntouchable = new()
    {
        new Account()
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "password123",
        },
        new Account()
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = "password456",
        },
        new Account()
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Johnson",
            Email = "michael.johnson@example.com",
            Password = "password789",
        }
    };

    private static List<Account> _accounts
    {
        get
        {
            return accountsUntouchable.Select(a => new Account
            {
                Id = a.Id,
                LastName =
                    a.LastName,
                FirstName =
                    a.FirstName,
                Email = a.Email,
                Password = a.Password
            }
            ).ToList();
        }
    }

    public static IEnumerable<object[]> ConstructorAdminServiceNullThrowsExceptionTestData { get; } = new List<object[]>
    {
        new object[] { A.Fake<IAdminService>(), null },
        new object[] { null, A.Fake<IPasswordCoder>() },
        new object[] { null, null }
    };

    public static IEnumerable<object[]> GetAccountsNoSkipOrTakeTestData { get; } = new List<object[]>
    {
        new object[]{ _accounts}
    };

    public static IEnumerable<object[]> GetAccountsSkipOrTakeNegativeTestData { get; } = new List<object[]>
    {
        new object[] { -1, 1},
        new object[] { 1, -1},
        new object[] { -1, -1}
    };

    public static IEnumerable<object[]> GetAccountsValidInputsTestData { get; } = new List<object[]>
    {
        new object[]
        {
            _accounts.GetRange(1, 2), 1 ,2
        },
        new object[]
        {
            _accounts.GetRange(0, 3), 0, 3
        }
    };

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

    public static IEnumerable<object[]> TrySignInAsyncThrowsNullExceptionTestData { get; } = new List<object[]>
    {
        new object[] { "Fake", null },
        new object[] { null, "Fake" },
        new object[] { null, null }
    };

    public static IEnumerable<object[]> TrySignInAsyncThrowsExceptionTestData { get; } = new List<object[]>
    {
        new object[] { "", "Fake" },
        new object[] { "Fake", "" },
        new object[] { "", "" }
    };

    public static IEnumerable<object[]> TrySignInAsyncAccountWithEmailAndPasswordExistTestData { get; } = new List<object[]>
    {
        new object[]
        {
            _accounts, _accounts[0]
        },
        new object[]
        {
            _accounts, _accounts[1]
        },
        new object[]
        {
            _accounts, _accounts[2]
        },
    };

    public static IEnumerable<object[]> TryLogInAccountWithEmailAndPasswordNotExistTestData { get; } = new List<object[]>
    {
        new object[]
        {
            _accounts, _accounts[0].Email, "notExistedPassword"
        },
        new object[]
        {
            _accounts, "notExistedEmail", "password456"
        },
        new object[]
        {
            _accounts, "notExistedEmail", "notExistedPassword"
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
        new object[]{ _accounts, 0, "NewEmail@gmail.com"},
        new object[]{ _accounts, 0, "NewEmail1@gmail.com"},
        new object[]{ _accounts, _accounts[0].Id, _accounts[0].Email },
        new object[]{ _accounts, _accounts[1].Id, _accounts[1].Email }
    };

    public static IEnumerable<object[]> CanTakeThisEmailThrowsArgumentExceptionTestData { get; } = new List<object[]>
    {
        new object[]{ _accounts, 0, _accounts[0].Email },
        new object[]{ _accounts, 0, _accounts[1].Email },
        new object[]{ _accounts, 0, _accounts[2].Email },
        new object[]{ _accounts, _accounts[1].Id, _accounts[0].Email },
        new object[]{ _accounts, _accounts[0].Id, _accounts[2].Email },
    };
}
