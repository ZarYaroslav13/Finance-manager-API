using FinanceManager.ApiService.Controllers;
using FinanceManager.ApiService.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using DomainLayer.Services.Wallets;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ApplicationLayerTests.Controllers;

[TestClass]
public class WalletControllerTests
{
    private WalletController _controller;
    private readonly IWalletService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<WalletController> _logger;
    private readonly int _userId = 1;
    private readonly string _email = "user@example.com";
    private readonly ClaimsPrincipal _user;
    private readonly ClaimsPrincipal _admin;
    private bool _isUserAdmin = false;

    public WalletControllerTests()
    {
        _service = A.Fake<IWalletService>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<WalletController>>();

        _controller = new(_service, _mapper, _logger);

        _user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(nameof(AccountDTO.Id), _userId.ToString()),
            new Claim(ClaimTypes.Name, _email),
            new Claim(ClaimTypes.Role, AccountService.NameAccountRole)

        }, "mock"));

        _admin = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(nameof(AccountDTO.Id), _userId.ToString()),
            new Claim(ClaimTypes.Name, _email),
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
    public void Constructor_WalletServiceIsNull_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new WalletController(null, _mapper, _logger));
    }

    [TestMethod]
    public async Task GetWallets_ShouldReturnWallets_WhenCalledByUser()
    {
        var walletModels = new List<WalletModel> { new WalletModel(), new WalletModel() };
        var walletDTOs = new List<WalletDTO> { new WalletDTO() { Id = 1 }, new WalletDTO() { Id = 2 } };

        A.CallTo(() => _service.GetAllWalletsOfAccountAsync(_userId)).Returns(walletModels);
        A.CallTo(() => _mapper.Map<WalletDTO>(A<WalletModel>._)).ReturnsNextFromSequence(walletDTOs.ToArray());

        var result = await _controller.GetWalletsAsync(_userId);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(walletDTOs);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldReturnNewWallet_WhenCalledByUser()
    {
        var walletDTO = new WalletDTO() { AccountId = _userId };
        var walletModel = new WalletModel() { AccountId = _userId };
        var newWalletDTO = new WalletDTO { Id = 1, AccountId = _userId };

        A.CallTo(() => _mapper.Map<WalletModel>(walletDTO)).Returns(walletModel);
        A.CallTo(() => _service.AddWalletAsync(walletModel)).Returns(walletModel);
        A.CallTo(() => _mapper.Map<WalletDTO>(walletModel)).Returns(newWalletDTO);

        var result = await _controller.CreateAsync(walletDTO);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(newWalletDTO);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldReturnUpdatedWallet_WhenCalledByOwner()
    {
        var walletDTO = new WalletDTO { Id = 1, AccountId = _userId };
        var walletModel = new WalletModel() { Id = 1, AccountId = _userId };

        A.CallTo(() => _mapper.Map<WalletModel>(walletDTO)).Returns(walletModel);
        A.CallTo(() => _service.UpdateWalletAsync(walletModel)).Returns(walletModel);
        A.CallTo(() => _mapper.Map<WalletDTO>(walletModel)).Returns(walletDTO);

        var result = await _controller.UpdateAsync(walletDTO);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(walletDTO);
    }

    [TestMethod]
    public void UpdateAsync_ShouldThrowUnauthorizedAccessException_WhenCalledByNonOwner()
    {
        var walletDTO = new WalletDTO { Id = 1, AccountId = _userId + 1 };

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.UpdateAsync(walletDTO));
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldDeleteWallet_WhenCalledByOwner()
    {
        int walletId = 1;

        A.CallTo(() => _service.IsAccountOwnerWalletAsync(_userId, walletId)).Returns(true);

        var result = await _controller.DeleteAsync(walletId);

        A.CallTo(() => _service.DeleteWalletByIdAsync(walletId)).MustHaveHappenedOnceExactly();
        result.Should().BeOfType<OkResult>();
    }

    [TestMethod]
    public void DeleteAsync_ShouldThrowUnauthorizedAccessException_WhenCalledByNonOwner()
    {
        int walletId = 1;

        A.CallTo(() => _service.IsAccountOwnerWalletAsync(_userId, walletId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.DeleteAsync(_userId));
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnWallet_WhenFound()
    {
        int walletId = 1;
        var walletModel = new WalletModel { Id = walletId };
        var walletDTO = new WalletDTO { Id = walletId };

        A.CallTo(() => _service.IsAccountOwnerWalletAsync(_userId, walletId)).Returns(true);
        A.CallTo(() => _service.FindWalletAsync(walletId)).Returns(walletModel);
        A.CallTo(() => _mapper.Map<WalletDTO>(walletModel)).Returns(walletDTO);

        var result = await _controller.GetByIdAsync(walletId);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(walletDTO);
    }

    [TestMethod]
    public void GetByIdAsync_ShouldThrowUnauthorizedAccessException_WhenCalledByNonOwner()
    {
        int walletId = 1;

        A.CallTo(() => _service.IsAccountOwnerWalletAsync(_userId, walletId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.GetByIdAsync(walletId));
    }

    [TestMethod]
    public async Task GetWalletsOfAccountAsync_ShouldReturnWallets_WhenCalledByAdmin()
    {
        _isUserAdmin = true;

        int accountId = _userId + 1;
        var walletModels = new List<WalletModel> { new WalletModel(), new WalletModel() };
        var walletDTOs = new List<WalletDTO> { new WalletDTO(), new WalletDTO() };

        A.CallTo(() => _service.GetAllWalletsOfAccountAsync(accountId)).Returns(walletModels);
        A.CallTo(() => _mapper.Map<WalletDTO>(A<WalletModel>._)).ReturnsNextFromSequence(walletDTOs.ToArray());

        var result = await _controller.GetWalletsAsync(accountId);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(walletDTOs);
    }

    [TestMethod]
    public async Task DeleteWalletOfAccountAsync_ShouldDeleteWallet_WhenCalledByAdmin()
    {
        _isUserAdmin = true;
        int walletId = 1;

        var result = await _controller.DeleteAsync(walletId);

        A.CallTo(() => _service.DeleteWalletByIdAsync(walletId)).MustHaveHappenedOnceExactly();
        result.Should().BeOfType<OkResult>();
    }
}
