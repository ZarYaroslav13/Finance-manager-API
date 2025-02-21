using AutoMapper;
using Infrastructure.Models;
using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Mapper.Profiles;

public class FinanceOperationProfile : Profile
{
    public FinanceOperationProfile()
    {
        CreateMap<FinanceOperation, FinanceOperationModel>()
            .ConvertUsing((dbFinanceOperation, domainFinanceOperation, context) =>
            {
                ArgumentNullException.ThrowIfNull(dbFinanceOperation.Type, "Mapping<FinanceOperation, FinanceOperationModel> value dbFinanceOperation.Type cannot be null");

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
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .Include<IncomeModel, FinanceOperation>()
            .Include<ExpenseModel, FinanceOperation>();

        CreateMap<IncomeModel, FinanceOperation>();
        CreateMap<ExpenseModel, FinanceOperation>();
    }
}
