using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DataLayer.Models;
using DomainLayer.Models;
using DomainLayer.Services.Finances;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApplicationLayer.Controllers;

public class FinanceOperationController : BaseController
{
    private readonly IFinanceService _financeService;

    public FinanceOperationController(IFinanceService financeService, IMapper mapper, ILogger<BaseController> logger) : base(mapper, logger)
    {
        _financeService = financeService ?? throw new ArgumentNullException(nameof(financeService));
    }

    [HttpGet("wallet/{walletId}")]
    public async Task<IActionResult> GetAllOfWalletAsync(int walletId, int index = 0, int count = 0)
    {
        var userId = GetUserId();
        _logger.LogInformation("GetAllOfWalletAsync called to retrieve finance operations for wallet Id: {WalletId}, starting at index: {Index}, with count: {Count}", walletId, index, count);

        if (!await _financeService.IsAccountOwnerOfWalletAsync(userId, walletId))
        {
            _logger.LogWarning($"Unauthorized access attempt to get wallet with Id: {walletId} information for user Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        var operations = (await _financeService.GetAllFinanceOperationOfWalletAsync(walletId, index, count))
            .Select(_mapper.Map<FinanceOperationDTO>)
            .ToList();

        _logger.LogInformation("{Count} finance operations retrieved for wallet Id: {WalletId}", operations.Count, walletId);

        return Ok(operations);
    }

    [HttpGet("type/{typeId}")]
    public async Task<IActionResult> GetAllOfTypeAsync(int typeId)
    {
        var userId = GetUserId();
        _logger.LogInformation("GetAllOfTypeAsync called to retrieve finance operations of type Id: {TypeId}", typeId);

        if (!await _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(userId, typeId))
        {
            _logger.LogWarning($"Unauthorized access attempt to get finance operation type with Id: {typeId} information for user Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        var operations = (await _financeService.GetAllFinanceOperationOfTypeAsync(typeId))
            .Select(_mapper.Map<FinanceOperationDTO>)
            .ToList();

        _logger.LogInformation("{Count} finance operations retrieved for type Id: {TypeId}", operations.Count, typeId);

        return Ok(operations);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] FinanceOperationDTO dto)
    {
        var userId = GetUserId();
        _logger.LogInformation("AddAsync called to add a new finance operation with type: {@Description}", dto.Type);

        if (!await _financeService.IsAccountOwnerOfWalletAsync(userId, dto.Type.WalletId))
        {
            _logger.LogWarning($"Unauthorized access attempt to add finance operation  to wallet with Id: {dto.Type.WalletId} by user with Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        var newOperation = _mapper.Map<FinanceOperationDTO>(
                await _financeService.AddFinanceOperationAsync(
                        _mapper.Map<FinanceOperationModel>(dto)));

        _logger.LogInformation("Finance operation with Id: {Id} added successfully", newOperation.Id);

        return Ok(newOperation);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] FinanceOperationDTO dto)
    {
        var userId = GetUserId();
        _logger.LogInformation("UpdateAsync called to update finance operation with Id: {Id}", dto.Id);

        if (!await _financeService.IsAccountOwnerOfFinanceOperationTypeAsync(userId, dto.Type.Id))
        {
            _logger.LogWarning($"Unauthorized access attempt to update finance operation of type with Id: {dto.Type.Id} by user with Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        var updatedOperation = _mapper.Map<FinanceOperationDTO>(
                await _financeService.UpdateFinanceOperationAsync(
                        _mapper.Map<FinanceOperationModel>(dto)));

        _logger.LogInformation("Finance operation with Id: {Id} updated successfully", updatedOperation.Id);

        return Ok(updatedOperation);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var userId = GetUserId();
        _logger.LogInformation("DeleteAsync called to remove finance operation with Id: {Id}", id);

        if (!await _financeService.IsAccountOwnerOfFinanceOperationAsync(userId, id))
        {
            _logger.LogWarning($"Unauthorized access attempt to delete finance operation with Id: {id} by user with Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        await _financeService.DeleteFinanceOperationAsync(id);

        _logger.LogInformation("Finance operation with Id: {Id} deleted successfully", id);

        return Ok();
    }
}
