namespace DomainLayer.Models;

public class Account : Base.Entity
{
    public string LastName { get; set; } = String.Empty;

    public string FirstName { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;

    public List<Wallet> Wallets { get; set; } = default!;
}
