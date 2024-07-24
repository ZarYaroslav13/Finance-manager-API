using DomainLayer.Models;

namespace DomainLayerTests.Data;

public class FinanceOperationDataProvider
{
    private static FinanceOperationType _randomIncomeType =
                new FinanceOperationType()
                {
                    Id = 3,
                    Description = "Description",
                    EntryType = DataLayer.Models.EntryType.Income,
                    Name = "Name",
                    WalletId = 1,
                    WalletName = "WalletName"
                };

    private static FinanceOperationType _randomExpenseType =
                new FinanceOperationType()
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
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            true
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            true
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new Income(_randomIncomeType){
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            true
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Date =  DateTime.MinValue,

            },
            new Income(_randomIncomeType){
                Id = 1,
                Date =  DateTime.MinValue,

            },
            true
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            true
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new Income(_randomIncomeType){
                Id = 2,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            false
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 2000,
                Date =  DateTime.MinValue,

            },
            false
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MaxValue,

            },
            false
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new Expense(_randomExpenseType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            false
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            null,
            false
        },
        new object[]
        {
            new Income(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            new Wallet(),
            false
        }
    };


}
