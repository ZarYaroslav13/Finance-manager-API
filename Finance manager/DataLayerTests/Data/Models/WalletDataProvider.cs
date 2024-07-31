using DataLayer.Models;

namespace DataLayerTests.Data.Models;

public class WalletDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Id = 1, Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account()},
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account()}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 2, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name1", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 2000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 2, Account = new Account(), FinanceOperationTypes = financeOperationTypes }
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account() }
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            null
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet()
        }
    };

    private static List<FinanceOperationType> financeOperationTypes = EntitiesTestDataProvider
        .FinanceOperationTypes
        .Where(fot => fot.WalletId == 1)
        .ToList();
}
