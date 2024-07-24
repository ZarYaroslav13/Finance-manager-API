namespace DomainLayer.Models;

public class Account : Base.Model
{
    public string LastName { get; set; } = String.Empty;

    public string FirstName { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;

    public List<Wallet> Wallets { get; set; } = new();

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Account account = (Account)obj;

        return Id == account.Id
               && FirstName == account.FirstName
               && LastName == account.LastName
               && Email == account.Email
               && Password == account.Password
               && AreEqualLists(Wallets, account.Wallets);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, Email, Password, Wallets);
    }
}
