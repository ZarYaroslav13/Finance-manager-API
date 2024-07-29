using DomainLayer.Models;

namespace DomainLayerTests.Data;

public class WalletDataProvider
{
    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            true
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new()},
            true
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), Incomes = new()},
            true
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, FinanceOperationTypes = new(), Incomes = new()},
            true
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            true
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            true
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            true
        },
        new object[]
        {
            new WalletModel(){ Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            true
        },

        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 2, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            false
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name1", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            false
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 200, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            false
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 2, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            false
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "eName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "eName", Balance = 100, AccountId = 1, FinanceOperationTypes = new(), Incomes = new()},
            false
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "fName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "fName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new() { new FinanceOperationTypeModel()}, Incomes = new()},
            false
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "iName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "iName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new()},
            false
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            null,
            false
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new AccountModel(),
            false
        }
    };
}
