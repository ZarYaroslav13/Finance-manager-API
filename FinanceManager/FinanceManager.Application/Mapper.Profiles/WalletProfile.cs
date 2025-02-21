using AutoMapper;
using FinanceManager.Domain.Models;
using FinanceManager.Application.Models;

namespace FinanceManager.Application.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<WalletModel, WalletDTO>().ReverseMap();
    }
}
