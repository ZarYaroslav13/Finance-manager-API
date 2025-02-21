namespace FinanceManager.Domain.Models.Base;

public class HumanModel : Model
{
    public string LastName { get; set; } = String.Empty;

    public string FirstName { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        HumanModel human = (HumanModel)obj;

        return FirstName == human.FirstName
               && LastName == human.LastName
               && Email == human.Email
               && Password == human.Password;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), FirstName, LastName, Email, Password);
    }
}
