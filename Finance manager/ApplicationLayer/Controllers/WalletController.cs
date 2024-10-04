using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Admins;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class WalletController : BaseController
{
    private readonly IWalletService _service;

    public WalletController(IWalletService service, IMapper mapper, ILogger<WalletController> logger) : base(mapper, logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet("~/finance-manager/accounts/{accountId:int}/wallets")]
    public async Task<IActionResult> GetWalletsAsync(int accountId)
    {
        int userId = GetUserId();
        string userRole = GetUserRole();

        _logger.LogInformation("GetWalletsOfAccountAsync called by user with id: {UserId} and role: {UserRole}, for account Id: {AccountId}",
            userId, userRole, accountId);

        if (userRole != AdminService.NameAdminRole && userId != accountId)
        {
            _logger.LogInformation("Access to get wallets information of account with {AccountId} is denied for  user with id: {UserId} and role: {UserRole}",
                accountId, userId, userRole);
            throw new UnauthorizedAccessException("Access denied");
        }

        var wallets = (await _service.GetAllWalletsOfAccountAsync(accountId))
                .Select(_mapper.Map<WalletDTO>)
                .ToList();

        _logger.LogInformation("{Count} wallets retrieved for account Id: {AccountId} by user with id: {UserId} and role: {UserRole}",
            wallets.Count, accountId, userId, userRole);

        return Ok(wallets);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        _logger.LogInformation("GetByIdAsync called to retrieve wallet Id: {WalletId}", id);

        int userId = GetUserId();

        if (!(await _service.IsAccountOwnerWalletAsync(userId, id)))
        {
            _logger.LogWarning($"Unauthorized access attempt to get wallet with Id: {id} by user Id: {userId}");
            throw new UnauthorizedAccessException($"Access to this wallet is denied");
        }

        var wallet = _mapper.Map<WalletDTO>(
                await _service.FindWalletAsync(id));

        _logger.LogInformation("Wallet Id: {WalletId} retrieved successfully", id);

        return Ok(wallet);
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
        {
            _logger.LogWarning($"Unauthorized access attempt to update wallet with Id: {wallet.Id} for user Id: {userId}");
            throw new UnauthorizedAccessException($"Access to this wallet is denied");
        }

        var updatedWallet = _mapper.Map<WalletDTO>(
                await _service.UpdateWalletAsync(
                    _mapper.Map<WalletModel>(wallet)));

        _logger.LogInformation("Wallet Id: {WalletId} updated successfully for user Id: {UserId}", updatedWallet.Id, userId);

        return Ok(updatedWallet);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        int userId = GetUserId();
        string userRole = GetUserRole();

        _logger.LogInformation("DeleteAsync called to remove wallet Id: {WalletId} for user Id: {UserId} and role: {UserRole}", id, userId, userRole);

        if (userRole != AdminService.NameAdminRole && !await _service.IsAccountOwnerWalletAsync(userId, id))
        {
            _logger.LogWarning($"Unauthorized access attempt to delete wallet Id: {id} by user Id: {userId} and role: {userRole}");
            throw new UnauthorizedAccessException($"Access to this wallet is denied");
        }

        await _service.DeleteWalletByIdAsync(id);

        _logger.LogInformation("Wallet Id: {WalletId} deleted successfully for user Id: {UserId} and role: {UserRole}", id, userId, userRole);

        return Ok();
    }
}
