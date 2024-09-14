using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Finances;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class FinanceOperationTypeController : BaseController
{
    private readonly IFinanceService _financeService;

    public FinanceOperationTypeController(IFinanceService financeService, IMapper mapper, ILogger<FinanceOperationTypeController> logger) : base(mapper, logger)
    {
        _financeService = financeService ?? throw new ArgumentNullException(nameof(financeService));
    }

    [HttpGet("types/wallet/{walletId}")]
    public async Task<IActionResult> GetAllAsync(int walletId)
    {
        var userId = GetUserId();

        _logger.LogInformation("GetAllAsync called to retrieve finance operation types for wallet Id: {WalletId}", walletId);

        if (!await _financeService.IsAccountOwnerOfWalletAsync(userId, walletId))
            throw new UnauthorizedAccessException($"Unauthorized access attempt to update wallet with Id: {walletId} for user Id: {userId}");

        var operationTypes = (await _financeService.GetAllFinanceOperationTypesOfWalletAsync(walletId))
            .Select(_mapper.Map<FinanceOperationTypeDTO>)
            .ToList();

        _logger.LogInformation("{Count} finance operation types retrieved for wallet Id: {WalletId}", operationTypes.Count, walletId);

        return Ok(operationTypes);
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddAsync([FromBody] FinanceOperationTypeDTO dto)
    {
        var userId = GetUserId();
        _logger.LogInformation("AddAsync called to add a new finance operation type with name: {Name}", dto.Name);

        if (!await _financeService.IsAccountOwnerOfWalletAsync(userId, dto.WalletId))
            throw new UnauthorizedAccessException($"Unauthorized access attempt to update wallet with Id: {dto.WalletId} for user Id: {userId}");

        var newOperationType = _mapper.Map<FinanceOperationTypeDTO>(
                await _financeService.AddFinanceOperationTypeAsync(
                        _mapper.Map<FinanceOperationTypeModel>(dto)));

        _logger.LogInformation("Finance operation type with Id: {Id} added successfully", newOperationType.Id);

        return Ok(newOperationType);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] FinanceOperationTypeDTO dto)
    {
        var userId = GetUserId();
        _logger.LogInformation("UpdateAsync called to update finance operation type with Id: {Id}", dto.Id);

        if (!await _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(userId, dto.Id))
            throw new UnauthorizedAccessException($"Unauthorized access attempt to update finance operation type with Id: {dto.Id} for user Id: {userId}");

        var updatedOperationType = _mapper.Map<FinanceOperationTypeDTO>(
                await _financeService.UpdateFinanceOperationTypeAsync(
                        _mapper.Map<FinanceOperationTypeModel>(dto)));

        _logger.LogInformation("Finance operation type with Id: {Id} updated successfully", updatedOperationType.Id);

        return Ok(updatedOperationType);
    }

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        _logger.LogInformation("DeleteAsync called to remove finance operation type with Id: {Id}", id);

        if (!(await _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(GetUserId(), id)))
            throw new UnauthorizedAccessException();

        await _financeService.DeleteFinanceOperationTypeAsync(id);

        _logger.LogInformation("Finance operation type with Id: {Id} deleted successfully", id);

        return Ok();
    }
}
