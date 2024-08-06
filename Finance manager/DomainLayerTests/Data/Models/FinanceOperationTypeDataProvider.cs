
using DomainLayer.Models;

namespace DomainLayerTests.Data.Models;

public class FinanceOperationTypeDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2, WalletName = "WalletName"},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2, WalletName = "WalletName"}
        },
        new object[]
        {
            new FinanceOperationTypeModel(){
                Id = 1,
                Description = "Description",
                EntryType = DataLayer.Models.EntryType.Expense,
                Name = "Name",
                WalletId = 2},
            new FinanceOperationTypeModel(){
                Id = 1,
                Description = "Description",
                EntryType = DataLayer.Models.EntryType.Expense,
                Name = "Name",
                WalletId = 2}
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name"},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name"}
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2,}
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, WalletId = 2,}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description1", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2,}
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name1", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2,}
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Income, Name = "Name", WalletId = 2,}
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 3,}
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2,},
            null
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Expense, Name = "Name", WalletId = 2},
            new WalletModel()
        }
    };
}
