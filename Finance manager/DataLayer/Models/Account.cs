namespace DataLayer.Models;

public class Account : Base.Entity
{
    public string LastName { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public List<Wallet>? Wallets { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Account))
            return false;

        Account account = (Account)obj;

        return Id == account.Id &&
                FirstName == account.FirstName &&
                LastName == account.LastName &&
                Email == account.Email &&
                Password == account.Password;
    }
}