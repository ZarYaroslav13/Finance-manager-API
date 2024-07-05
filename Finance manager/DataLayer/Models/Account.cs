namespace DataLayer.Models;

public class Account : Base.Entity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public List<Wallet> Wallets { get; set; }
}