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

    public FinanceOperationTypeController(IFinanceService financeService, IMapper mapper, ILogger<BaseController> logger) : base(mapper, logger)
    {
        _financeService = financeService ?? throw new ArgumentNullException(nameof(financeService));
    }

    [HttpGet("types/wallet/{walletId}")]
    public async Task<List<FinanceOperationTypeDTO>> GetAllAsync(int walletId)
    {
        _logger.LogInformation("GetAllAsync called to retrieve finance operation types for wallet Id: {WalletId}", walletId);

        var operationTypes = (await _financeService.GetAllFinanceOperationTypesOfWalletAsync(walletId))
            .Select(_mapper.Map<FinanceOperationTypeDTO>)
            .ToList();

        _logger.LogInformation("{Count} finance operation types retrieved for wallet Id: {WalletId}", operationTypes.Count, walletId);

        return operationTypes;
    }

    [HttpPost("create")]
    public async Task<FinanceOperationTypeDTO> AddAsync([FromBody] FinanceOperationTypeDTO dto)
    {
        _logger.LogInformation("AddAsync called to add a new finance operation type with name: {Name}", dto.Name);

        var newOperationType = _mapper.Map<FinanceOperationTypeDTO>(
                await _financeService.AddFinanceOperationTypeAsync(
                        _mapper.Map<FinanceOperationTypeModel>(dto)));

        _logger.LogInformation("Finance operation type with Id: {Id} added successfully", newOperationType.Id);

        return newOperationType;
    }

    [HttpPut("update")]
    public async Task<FinanceOperationTypeDTO> UpdateAsync([FromBody] FinanceOperationTypeDTO dto)
    {
        _logger.LogInformation("UpdateAsync called to update finance operation type with Id: {Id}", dto.Id);

        var updatedOperationType = _mapper.Map<FinanceOperationTypeDTO>(
                await _financeService.UpdateFinanceOperationTypeAsync(
                        _mapper.Map<FinanceOperationTypeModel>(dto)));

        _logger.LogInformation("Finance operation type with Id: {Id} updated successfully", updatedOperationType.Id);

        return updatedOperationType;
    }

    [HttpDelete("remove/{id}")]
    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation("DeleteAsync called to remove finance operation type with Id: {Id}", id);

        await _financeService.DeleteFinanceOperationTypeAsync(id);

        _logger.LogInformation("Finance operation type with Id: {Id} deleted successfully", id);
    }

}
