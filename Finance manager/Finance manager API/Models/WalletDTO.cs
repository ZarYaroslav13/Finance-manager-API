namespace Finance_manager_API.Models;

public class WalletDTO : Base.ModelDTO
{
    public string Name { get; set; } = String.Empty;

    public int Balance { get; set; }

    public List<FinanceOperationTypeDTO> FinanceOperationTypes { get; set; } = default!;

    public List<FinanceOperationDTO> Incomes { get; set; } = default!;

    public List<FinanceOperationDTO> Expenses { get; set; } = default!;

    public int AccountId { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        WalletDTO wallet = (WalletDTO)obj;

        return Id == wallet.Id
                && Name == wallet.Name
                && Balance == wallet.Balance
                && AccountId == wallet.AccountId
                && AreEqualLists(FinanceOperationTypes, wallet.FinanceOperationTypes)
                && AreEqualLists(Incomes, wallet.Incomes)
                && AreEqualLists(Expenses, wallet.Expenses);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Balance, AccountId, FinanceOperationTypes, Incomes, Expenses);
    }
}
