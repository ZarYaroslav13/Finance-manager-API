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
        return (await _financeService.GetAllFinanceOperationTypesOfWalletAsync(walletId))
            .Select(_mapper.Map<FinanceOperationTypeDTO>)
            .ToList();
    }

    [HttpPost("create")]
    public async Task<FinanceOperationTypeDTO> AddAsync([FromBody] FinanceOperationTypeDTO dto)
    {
        return _mapper.Map<FinanceOperationTypeDTO>(
                await _financeService.AddFinanceOperationTypeAsync(
                        _mapper.Map<FinanceOperationTypeModel>(dto)));
    }

    [HttpPut("update")]
    public async Task<FinanceOperationTypeDTO> UpdateAsync([FromBody] FinanceOperationTypeDTO dto)
    {
        return _mapper.Map<FinanceOperationTypeDTO>(
                await _financeService.UpdateFinanceOperationTypeAsync(
                        _mapper.Map<FinanceOperationTypeModel>(dto)));
    }

    [HttpDelete("remove/{id}")]
    public async Task DeleteAsync(int id)
    {
        await _financeService.DeleteFinanceOperationTypeAsync(id);
    }
}
