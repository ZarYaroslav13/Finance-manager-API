using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Finances;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class FinanceOperationController : BaseController
{
    private readonly IFinanceService _financeService;

    public FinanceOperationController(IFinanceService financeService, IMapper mapper, ILogger<BaseController> logger) : base(mapper, logger)
    {
        _financeService = financeService ?? throw new ArgumentNullException(nameof(financeService));
    }

    [HttpGet("operations/wallet/{walletId}")]
    public async Task<List<FinanceOperationDTO>> GetAllOfWalletAsync(int walletId, int index = 0, int count = 0)
    {
        _logger.LogInformation("GetAllOfWalletAsync called to retrieve finance operations for wallet Id: {WalletId}, starting at index: {Index}, with count: {Count}", walletId, index, count);

        var operations = (await _financeService.GetAllFinanceOperationOfWalletAsync(walletId, index, count))
            .Select(_mapper.Map<FinanceOperationDTO>)
            .ToList();

        _logger.LogInformation("{Count} finance operations retrieved for wallet Id: {WalletId}", operations.Count, walletId);

        return operations;
    }

    [HttpGet("operations/type/{typeId}")]
    public async Task<List<FinanceOperationDTO>> GetAllOfTypeAsync(int typeId)
    {
        _logger.LogInformation("GetAllOfTypeAsync called to retrieve finance operations of type Id: {TypeId}", typeId);

        var operations = (await _financeService.GetAllFinanceOperationOfTypeAsync(typeId))
            .Select(_mapper.Map<FinanceOperationDTO>)
            .ToList();

        _logger.LogInformation("{Count} finance operations retrieved for type Id: {TypeId}", operations.Count, typeId);

        return operations;
    }

    [HttpPost("create")]
    public async Task<FinanceOperationDTO> AddAsync([FromBody] FinanceOperationDTO dto)
    {
        _logger.LogInformation("AddAsync called to add a new finance operation with type: {@Description}", dto.Type);

        var newOperation = _mapper.Map<FinanceOperationDTO>(
                await _financeService.AddFinanceOperationAsync(
                        _mapper.Map<FinanceOperationModel>(dto)));

        _logger.LogInformation("Finance operation with Id: {Id} added successfully", newOperation.Id);

        return newOperation;
    }

    [HttpPut("update/{id}")]
    public async Task<FinanceOperationDTO> UpdateAsync([FromBody] FinanceOperationDTO dto)
    {
        _logger.LogInformation("UpdateAsync called to update finance operation with Id: {Id}", dto.Id);

        var updatedOperation = _mapper.Map<FinanceOperationDTO>(
                await _financeService.UpdateFinanceOperationAsync(
                        _mapper.Map<FinanceOperationModel>(dto)));

        _logger.LogInformation("Finance operation with Id: {Id} updated successfully", updatedOperation.Id);

        return updatedOperation;
    }

    [HttpDelete("remove/{id}")]
    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation("DeleteAsync called to remove finance operation with Id: {Id}", id);

        await _financeService.DeleteFinanceOperationAsync(id);

        _logger.LogInformation("Finance operation with Id: {Id} deleted successfully", id);
    }
}
