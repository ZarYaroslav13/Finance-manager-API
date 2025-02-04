using Infrastructure;
using Infrastructure.Models;

namespace InfractructureTests.Data.Models;

public class FinanceOperationDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            }
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3
            },
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3
            }
        },
        new object[]
        {
            new FinanceOperation(){
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new FinanceOperation(){
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            }
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new FinanceOperation(){
                Id = 1,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            }
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                TypeId = 3,
                Type = randomFinanceOperationType
            }
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new FinanceOperation(){
                Id = 2,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            }
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new FinanceOperation(){
                Id = 1,
                Amount = 2000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            }
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MaxValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            }
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 4,
                Type = FillerBbData.FinanceOperationTypes.FirstOrDefault(fo => fo.Id == 4)
            }
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            null
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new Wallet()
        }
    };

    private static FinanceOperationType randomFinanceOperationType = EntitiesTestDataProvider.FinanceOperationTypes.FirstOrDefault(fo => fo.Id == 3);
}
