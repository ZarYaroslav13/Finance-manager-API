using FinanceManager.ApiService.Controllers.Base;
using FinanceManager.Application.Models;
using AutoMapper;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services.Finances;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.ApiService.Controllers;

public class FinanceOperationTypeController : BaseController
{
    private readonly IFinanceService _financeService;

    public FinanceOperationTypeController(IFinanceService financeService, IMapper mapper, ILogger<FinanceOperationTypeController> logger) : base(mapper, logger)
    {
        _financeService = financeService ?? throw new ArgumentNullException(nameof(financeService));
    }

    [HttpGet("wallet/{walletId}")]
    public async Task<IActionResult> GetAllAsync(int walletId)
    {
        var userId = GetUserId();

        _logger.LogInformation("GetAllAsync called to retrieve finance operation types for wallet Id: {WalletId}", walletId);

        if (!await _financeService.IsAccountOwnerOfWalletAsync(userId, walletId))
        {
            _logger.LogWarning($"Unauthorized access attempt to update wallet with Id: {walletId} for user Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        var operationTypes = (await _financeService.GetAllFinanceOperationTypesOfWalletAsync(walletId))
            .Select(_mapper.Map<FinanceOperationTypeDTO>)
            .ToList();

        _logger.LogInformation("{Count} finance operation types retrieved for wallet Id: {WalletId}", operationTypes.Count, walletId);

        return Ok(operationTypes);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] FinanceOperationTypeDTO dto)
    {
        var userId = GetUserId();
        _logger.LogInformation("AddAsync called to add a new finance operation type with name: {Name}", dto.Name);

        if (!await _financeService.IsAccountOwnerOfWalletAsync(userId, dto.WalletId))
        {
            _logger.LogWarning($"Unauthorized access attempt to update wallet with Id: {dto.WalletId} for user Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        var newOperationType = _mapper.Map<FinanceOperationTypeDTO>(
                await _financeService.AddFinanceOperationTypeAsync(
                        _mapper.Map<FinanceOperationTypeModel>(dto)));

        _logger.LogInformation("Finance operation type with Id: {Id} added successfully", newOperationType.Id);

        return Ok(newOperationType);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] FinanceOperationTypeDTO dto)
    {
        var userId = GetUserId();
        _logger.LogInformation("UpdateAsync called to update finance operation type with Id: {Id}", dto.Id);

        if (!await _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(userId, dto.Id))
        {
            _logger.LogWarning($"Unauthorized access attempt to update finance operation type with Id: {dto.Id} for user Id: {userId}");
            throw new UnauthorizedAccessException("Access to this finance operation type is denied");
        }

        var updatedOperationType = _mapper.Map<FinanceOperationTypeDTO>(
                await _financeService.UpdateFinanceOperationTypeAsync(
                        _mapper.Map<FinanceOperationTypeModel>(dto)));

        _logger.LogInformation("Finance operation type with Id: {Id} updated successfully", updatedOperationType.Id);

        return Ok(updatedOperationType);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        int userId = GetUserId();
        _logger.LogInformation("DeleteAsync called to remove finance operation type with Id: {Id}", id);

        if (!await _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(GetUserId(), id))
        {
            _logger.LogWarning($"Unauthorized access attempt to update finance operation type with Id: {id} for user Id: {userId}");
            throw new UnauthorizedAccessException("Access to this finance operation type is denied");
        }

        await _financeService.DeleteFinanceOperationTypeAsync(id);

        _logger.LogInformation("Finance operation type with Id: {Id} deleted successfully", id);

        return Ok();
    }
}
