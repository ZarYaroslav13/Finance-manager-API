using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Services.Finances;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    public class FinanceReportController : BaseController
    {
        private readonly IFinanceReportCreator _creator;
        private readonly IWalletService _walletService;

        public FinanceReportController(IFinanceReportCreator financeReportCreator, IWalletService walletService, IMapper mapper, ILogger<BaseController> logger) : base(mapper, logger)
        {
            _creator = financeReportCreator ?? throw new ArgumentNullException(nameof(financeReportCreator));
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        }

        [HttpPost("CreateDaily")]
        public async Task<FinanceReportDTO> CreateReportAsync(int walletId, DateTime date)
        {
            var wallet = await _walletService.FindWalletAsync(walletId);

            return _mapper.Map<FinanceReportDTO>(
                    await _creator.CreateFinanceReportAsync(wallet, date));
        }

        [HttpPost("CreatePeriodReport")]
        public async Task<FinanceReportDTO> CreateReportAsync(int walletId, DateTime startDate, DateTime endDate)
        {
            var wallet = await _walletService.FindWalletAsync(walletId);

            return _mapper.Map<FinanceReportDTO>(
                    await _creator.CreateFinanceReportAsync(wallet, startDate, endDate));
        }
    }
}
