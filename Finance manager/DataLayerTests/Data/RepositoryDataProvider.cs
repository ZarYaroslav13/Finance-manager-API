using DataLayer.Models;

namespace DataLayerTests.Data;

public static class RepositoryDataProvider
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

    public static IEnumerable<object[]> GetAllIntArgumentsAreLessThenZeroTestData { get; } = new List<object[]>
    {
        new object[]
        {
            0, -1
        },
        new object[]
        {
            -1, 0
        },
        new object[]
        {
            -1, -1
        },
    };

    public static IEnumerable<object[]> GetAllWithSkipAndTakeTestData { get; } = new List<object[]>
    {
        new object[]
        {
            EntitiesTestDataProvider.Accounts,
            EntitiesTestDataProvider.Accounts.GetRange(0, 3),
            0,
            3
        },
        new object[]
        {
            EntitiesTestDataProvider.Accounts,
            EntitiesTestDataProvider.Accounts.GetRange(1, 3),
            1,
            3
        },
        new object[]
        {
            EntitiesTestDataProvider.Accounts,
            EntitiesTestDataProvider.Accounts.GetRange(3, 2),
            3,
            2
        }
    };
}
