using DataLayer.Models.Base;

namespace DataLayer.Models;

public class Transaction : Entity
{
    public uint Amount { get; set; }

    public DateTime Date { get; set; }

    public int TypeId { get; set; }

    public TransactionType Type { get; set; } = default!;
}
