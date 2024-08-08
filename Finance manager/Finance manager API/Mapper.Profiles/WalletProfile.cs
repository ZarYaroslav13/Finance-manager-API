using AutoMapper;
using DomainLayer.Models;
using Finance_manager_API.Models;

namespace Finance_manager_API.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<WalletModel, WalletDTO>().ReverseMap();
    }
}
