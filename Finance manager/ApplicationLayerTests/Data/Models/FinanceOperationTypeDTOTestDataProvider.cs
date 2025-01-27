using ApplicationLayer.Models;
using DomainLayer.Models;

namespace ApplicationLayerTests.Data.Models;

public static class FinanceOperationTypeDTOTestDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2, WalletName = "WalletName"},
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2, WalletName = "WalletName"}
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){
                Id = 1,
                Description = "Description",
                EntryType = Infrastructure.Models.EntryType.Expense,
                Name = "Name",
                WalletId = 2},
            new FinanceOperationTypeDTO(){
                Id = 1,
                Description = "Description",
                EntryType = Infrastructure.Models.EntryType.Expense,
                Name = "Name",
                WalletId = 2}
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name"},
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name"}
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeDTO(){ Id = 1, EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2,}
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, WalletId = 2,},
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, WalletId = 2,}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description1", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2,}
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name1", WalletId = 2,},
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2,}
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Income, Name = "Name", WalletId = 2,}
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 3,}
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            null
        },
        new object[]
        {
            new FinanceOperationTypeDTO(){ Id = 1, Description = "Description", EntryType = Infrastructure.Models.EntryType.Expense, Name = "Name", WalletId = 2},
            new WalletModel()
        }
    };
}
