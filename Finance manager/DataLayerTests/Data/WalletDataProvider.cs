using DataLayer;
using DataLayer.Models;

namespace DataLayerTests.Data;

public class WalletDataProvider
{
    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            true
        },
        new object[]
        {
            new Wallet() { Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            true
        },
        new object[]
        {
            new Wallet() { Id = 1, Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            true
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            true
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, FinanceOperationTypes = financeOperationTypes },
            true
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account()},
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account()},
            true
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 2, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            false
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name1", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            false
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 2000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            false
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 2, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            false
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(){ Id = 2}, FinanceOperationTypes = financeOperationTypes },
            false
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account() },
            false
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            null,
            false
        },
        new object[]
        {
            new Wallet() { Id = 1, Name = "Name", Balance = 1000, AccountId = 1, Account = new Account(), FinanceOperationTypes = financeOperationTypes },
            new Wallet(),
            false
        }
    };

    private static List<FinanceOperationType> financeOperationTypes = FillerBbData
        .FinanceOperationTypes
        .Where(fot => fot.WalletId == 1)
        .ToList();
}
