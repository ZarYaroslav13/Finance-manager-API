using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Models;

public class FinanceOperationDTO : Base.ModelDTO
{
    [Range(0, int.MaxValue)]
    public int Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public FinanceOperationTypeDTO Type { get; set; }

    public virtual void ChangeFinanceOperationType(FinanceOperationTypeDTO type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        var financeOperation = (FinanceOperationDTO)obj;

        return Amount == financeOperation.Amount
            && Date == financeOperation.Date
            && Type == financeOperation.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Amount, Date, Type);
    }
}
