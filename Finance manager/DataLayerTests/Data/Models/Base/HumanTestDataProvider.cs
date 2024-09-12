using DataLayer.Models.Base;

namespace DataLayerTests.Data.Models.Base;

public static class HumanTestDataProvider
{
    public static IEnumerable<object[]> EqualsSamePropertiesReturnsTrueTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            }
        }
    };

    public static IEnumerable<object[]> EqualsDifferentPropertiesReturnsFalseTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new Human
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
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new Human
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
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new Human
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
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Password = "password123"
            }
        },
        new object[]
        {
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password456"
            }
        }
    };

    public static IEnumerable<object[]> GetHashCodeSamePropertiesReturnsSameHashCodeTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            }
        }
    };

    public static IEnumerable<object[]> GetHashCodeDifferentPropertiesReturnsDifferentHashCodeTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new Human
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            },
            new Human
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Password = "password456"
            }
        }
    };
}
