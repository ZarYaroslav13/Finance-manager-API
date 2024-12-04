using ApplicationLayer.Controllers;
using ApplicationLayer.Models;
using ApplicationLayer.Security.Jwt;
using ApplicationLayerTests.Data.Controllers;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;


namespace ApplicationLayerTests.Controllers;

[TestClass]
public class LoginControllerTests
{
    protected readonly IAdminService _adminService;
    protected readonly IAccountService _accountService;
    protected readonly ITokenManager _tokenManager;
    protected readonly IMapper _mapper;
    protected readonly ILogger<LoginController> _logger;
    protected readonly LoginController _controller;

    public LoginControllerTests()
    {
        _adminService = A.Fake<IAdminService>();
        _accountService = A.Fake<IAccountService>();
        _tokenManager = A.Fake<ITokenManager>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<LoginController>>();

        _controller = new(_adminService, _accountService, _tokenManager, _mapper, _logger);
    }

    [TestMethod]
    [DynamicData(nameof(LoginControllerTestDateProvider.ConstructorArgumentIsEqualNullThrowsArgumentNullExceptionTestData), typeof(LoginControllerTestDateProvider))]
    public void Constructor_ArgumentAreEqualNull_ThrowsArgumentNullException(
        IAdminService adminService,
        IAccountService accountService,
        ITokenManager tokenManager)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new LoginController(adminService, accountService, tokenManager, _mapper, _logger));
    }

    [TestMethod]
    [DynamicData(nameof(LoginControllerTestDateProvider.SignInAsyncInvalidCredentialReturnsBadRequestTestData), typeof(LoginControllerTestDateProvider))]
    public async Task SignInAsync_InvalidCredentials_ReturnsBadRequest(string email, string password)
    {
        A.CallTo(() => _tokenManager.GetAccountIdentityAsync(email, password)).Returns<ClaimsIdentity>(null);

        var result = await _controller.SignInAsync(email, password);

        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult.StatusCode.Should().Be(400);
    }

    [TestMethod]
    [DynamicData(nameof(LoginControllerTestDateProvider.SignInAsyncInvalidCredentialReturnsBadRequestTestData), typeof(LoginControllerTestDateProvider))]
    public async Task SignInAdminAsync_InvalidCredentials_ReturnsBadRequest(string email, string password)
    {
        A.CallTo(() => _tokenManager.GetAdminIdentityAsync(email, password)).Returns<ClaimsIdentity>(null);

        var result = await _controller.SignInAdminAsync(email, password);

        var badRequestResult = result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult.StatusCode.Should().Be(400);
    }

    [TestMethod]
    [DynamicData(nameof(LoginControllerTestDateProvider.SignInAsyncNullOrEmptyCredentialsReturnsBadRequestTestData), typeof(LoginControllerTestDateProvider))]
    public async Task SignInAsync_NullOrEmptyCredentials_ReturnsBadRequest(string email, string password)
    {
        var result = await _controller.SignInAsync(email, password);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [TestMethod]
    [DynamicData(nameof(LoginControllerTestDateProvider.SignInAsyncNullOrEmptyCredentialsReturnsBadRequestTestData), typeof(LoginControllerTestDateProvider))]
    public async Task SignInAdminAsync_NullOrEmptyCredentials_ReturnsBadRequest(string email, string password)
    {
        var result = await _controller.SignInAdminAsync(email, password);

        result.Should().BeOfType<BadRequestObjectResult>();
        (result as BadRequestObjectResult).StatusCode.Should().Be(400);
    }

    [TestMethod]
    public async Task SignInAdminAsync_InvalidCredentials_ReturnsBadRequest()
    {
        var email = "test@example.com";
        var password = "invalidpassword";

        var result = await _controller.SignInAsync(email, password);

        result.Should().BeOfType<BadRequestObjectResult>();
        (result as BadRequestObjectResult).StatusCode.Should().Be(400);
    }
    [TestMethod]
    public async Task SignInAsync_ValidCredentials_ReturnsJsonResultWithToken()
    {
        var email = "test@example.com";
        var password = "validpassword";
        var token = "fake-jwt-token";
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email), new(nameof(AccountDTO.Id), "1") });
        A.CallTo(() => _tokenManager.GetAccountIdentityAsync(email, password))
            .Returns(Task.FromResult(identity));
        A.CallTo(() => _tokenManager.CreateToken(identity)).Returns(token);

        var result = await _controller.SignInAsync(email, password);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);

        var response = okResult.Value;
        response.Should().BeEquivalentTo(new
        {
            access_token = token,
            email = identity.Name
        });
    }

    [TestMethod]
    public async Task SignInAdminAsync_ValidCredentials_ReturnsOkResult()
    {
        var email = "admin@example.com";
        var password = "validAdminPassword";
        var identity = A.Fake<ClaimsIdentity>();
        var token = "validAdminToken";

        A.CallTo(() => _tokenManager.GetAdminIdentityAsync(email, password)).Returns(identity);
        A.CallTo(() => _tokenManager.CreateToken(identity)).Returns(token);

        var result = await _controller.SignInAdminAsync(email, password);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);

        var response = okResult.Value;
        response.Should().BeEquivalentTo(new
        {
            access_token = token,
            email = identity.Name
        });
    }

    [TestMethod]
    public async Task CreateAsync_ValidAccount_ReturnsOkResult()
    {
        var accountDto = new AccountDTO
        {
            Email = "new@example.com",
            Password = "password123",
        };

        var accountModel = A.Fake<AccountModel>();
        var newAccountDto = A.Fake<AccountDTO>();

        A.CallTo(() => _mapper.Map<AccountModel>(accountDto)).Returns(accountModel);
        A.CallTo(() => _accountService.AddAccountAsync(accountModel)).Returns(Task.FromResult(accountModel));
        A.CallTo(() => _mapper.Map<AccountDTO>(accountModel)).Returns(newAccountDto);

        var result = await _controller.CreateAsync(accountDto);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(newAccountDto);
    }

}
