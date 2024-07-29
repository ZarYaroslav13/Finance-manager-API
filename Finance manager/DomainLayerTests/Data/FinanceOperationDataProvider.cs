using DomainLayer.Models;

namespace DomainLayerTests.Data;

public class FinanceOperationDataProvider
{
    private static FinanceOperationTypeModel _randomIncomeType =
                new FinanceOperationTypeModel()
                {
                    Id = 3,
                    Description = "Description",
                    EntryType = DataLayer.Models.EntryType.Income,
                    Name = "Name",
                    WalletId = 1,
                    WalletName = "WalletName"
                };

    private static FinanceOperationTypeModel _randomExpenseType =
                new FinanceOperationTypeModel()
                {
                    Id = 4,
                    Description = "Description",
                    EntryType = DataLayer.Models.EntryType.Exponse,
                    Name = "Name",
                    WalletId = 1,
                    WalletName = "WalletName"
                };

    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            true
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            true
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new IncomeModel(_randomIncomeType){
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            true
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Date =  DateTime.MinValue,

            },
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Date =  DateTime.MinValue,

            },
            true
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            true
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new IncomeModel(_randomIncomeType){
                Id = 2,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            false
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 2000,
                Date =  DateTime.MinValue,

            },
            false
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MaxValue,

            },
            false
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new ExpenseModel(_randomExpenseType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            false
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            null,
            false
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            new WalletModel(),
            false
        }
    };


}
