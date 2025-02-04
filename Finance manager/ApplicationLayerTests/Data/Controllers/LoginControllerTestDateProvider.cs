using Server.Security.Jwt;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using FakeItEasy;

namespace ApplicationLayerTests.Data.Controllers;

public static class LoginControllerTestDateProvider
{
    public static IEnumerable<object[]> ConstructorArgumentIsEqualNullThrowsArgumentNullExceptionTestData { get; } = new List<object[]>()
    {
        new object[] { A.Fake<IAdminService>(), A.Fake<IAccountService>(), null },
        new object[] { A.Fake<IAdminService>(), null, A.Fake<ITokenManager>() },
        new object[] { null, A.Fake<IAccountService>(), A.Fake<ITokenManager>() },
        new object[] { A.Fake<IAdminService>(), null, null },
        new object[] { null, A.Fake<IAccountService>(), null },
        new object[] { null, null, A.Fake<ITokenManager>() },
        new object[] { null, null, null },
    };

    public static IEnumerable<object[]> SignInAsyncInvalidCredentialReturnsBadRequestTestData { get; } = new List<object[]>()
    {
        new object[] { "test@example.com", "invalidpassword"}
    };

    public static IEnumerable<object[]> SignInAsyncNullOrEmptyCredentialsReturnsBadRequestTestData { get; } = new List<object[]>()
    {
        new object[] { "test@example.com", ""},
        new object[] { "test@example.com", " "},
        new object[] { "test@example.com", null},
        new object[] { "", "password"},
        new object[] { " ", "password"},
        new object[] { null, "password"},
        new object[] { "", ""},
        new object[] { "", " "},
        new object[] { "", null},
        new object[] { " ", ""},
        new object[] { null, ""},
        new object[] { " ", " "},
        new object[] { " ", null},
        new object[] { null, " "},
        new object[] { null, null},
    };
}
