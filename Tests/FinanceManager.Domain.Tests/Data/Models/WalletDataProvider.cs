using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Tests.Data.Models;

public class WalletDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 2, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name1", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 200, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 2, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "fName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletModel(){ Id = 1, Name = "fName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new() { new FinanceOperationTypeModel()}, Incomes = new()}
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            null
        },
        new object[]
        {
            new WalletModel(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new AccountModel()
        }
    };
}
