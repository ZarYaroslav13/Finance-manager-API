using AutoMapper;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using DomainLayer.Mapper.Profiles;
using DomainLayer.Models;
using FakeItEasy;

namespace DomainLayerTests.Data;

public static class CRUDServiceTestsDataProvider
{
    private static IMapper _mapper = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<AccountProfile>();
    }).CreateMapper();
    private static List<Account> _dbAccounts = DbEntitiesTestDataProvider.Accounts;

    public static IEnumerable<object[]> ConstructorExceptions { get; } = new List<object[]>
    {
        new object[] { A.Dummy<IUnitOfWork>(), null},
        new object[] { null, A.Fake<IMapper>() },
        new object[] { null, null }
    };

    public static IEnumerable<object[]> DataForFindTests { get; } = new List<object[]>
    {
        new object[]
        {
            _dbAccounts, _dbAccounts
                .Select(_mapper.Map<AccountModel>)
                .LastOrDefault()
        }
    };
}
