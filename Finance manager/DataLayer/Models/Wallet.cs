using DataLayer.Models.Base;

namespace DataLayer.Models;

public class Wallet : Entity
{
    public int Balance { get; set; }

    public List<TransactionType> TransactionTypes { get; set; }

    public List<Transaction> Transactions { get; set; }

    public int AccountId { get; set; }

    public Account Account { get; set; }
}
