using ApplicationLayer.Controllers;
using ApplicationLayer.Models;
using ApplicationLayerTests.Data.Controllers;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Finances;
using DomainLayer.Services.Wallets;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ApplicationLayerTests.Controllers;

[TestClass]
public class FinanceReportControllerTests
{
    private readonly FinanceReportController _controller;
    private readonly IFinanceReportCreator _creator;
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;
    private readonly ILogger<FinanceReportController> _logger;
    private ClaimsPrincipal _user;
    private readonly int _userId = 1;
    private readonly string _email = "user@example.com";

    public FinanceReportControllerTests()
    {
        _creator = A.Fake<IFinanceReportCreator>();
        _walletService = A.Fake<IWalletService>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<FinanceReportController>>();

        _controller = new FinanceReportController(_creator, _walletService, _mapper, _logger);

        _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(nameof(AccountDTO.Id), _userId.ToString()),
            new Claim(ClaimTypes.Name, _email)
        }, "mock"));

        var httpContext = A.Fake<HttpContext>();
        A.CallTo(() => httpContext.User).Returns(_user);
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportControllerTestDataProvider.ConstructorArgumentsAreNullThrowsArgumentNullExceptionTestData), typeof(FinanceReportControllerTestDataProvider))]
    public void Constructor_ArgumentsAreNull_ThrowsArgumentNullException(IFinanceReportCreator financeReportCreator, IWalletService walletService)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new FinanceReportController(financeReportCreator, walletService, _mapper, _logger));
    }

    [TestMethod]
    public async Task CreateReportAsync_Daily_ShouldReturnFinanceReport_WhenCalledByOwner()
    {
        int walletId = 1;
        Period period = new() { StartDate = DateTime.MinValue, EndDate = DateTime.MinValue };
        DateTime date = DateTime.UtcNow;
        var walletModel = new WalletModel() { Id = walletId, Name = "name" };
        var reportModel = new FinanceReportModel(walletId, "name", period);
        var reportDTO = new FinanceReportDTO(walletId, "name", A.Dummy<int>(), A.Dummy<int>(), A.Dummy<List<FinanceOperationDTO>>(), period);

        A.CallTo(() => _walletService.IsAccountOwnerWalletAsync(_userId, walletId)).Returns(true);
        A.CallTo(() => _walletService.FindWalletAsync(walletId)).Returns(walletModel);
        A.CallTo(() => _creator.CreateFinanceReportAsync(walletModel, date)).Returns(reportModel);
        A.CallTo(() => _mapper.Map<FinanceReportDTO>(reportModel)).Returns(reportDTO);

        var result = await _controller.CreateReportAsync(walletId, date);

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(reportDTO);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public void CreateReportAsync_Daily_ShouldThrowUnauthorizedAccessException_WhenCalledByNonOwner()
    {
        int walletId = 1;
        A.CallTo(() => _walletService.IsAccountOwnerWalletAsync(_userId, walletId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.CreateReportAsync(walletId, A.Dummy<DateTime>()));
    }

    [TestMethod]
    public async Task CreateReportAsync_Period_ShouldReturnFinanceReport_WhenCalledByOwner()
    {
        int walletId = 1;
        Period period = new() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };
        var walletModel = new WalletModel() { Id = walletId, Name = "name" };
        var reportModel = new FinanceReportModel(walletId, "name", period);
        var reportDTO = new FinanceReportDTO(walletId, "name", A.Dummy<int>(), A.Dummy<int>(), A.Dummy<List<FinanceOperationDTO>>(), period);

        A.CallTo(() => _walletService.IsAccountOwnerWalletAsync(_userId, walletId)).Returns(true);
        A.CallTo(() => _walletService.FindWalletAsync(walletId)).Returns(walletModel);
        A.CallTo(() => _creator.CreateFinanceReportAsync(walletModel, period.StartDate, period.EndDate)).Returns(reportModel);
        A.CallTo(() => _mapper.Map<FinanceReportDTO>(reportModel)).Returns(reportDTO);

        var result = await _controller.CreateReportAsync(walletId, period.StartDate, period.EndDate);

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(reportDTO);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public async Task CreateReportAsync_Period_ShouldThrowUnauthorizedAccessException_WhenCalledByNonOwner()
    {
        int walletId = 1;
        A.CallTo(() => _walletService.IsAccountOwnerWalletAsync(_userId, walletId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.CreateReportAsync(walletId, A.Dummy<DateTime>(), A.Dummy<DateTime>()));
    }
}
