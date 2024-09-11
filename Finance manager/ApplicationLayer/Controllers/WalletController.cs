﻿using ApplicationLayer.Controllers.Base;
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
        int userId = GetUserId();
        _logger.LogInformation("GetWallets called for user Id: {UserId}", userId);

        var wallets = (await _service.GetAllWalletsOfAccountAsync(userId))
                .Select(_mapper.Map<WalletDTO>)
                .ToList();

        _logger.LogInformation("{Count} wallets retrieved for user Id: {UserId}", wallets.Count, userId);

        return wallets;
    }

    [HttpPost("create")]
    public async Task<WalletDTO> CreateAsync([FromBody] WalletDTO wallet)
    {
        wallet.AccountId = GetUserId();
        _logger.LogInformation("CreateAsync called to create wallet for account Id: {AccountId}", wallet.AccountId);

        var newWallet = _mapper.Map<WalletDTO>(
                await _service.AddWalletAsync(
                    _mapper.Map<WalletModel>(wallet)));

        _logger.LogInformation("Wallet created successfully with Id: {WalletId} for account Id: {AccountId}", newWallet.Id, wallet.AccountId);

        return newWallet;
    }

    [HttpPut("update")]
    public async Task<WalletDTO> UpdateAsync([FromBody] WalletDTO wallet)
    {
        int userId = GetUserId();
        _logger.LogInformation("UpdateAsync called to update wallet Id: {WalletId} for user Id: {UserId}", wallet.Id, userId);

        if (wallet.AccountId != userId)
        {
            throw new UnauthorizedAccessException($"Unauthorized access attempt to update wallet Id: {wallet.Id} for user Id: {userId}");
        }

        var updatedWallet = _mapper.Map<WalletDTO>(
                await _service.UpdateWalletAsync(
                    _mapper.Map<WalletModel>(wallet)));

        _logger.LogInformation("Wallet Id: {WalletId} updated successfully for user Id: {UserId}", updatedWallet.Id, userId);

        return updatedWallet;
    }

    [HttpDelete("remove/{id}")]
    public async Task DeleteAsync(int id)
    {
        int userId = GetUserId();
        _logger.LogInformation("DeleteAsync called to remove wallet Id: {WalletId} for user Id: {UserId}", id, userId);

        if (!(await _service.IsAccountOwnerWalletAsync(userId, id)))
        {
            throw new UnauthorizedAccessException($"Unauthorized access attempt to delete wallet Id: {id} by user Id: {userId}");
        }

        await _service.DeleteWalletByIdAsync(id);

        _logger.LogInformation("Wallet Id: {WalletId} deleted successfully for user Id: {UserId}", id, userId);
    }

    [HttpGet("wallets/{id}")]
    public async Task<WalletDTO> GetByIdAsync(int id)
    {
        _logger.LogInformation("GetByIdAsync called to retrieve wallet Id: {WalletId}", id);

        var wallet = _mapper.Map<WalletDTO>(
                await _service.FindWalletAsync(id));

        _logger.LogInformation("Wallet Id: {WalletId} retrieved successfully", id);

        return wallet;
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpGet("wallets/account/{accountId}/admin")]
    public async Task<List<WalletDTO>> GetWalletsOfAccountAsync(int accountId)
    {
        _logger.LogInformation("GetWalletsOfAccountAsync called by admin for account Id: {AccountId}", accountId);

        var wallets = (await _service.GetAllWalletsOfAccountAsync(accountId))
                .Select(_mapper.Map<WalletDTO>)
                .ToList();

        _logger.LogInformation("{Count} wallets retrieved for account Id: {AccountId} by admin", wallets.Count, accountId);

        return wallets;
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpDelete("remove/{id}/admin")]
    public async Task DeleteWalletOfAccountAsync(int id)
    {
        _logger.LogInformation("DeleteWalletOfAccountAsync called by admin to delete wallet Id: {WalletId}", id);

        await _service.DeleteWalletByIdAsync(id);

        _logger.LogInformation("Admin deleted wallet Id: {WalletId} successfully", id);
    }
}
