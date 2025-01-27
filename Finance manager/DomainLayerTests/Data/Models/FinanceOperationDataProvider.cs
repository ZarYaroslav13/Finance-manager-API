using DomainLayer.Models;

namespace DomainLayerTests.Data.Models;

public class FinanceOperationDataProvider
{
    private static FinanceOperationTypeModel _randomIncomeType =
                new FinanceOperationTypeModel()
                {
                    Id = 3,
                    Description = "Description",
                    EntryType = Infrastructure.Models.EntryType.Income,
                    Name = "Name",
                    WalletId = 1,
                    WalletName = "WalletName"
                };

    private static FinanceOperationTypeModel _randomExpenseType =
                new FinanceOperationTypeModel()
                {
                    Id = 4,
                    Description = "Description",
                    EntryType = Infrastructure.Models.EntryType.Expense,
                    Name = "Name",
                    WalletId = 1,
                    WalletName = "WalletName"
                };

    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
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
            }
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
            }
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

            }
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

            }
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
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
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

            }
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

            }
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

            }
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
            }
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            null
        },
        new object[]
        {
            new IncomeModel(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            new WalletModel()
        }
    };
}
