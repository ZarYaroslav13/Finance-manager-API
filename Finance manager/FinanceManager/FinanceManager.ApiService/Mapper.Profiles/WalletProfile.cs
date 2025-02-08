using AutoMapper;
using DomainLayer.Models;
using FinanceManager.ApiService.Models;

namespace FinanceManager.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<WalletModel, WalletDTO>().ReverseMap();
    }
}
