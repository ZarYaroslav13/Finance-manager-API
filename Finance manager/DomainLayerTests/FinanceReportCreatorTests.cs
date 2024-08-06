using DomainLayer;
using DomainLayer.Models;
using DomainLayer.Services.FinanceOperations;
using DomainLayerTests.Data;
using FakeItEasy;

namespace DomainLayerTests;

[TestClass]
public class FinanceReportCreatorTests
{
    private readonly IFinanceService _service;
    private readonly FinanceReportCreator _creator;

    public FinanceReportCreatorTests()
    {
        _service = A.Fake<IFinanceService>();

        _creator = new(_service);
    }

    [TestMethod]
    public void Constructor_ContructorArgumentValuesAreNull_ThrowsException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new FinanceReportCreator(null));
    }

    [TestMethod]
    public void CreateFinanceReport_ArgumentValuesAreNull_ThrowsException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _creator.CreateFinanceReport(null, new DateTime(), new DateTime()));
    }

    [TestMethod]
    public void CreateFinanceReport_StartDateIsLaterthenEndDate_ArgumentOutOfRangeException()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(()
                => _creator.CreateFinanceReport(A.Dummy<WalletModel>(), DateTime.MaxValue, DateTime.MinValue));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportCreatorTestsDataProvider.CreateFinanceReportTestData), typeof(FinanceReportCreatorTestsDataProvider))]
    public void CreateFinanceReport_GeneratedAndExpectedReportsAreEqual_FinanceReport(WalletModel wallet, FinanceReportModel expected)
    {
        A.CallTo(() => _service.GetAllFinanceOperationOfWallet(wallet.Id)).Returns(expected.Operations);

        var result = _creator.CreateFinanceReport(wallet, expected.Period.StartDate, expected.Period.EndDate);
        result.Operations = result.Operations.OrderBy(fo => fo.Id).ToList();

        A.CallTo(() => _service.GetAllFinanceOperationOfWallet(wallet.Id)).MustHaveHappenedOnceExactly();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportCreatorTestsDataProvider.CreateDailyFinanceReportTestData), typeof(FinanceReportCreatorTestsDataProvider))]
    public void CreateDailyFinanceReport_GeneratedDailyReportISAsExpected_FinanceReport(WalletModel wallet, FinanceReportModel expected)
    {
        A.CallTo(() => _service.GetAllFinanceOperationOfWallet(wallet.Id)).Returns(expected.Operations);

        var result = _creator.CreateFinanceReport(wallet, expected.Period.StartDate);
        result.Operations = result.Operations.OrderBy(fo => fo.Id).ToList();

        A.CallTo(() => _service.GetAllFinanceOperationOfWallet(wallet.Id)).MustHaveHappenedOnceExactly();

        Assert.AreEqual(expected, result);
    }
}