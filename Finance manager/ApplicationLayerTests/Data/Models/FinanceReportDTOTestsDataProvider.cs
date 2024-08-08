using DataLayer.Models;
using DomainLayer.Models;
using Finance_manager_API.Models;

namespace ApplicationLayerTests.Data.Models;

public static class FinanceReportDTOTestsDataProvider
{
    private static WalletModel _wallet = new() { Id = 1, Name = "test" };
    private static Period _period = new() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };
    private static int _totalIncome = 54;
    private static int _totalExpense = 19;
    public static List<FinanceOperationDTO> FinanceOperations = new()
    {
        new IncomeDTO(new FinanceOperationTypeDTO() { EntryType = EntryType.Income}) { Amount = 23},
        new IncomeDTO(new FinanceOperationTypeDTO() { EntryType = EntryType.Income}) { Amount = 31},
        new ExpenseDTO(new FinanceOperationTypeDTO() { EntryType = EntryType.Expense}) { Amount = 12},
        new ExpenseDTO(new FinanceOperationTypeDTO() { EntryType = EntryType.Expense }) { Amount = 7 }
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
