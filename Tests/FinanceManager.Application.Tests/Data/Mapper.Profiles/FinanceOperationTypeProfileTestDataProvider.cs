using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Tests.Data.Mapper.Profiles;

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
                EntryType = Infrastructure.Models.EntryType.Income,
                WalletId = 1,
                WalletName = "WalletName"
            }
        }
    };
}
