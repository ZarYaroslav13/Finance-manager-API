using DataLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests.TestData;

public class FinanceOperationDataProvider
{
    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
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
            },
            true
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
            },
            true
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
            },
            true
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
            },
            true
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
            },
            true
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
                Id = 2,
                Amount = 1000,
                Date =  DateTime.MinValue,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            false
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
            },
            false
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
            },
            false
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
            },
            false
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            null,
            false
        },
        new object[]
        {
            new FinanceOperation(){
                Id = 1,
                Amount = 1000,
                TypeId = 3,
                Type = randomFinanceOperationType
            },
            new Wallet(),
            false
        }
    };

    private static FinanceOperationType randomFinanceOperationType = FillerBbData.FinanceOperationTypes.FirstOrDefault(fo => fo.Id == 3);
}
