using AutoMapper;
using FinanceManager.Domain.Models;
using FinanceManager.Application.Models;

namespace FinanceManager.Application.Mapper.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<AdminModel, AdminDTO>().ReverseMap();
    }
}
