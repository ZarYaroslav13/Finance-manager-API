using DomainLayer.Models.Base;

namespace DomainLayer.Models;

public class Report : Entity
{
    public int TotalIncome { get; set; }

    public int TotalExpense { get; set; }

    public List<FinanceOperation> AllOperations { get; set; } = default!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
