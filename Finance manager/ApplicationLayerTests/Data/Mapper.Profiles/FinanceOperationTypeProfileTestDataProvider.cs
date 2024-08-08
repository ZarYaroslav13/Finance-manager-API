using DomainLayer.Models;

namespace ApplicationLayerTests.Data.Mapper.Profiles;

public static class FinanceOperationTypeProfileTestDataProvider
{
    public static IEnumerable<object[]> DomainFinanceOperationTypeModel { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationTypeModel()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                EntryType = DataLayer.Models.EntryType.Income,
                WalletId = 1,
                WalletName = "WalletName"
            }
        }
    };
}
