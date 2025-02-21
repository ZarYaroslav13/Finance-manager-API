using FinanceManager.Domain.Models.Base;

namespace FinanceManager.Domain.Models;

public class AccountModel : HumanModel
{
    public List<WalletModel> Wallets { get; set; } = new();

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        AccountModel account = (AccountModel)obj;

        return AreEqualLists(Wallets, account.Wallets);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), GetHashCodeOfList(Wallets));
    }
}
