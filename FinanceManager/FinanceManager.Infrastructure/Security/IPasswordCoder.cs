namespace Infrastructure.Security;

public interface IPasswordCoder
{
    public string ComputeSHA256Hash(string input);
}
