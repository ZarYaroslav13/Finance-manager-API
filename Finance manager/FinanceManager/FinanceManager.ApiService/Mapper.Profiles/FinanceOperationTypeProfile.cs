using AutoMapper;
using DomainLayer.Models;
using FinanceManager.Web.Models;

namespace FinanceManager.Mapper.Profiles;

public class FinanceOperationTypeProfile : Profile
{
    public FinanceOperationTypeProfile()
    {
        CreateMap<FinanceOperationTypeModel, FinanceOperationTypeDTO>().ReverseMap();
    }
}
