using AutoMapper;
using Infrastructure.Models;
using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Mapper.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<Admin, AdminModel>().ReverseMap();
    }
}
