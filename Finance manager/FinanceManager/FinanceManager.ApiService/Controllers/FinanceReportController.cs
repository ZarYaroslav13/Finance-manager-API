using FinanceManager.ApiService.Controllers.Base;
using FinanceManager.ApiService.Models;
using AutoMapper;
using DomainLayer.Services.Finances;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.ApiService.Controllers;

public class FinanceReportController : BaseController
{
    private readonly IFinanceReportCreator _creator;
    private readonly IWalletService _walletService;

    public FinanceReportController(IFinanceReportCreator financeReportCreator, IWalletService walletService, IMapper mapper, ILogger<FinanceReportController> logger) : base(mapper, logger)
    {
        _creator = financeReportCreator ?? throw new ArgumentNullException(nameof(financeReportCreator));
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
    }

    [HttpPost("daily")]
    public async Task<IActionResult> CreateReportAsync(int walletId, DateTime date)
    {
        int userId = GetUserId();
        _logger.LogInformation("CreateReportAsync (daily) called by user with Id: {UserId} to create finance report for wallet Id: {WalletId} on date: {Date}", userId, walletId, date);

        if (!await _walletService.IsAccountOwnerWalletAsync(userId, walletId))
        {
            _logger.LogWarning($"Unauthorized access attempt to get wallet information with Id: {walletId} for user Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        var wallet = await _walletService.FindWalletAsync(walletId);

        var report = _mapper.Map<FinanceReportDTO>(
                await _creator.CreateFinanceReportAsync(wallet, date));

        _logger.LogInformation("Finance report (daily) created successfully for wallet Id: {WalletId} on date: {Date}", walletId, date);

        return Ok(report);
    }

    [HttpPost("period")]
    public async Task<IActionResult> CreateReportAsync(int walletId, DateTime startDate, DateTime endDate)
    {
        int userId = GetUserId();
        _logger.LogInformation("CreateReportAsync (period) called to create finance report for wallet Id: {WalletId} from {StartDate} to {EndDate}", walletId, startDate, endDate);

        if (!(await _walletService.IsAccountOwnerWalletAsync(userId, walletId)))
        {
            _logger.LogWarning($"Unauthorized access attempt to get wallet information with Id: {walletId} for user Id: {userId}");
            throw new UnauthorizedAccessException("Access to this wallet is denied");
        }

        var wallet = await _walletService.FindWalletAsync(walletId);

        var report = _mapper.Map<FinanceReportDTO>(
                await _creator.CreateFinanceReportAsync(wallet, startDate, endDate));

        _logger.LogInformation("Finance report (period) created successfully for wallet Id: {WalletId} from {StartDate} to {EndDate}", walletId, startDate, endDate);

        return Ok(report);
    }
}
