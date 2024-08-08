using DomainLayer.Models;
using Finance_manager_API.Models;

namespace ApplicationLayerTests.Data.Models;

public static class FinanceOperationDTOTestDataProvider
{
    private static FinanceOperationTypeDTO _randomIncomeType =
                new FinanceOperationTypeDTO()
                {
                    Id = 3,
                    Description = "Description",
                    EntryType = DataLayer.Models.EntryType.Income,
                    Name = "Name",
                    WalletId = 1,
                    WalletName = "WalletName"
                };

    private static FinanceOperationTypeDTO _randomExpenseType =
                new FinanceOperationTypeDTO()
                {
                    Id = 4,
                    Description = "Description",
                    EntryType = DataLayer.Models.EntryType.Expense,
                    Name = "Name",
                    WalletId = 1,
                    WalletName = "WalletName"
                };

    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            }
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            },
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            }
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new IncomeDTO(_randomIncomeType){
                Amount = 1000,
                Date =  DateTime.MinValue,

            }
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Date =  DateTime.MinValue,

            },
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Date =  DateTime.MinValue,

            }
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new IncomeDTO(_randomIncomeType){
                Id = 2,
                Amount = 1000,
                Date =  DateTime.MinValue,

            }
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 2000,
                Date =  DateTime.MinValue,

            }
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MaxValue,

            }
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,

            },
            new ExpenseDTO(_randomExpenseType){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
            }
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            null
        },
        new object[]
        {
            new IncomeDTO(_randomIncomeType){
                Id = 1,
                Amount = 1000,

            },
            new WalletModel()
        }
    };
}
