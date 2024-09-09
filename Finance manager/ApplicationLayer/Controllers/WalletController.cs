using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class WalletController : BaseController
{
    private readonly IWalletService _service;

    public WalletController(IWalletService service, IMapper mapper, ILogger<WalletController> logger) : base(mapper, logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet("wallets")]
    public async Task<List<WalletDTO>> GetWallets()
    {
        return (await _service.GetAllWalletsOfAccountAsync(GetUserId()))
                .Select(_mapper.Map<WalletDTO>)
                .ToList();
    }

    [HttpPost("create")]
    public async Task<WalletDTO> CreateAsync([FromBody] WalletDTO wallet)
    {
        wallet.AccountId = GetUserId();

        return _mapper.Map<WalletDTO>(
                await _service.AddWalletAsync(
                    _mapper.Map<WalletModel>(wallet)));
    }

    [HttpPut("update")]
    public async Task<WalletDTO> UpdateAsync([FromBody] WalletDTO wallet)
    {
        if (wallet.AccountId != GetUserId())
            throw new UnauthorizedAccessException(nameof(wallet.AccountId));

        return _mapper.Map<WalletDTO>(
                await _service.UpdateWalletAsync(
                    _mapper.Map<WalletModel>(wallet)));
    }

    [HttpDelete("remove/{id}")]
    public async Task DeleteAsync(int id)
    {
        if (!(await _service.IsAccountOwnerWalletAsync(GetUserId(), id)))
            throw new UnauthorizedAccessException(nameof(id));

        await _service.DeleteWalletByIdAsync(id);
    }

    [HttpGet("wallets/{id}")]
    public async Task<WalletDTO> GetByIdAsync(int id)
    {
        return _mapper.Map<WalletDTO>(
                await _service.FindWalletAsync(id));
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpGet("wallets/account/{accountId}/admin")]
    public async Task<List<WalletDTO>> GetWalletsOfAccountAsync(int accountId)
    {
        return (await _service.GetAllWalletsOfAccountAsync(accountId))
                .Select(_mapper.Map<WalletDTO>)
                .ToList();
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpDelete("remove/{id}/admin")]
    public async Task DeleteWalletOfAccountAsync(int id)
    {
        await _service.DeleteWalletByIdAsync(id);
    }
}
