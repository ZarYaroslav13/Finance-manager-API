using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Admins;
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

    [HttpGet]
    public async Task<IActionResult> GetWallets()
    {
        int userId = GetUserId();
        _logger.LogInformation("GetWallets called for user Id: {UserId}", userId);

        var wallets = (await _service.GetAllWalletsOfAccountAsync(userId))
                .Select(_mapper.Map<WalletDTO>)
                .ToList();

        _logger.LogInformation("{Count} wallets retrieved for user Id: {UserId}", wallets.Count, userId);

        return Ok(wallets);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] WalletDTO wallet)
    {
        wallet.AccountId = GetUserId();
        _logger.LogInformation("CreateAsync called to create wallet for account Id: {AccountId}", wallet.AccountId);

        var newWallet = _mapper.Map<WalletDTO>(
                await _service.AddWalletAsync(
                    _mapper.Map<WalletModel>(wallet)));

        _logger.LogInformation("Wallet created successfully with Id: {WalletId} for account Id: {AccountId}", newWallet.Id, wallet.AccountId);

        return Ok(newWallet);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] WalletDTO wallet)
    {
        int userId = GetUserId();
        _logger.LogInformation("UpdateAsync called to update wallet Id: {WalletId} for user Id: {UserId}", wallet.Id, userId);

        if (wallet.AccountId != userId)
            throw new UnauthorizedAccessException($"Unauthorized access attempt to update wallet with Id: {wallet.Id} for user Id: {userId}");

        var updatedWallet = _mapper.Map<WalletDTO>(
                await _service.UpdateWalletAsync(
                    _mapper.Map<WalletModel>(wallet)));

        _logger.LogInformation("Wallet Id: {WalletId} updated successfully for user Id: {UserId}", updatedWallet.Id, userId);

        return Ok(updatedWallet);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        int userId = GetUserId();
        _logger.LogInformation("DeleteAsync called to remove wallet Id: {WalletId} for user Id: {UserId}", id, userId);

        if (!(await _service.IsAccountOwnerWalletAsync(userId, id)))
        {
            throw new UnauthorizedAccessException($"Unauthorized access attempt to delete wallet Id: {id} by user Id: {userId}");
        }

        await _service.DeleteWalletByIdAsync(id);

        _logger.LogInformation("Wallet Id: {WalletId} deleted successfully for user Id: {UserId}", id, userId);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        _logger.LogInformation("GetByIdAsync called to retrieve wallet Id: {WalletId}", id);

        int userId = GetUserId();

        if (!(await _service.IsAccountOwnerWalletAsync(userId, id)))
        {
            throw new UnauthorizedAccessException($"Unauthorized access attempt to get wallet with Id: {id} by user Id: {userId}");
        }

        var wallet = _mapper.Map<WalletDTO>(
                await _service.FindWalletAsync(id));

        _logger.LogInformation("Wallet Id: {WalletId} retrieved successfully", id);

        return Ok(wallet);
    }

    [Authorize(Policy = AdminService.AdminPolicy)]
    [HttpGet("account/{accountId}/for-admins")]
    public async Task<IActionResult> GetWalletsOfAccountAsync(int accountId)
    {
        _logger.LogInformation("GetWalletsOfAccountAsync called by admin for account Id: {AccountId}", accountId);

        var wallets = (await _service.GetAllWalletsOfAccountAsync(accountId))
                .Select(_mapper.Map<WalletDTO>)
                .ToList();

        _logger.LogInformation("{Count} wallets retrieved for account Id: {AccountId} by admin", wallets.Count, accountId);

        return Ok(wallets);
    }

    [Authorize(Policy = AdminService.AdminPolicy)]
    [HttpDelete("{id}/for-admins")]
    public async Task<IActionResult> DeleteWalletOfAccountAsync(int id)
    {
        _logger.LogInformation("DeleteWalletOfAccountAsync called by admin to delete wallet Id: {WalletId}", id);

        await _service.DeleteWalletByIdAsync(id);

        _logger.LogInformation("Admin deleted wallet Id: {WalletId} successfully", id);

        return Ok();
    }
}
