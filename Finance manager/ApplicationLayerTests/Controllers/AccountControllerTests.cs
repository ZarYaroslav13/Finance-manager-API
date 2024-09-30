using ApplicationLayer.Controllers;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ApplicationLayerTests.Controllers;

[TestClass]
public class AccountControllerTests
{
    private AccountController _controller;
    private readonly IAccountService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountController> _logger;
    private ClaimsPrincipal user;

    public AccountControllerTests()
    {
        _service = A.Fake<IAccountService>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<AccountController>>();

        _controller = new(_service, _mapper, _logger);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(nameof(AccountDTO.Id), "1"),
            new Claim(ClaimTypes.Name, "user@example.com")
        }, "mock"));

        var httpContext = A.Fake<HttpContext>();
        A.CallTo(() => httpContext.User).Returns(user);
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
        _controller.Delete();

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
        var accountDto = new AccountDTO { Id = 1, Email = "admin@example.com" };
        var accountModel = A.Fake<AccountModel>();
        var updatedAccountDto = A.Fake<AccountDTO>();

        A.CallTo(() => _mapper.Map<AccountModel>(accountDto)).Returns(accountModel);
        A.CallTo(() => _service.UpdateAccountAsync(accountModel)).Returns(Task.FromResult(accountModel));
        A.CallTo(() => _mapper.Map<AccountDTO>(accountModel)).Returns(updatedAccountDto);

        var result = await _controller.AdminUpdateAccountAsync(accountDto.Id, accountDto);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(updatedAccountDto);
    }

    [TestMethod]
    public void DeleteById_AdminRole_CallsServiceAndLogs()
    {
        var result = _controller.DeleteUserById(1);

        result.Should().BeOfType<OkResult>();

        A.CallTo(() => _service.DeleteAccountWithId(1)).MustHaveHappenedOnceExactly();
    }
}
