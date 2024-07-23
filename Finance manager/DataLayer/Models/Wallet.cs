using DataLayer.Models.Base;

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
        if (FinanceOperationTypes == null)
            return null;

        List<FinanceOperation> result = new();

        foreach (var transactionType in FinanceOperationTypes)
        {
            result.AddRange(transactionType.Transactions);
        }

        return result;
    }
}
