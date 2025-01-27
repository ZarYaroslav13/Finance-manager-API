using Infrastructure.Models;
using DomainLayer.Models;

namespace DomainLayerTests.Data.Services;

public static class AdminServiceTestsDataProvider
{
    public static IEnumerable<object[]> GetAdminsShouldReturnMappedAdminModelsTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new List<Admin> { new Admin { Email = "test@test.com", Password = "hashedPassword" } },
            new List<AdminModel> { new AdminModel { Email = "test@test.com", Password = "hashedPassword" } }
        }
    };

    public static IEnumerable<object[]> TrySignInAsyncWithValidCredentialsShouldReturnAdminModelTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new Admin { Email = "test@test.com", Password = "password" },
            new AdminModel { Email = "test@test.com", Password = "password" }
        }
    };

    public static IEnumerable<object[]> TrySignInAsyncWithInvalidCredentialsShouldReturnNullTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new List<Admin> { new Admin { Email = "test@test.com", Password = "hashedPassword" } },
            "test@test.com",
            "wrongpassword"
        },
        new object[]
        {
            new List<Admin> { new Admin { Email = "test@test.com", Password = "hashedPassword" } },
            "wrongtest@test.com",
            "hashedPassword"
        }
    };

    public static IEnumerable<object[]> IsItAdminWithAdminEmailShouldReturnTrueTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new List<Admin> { new Admin { Email = "test@test.com" }, new Admin { Email = "test@test1.com" }, new Admin { Email = "test@test2.com" } },
            "test@test.com"
        },
        new object[]
        {
            new List<Admin> { new Admin { Email = "test@test.com" }, new Admin { Email = "test@test1.com" }, new Admin { Email = "test@test2.com" } },
            "test@test1.com"
        },
        new object[]
        {
            new List<Admin> { new Admin { Email = "test@test.com" }, new Admin { Email = "test@test1.com" }, new Admin { Email = "test@test2.com" } },
            "test@test2.com"
        }
    };

    public static IEnumerable<object[]> IsItAdminWithNonAdminEmailShouldReturnFalseTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new List<Admin> { new Admin { Email = "test@test.com" }, new Admin { Email = "test@test1.com" }, new Admin { Email = "test@test2.com" } },
            "nonadmin@test.com"
        }
    };
}
