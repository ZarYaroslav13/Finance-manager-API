using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests.Data;

public  class RepositoryDataProvider
{
    static RepositoryDataProvider()
    {
        _orderedAccountListForGetAll
            .ForEach(
                a => a.Wallets = EntitiesTestDataProvider.Wallets
                    .Where(
                        w => w.AccountId == a.Id)
                    .ToList());

        _accountsWithIdMoreThen3ListForGetAll.ForEach(a => a.Wallets = null);
    }

    private static List<Account> _orderedAccountListForGetAll = new(
        EntitiesTestDataProvider.Accounts
            .OrderBy(a => a.LastName)
            .Select(a => new Account()
            {
                Id = a.Id,
                LastName = a.LastName,
                FirstName = a.FirstName,
                Email = a.Email,
                Password = a.Password
            })
            .ToList());

    public static IEnumerable<object[]> OrderedAccountListForGetAll { get; } = new List<object[]>()
    {
        new object[]
        {
            _orderedAccountListForGetAll
        }
    };

    private static List<Account> _accountsWithIdMoreThen3ListForGetAll = new(
        EntitiesTestDataProvider.Accounts
            .Where(a => a.Id > 3)
            .Select(a => new Account()
            {
                Id = a.Id,
                LastName = a.LastName,
                FirstName = a.FirstName,
                Email = a.Email,
                Password = a.Password
            }).ToList());

    public static IEnumerable<object[]> AccountsWithIdMoreThen3ListForGetAll { get; } = new List<object[]>()
    {
        new object[]
        {
            _accountsWithIdMoreThen3ListForGetAll
        }
    };

    public static IEnumerable<object[]> AccountWithIdEqual2ForGetById { get; } = new List<object[]>()
    {
        new object[]
        {
            EntitiesTestDataProvider.Accounts.FirstOrDefault(a => a.Id == 2)
        }
    };
}
