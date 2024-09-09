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
        return (await _financeService.GetAllFinanceOperationOfWalletAsync(walletId, index, count))
            .Select(_mapper.Map<FinanceOperationDTO>)
            .ToList();
    }

    [HttpGet("operations/type/{typeId}")]
    public async Task<List<FinanceOperationDTO>> GetAllOfTypeAsync(int typeId)
    {
        return (await _financeService.GetAllFinanceOperationOfTypeAsync(typeId))
            .Select(_mapper.Map<FinanceOperationDTO>)
            .ToList();
    }

    [HttpPost("create")]
    public async Task<FinanceOperationDTO> AddAsync([FromBody] FinanceOperationDTO dto)
    {
        return _mapper.Map<FinanceOperationDTO>(
                await _financeService.AddFinanceOperationAsync(
                        _mapper.Map<FinanceOperationModel>(dto)));
    }

    [HttpPut("update/{id}")]
    public async Task<FinanceOperationDTO> UpdateAsync([FromBody] FinanceOperationDTO dto)
    {
        return _mapper.Map<FinanceOperationDTO>(
                await _financeService.UpdateFinanceOperationAsync(
                        _mapper.Map<FinanceOperationModel>(dto)));
    }

    [HttpDelete("remove/{id}")]
    public async Task DeleteAsync(int id)
    {
        await _financeService.DeleteFinanceOperationAsync(id);
    }
}
