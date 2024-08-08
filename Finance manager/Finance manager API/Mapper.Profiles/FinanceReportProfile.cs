using AutoMapper;
using DomainLayer.Models;
using Finance_manager_API.Models;

namespace Finance_manager_API.Mapper.Profiles;

public class FinanceReportProfile : Profile
{
    public FinanceReportProfile()
    {
        CreateMap<FinanceReportModel, FinanceReportDTO>()
            .ConstructUsing(
                src => 
                    new FinanceReportDTO(src.WalletId, src.WalletName, src.TotalIncome, src.TotalExpense, src.Period));
    }
}
