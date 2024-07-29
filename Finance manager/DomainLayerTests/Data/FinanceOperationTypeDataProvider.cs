
using DomainLayer.Models;

namespace DomainLayerTests.Data;

public class FinanceOperationTypeDataProvider
{
    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2, WalletName = "WalletName"},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2, WalletName = "WalletName"},
            true
        },
        new object[]
        {
            new FinanceOperationTypeModel(){
                Id = 1,
                Description = "Description",
                EntryType = DataLayer.Models.EntryType.Exponse,
                Name = "Name",
                WalletId = 2},
            new FinanceOperationTypeModel(){
                Id = 1,
                Description = "Description",
                EntryType = DataLayer.Models.EntryType.Exponse,
                Name = "Name",
                WalletId = 2},
            true
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name"},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name"},
            true
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2,},
            true
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, WalletId = 2,},
            true
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description1", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2,},
            false
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name1", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2,},
            false
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Income, Name = "Name", WalletId = 2,},
            false
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 3,},
            false
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2,},
            null,
            false
        },
        new object[]
        {
            new FinanceOperationTypeModel(){ Id = 1, Description = "Description", EntryType = DataLayer.Models.EntryType.Exponse, Name = "Name", WalletId = 2},
            new WalletModel(),
            false
        }
    };
}
