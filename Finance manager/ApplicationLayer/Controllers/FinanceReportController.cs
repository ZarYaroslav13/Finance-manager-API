using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Services.Finances;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class FinanceReportController : BaseController
{
    private readonly IFinanceReportCreator _creator;
    private readonly IWalletService _walletService;

    public FinanceReportController(IFinanceReportCreator financeReportCreator, IWalletService walletService, IMapper mapper, ILogger<BaseController> logger) : base(mapper, logger)
    {
        _creator = financeReportCreator ?? throw new ArgumentNullException(nameof(financeReportCreator));
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
    }

    [HttpPost("create/daily")]
    public async Task<FinanceReportDTO> CreateReportAsync(int walletId, DateTime date)
    {
        _logger.LogInformation("CreateReportAsync (daily) called to create finance report for wallet Id: {WalletId} on date: {Date}", walletId, date);

        var wallet = await _walletService.FindWalletAsync(walletId);

        var report = _mapper.Map<FinanceReportDTO>(
                await _creator.CreateFinanceReportAsync(wallet, date));

        _logger.LogInformation("Finance report (daily) created successfully for wallet Id: {WalletId} on date: {Date}", walletId, date);

        return report;
    }

    [HttpPost("create/period")]
    public async Task<FinanceReportDTO> CreateReportAsync(int walletId, DateTime startDate, DateTime endDate)
    {
        _logger.LogInformation("CreateReportAsync (period) called to create finance report for wallet Id: {WalletId} from {StartDate} to {EndDate}", walletId, startDate, endDate);

        var wallet = await _walletService.FindWalletAsync(walletId);

        var report = _mapper.Map<FinanceReportDTO>(
                await _creator.CreateFinanceReportAsync(wallet, startDate, endDate));

        _logger.LogInformation("Finance report (period) created successfully for wallet Id: {WalletId} from {StartDate} to {EndDate}", walletId, startDate, endDate);

        return report;
    }
}
