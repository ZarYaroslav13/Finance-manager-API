using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using FakeItEasy;
using System.Security.Claims;

namespace ApplicationLayerTests.Data.Security.Jwt;

public static class TokenManagerTestDataProvider
{
    public static IEnumerable<object[]> ConstructorArgumentsAreNullThrowsArgumentNullExceptionTestData { get; } = new List<object[]>
    {
        new object[] { null, null, null},
        new object[] { null, A.Fake<IAdminService>(), A.Fake<IMapper>()},
        new object[] { A.Fake<IAccountService>(), null, A.Fake<IMapper>()},
        new object[] { A.Fake<IAccountService>(), A.Fake<IAdminService>(), null},
        new object[] { A.Fake<IAccountService>(), null, null},
        new object[] { null, A.Fake<IAdminService>(), null},
        new object[] { null, null, A.Fake<IMapper>()},
    };

    public static IEnumerable<object[]> GetIdentityAsyncNullOrEmptyEmailOrPasswordThrowsArgumentNullExceptionTestData { get; } = new List<object[]>
    {
        new object[]{ null, "password" },
        new object[]{ "email@example.com", null },
        new object[]{ null, null },
        new object[]{ "", "password" },
        new object[]{ "email@example.com", "" },
        new object[]{ "", "" },
        new object[]{ " ", "password" },
        new object[]{ "email@example.com", " " },
        new object[]{ " ", " " }
    };

    public static IEnumerable<object[]> GetAccountIdentityAsyncValidCredentialsReturnsClaimsIdentityTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new AccountModel()
            {
                Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email@gmail.com", Password = "password"
            },
            new ClaimsIdentity(new List<Claim>
            {
                new(nameof(AccountDTO.Id), "1"),
                new(ClaimsIdentity.DefaultNameClaimType, "Email@gmail.com"),
                new(ClaimsIdentity.DefaultRoleClaimType, AccountService.NameAccountRole)
            }, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType)
        }
    };

    public static IEnumerable<object[]> GetAdminIdentityAsyncValidCredentialsReturnsClaimsIdentityTestData { get; } = new List<object[]>
    {
        new object[]
        {
            new AdminModel()
            {
                Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "EmailAdmin@gmail.com", Password = "password"
            },
            new ClaimsIdentity(new List<Claim>
            {
                new(nameof(AdminDTO.Id), "1"),
                new(ClaimsIdentity.DefaultNameClaimType, "EmailAdmin@gmail.com"),
                new(ClaimsIdentity.DefaultRoleClaimType, AdminService.NameAdminRole)
            }, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType)
        }
    };
}
