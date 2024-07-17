﻿namespace DomainLayer.Models;

public abstract class FinanceOperation : Base.Entity
{
    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public FinanceOperationType Type { get; set; } = default!;
}
