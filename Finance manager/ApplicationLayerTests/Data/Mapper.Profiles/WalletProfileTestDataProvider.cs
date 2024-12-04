using DataLayer.Models;
using DomainLayer.Models;

namespace ApplicationLayerTests.Data.Mapper.Profiles;

public static class WalletProfileTestDataProvider
{
    private static List<FinanceOperationTypeModel> _financeOperationTypes = new()
    {
        new FinanceOperationTypeModel() { Id = 1, EntryType = EntryType.Income},
        new FinanceOperationTypeModel() { Id = 2, EntryType = EntryType.Income},
        new FinanceOperationTypeModel() { Id = 3, EntryType = EntryType.Expense},
        new FinanceOperationTypeModel() { Id = 4, EntryType = EntryType.Expense}
    };

    private static List<IncomeModel> _incomeModels = new()
    {
        new IncomeModel(_financeOperationTypes[0]) { Id = 0, Amount = 23},
        new IncomeModel(_financeOperationTypes[1]) { Id = 1, Amount = 31}
    };

    private static List<ExpenseModel> _expenseModels = new()
    {
        new ExpenseModel(_financeOperationTypes[2]) { Id = 2, Amount = 12},
        new ExpenseModel(_financeOperationTypes[3]) { Id = 3, Amount = 7 }
    };

    private static WalletModel _wallet = new()
    {
        Id = 1,
        Name = "WalletName",
        Balance = 200,
        AccountId = 1,
        Incomes = _incomeModels,
        Expenses = _expenseModels,
        FinanceOperationTypes = _financeOperationTypes
    };

    public static IEnumerable<object[]> DomainWallet { get; } = new List<object[]>()
    {
        new object[]
        {
            _wallet
        }
    };
}
