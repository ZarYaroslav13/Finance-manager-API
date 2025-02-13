using FinanceManager.ApiService.Models;
using FinanceManager.ApiService.Security;
using FinanceManager.ApiService.Security.Jwt;
using ApplicationLayerTests.Data.Security.Jwt;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using FakeItEasy;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApplicationLayerTests.Security.Jwt;

[TestClass]
public class TokenManagerTests
{
    private readonly IAccountService _accountService;
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;
    private readonly ITokenManager _tokenManager;
    private readonly IOptions<AuthOptions> _diAuthOptions;
    private readonly AuthOptions _authOptions;

    public TokenManagerTests()
    {
        _accountService = A.Fake<IAccountService>();
        _adminService = A.Fake<IAdminService>();
        _mapper = A.Fake<IMapper>();
        _diAuthOptions = A.Fake<IOptions<AuthOptions>>();
        _authOptions = A.Fake<AuthOptions>();

        A.CallTo(() => _diAuthOptions.Value).Returns(_authOptions);

        _tokenManager = new TokenManager(_accountService, _adminService, _mapper, _diAuthOptions);
    }

    [TestMethod]
    [DynamicData(nameof(TokenManagerTestDataProvider.ConstructorArgumentsAreNullThrowsArgumentNullExceptionTestData), typeof(TokenManagerTestDataProvider))]
    public void Constructor_ArgumentsAreNull_ThrowsArgumentNullException(IOptions<AuthOptions> options, IAccountService accountService, IAdminService adminService, IMapper mapper)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new TokenManager(accountService, adminService, mapper, options));
    }

    [TestMethod]
    [DynamicData(nameof(TokenManagerTestDataProvider.GetIdentityAsyncNullOrEmptyEmailOrPasswordThrowsArgumentNullExceptionTestData), typeof(TokenManagerTestDataProvider))]
    public void GetAccountIdentityAsync_NullOrEmptyEmailOrPassword_ThrowsArgumentNullException(string email, string password)
    {
        Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _tokenManager.GetAccountIdentityAsync(email, password));
    }

    [TestMethod]
    [DynamicData(nameof(TokenManagerTestDataProvider.GetIdentityAsyncNullOrEmptyEmailOrPasswordThrowsArgumentNullExceptionTestData), typeof(TokenManagerTestDataProvider))]
    public void GetAdminIdentityAsync_NullOrEmptyEmailOrPassword_ThrowsArgumentNullException(string email, string password)
    {
        Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _tokenManager.GetAdminIdentityAsync(email, password));
    }

    [TestMethod]
    public void CreateToken_ValidIdentity_ReturnsJwtToken()
    {
        var identity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, "test@example.com")
        });

        var token = _tokenManager.CreateToken(identity);

        Assert.IsNotNull(token);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        Assert.AreEqual(_authOptions.ISSUER, jwtToken.Issuer);
        Assert.AreEqual(_authOptions.AUDIENCE, jwtToken.Audiences.FirstOrDefault());
    }

    [TestMethod]
    [DynamicData(nameof(TokenManagerTestDataProvider.GetAccountIdentityAsyncValidCredentialsReturnsClaimsIdentityTestData), typeof(TokenManagerTestDataProvider))]
    public async Task GetAccountIdentityAsync_ValidCredentials_ReturnsClaimsIdentity(AccountModel account, ClaimsIdentity identity)
    {
        A.CallTo(() => _accountService.TrySignInAsync(account.Email, account.Password))
            .Returns(account);
        A.CallTo(() => _mapper.Map<AccountDTO>(account))
            .ReturnsLazily((object a) =>
            {
                var acc = (AccountModel)a;

                return new()
                {
                    Id = acc.Id,
                    FirstName = acc.FirstName,
                    LastName = acc.LastName,
                    Email = acc.Email,
                    Password = acc.Password
                };
            });
        A.CallTo(() => _accountService.GetNameAccountRole())
            .Returns(AccountService.NameAccountRole);

        var result = await _tokenManager.GetAccountIdentityAsync(account.Email, account.Password);

        Assert.IsNotNull(result);
        Assert.AreEqual(identity.Claims.Count(), result.Claims.Count());

        var identityClaims = identity.Claims.ToList();
        var resultClaims = result.Claims.ToList();

        for (int i = 0; i < identityClaims.Count; i++)
        {
            Assert.AreEqual(identityClaims[i].Type, resultClaims[i].Type);
            Assert.AreEqual(identityClaims[i].Value, resultClaims[i].Value);
        }
    }

    [TestMethod]
    public async Task GetAccountIdentityAsync_InvalidCredentials_ReturnsNull()
    {
        var email = "test@example.com";
        var password = "password";

        A.CallTo(() => _accountService.TrySignInAsync(email, password))
            .Returns(Task.FromResult<AccountModel>(null));
        A.CallTo(() => _mapper.Map<AccountDTO>(null))
            .Returns(null);

        var result = await _tokenManager.GetAccountIdentityAsync(email, password);

        Assert.IsNull(result);
    }

    [TestMethod]
    [DynamicData(nameof(TokenManagerTestDataProvider.GetAdminIdentityAsyncValidCredentialsReturnsClaimsIdentityTestData), typeof(TokenManagerTestDataProvider))]
    public async Task GetAdminIdentityAsync_ValidCredentials_ReturnsClaimsIdentity(AdminModel admin, ClaimsIdentity identity)
    {
        A.CallTo(() => _adminService.TrySignInAsync(admin.Email, admin.Password))
            .Returns(admin);
        A.CallTo(() => _mapper.Map<AdminDTO>(admin))
            .ReturnsLazily((object a) =>
            {
                var acc = (AdminModel)a;

                return new()
                {
                    Id = acc.Id,
                    FirstName = acc.FirstName,
                    LastName = acc.LastName,
                    Email = acc.Email,
                    Password = acc.Password
                };
            });
        A.CallTo(() => _adminService.GetNameAdminRole())
            .Returns(AdminService.NameAdminRole);

        var result = await _tokenManager.GetAdminIdentityAsync(admin.Email, admin.Password);

        Assert.IsNotNull(result);
        Assert.AreEqual(identity.Claims.Count(), result.Claims.Count());

        var identityClaims = identity.Claims.ToList();
        var resultClaims = result.Claims.ToList();

        for (int i = 0; i < identityClaims.Count; i++)
        {
            Assert.AreEqual(identityClaims[i].Type, resultClaims[i].Type);
            Assert.AreEqual(identityClaims[i].Value, resultClaims[i].Value);
        }
    }

    [TestMethod]
    public async Task GetAdminIdentityAsync_InvalidCredentials_ReturnsNull()
    {
        // Arrange
        var email = "admin@example.com";
        var password = "password";

        A.CallTo(() => _adminService.TrySignInAsync(email, password))
            .Returns(Task.FromResult<AdminModel>(null));
        A.CallTo(() => _mapper.Map<AdminDTO>(null))
            .Returns(null);

        var result = await _tokenManager.GetAdminIdentityAsync(email, password);

        Assert.IsNull(result);
    }
}
