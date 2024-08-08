using ApplicationLayer.Models;

namespace ApplicationLayerTests.Data.Mapper.Profiles;

public static class FinanceOperationProfileTestDataProvider
{
    public static IEnumerable<object[]> FinanceOperation { get; } = new List<object[]>
    {
        new object[]
        {
            new IncomeDTO(
                new FinanceOperationTypeDTO()
                {
                    Id = 1, Name = "TypeName", Description = "Description", EntryType = DataLayer.Models.EntryType.Income, WalletId = 2, WalletName = "WalletName"
                })
            {
                Id = 3, Amount = 713, Date = DateTime.Now
            }
        },
         new object[]
        {
            new ExpenseDTO(
                new FinanceOperationTypeDTO()
                {
                    Id = 1, Name = "TypeName", Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, WalletId = 2, WalletName = "WalletName"
                })
            {
                Id = 3, Amount = 713, Date = DateTime.Now
            }
        }
    };
}
