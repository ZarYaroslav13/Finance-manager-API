using Infrastructure.Models;
using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Tests.Data.Models;

public static class FinanceReportTestsDataProvider
{
    private static WalletModel _wallet = new() { Id = 1, Name = "test" };
    private static Period _period = new() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };
    private static List<FinanceOperationModel> _financeOperations = new()
    {
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = EntryType.Income}) { Amount = 23},
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = EntryType.Income}) { Amount = 31},
        new ExpenseModel(new FinanceOperationTypeModel() { EntryType = EntryType.Expense}) { Amount = 12},
        new ExpenseModel(new FinanceOperationTypeModel() { EntryType = EntryType.Expense }) { Amount = 7 }
    };

    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations }
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){Operations = _financeOperations }
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1,},
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1,}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id + 1, _wallet.Name, _period){ Id = 1, Operations = _financeOperations }
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name + '1', _period){ Id = 1, Operations = _financeOperations }
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 100, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, new Period()){ Id = 100, Operations = _financeOperations }
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 2, Operations = _financeOperations }
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = new() }
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            null
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new WalletModel()
        }
    };

    public static IEnumerable<object[]> FinanceOperationsAndTheirIncomeExpense { get; } = new List<object[]>
    {
        new object[]
        {
            _financeOperations,
            _financeOperations.OfType<IncomeModel>().Select(i => i.Amount).Sum(),
            _financeOperations.OfType<ExpenseModel>().Select(i => i.Amount).Sum()
        }
    };
}
