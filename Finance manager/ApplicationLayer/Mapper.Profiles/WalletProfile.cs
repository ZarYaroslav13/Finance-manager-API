using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<WalletModel, WalletDTO>().ReverseMap();
    }
}
