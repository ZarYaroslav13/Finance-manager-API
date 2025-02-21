using FinanceManager.Domain.Models;
using FinanceManager.Domain.Tests.Data.Models;
using FakeItEasy;

namespace FinanceManager.Domain.Tests.Models;

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
    public void Equals_FinanceReportsAreEqual_True(FinanceReportModel fr1, FinanceReportModel fr2)
    {
        Assert.AreEqual(fr1, fr1);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportTestsDataProvider.MethodEqualsResultFalseData), typeof(FinanceReportTestsDataProvider))]
    public void Equals_FinanceReportsAreNotEqual_False(FinanceReportModel fr1, object fr2)
    {
        Assert.AreNotEqual(fr1, fr2);
    }


    [TestMethod]
    [DynamicData(nameof(FinanceReportTestsDataProvider.MethodEqualsResultTrueData), typeof(FinanceReportTestsDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(FinanceReportModel fr1, FinanceReportModel fr2)
    {
        var hash1 = fr1.GetHashCode();
        var hash2 = fr2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
