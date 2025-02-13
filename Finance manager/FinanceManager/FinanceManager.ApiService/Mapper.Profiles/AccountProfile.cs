using AutoMapper;
using DomainLayer.Models;
using FinanceManager.ApiService.Models;

namespace FinanceManager.ApiService.Mapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountModel, AccountDTO>().ReverseMap();
    }
}
