using AutoMapper;
using DataLayer.UnitOfWork;
using FakeItEasy;

namespace DomainLayerTests.Data;

public class FinanceReportCreatorTestsDataProvider
{
    public static IEnumerable<object[]> ConstructorException { get; } = new List<object[]>()
    {
        new object[] { A.Fake<IUnitOfWork>(), null },
        new object[] { null, A.Fake<IMapper>() },
        new object[] { null, null }
    };
}
