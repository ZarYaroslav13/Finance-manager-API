using AutoMapper;
using DomainLayer.Models;
using FinanceManager.Web.Models;

namespace FinanceManager.Mapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountModel, AccountDTO>().ReverseMap();
    }
}
