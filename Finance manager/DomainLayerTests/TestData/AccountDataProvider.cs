using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerTests.TestData;

public class AccountDataProvider
{
    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password", Wallets = new()},
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password", Wallets = new()},
            true
        },
        new object[]
        {
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            true
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"},
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"},
            true
        },
        new object[]
        {
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            true
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 2, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            false
        },
        new object[]
        {
            new Account(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            false
        },
        new object[]
        {
            new Account(),
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            false
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(),
            false
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", },
            false
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            null,
            false
        },
        new object[]
        {
            new Account(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new Wallet(),
            false
        }
    };
}
