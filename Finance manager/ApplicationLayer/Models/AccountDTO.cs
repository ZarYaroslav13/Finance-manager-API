namespace ApplicationLayer.Models;

public class AccountDTO : Base.ModelDTO
{
    public string LastName { get; set; } = String.Empty;

    public string FirstName { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        AccountDTO account = (AccountDTO)obj;

        return Id == account.Id
               && FirstName == account.FirstName
               && LastName == account.LastName
               && Email == account.Email
               && Password == account.Password;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, Email, Password);
    }
}
