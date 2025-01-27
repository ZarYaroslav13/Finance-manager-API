using AutoMapper;
using Infrastructure.Models;
using DomainLayer.Models;

namespace DomainLayer.Mapper.Profiles;

public class FinanceOperationTypeProfile : Profile
{
    public FinanceOperationTypeProfile()
    {
        CreateMap<FinanceOperationType, FinanceOperationTypeModel>()
           .ForMember(dest => dest.WalletName, opt => opt.MapFrom(src => (src.Wallet != null) ? src.Wallet.Name : string.Empty));

        CreateMap<FinanceOperationTypeModel, FinanceOperationType>();
    }
}
