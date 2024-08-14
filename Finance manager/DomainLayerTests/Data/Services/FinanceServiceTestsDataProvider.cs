using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayerTests.Data.Services;

public static class FinanceServiceTestsDataProvider
{
    public static List<FinanceOperationType> FinanceOperationTypes = new()
    {
        new FinanceOperationType()
                {
                    Id = 1,
                    Name = "Salary",
                    Description = "Monthly salary",
                    EntryType = EntryType.Income,
                    WalletId = 1
                },
        new FinanceOperationType()
                {
                    Id = 2,
                    Name = "Groceries",
                    Description = "Weekly groceries",
                    EntryType = EntryType.Expense,
                    WalletId = 1
                },
        new FinanceOperationType()
                {
                    Id = 3,
                    Name = "Rent",
                    Description = "Monthly rent payment",
                    EntryType = EntryType.Expense,
                    WalletId = 1
                }
    };

    public static List<List<FinanceOperation>> FinanceOperations = new()
    {
        new List<FinanceOperation>
        {
            new FinanceOperation() { Id = 1, Amount = 500, Date = new DateTime(2024, 1, 1), TypeId = FinanceOperationTypes[0].Id, Type = FinanceOperationTypes[0] },
            new FinanceOperation() { Id = 2, Amount = 100, Date = new DateTime(2024, 1, 2), TypeId = FinanceOperationTypes[0].Id, Type = FinanceOperationTypes[0] },
            new FinanceOperation() { Id = 3, Amount = 750, Date = new DateTime(2024, 1, 3), TypeId = FinanceOperationTypes[0].Id, Type = FinanceOperationTypes[0] },
            new FinanceOperation() { Id = 4, Amount = 120, Date = new DateTime(2024, 1, 4), TypeId = FinanceOperationTypes[0].Id, Type = FinanceOperationTypes[0] }
        },
        new List<FinanceOperation>
        {
            new FinanceOperation() { Id = 1, Amount = 500, Date = new DateTime(2024, 1, 1), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] },
            new FinanceOperation() { Id = 2, Amount = 100, Date = new DateTime(2024, 1, 2), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] },
            new FinanceOperation() { Id = 3, Amount = 750, Date = new DateTime(2024, 1, 3), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] },
            new FinanceOperation() { Id = 4, Amount = 120, Date = new DateTime(2024, 1, 4), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] }
        },
        new List<FinanceOperation>
        {
            new FinanceOperation() { Id = 1, Amount = 500, Date = new DateTime(2024, 1, 1), TypeId = FinanceOperationTypes[2].Id, Type = FinanceOperationTypes[2] },
            new FinanceOperation() { Id = 2, Amount = 100, Date = new DateTime(2024, 1, 2), TypeId = FinanceOperationTypes[2].Id, Type = FinanceOperationTypes[2] },
            new FinanceOperation() { Id = 3, Amount = 750, Date = new DateTime(2024, 1, 3), TypeId = FinanceOperationTypes[2].Id, Type = FinanceOperationTypes[2] },
            new FinanceOperation() { Id = 4, Amount = 120, Date = new DateTime(2024, 1, 4), TypeId = FinanceOperationTypes[2].Id, Type = FinanceOperationTypes[2] }
        }
    };

    public static IEnumerable<object[]> GetAllFinanceOperationTypesOfWalletTestData { get; } = new List<object[]>
    {
        new object[]
        {
            FinanceOperationTypes, 1
        }
    };

    public static IEnumerable<object[]> AddFinanceOperationTypeTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationTypeModel()
            {
                Id = 0,
                Description = "Description",
                EntryType = EntryType.Income,
                Name = "Name"
            },
            new FinanceOperationType()
            {
                Id = 0,
                Description = "Description",
                EntryType = EntryType.Income,
                Name = "Name"
            }
        }
    };

    public static IEnumerable<object[]> UpdateFinanceOperationTypeTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new FinanceOperationTypeModel()
            {
                Id = 1,
                Description = "Description",
                EntryType = EntryType.Income,
                Name = "Name"
            },
            new FinanceOperationType()
            {
                Id = 1,
                Description = "Description",
                EntryType = EntryType.Income,
                Name = "Name"
            }
        }
    };

    public static IEnumerable<object[]> DeleteFinanceOperationTypeTestData { get; } = new List<object[]>
    {
        new object[]
        {
            1,
            new List<FinanceOperation>
            {
                new FinanceOperation() { Id = 1, Amount = 500, Date = new DateTime(2024, 1, 1), TypeId = FinanceOperationTypes[0].Id, Type = FinanceOperationTypes[0] },
                new FinanceOperation() { Id = 2, Amount = 100, Date = new DateTime(2024, 1, 2), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] },
                new FinanceOperation() { Id = 3, Amount = 750, Date = new DateTime(2024, 1, 3), TypeId = FinanceOperationTypes[2].Id, Type = FinanceOperationTypes[2] }
            }
        }
    };

    public static IEnumerable<object[]> GetAllFinanceOperationsOfTypeTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new List<FinanceOperation>()
            {
                new FinanceOperation() { Id = 1, Amount = 500, Date = new DateTime(2024, 1, 1), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] },
                new FinanceOperation() { Id = 2, Amount = 100, Date = new DateTime(2024, 1, 2), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] },
                new FinanceOperation() { Id = 3, Amount = 750, Date = new DateTime(2024, 1, 3), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] },
                new FinanceOperation() { Id = 4, Amount = 120, Date = new DateTime(2024, 1, 4), TypeId = FinanceOperationTypes[1].Id, Type = FinanceOperationTypes[1] }
            },
            FinanceOperationTypes[1].Id
        }
    };

    public static IEnumerable<object[]> AddFinanceOperationTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new IncomeModel(new FinanceOperationTypeModel()
            {
                Id = 2,
                Description = "Description",
                EntryType = EntryType.Income,
                Name = "Name"
            })
            {
                Id = 0, Amount = 100, Date = DateTime.MinValue
            },
            new FinanceOperation()
            {
                Id = 0, Amount = 100, Date = DateTime.MinValue, TypeId = 2, Type = new FinanceOperationType()
                {
                    Id = 2,
                    Description = "Description",
                    EntryType = EntryType.Income,
                    Name = "Name"
                }
            }
        }
    };

    public static IEnumerable<object[]> UpdateFinanceOperationTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new IncomeModel(new FinanceOperationTypeModel()
            {
                Id = 2,
                Description = "Description",
                EntryType = EntryType.Income,
                Name = "Name"
            })
            {
                Id = 1, Amount = 100, Date = DateTime.MinValue
            },
            new FinanceOperation()
            {
                Id = 1, Amount = 100, Date = DateTime.MinValue, TypeId = 2, Type = new FinanceOperationType()
                {
                    Id = 2,
                    Description = "Description",
                    EntryType = EntryType.Income,
                    Name = "Name"
                }
            }
        }
    };
}
