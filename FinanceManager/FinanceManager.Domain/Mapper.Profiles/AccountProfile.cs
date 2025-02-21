using AutoMapper;
using Infrastructure.Models;
using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Mapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountModel>().ReverseMap();
    }
}
