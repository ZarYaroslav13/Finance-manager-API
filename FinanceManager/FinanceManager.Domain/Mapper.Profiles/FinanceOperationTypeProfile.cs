using AutoMapper;
using Infrastructure.Models;
using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Mapper.Profiles;

public class FinanceOperationTypeProfile : Profile
{
    public FinanceOperationTypeProfile()
    {
        CreateMap<FinanceOperationType, FinanceOperationTypeModel>()
           .ForMember(dest => dest.WalletName, opt => opt.MapFrom(src => (src.Wallet != null) ? src.Wallet.Name : string.Empty));

        CreateMap<FinanceOperationTypeModel, FinanceOperationType>();
    }
}
