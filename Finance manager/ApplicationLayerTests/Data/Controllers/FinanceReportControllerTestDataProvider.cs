using DomainLayer.Services.Finances;
using DomainLayer.Services.Wallets;
using FakeItEasy;

namespace ApplicationLayerTests.Data.Controllers;

public static class FinanceReportControllerTestDataProvider
{
    public static IEnumerable<object[]> ConstructorArgumentsAreNullThrowsArgumentNullExceptionTestData { get; } = new List<object[]>
    {
        new object[]{ A.Dummy<IFinanceReportCreator>(), null },
        new object[]{ null, A.Dummy<IWalletService>() },
        new object[]{ null, null },
    };
}
