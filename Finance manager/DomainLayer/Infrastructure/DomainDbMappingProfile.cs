using AutoMapper;
using DomainLayer.Models;

namespace DomainLayer.Infrastructure;

public class DomainDbMappingProfile : Profile
{
    public DomainDbMappingProfile()
    {
        CreateMap<DataLayer.Models.Account, Account>().ReverseMap();

        CreateMap<DataLayer.Models.Wallet, Wallet>()
            .ForMember(dest => dest.Incomes, opt => opt.MapFrom(src => src.GetFinanceOperations()
                .Where(fo => fo.Type.EntryType == DataLayer.Models.EntryType.Income)))
            .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.GetFinanceOperations()
                .Where(fo => fo.Type.EntryType == DataLayer.Models.EntryType.Exponse)));

        CreateMap<DataLayer.Models.FinanceOperationType, FinanceOperationType>()
            .ForMember(dest => dest.WalletName, opt => opt.MapFrom(src => src.Wallet.Name))
            .ReverseMap();

        /////????????????????????//////
        CreateMap<DataLayer.Models.FinanceOperation, FinanceOperation>();
        /////????????????????????//////
        
        CreateMap<Wallet, DataLayer.Models.Wallet>();

        CreateMap<FinanceOperation, DataLayer.Models.FinanceOperation>()
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type.Id))
            .Include<Income, DataLayer.Models.FinanceOperation>()
            .Include<Expense, DataLayer.Models.FinanceOperation>();

        CreateMap<Income, DataLayer.Models.FinanceOperation>();
        CreateMap<Expense, DataLayer.Models.FinanceOperation>();
    }
}
