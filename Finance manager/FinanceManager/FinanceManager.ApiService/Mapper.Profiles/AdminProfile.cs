using AutoMapper;
using DomainLayer.Models;
using FinanceManager.ApiService.Models;

namespace FinanceManager.ApiService.Mapper.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<AdminModel, AdminDTO>().ReverseMap();
    }
}
