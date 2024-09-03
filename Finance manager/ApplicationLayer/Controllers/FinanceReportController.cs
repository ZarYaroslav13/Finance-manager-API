using ApplicationLayer.Controllers.Base;
using AutoMapper;

namespace ApplicationLayer.Controllers
{
    public class FinanceReportController : BaseController
    {


        public FinanceReportController(IMapper mapper, ILogger<BaseController> logger) : base(mapper, logger)
        {
        }
    }
}
