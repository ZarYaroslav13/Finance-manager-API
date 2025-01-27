namespace Infrastructure.Models.Base;

public class Human : Entity
{
    public string LastName { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        var human = (Human)obj;

        return Id == human.Id
                && FirstName == human.FirstName
                && LastName == human.LastName
                && Email == human.Email
                && Password == human.Password;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), LastName, FirstName, Email, Password);
    }
}
