using DataLayer.Models.Base;

namespace DataLayer.Models;

public class Wallet : Entity
{
    public int Balance { get; set; }

    public List<TransactionType>? TransactionTypes { get; set; }

    public List<Transaction>? Transactions => GetTransactions();

    public int AccountId { get; set; }

    public Account Account { get; set; } = default!;

    private List<Transaction> GetTransactions()
    {
        List<Transaction> result = new();
        if (TransactionTypes == null)
        {
            return null;
        }

        foreach (var transactionType in TransactionTypes) { 
            result.AddRange(transactionType.Transactions);
        }

        return result;
    }
}
