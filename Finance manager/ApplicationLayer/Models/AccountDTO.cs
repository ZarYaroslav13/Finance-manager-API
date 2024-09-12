namespace ApplicationLayer.Models;

public class AccountDTO : Base.HumanDTO
{
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
