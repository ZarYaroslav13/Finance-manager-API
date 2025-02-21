using FinanceManager.Application.Models;
using Infrastructure.Models;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Tests.Data.Models;

public static class FinanceReportDTOTestsDataProvider
{
    private static WalletModel _wallet = new() { Id = 1, Name = "test" };
    private static Period _period = new() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };
    private static int _totalIncome = 54;
    private static int _totalExpense = 19;
    public static List<FinanceOperationDTO> FinanceOperations = new()
    {
        new IncomeDTO() { Amount = 23, Type = new() { EntryType = EntryType.Income}},
        new IncomeDTO() { Amount = 31, Type = new() { EntryType = EntryType.Income } },
        new ExpenseDTO() { Amount = 12, Type = new() { EntryType = EntryType.Expense } },
        new ExpenseDTO() { Amount = 7, Type = new() { EntryType = EntryType.Expense } }
    };

    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations, _period){ Id = 1 },
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations, _period){ Id = 1 }
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 1},
            new FinanceReportDTO(_wallet.Id + 1, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations, _period){ Id = 1}
        },
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 1 },
            new FinanceReportDTO(_wallet.Id, _wallet.Name + '1', _totalIncome, _totalExpense, FinanceOperations, _period){ Id = 1 }
        },
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 100 },
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  new(),  new Period()){ Id = 100 }
        },
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 1 },
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 2 }
        },
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome+1, _totalExpense,  FinanceOperations,  _period){ Id = 1 },
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 1}
        },
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 1 },
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense+1,  FinanceOperations,  _period){ Id = 1}
        },
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 1 },
            null
        },
        new object[]
        {
            new FinanceReportDTO(_wallet.Id, _wallet.Name, _totalIncome, _totalExpense,  FinanceOperations,  _period){ Id = 1 },
            new object()
        }
    };

    public static IEnumerable<object[]> ConstructorThrowExceptionTestData { get; } = new List<object[]>
    {
        new object[]
        {
            null, FinanceOperations
        },
        new object[]
        {
            "name", null
        }
    };

}
