using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Mapper.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<AdminModel, AdminDTO>().ReverseMap();
    }
}
