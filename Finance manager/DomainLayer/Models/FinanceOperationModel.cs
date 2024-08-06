﻿namespace DomainLayer.Models;

public abstract class FinanceOperationModel : Base.Model
{
    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public FinanceOperationTypeModel Type { get; private set; } = new();

    public FinanceOperationModel(FinanceOperationTypeModel type)
    {
        ChangeFinanceOperationType(type);
    }

    public virtual void ChangeFinanceOperationType(FinanceOperationTypeModel type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var financeOperation = (FinanceOperationModel)obj;

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
