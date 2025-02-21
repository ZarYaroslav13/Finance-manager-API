using FinanceManager.Domain.Models;
using FinanceManager.Domain.Models.Base;

namespace FinanceManager.Domain.Tests.Data.Models.Base;

public static class HumanModelTestsDataProvider
{
    public static IEnumerable<object[]> EqualsSameValuesReturnsTrueTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            }
        }
    };

    public static IEnumerable<object[]> EqualsDifferentValuesReturnsFalseTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new HumanModel
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            }
        },
        new object[]
        {
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new HumanModel
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            }
        },
        new object[]
        {
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.doe@example.com",
                Password = "password123"
            }
        },
        new object[]
        {
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new HumanModel
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.smith@example.com",
                Password = "password123"
            }
        },
        new object[]
        {
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password456"
            }
        },
        new object[]
        {
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            null
        },
        new object[]
        {
            new HumanModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new AccountModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password456"
            }
        }
    };
}
