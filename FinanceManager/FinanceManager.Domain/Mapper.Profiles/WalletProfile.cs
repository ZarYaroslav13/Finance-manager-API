using AutoMapper;
using Infrastructure.Models;
using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<Wallet, WalletModel>()
            .ForMember(dest => dest.Incomes, opt => opt.MapFrom(src => src.GetFinanceOperations()
                .Where(fo => fo.Type.EntryType == EntryType.Income)))
            .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.GetFinanceOperations()
                .Where(fo => fo.Type.EntryType == EntryType.Expense)));

        CreateMap<WalletModel, Wallet>();
    }
}
