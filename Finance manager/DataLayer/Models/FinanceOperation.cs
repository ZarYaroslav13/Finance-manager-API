using DataLayer.Models.Base;

namespace DataLayer.Models;

public class FinanceOperation : Entity
{
    public uint Amount { get; set; }

    public DateTime Date { get; set; }

    public int TypeId { get; set; }

    public FinanceOperationType Type { get; set; } = default!;
}
