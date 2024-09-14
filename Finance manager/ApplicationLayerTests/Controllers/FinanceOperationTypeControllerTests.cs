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
public class FinanceOperationTypeControllerTests
{
    private readonly FinanceOperationTypeController _controller;
    private readonly IFinanceService _financeService;
    private readonly IMapper _mapper;
    private readonly ILogger<FinanceOperationTypeController> _logger;
    private ClaimsPrincipal _user;
    private readonly int _userId = 1;
    private readonly string _email = "user@example.com";

    public FinanceOperationTypeControllerTests()
    {
        _financeService = A.Fake<IFinanceService>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<FinanceOperationTypeController>>();

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
        Assert.ThrowsException<ArgumentNullException>(() => new FinanceOperationTypeController(null, _mapper, _logger));
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnFinanceOperationTypes_WhenCalledByOwner()
    {
        int walletId = 1;
        var financeOperationTypes = new List<FinanceOperationTypeModel>
        {
            new FinanceOperationTypeModel { Id = 1, Name = "Deposit" },
            new FinanceOperationTypeModel { Id = 2, Name = "Withdrawal" }
        };
        var financeOperationTypeDTOs = new List<FinanceOperationTypeDTO>
        {
            new FinanceOperationTypeDTO { Id = 1, Name = "Deposit" },
            new FinanceOperationTypeDTO { Id = 2, Name = "Withdrawal" }
        };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, walletId)).Returns(true);
        A.CallTo(() => _financeService.GetAllFinanceOperationTypesOfWalletAsync(walletId)).Returns(financeOperationTypes);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeDTO>(A<FinanceOperationTypeModel>._))
            .ReturnsLazily((object model) => financeOperationTypeDTOs.FirstOrDefault(dto => dto.Id == ((FinanceOperationTypeModel)model).Id));

        var result = await _controller.GetAllAsync(walletId);

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(financeOperationTypeDTOs);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public void GetAllAsync_ShouldThrowUnauthorizedAccessException_WhenCalledByNonOwner()
    {
        int walletId = 1;

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, walletId)).Returns(false);

         Assert.ThrowsExceptionAsync<UnauthorizedAccessException> (() => _controller.GetAllAsync(walletId));
    }

    [TestMethod]
    public async Task AddAsync_ShouldReturnNewOperationType_WhenCalledByOwner()
    {
        var dto = new FinanceOperationTypeDTO
        {
            Id = 0,
            Name = "Investment",
            WalletId = 1
        };
        var newOperationTypeModel = new FinanceOperationTypeModel
        {
            Id = 1,
            Name = "Investment",
            WalletId = dto.WalletId
        };
        var newOperationTypeDTO = new FinanceOperationTypeDTO
        {
            Id = 1,
            Name = "Investment",
            WalletId = dto.WalletId
        };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, dto.WalletId)).Returns(true);
        A.CallTo(() => _financeService.AddFinanceOperationTypeAsync(A<FinanceOperationTypeModel>._)).Returns(newOperationTypeModel);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeDTO>(A<FinanceOperationTypeModel>._)).Returns(newOperationTypeDTO);

        var result = await _controller.AddAsync(dto);

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(newOperationTypeDTO);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public void AddAsync_ShouldThrowUnauthorizedAccessException_WhenCalledByNonOwner()
    {
        var dto = new FinanceOperationTypeDTO
        {
            Name = "Investment",
            WalletId = 1
        };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, dto.WalletId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.GetAllAsync(dto.WalletId));
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldReturnUpdatedOperationType_WhenCalledByOwner()
    {
        var dto = new FinanceOperationTypeDTO
        {
            Id = 1,
            Name = "Updated Investment",
            WalletId = 1
        };
        var updatedOperationTypeModel = new FinanceOperationTypeModel
        {
            Id = dto.Id,
            Name = dto.Name,
            WalletId = dto.WalletId
        };
        var updatedOperationTypeDTO = new FinanceOperationTypeDTO
        {
            Id = dto.Id,
            Name = dto.Name,
            WalletId = dto.WalletId
        };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, dto.WalletId)).Returns(true);
        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(_userId, dto.Id)).Returns(true);
        A.CallTo(() => _financeService.UpdateFinanceOperationTypeAsync(A<FinanceOperationTypeModel>.Ignored)).Returns(updatedOperationTypeModel);
        A.CallTo(() => _mapper.Map<FinanceOperationTypeDTO>(A<FinanceOperationTypeModel>.Ignored)).Returns(updatedOperationTypeDTO);

        var result = await _controller.UpdateAsync(dto) as OkObjectResult;

        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(updatedOperationTypeDTO);
        okResult.StatusCode.Should().Be(200);
    }

    [TestMethod]
    public void UpdateAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotOwnerOfWallet()
    {
        var dto = new FinanceOperationTypeDTO
        {
            Id = 1,
            Name = "Investment",
            WalletId = 1
        };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, dto.WalletId)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.UpdateAsync(dto));
    }

    [TestMethod]
    public void UpdateAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotOwnerOfOperationType()
    {
        var dto = new FinanceOperationTypeDTO
        {
            Id = 1,
            Name = "Investment",
            WalletId = 1
        };

        A.CallTo(() => _financeService.IsAccountOwnerOfWalletAsync(_userId, dto.WalletId)).Returns(true);
        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(_userId, dto.Id)).Returns(false);

        Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _controller.UpdateAsync(dto));
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldReturnOk_WhenCalledByOwner()
    {
        int operationTypeId = 1;

        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(_userId, operationTypeId)).Returns(true);

        var result = await _controller.DeleteAsync(operationTypeId) as OkResult;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(200);
        A.CallTo(() => _financeService.DeleteFinanceOperationTypeAsync(operationTypeId)).MustHaveHappenedOnceExactly();
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldThrowUnauthorizedAccessException_WhenUserIsNotOwner()
    {
        int operationTypeId = 1;

        A.CallTo(() => _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(_userId, operationTypeId)).Returns(false);

        Func<Task> act = async () => await _controller.DeleteAsync(operationTypeId);

        await act.Should().ThrowAsync<UnauthorizedAccessException>();
        A.CallTo(() => _financeService.DeleteFinanceOperationTypeAsync(operationTypeId)).MustNotHaveHappened();
    }
}
