using ApplicationLayer.Models;
using DomainLayer.Models;

namespace ApplicationLayerTests.Data.Models;

public static class WalletDTOTestDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new()},
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "Name", AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 2, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "Name1", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 200, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 2, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "eName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "eName", Balance = 100, AccountId = 1, FinanceOperationTypes = new(), Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "fName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "fName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new() { new FinanceOperationTypeDTO()}, Incomes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "iName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new WalletDTO(){ Id = 1, Name = "iName", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new()}
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            null
        },
        new object[]
        {
            new WalletDTO(){ Id = 1, Name = "Name", Balance = 100, AccountId = 1, Expenses = new(), FinanceOperationTypes = new(), Incomes = new()},
            new AccountModel()
        }
    };
}
