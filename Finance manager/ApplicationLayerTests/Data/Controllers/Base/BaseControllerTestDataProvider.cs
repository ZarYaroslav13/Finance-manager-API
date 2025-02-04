using Server.Controllers;
using AutoMapper;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace ApplicationLayerTests.Data.Controllers.Base;

public static class BaseControllerTestDataProvider
{
    public static IEnumerable<object[]> ConstructorArgumentIsEqualNullArgumentNullExceptionTestData { get; } = new List<object[]>
    {
        new object[] { null, A.Fake<ILogger<AccountController>>()},
        new object[] { A.Fake<IMapper>(), null},
        new object[] { null , null}
    };
}
