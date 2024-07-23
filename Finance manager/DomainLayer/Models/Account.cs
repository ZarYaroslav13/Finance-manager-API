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
        if (obj == null || obj.GetType() != typeof(Account))
            return false;

        Account account = (Account)obj;

        return Id == account.Id &&
                FirstName == account.FirstName &&
                LastName == account.LastName &&
                Email == account.Email &&
                Password == account.Password
                && Enumerable.SequenceEqual(Wallets, account.Wallets);
    }
}
