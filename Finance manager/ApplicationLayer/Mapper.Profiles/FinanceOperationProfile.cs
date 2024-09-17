using ApplicationLayer.Models;
using AutoMapper;
using DataLayer.Models;
using DomainLayer.Models;

namespace ApplicationLayer.Mapper.Profiles;

public class FinanceOperationProfile : Profile
{
    public FinanceOperationProfile()
    {
        CreateMap<FinanceOperationModel, FinanceOperationDTO>()
            .ConvertUsing((financeOperationModel, financeOperationDTO, context) =>
            {
                ArgumentNullException.ThrowIfNull(financeOperationModel.Type, "Mapping<FinanceOperationModel, FinanceOperationDTO> value financeOperationDTO.Type cannot be null");

                switch (financeOperationModel.Type.EntryType)
                {
                    case EntryType.Income:
                        return context.Mapper.Map<IncomeDTO>(financeOperationModel);
                    case EntryType.Expense:
                        return context.Mapper.Map<ExpenseDTO>(financeOperationModel);
                    default:
                        throw new ArgumentException(nameof(financeOperationModel.Type));
                }
            });

        CreateMap< FinanceOperationDTO, FinanceOperationModel>()
            .ConvertUsing((financeOperationDTO, financeOperationModel, context) =>
            {
                ArgumentNullException.ThrowIfNull(financeOperationDTO.Type, "Mapping<FinanceOperationDTO, FinanceOperationModel> value financeOperationDTO.Type cannot be null");

                switch (financeOperationDTO.Type.EntryType)
                {
                    case EntryType.Income:
                        return context.Mapper.Map<IncomeModel>(financeOperationDTO);
                    case EntryType.Expense:
                        return context.Mapper.Map<ExpenseModel>(financeOperationDTO);
                    default:
                        throw new ArgumentException(nameof(financeOperationDTO.Type));
                }
            });

        CreateMap<IncomeModel, IncomeDTO>().ReverseMap();
        CreateMap<ExpenseModel, ExpenseDTO>().ReverseMap();

        CreateMap<FinanceOperationModel, IncomeDTO>();
        CreateMap<FinanceOperationModel, ExpenseDTO>();

        CreateMap<IncomeDTO, FinanceOperationModel>()
            .ConvertUsing((incomeDTO, financeOperationModel, context) =>
            {
                return context.Mapper.Map<IncomeModel>(incomeDTO);
            });

        CreateMap<ExpenseDTO, FinanceOperationModel>()
            .ConvertUsing((expenseDTO, financeOperationModel, context) =>
            {
                return context.Mapper.Map<ExpenseModel>(expenseDTO);
            });


        CreateMap<FinanceOperationDTO, IncomeModel>();
        CreateMap<FinanceOperationDTO, ExpenseModel>();

        CreateMap<IncomeModel, FinanceOperationDTO>()
            .ConvertUsing((incomeModel, financeOperationDTO, context) =>
            {
                return context.Mapper.Map<IncomeDTO>(incomeModel);
            });

        CreateMap<ExpenseModel, FinanceOperationDTO>()
            .ConvertUsing((expenseModel, financeOperationDTO, context) =>
            {
                return context.Mapper.Map<ExpenseDTO>(expenseModel);
            });
    }
}
