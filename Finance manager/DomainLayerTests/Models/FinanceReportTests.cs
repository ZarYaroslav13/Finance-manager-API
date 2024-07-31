using AutoMapper;
using DomainLayer.Mapper.Profiles;
using DomainLayer.Models;
using DomainLayerTests.Data;
using DomainLayerTests.Data.Models;
using FakeItEasy;

namespace DomainLayerTests.Models;

[TestClass]
public class FinanceReportTests
{
    [TestMethod]
    public void Constructor_ArgumentsAreNull_ThorwsException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new FinanceReportModel(A.Dummy<int>(), null, A.Dummy<Period>()));
    }

    [TestMethod]
    public void Constructor_ArgumentsArePassedCorrectly_FinanceReport()
    {
        const int walletId = 1;
        const string walletName = "Name";
        Period period = new Period() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };

        var result = new FinanceReportModel(walletId, walletName, period);

        Assert.IsNotNull(result);
        Assert.AreEqual(walletId, result.WalletId);
        Assert.AreEqual(walletName, result.WalletName);
        Assert.AreEqual(period, result.Period);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportTestsDataProvider.FinanceOperationsAndTheirIncomeExpense), typeof(FinanceReportTestsDataProvider))]
    public void AssignOperations_AssignFinaceOperationsIsCorrectly_FinanceOperationsList(
        List<FinanceOperationModel> financeOperations,
        int totalIncome,
        int totalExpense)
    {
        const int walletId = 1;
        const string walletName = "Name";
        Period period = new Period() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };

        var report = new FinanceReportModel(walletId, walletName, period);

        report.Operations = financeOperations;

        Assert.AreEqual(totalIncome, report.TotalIncome);
        Assert.AreEqual(totalExpense, report.TotalExpense);
        CollectionAssert.AreEqual(financeOperations, report.Operations);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportTestsDataProvider.MethodEqualsResultTrueData), typeof(FinanceReportTestsDataProvider))]
    public void Equals_FinanceReportsAreEqual_True(FinanceReportModel fr1, object fr2)
    {
        Assert.AreEqual(fr1, fr1);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportTestsDataProvider.MethodEqualsResultFalseData), typeof(FinanceReportTestsDataProvider))]
    public void Equals_FinanceReportsAreEqual_False(FinanceReportModel fr1, object fr2)
    {
        Assert.AreNotEqual(fr1, fr2);
    }
}
