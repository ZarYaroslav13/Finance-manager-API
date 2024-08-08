using AutoMapper;
using DomainLayer.Models;
using ApplicationLayer.Models;

namespace ApplicationLayer.Mapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountModel, AccountDTO>().ReverseMap();
    }
}
