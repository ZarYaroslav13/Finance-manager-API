using DomainLayer.Models;

namespace ApplicationLayerTests.Data.Mapper.Profiles;

public static class AdminProfileTestDataProvider
{
    public static IEnumerable<object[]> DomainAdmin { get; } = new List<object[]>
    {
        new object[]
        {
            new AdminModel
            {
                Id = 2,
                LastName = "LastName",
                FirstName = "FirstName",
                Email = "email@gmail.com",
                Password = "password"
            }
        }
    };
}
