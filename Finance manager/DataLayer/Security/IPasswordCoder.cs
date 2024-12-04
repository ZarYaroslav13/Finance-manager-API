namespace DataLayer.Security;

public interface IPasswordCoder
{
    public string ComputeSHA256Hash(string input);
}
