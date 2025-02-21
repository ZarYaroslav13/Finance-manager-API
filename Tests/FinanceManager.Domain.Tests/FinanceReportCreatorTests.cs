using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services.Finances;
using FinanceManager.Domain.Tests.Data;
using FakeItEasy;

namespace FinanceManager.Domain.Tests;

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
    public void CreateFinanceReportAsync_ArgumentValuesAreNull_ThrowsException()
    {
        Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _creator.CreateFinanceReportAsync(null, new DateTime(), new DateTime()));
    }

    [TestMethod]
    public void CreateFinanceReportAsync_StartDateIsLaterthenEndDate_ArgumentOutOfRangeException()
    {
        Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(()
                => _creator.CreateFinanceReportAsync(A.Dummy<WalletModel>(), DateTime.MaxValue, DateTime.MinValue));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportCreatorTestsDataProvider.CreateFinanceReportTestData), typeof(FinanceReportCreatorTestsDataProvider))]
    public async Task CreateFinanceReportAsync_GeneratedAndExpectedReportsAreEqual_FinanceReport(WalletModel wallet, FinanceReportModel expected)
    {
        A.CallTo(() => _service.GetAllFinanceOperationOfWalletAsync(wallet.Id, expected.Period.StartDate, expected.Period.EndDate)).Returns(expected.Operations);

        var result = await _creator.CreateFinanceReportAsync(wallet, expected.Period.StartDate, expected.Period.EndDate);
        result.Operations = result.Operations.OrderBy(fo => fo.Id).ToList();

        A.CallTo(() => _service.GetAllFinanceOperationOfWalletAsync(wallet.Id, expected.Period.StartDate, expected.Period.EndDate)).MustHaveHappenedOnceExactly();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportCreatorTestsDataProvider.CreateDailyFinanceReportTestData), typeof(FinanceReportCreatorTestsDataProvider))]
    public async Task CreateDailyFinanceReport_GeneratedDailyReportISAsExpected_FinanceReport(WalletModel wallet, FinanceReportModel expected)
    {
        A.CallTo(() => _service.GetAllFinanceOperationOfWalletAsync(wallet.Id, expected.Period.StartDate, expected.Period.StartDate)).Returns(expected.Operations);

        var result = await _creator.CreateFinanceReportAsync(wallet, expected.Period.StartDate);
        result.Operations = result.Operations.OrderBy(fo => fo.Id).ToList();

        A.CallTo(() => _service.GetAllFinanceOperationOfWalletAsync(wallet.Id, expected.Period.StartDate, expected.Period.StartDate)).MustHaveHappenedOnceExactly();

        Assert.AreEqual(expected, result);
    }
}