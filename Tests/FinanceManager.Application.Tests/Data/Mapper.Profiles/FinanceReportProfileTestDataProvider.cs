using Infrastructure.Models;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Tests.Data.Mapper.Profiles;

public static class FinanceReportProfileTestDataProvider
{
    private static List<FinanceOperationTypeModel> _financeOperationTypes = new()
    {
        new FinanceOperationTypeModel() { Id = 1, EntryType = EntryType.Income},
        new FinanceOperationTypeModel() { Id = 2, EntryType = EntryType.Income},
        new FinanceOperationTypeModel() { Id = 3, EntryType = EntryType.Expense},
        new FinanceOperationTypeModel() { Id = 4, EntryType = EntryType.Expense}
    };

    private static List<FinanceOperationModel> _financeOperations = new()
    {
        new IncomeModel(_financeOperationTypes[0]) { Id = 0, Amount = 23},
        new IncomeModel(_financeOperationTypes[1]) { Id = 1, Amount = 31},
        new ExpenseModel(_financeOperationTypes[2]) { Id = 2, Amount = 12},
        new ExpenseModel(_financeOperationTypes[3]) { Id = 3, Amount = 7 }
    };

    public static IEnumerable<object[]> DomainFinanceReport { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceReportModel(1, "Name", new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue})
            {
                 Operations = _financeOperations
            }
        }
    };
}
