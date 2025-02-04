using Server.Models;
using AutoMapper;
using DomainLayer.Models;

namespace Server.Mapper.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<AdminModel, AdminDTO>().ReverseMap();
    }
}
