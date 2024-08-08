using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Mapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountModel, AccountDTO>().ReverseMap();
    }
}
