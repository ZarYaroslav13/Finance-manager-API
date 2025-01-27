using ApplicationLayer.Models;

namespace ApplicationLayerTests.Data.Mapper.Profiles;

public static class FinanceOperationProfileTestDataProvider
{
    public static IEnumerable<object[]> FinanceOperation { get; } = new List<object[]>
    {
        new object[]
        {
            new IncomeDTO()
            {
                Id = 3, Amount = 713, Date = DateTime.Now, Type = new FinanceOperationTypeDTO()
                {
                    Id = 1, Name = "TypeName", Description = "Description", EntryType = Infrastructure.Models.EntryType.Income, WalletId = 2, WalletName = "WalletName"
                }
            }
        },
         new object[]
        {
            new ExpenseDTO()
            {
                Id = 3, Amount = 713, Date = DateTime.Now, Type = new FinanceOperationTypeDTO()
                {
                    Id = 1, Name = "TypeName", Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, WalletId = 2, WalletName = "WalletName"
                }
            }
        }
    };
}
