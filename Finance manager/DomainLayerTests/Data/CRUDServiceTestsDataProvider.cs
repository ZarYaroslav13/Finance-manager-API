using AutoMapper;
using DataLayer.UnitOfWork;
using FakeItEasy;

namespace DomainLayerTests.Data;

public class CRUDServiceTestsDataProvider
{
    public static IEnumerable<object[]> ConstructorExceptions { get; } = new List<object[]>
    {
        new object[] { A.Dummy<IUnitOfWork>(), null},
        new object[] { null, A.Fake<IMapper>() },
        new object[] { null, null }
    };
}
