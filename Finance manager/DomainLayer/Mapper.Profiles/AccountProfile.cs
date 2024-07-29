using AutoMapper;
using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayer.Mapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountModel>().ReverseMap();
    }
}
