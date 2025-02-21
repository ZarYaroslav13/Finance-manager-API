using AutoMapper;
using Infrastructure.Models;
using FinanceManager.Domain.Mapper.Profiles;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Tests.TestHelpers;

namespace FinanceManager.Domain.Tests.Mapper.Profiles;

[TestClass]
public class AdminProfileTests
{
    private readonly IMapper _mapper;

    public AdminProfileTests()
    {
        _mapper = new MapperConfiguration(
            cfg =>
                cfg.AddProfile<AdminProfile>())
            .CreateMapper();
    }

    [TestMethod]
    public void Map_AdminDataMappedCorrectly_AdminModel()
    {
        var dbAdmin = new Admin()
        {
            Id = 2,
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "email@gmail.com",
            Password = "password"
        };

        var domainAdmin = _mapper.Map<AdminModel>(dbAdmin);

        Assert.That.AreEqual(dbAdmin, domainAdmin);
    }

    [TestMethod]
    public void Map_AdminModelDataMappedCorrectly_Admin()
    {
        var dbAdmin = new Admin()
        {
            Id = 2,
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "email@gmail.com",
            Password = "password"
        };

        var mappedDbAdmin = _mapper
            .Map<Admin>(
                _mapper
                    .Map<AdminModel>(dbAdmin));

        Assert.AreEqual(dbAdmin, mappedDbAdmin);
    }

}
