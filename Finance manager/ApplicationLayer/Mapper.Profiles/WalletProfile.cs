using AutoMapper;
using DomainLayer.Models;
using ApplicationLayer.Models;

namespace ApplicationLayer.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<WalletModel, WalletDTO>().ReverseMap();
    }
}
