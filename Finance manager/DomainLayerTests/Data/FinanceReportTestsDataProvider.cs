using DomainLayer.Models;

namespace DomainLayerTests.Data;

public class FinanceReportTestsDataProvider
{
    private static WalletModel _wallet = new() { Id = 1, Name = "test" };
    private static Period _period = new() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };
    private static List<FinanceOperationModel> _financeOperations = new()
    {
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = DataLayer.Models.EntryType.Income}) { Amount = 23},
        new IncomeModel(new FinanceOperationTypeModel() { EntryType = DataLayer.Models.EntryType.Income}) { Amount = 31},
        new ExpenseModel(new FinanceOperationTypeModel() { EntryType = DataLayer.Models.EntryType.Exponse}) { Amount = 12},
        new ExpenseModel(new FinanceOperationTypeModel() { EntryType = DataLayer.Models.EntryType.Exponse }) { Amount = 7 }
    };

    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            true
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){Operations = _financeOperations },
            true
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1,},
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1,},
            true
        },

        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id + 1, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            false
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name + '1', _period){ Id = 1, Operations = _financeOperations },
            false
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 100, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, new Period()){ Id = 100, Operations = _financeOperations },
            false
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 2, Operations = _financeOperations },
            false
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = new() },
            false
        },


        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            null,
            false
        },
        new object[]
        {
            new FinanceReportModel(_wallet.Id, _wallet.Name, _period){ Id = 1, Operations = _financeOperations },
            new WalletModel(),
            false
        }
    };
}
