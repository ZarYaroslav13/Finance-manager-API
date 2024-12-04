using ApplicationLayer.Controllers;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Finances;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ApplicationLayerTests.Controllers;

[TestClass]
public class FinanceOperationControllerTests
{
    private readonly FinanceOperationController _controller;
    private readonly IFinanceService _financeService;
    private readonly IMapper _mapper;
    private readonly ILogger<FinanceOperationController> _logger;
    private ClaimsPrincipal _user;
    private readonly int _userId = 1;
    private readonly string _email = "user@example.com";

    public FinanceOperationControllerTests()
    {
        _financeService = A.Fake<IFinanceService>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<FinanceOperationController>>();

        _controller = new(_financeService, _mapper, _logger);

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
    public void Constructor_ArgumentsAreNull_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new FinanceOperationController(null, _mapper, _logger));
    }

    [TestMethod]
    public async Task GetAllOfWalletAsync_ShouldReturnListOfFinanceOperations_WhenCalledByOwner()
    {
        int walletId = 1;
        int index = 0;
        int count = 5;

        var financeOperations = new List<FinanceOperationModel> { new IncomeModel(new()), new IncomeModel(new()) };
        var financeOperationDTOs = new List<FinanceOperationDTO> { new IncomeDTO(), new IncomeDTO() };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, walletId)).Returns(true);
        A.CallTo(() => _financeService.GetAllFinanceOperationOfWalletAsync(walletId, index, count)).Returns(Task.FromResult(financeOperations));
        A.CallTo(() => _mapper.Map<FinanceOperationDTO>(A<FinanceOperationModel>._)).ReturnsNextFromSequence(financeOperationDTOs.ToArray());

        var result = await _controller.GetAllOfWalletAsync(walletId, index, count);

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(financeOperationDTOs);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public void GetAllOfWalletAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotOwner()
    {
        int walletId = 1;

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, walletId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.GetAllOfWalletAsync(walletId));

        A.CallTo(() => _financeService.GetAllFinanceOperationOfWalletAsync(walletId, A<int>._, A<int>._)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task GetAllOfTypeAsync_ShouldReturnListOfFinanceOperations_WhenCalledByOwner()
    {
        int typeId = 1;

        var financeOperations = new List<FinanceOperationModel> { new IncomeModel(new()), new IncomeModel(new()) };
        var financeOperationDTOs = new List<FinanceOperationDTO> { new IncomeDTO(), new IncomeDTO() };

        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(_userId, typeId)).Returns(true);
        A.CallTo(() => _financeService.GetAllFinanceOperationOfTypeAsync(typeId)).Returns(Task.FromResult(financeOperations));
        A.CallTo(() => _mapper.Map<FinanceOperationDTO>(A<FinanceOperationModel>._)).ReturnsNextFromSequence(financeOperationDTOs.ToArray());

        var result = await _controller.GetAllOfTypeAsync(typeId);

        A.CallTo(() => _financeService.GetAllFinanceOperationOfTypeAsync(typeId)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(financeOperationDTOs);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public void GetAllOfTypeAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotOwner()
    {
        int typeId = 1;

        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(_userId, typeId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.GetAllOfTypeAsync(typeId));

        A.CallTo(() => _financeService.GetAllFinanceOperationOfTypeAsync(typeId)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task AddAsync_ShouldReturnOkWithNewFinanceOperation_WhenUserIsOwnerOfWallet()
    {
        var walletId = 1;
        var dto = new IncomeDTO() { Type = new() { WalletId = walletId } };
        var financeOperationModel = new IncomeModel(new());
        var expectedOperation = new IncomeDTO() { Id = 1, Type = new() { WalletId = walletId } };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, walletId)).Returns(true);
        A.CallTo(() => _mapper.Map<FinanceOperationModel>(dto)).Returns(financeOperationModel);
        A.CallTo(() => _financeService.AddFinanceOperationAsync(financeOperationModel)).Returns(financeOperationModel);
        A.CallTo(() => _mapper.Map<FinanceOperationDTO>(financeOperationModel)).Returns(expectedOperation);

        var result = await _controller.AddAsync(dto);

        A.CallTo(() => _financeService.AddFinanceOperationAsync(A<FinanceOperationModel>._)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(expectedOperation);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public void AddAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotOwnerOfWallet()
    {
        var walletId = 1;
        var dto = new IncomeDTO() { Id = 1, Type = new() { WalletId = walletId } };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, walletId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.AddAsync(dto));

        A.CallTo(() => _financeService.AddFinanceOperationAsync(A<FinanceOperationModel>._)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldReturnOkWithUpdatedFinanceOperation_WhenUserIsOwnerOfOperationType()
    {
        var operationTypeId = 2;
        var dto = new IncomeDTO()
        {
            Id = 1,
            Type = new() { Id = operationTypeId }
        };
        var financeOperationModel = new IncomeModel(new());
        var updatedOperation = new IncomeDTO() { Id = 1, Type = new() { Id = operationTypeId } };

        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(_userId, operationTypeId)).Returns(true);
        A.CallTo(() => _mapper.Map<FinanceOperationModel>(dto)).Returns(financeOperationModel);
        A.CallTo(() => _financeService.UpdateFinanceOperationAsync(financeOperationModel)).Returns(financeOperationModel);
        A.CallTo(() => _mapper.Map<FinanceOperationDTO>(financeOperationModel)).Returns(updatedOperation);

        var result = await _controller.UpdateAsync(dto.Id, dto);

        A.CallTo(() => _financeService.UpdateFinanceOperationAsync(A<FinanceOperationModel>._)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(updatedOperation);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public void UpdateAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotOwnerOfOperationType()
    {
        var operationTypeId = 2;
        var dto = new IncomeDTO()
        {
            Id = 1,
            Type = new() { Id = operationTypeId }
        };

        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(_userId, operationTypeId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.UpdateAsync(dto.Id, dto));

        A.CallTo(() => _financeService.UpdateFinanceOperationAsync(A<FinanceOperationModel>._)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldReturnOk_WhenUserIsOwnerOfFinanceOperation()
    {
        var operationId = 2;

        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationAsync(_userId, operationId)).Returns(true);

        var result = await _controller.DeleteAsync(operationId) as OkResult;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(StatusCodes.Status200OK);
        A.CallTo(() => _financeService.DeleteFinanceOperationAsync(operationId)).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    public void DeleteAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotOwnerOfFinanceOperation()
    {
        var operationId = 2;

        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationAsync(_userId, operationId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.DeleteAsync(operationId));

        A.CallTo(() => _financeService.DeleteFinanceOperationAsync(operationId)).MustNotHaveHappened();
    }
}
