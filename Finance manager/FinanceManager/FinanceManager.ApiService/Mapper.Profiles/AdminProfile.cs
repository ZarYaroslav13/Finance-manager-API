using AutoMapper;
using DomainLayer.Models;
using FinanceManager.ApiService.Models;

namespace FinanceManager.Mapper.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<AdminModel, AdminDTO>().ReverseMap();
    }
}
