using DataLayer;
using DataLayer.Models;

namespace DataLayerTests.Data;

public class FinanceOperationTypeDataProvider
{
    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name", WalletId = 2},
            true
        },
        new object[]
        {
            new FinanceOperationType(){
                Id = 1,
                Description = "Description",
                EntryType = EntryType.Exponse,
                FinanceOperations = FillerBbData.FinanceOperations
                    .Where(fo => fo.TypeId == 1)
                    .ToList(),
                Name = "Name",
                WalletId = 2},
            new FinanceOperationType(){
                Id = 1,
                Description = "Description",
                EntryType = EntryType.Exponse,
                FinanceOperations = FillerBbData.FinanceOperations
                    .Where(fo => fo.TypeId == 1)
                    .ToList(),
                Name = "Name",
                WalletId = 2},
            true
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name"},
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name"},
            true
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationType(){ Id = 1, EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            true
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, WalletId = 2,},
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, WalletId = 2,},
            true
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description1", EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            false
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name1", WalletId = 2,},
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            false
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Income, Name = "Name", WalletId = 2,},
            false
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name", WalletId = 3,},
            false
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, Name = "Name", WalletId = 2,},
            null,
            false
        },
        new object[]
        {
            new FinanceOperationType(){ Id = 1, Description = "Description", EntryType = EntryType.Exponse, FinanceOperations = null, Name = "Name", WalletId = 2},
            new Wallet(),
            false
        }
    };
}
