using AutoMapper;
using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayer.Mapper.Profiles;

public class FinanceOperationProfile : Profile
{
    public FinanceOperationProfile()
    {
        CreateMap<FinanceOperation, FinanceOperationModel>()
            .ConvertUsing((dbFinanceOperation, domainFinanceOperation, context) =>
            {
                ArgumentNullException.ThrowIfNull(dbFinanceOperation.Type);

                switch (dbFinanceOperation.Type.EntryType)
                {
                    case EntryType.Income:
                        return context.Mapper.Map<IncomeModel>(dbFinanceOperation);
                    case EntryType.Expense:
                        return context.Mapper.Map<ExpenseModel>(dbFinanceOperation);
                    default:
                        throw new ArgumentException(nameof(dbFinanceOperation.Type));
                }
            });

        CreateMap<FinanceOperation, IncomeModel>();
        CreateMap<FinanceOperation, ExpenseModel>();

        CreateMap<FinanceOperationModel, FinanceOperation>()
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type.Id))
            .Include<IncomeModel, FinanceOperation>()
            .Include<ExpenseModel, FinanceOperation>();

        CreateMap<IncomeModel, FinanceOperation>();
        CreateMap<ExpenseModel, FinanceOperation>();
    }
}
