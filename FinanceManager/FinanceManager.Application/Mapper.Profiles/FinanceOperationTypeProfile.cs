using AutoMapper;
using FinanceManager.Domain.Models;
using FinanceManager.Application.Models;

namespace FinanceManager.Application.Mapper.Profiles;

public class FinanceOperationTypeProfile : Profile
{
    public FinanceOperationTypeProfile()
    {
        CreateMap<FinanceOperationTypeModel, FinanceOperationTypeDTO>().ReverseMap();
    }
}
