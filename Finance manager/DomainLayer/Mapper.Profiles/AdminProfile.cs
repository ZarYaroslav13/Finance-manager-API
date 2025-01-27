using AutoMapper;
using Infrastructure.Models;
using DomainLayer.Models;

namespace DomainLayer.Mapper.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<Admin, AdminModel>().ReverseMap();
    }
}
