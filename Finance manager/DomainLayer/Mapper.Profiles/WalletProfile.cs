using AutoMapper;
using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayer.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<Wallet, WalletModel>()
            .ForMember(dest => dest.Incomes, opt => opt.MapFrom(src => src.GetFinanceOperations()
                .Where(fo => fo.Type.EntryType == EntryType.Income)))
            .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.GetFinanceOperations()
                .Where(fo => fo.Type.EntryType == EntryType.Exponse)));

        CreateMap<WalletModel, Wallet>();
    }
}
