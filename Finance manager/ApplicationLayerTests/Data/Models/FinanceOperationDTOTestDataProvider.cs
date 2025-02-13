using FinanceManager.ApiService.Models;
using DomainLayer.Models;

namespace ApplicationLayerTests.Data.Models;

public static class FinanceOperationDTOTestDataProvider
{
    private static FinanceOperationTypeDTO _randomIncomeType =
                new FinanceOperationTypeDTO()
                {
                    Id = 3,
                    Description = "Description",
                    EntryType = Infrastructure.Models.EntryType.Income,
                    Name = "Name",
                    WalletId = 1,
                    WalletName = "WalletName"
                };

    private static FinanceOperationTypeDTO _randomExpenseType =
                new FinanceOperationTypeDTO()
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
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType
            },
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType
            }
        },
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType
            },
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType
            }
        },
        new object[]
        {
            new IncomeDTO(){
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            },
            new IncomeDTO(){
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            }
        },
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType
            },
            new IncomeDTO(){
                Id = 1,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            }
        },
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Type = _randomIncomeType

            },
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Type = _randomIncomeType

            },
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            },
            new IncomeDTO(){
                Id = 2,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            }
        },
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            },
            new IncomeDTO(){
                Id = 1,
                Amount = 2000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            }
        },
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            },
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MaxValue,
                Type = _randomIncomeType

            }
        },
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomIncomeType

            },
            new ExpenseDTO(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                Type = _randomExpenseType
            }
        },
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Type = _randomIncomeType
            },
            null
        },
        new object[]
        {
            new IncomeDTO(){
                Id = 1,
                Amount = 1000,
                Type = _randomIncomeType
            },
            new WalletModel()
        }
    };
}
