using FinanceManager.ApiService.Models.Base;

namespace ApplicationLayerTests.Data.Models;

public static class HumantDTOTestDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new HumanDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new HumanDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"},
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Password = "Password"}
        },
        new object[]
        {
            new HumanDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new HumanDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new HumanDTO(){ Id = 2, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new HumanDTO(){ Id = 1, FirstName = "FirstName", Email = "Email", Password = "Password"},
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new HumanDTO(),
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"}
        },
        new object[]
        {
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new HumanDTO()
        },
        new object[]
        {
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", }
        },
        new object[]
        {
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            null
        },
        new object[]
        {
            new HumanDTO(){ Id = 1, LastName = "LastName", FirstName = "FirstName", Email = "Email", Password = "Password"},
            new ModelDTO()
        }
    };
}
