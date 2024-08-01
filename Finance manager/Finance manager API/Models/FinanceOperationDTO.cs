namespace Finance_manager_API.Models;

public class FinanceOperationDTO : Base.ModelDTO
{
    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public FinanceOperationTypeDTO Type { get; private set; } = new();

    public FinanceOperationDTO(FinanceOperationTypeDTO type)
    {
        ChangeFinanceOperationType(type);
    }

    public void ChangeFinanceOperationType(FinanceOperationTypeDTO type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var financeOperation = (FinanceOperationDTO)obj;

        return Id == financeOperation.Id
            && Amount == financeOperation.Amount
            && Date == financeOperation.Date
            && Type == financeOperation.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Amount, Date, Type);
    }
}
