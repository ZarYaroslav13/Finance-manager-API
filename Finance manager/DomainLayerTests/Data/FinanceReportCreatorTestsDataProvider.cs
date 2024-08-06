using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayerTests.Data;

public static class FinanceReportCreatorTestsDataProvider
{
    private static WalletModel _wallet = new()
    {
        Id = 1,
        Name = "WalletName"
    };

    private static List<FinanceOperationModel> _weekFinanceOperation = new()
    {
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = EntryType.Income}) { Amount = 23},
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = EntryType.Income}) { Amount = 31},
        new ExpenseModel(new FinanceOperationTypeModel() { EntryType = EntryType.Exponse}) { Amount = 12},
        new ExpenseModel(new FinanceOperationTypeModel() { EntryType = EntryType.Exponse }) { Amount = 7 }
    };
    private static FinanceReportModel _weekReport = new(_wallet.Id, _wallet.Name, new Period())
    {
        Operations = _weekFinanceOperation
    };
    public static IEnumerable<object[]> CreateFinanceReportTestData { get; } = new List<object[]>
    {
        new object[]
        {
            _wallet, _weekReport

        }
    };

    private static List<FinanceOperationModel> _dailyFinanceOperation = new()
    {
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = EntryType.Income}) { Amount = 23},
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = EntryType.Income}) { Amount = 31},
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = EntryType.Income}) { Amount = 12},
        new ExpenseModel(new FinanceOperationTypeModel() { EntryType = EntryType.Exponse }) { Amount = 7 }
    };
    private static FinanceReportModel _dailyReport = new(_wallet.Id, _wallet.Name, new Period())
    {
        Operations = _dailyFinanceOperation
    };
    public static IEnumerable<object[]> CreateDailyFinanceReportTestData { get; } = new List<object[]>
    {
        new object[]
        {
            _wallet, _dailyReport

        }
    };
}
