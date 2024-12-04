using DataLayer.Models.Base;

namespace DataLayer.Models;

public class Account : Human
{
    public List<Wallet>? Wallets { get; set; }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        var account = (Account)obj;

        return AreEqualLists(Wallets, account.Wallets);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), GetHashCodeOfList(Wallets));
    }
}