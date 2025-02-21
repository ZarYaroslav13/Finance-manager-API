using FinanceManager.Application.Mapper.Profiles;
using FinanceManager.Application.Models;
using FinanceManager.Application.Tests.Data.Mapper.Profiles;
using FinanceManager.Application.Tests.TestToolExtensions;
using AutoMapper;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Tests.Mapper.Profiles;

[TestClass]
public class AdminProfileTests
{
    private readonly IMapper _mapper;

    public AdminProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<AdminProfile>();
                })
            .CreateMapper();
    }

    [TestMethod]
    [DynamicData(nameof(AdminProfileTestDataProvider.DomainAdmin), typeof(AdminProfileTestDataProvider))]
    public void Map_AdminDataMappedCorrectly_AdminModels(AdminModel domainAdmin)
    {
        var appAdmin = _mapper.Map<AdminDTO>(domainAdmin);

        Assert.That.AreEqual(domainAdmin, appAdmin);
    }

    [TestMethod]
    [DynamicData(nameof(AdminProfileTestDataProvider.DomainAdmin), typeof(AdminProfileTestDataProvider))]
    public void Map_AdminDataAreNotLostAfterMapping_AdminModels(AdminModel domainAdmin)
    {
        var mappedomainAdmin = _mapper
            .Map<AdminModel>(
                _mapper
                    .Map<AdminDTO>(domainAdmin));

        Assert.AreEqual(domainAdmin, mappedomainAdmin);
    }
}
