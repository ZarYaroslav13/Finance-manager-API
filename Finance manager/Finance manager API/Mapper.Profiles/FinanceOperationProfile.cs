using AutoMapper;
using DataLayer.Models;
using DomainLayer.Models;
using Finance_manager_API.Models;

namespace Finance_manager_API.Mapper.Profiles;

public class FinanceOperationProfile : Profile
{
    public FinanceOperationProfile()
    {
        CreateMap<FinanceOperationModel, FinanceOperationDTO>()
            .ConvertUsing((financeOperationModel, financeOperationDTO, context) =>
            {
                ArgumentNullException.ThrowIfNull(financeOperationModel.Type);

                switch (financeOperationModel.Type.EntryType)
                {
                    case EntryType.Income:
                        return context.Mapper.Map<IncomeDTO>(financeOperationModel);
                    case EntryType.Expense:
                        return context.Mapper.Map<ExpenseDTO>(financeOperationModel);
                    default:
                        throw new ArgumentException(nameof(financeOperationModel.Type));
                }
            });

        CreateMap<IncomeModel, IncomeDTO>().ReverseMap();
        CreateMap<ExpenseModel, ExpenseDTO>().ReverseMap();

        CreateMap<FinanceOperationModel, IncomeDTO>();
        CreateMap<FinanceOperationModel, ExpenseDTO>();
    }
}
