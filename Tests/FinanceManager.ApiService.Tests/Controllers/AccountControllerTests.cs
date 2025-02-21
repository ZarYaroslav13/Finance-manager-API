using FinanceManager.ApiService.Controllers;
using FinanceManager.Application.Models;
using AutoMapper;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services.Accounts;
using FinanceManager.Domain.Services.Admins;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FinanceManager.ApiService.Tests.Controllers;

[TestClass]
public class AccountControllerTests
{
    private AccountController _controller;
    private readonly IAccountService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountController> _logger;
    private readonly ClaimsPrincipal _user;
    private readonly ClaimsPrincipal _admin;
    private bool _isUserAdmin = false;

    public AccountControllerTests()
    {
        _service = A.Fake<IAccountService>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<AccountController>>();

        _controller = new(_service, _mapper, _logger);

        _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(nameof(AccountDTO.Id), "1"),
            new Claim(ClaimTypes.Name, "user@example.com"),
            new Claim(ClaimTypes.Role, AccountService.NameAccountRole)
        }, "mock"));

        _admin = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(nameof(AccountDTO.Id), "1"),
            new Claim(ClaimTypes.Name, "user@example.com"),
            new Claim(ClaimTypes.Role, AdminService.NameAdminRole)
        }, "mock"));

        var httpContext = A.Fake<HttpContext>();
        A.CallTo(() => httpContext.User).ReturnsLazily(() => _isUserAdmin == true ? _admin : _user);
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };
    }

    [TestMethod]
    public void Constructor_AccountServiceIsNull_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AccountController(null, _mapper, _logger));
    }

    [TestMethod]
    public async Task UpdateAsync_ValidAccount_ReturnsOkResult()
    {
        var accountDto = new AccountDTO { Id = 1, Email = "user@example.com" };
        var accountModel = A.Fake<AccountModel>();
        var updatedAccountDto = A.Fake<AccountDTO>();

        A.CallTo(() => _mapper.Map<AccountModel>(accountDto)).Returns(accountModel);
        A.CallTo(() => _service.UpdateAccountAsync(accountModel)).Returns(Task.FromResult(accountModel));
        A.CallTo(() => _mapper.Map<AccountDTO>(accountModel)).Returns(updatedAccountDto);

        var result = await _controller.UpdateAsync(accountDto);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(updatedAccountDto);
    }

    [TestMethod]
    public void UpdateAsync_InvalidAccount_ThrowsUnauthorizedAccessException()
    {
        var accountDto = new AccountDTO { Id = 2, Email = "user@example.com" };
        var accountModel = A.Fake<AccountModel>();
        var updatedAccountDto = A.Fake<AccountDTO>();

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.UpdateAsync(accountDto));
    }

    [TestMethod]
    public void Delete_ValidUserId_CallsServiceAndLogs()
    {
        _controller.DeleteUserById(1);

        A.CallTo(() => _service.DeleteAccountWithId(1)).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    public async Task GetAllAsync_AdminRole_ReturnsOkResult()
    {
        var accounts = new List<AccountModel> { A.Fake<AccountModel>(), A.Fake<AccountModel>() };
        var accountDtos = accounts.Select(a => A.Fake<AccountDTO>()).ToList();

        A.CallTo(() => _service.GetAccountsAsync("user@example.com", 0, 10)).Returns(Task.FromResult(accounts));
        A.CallTo(() => _mapper.Map<AccountDTO>(A<AccountModel>.Ignored)).ReturnsNextFromSequence(accountDtos.ToArray());

        var result = await _controller.GetAllAsync(0, 10);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(accountDtos);
    }

    [TestMethod]
    public async Task AdminUpdateAccountAsync_ValidAccount_ReturnsOkResult()
    {
        _isUserAdmin = true;

        var accountDto = new AccountDTO { Id = 2, Email = "admin@example.com" };
        var accountModel = A.Fake<AccountModel>();
        var updatedAccountDto = A.Fake<AccountDTO>();

        A.CallTo(() => _mapper.Map<AccountModel>(accountDto)).Returns(accountModel);
        A.CallTo(() => _service.UpdateAccountAsync(accountModel)).Returns(Task.FromResult(accountModel));
        A.CallTo(() => _mapper.Map<AccountDTO>(accountModel)).Returns(updatedAccountDto);

        var result = await _controller.UpdateAsync(accountDto);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(updatedAccountDto);
    }

    [TestMethod]
    public void DeleteById_AdminRole_CallsServiceAndLogs()
    {
        _isUserAdmin = true;

        int randomAccountId = 3;

        var result = _controller.DeleteUserById(randomAccountId);

        result.Should().BeOfType<OkResult>();

        A.CallTo(() => _service.DeleteAccountWithId(randomAccountId)).MustHaveHappenedOnceExactly();
    }
}
