using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Mapper.Profiles;

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
