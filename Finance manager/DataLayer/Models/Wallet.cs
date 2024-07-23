﻿using DataLayer.Models.Base;

namespace DataLayer.Models;

public class Wallet : Entity
{
    public string Name { get; set; } = string.Empty;

    public int Balance { get; set; }

    public List<FinanceOperationType>? FinanceOperationTypes { get; set; } = new();

    public int AccountId { get; set; }

    public Account Account { get; set; } = default!;

    public List<FinanceOperation> GetFinanceOperations()
    {
        List<FinanceOperation> result = new();

        if (FinanceOperationTypes != null)
        {
            foreach (var transactionType in FinanceOperationTypes)
            {
                if (transactionType.FinanceOperations == null)
                    continue;

                result.AddRange(transactionType.FinanceOperations);
            }
        }

        return result;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Wallet))
            return false;

        var wallet = (Wallet)obj;

        return Id == wallet.Id
                && Name == wallet.Name
                && Balance == wallet.Balance
                && AccountId == wallet.AccountId
                && Account == wallet.Account
                && AreEqualLists(FinanceOperationTypes, wallet.FinanceOperationTypes)
                && AreEqualLists(GetFinanceOperations(), wallet.GetFinanceOperations());
    }
}
