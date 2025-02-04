using Server.Models;
using AutoMapper;
using DomainLayer.Models;

namespace Server.Mapper.Profiles;

public class WalletProfile : Profile
{
    public WalletProfile()
    {
        CreateMap<WalletModel, WalletDTO>().ReverseMap();
    }
}
