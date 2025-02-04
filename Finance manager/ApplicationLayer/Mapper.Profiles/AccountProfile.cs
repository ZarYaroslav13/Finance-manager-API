using Server.Models;
using AutoMapper;
using DomainLayer.Models;

namespace Server.Mapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountModel, AccountDTO>().ReverseMap();
    }
}
