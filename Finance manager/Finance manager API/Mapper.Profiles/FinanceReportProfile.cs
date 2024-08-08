using AutoMapper;
using DomainLayer.Models;
using Finance_manager_API.Models;

namespace Finance_manager_API.Mapper.Profiles;

public class FinanceReportProfile : Profile
{
    public FinanceReportProfile()
    {
        CreateMap<FinanceReportModel, FinanceReportDTO>()
             .ConstructUsing((src, context) =>
             {
                 var operations = src.Operations.Select(op => context.Mapper.Map<FinanceOperationDTO>(op)).ToList();
                 return new FinanceReportDTO(src.WalletId, src.WalletName, src.TotalIncome, src.TotalExpense, operations, src.Period);
             });
    }
}
