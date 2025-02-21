using AutoMapper;
using FinanceManager.Domain.Models;
using FinanceManager.Application.Models;

namespace FinanceManager.Application.Mapper.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountModel, AccountDTO>().ReverseMap();
    }
}
