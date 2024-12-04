using ApplicationLayer.Models;
using ApplicationLayerTests.Data.Models;
using DomainLayer.Models;
using FakeItEasy;

namespace ApplicationLayerTests.Models;

[TestClass]
public class FinanceReportDTOTests
{
    [TestMethod]
    [DynamicData(nameof(FinanceReportDTOTestsDataProvider.ConstructorThrowExceptionTestData), typeof(FinanceReportDTOTestsDataProvider))]
    public void Constructor_ArgumentsAreNull_ThorwsException(string walletName, List<FinanceOperationDTO> operations)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new FinanceReportDTO(A.Dummy<int>(), walletName, A.Dummy<int>(), A.Dummy<int>(), operations, A.Dummy<Period>()));
    }

    [TestMethod]
    public void Constructor_ArgumentsArePassedCorrectly_FinanceReport()
    {
        const int walletId = 1;
        const string walletName = "Name";
        const int totalIncome = 100;
        const int totalExpense = 50;
        Period period = new Period() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };

        var result = new FinanceReportDTO(walletId, walletName, totalIncome, totalExpense, FinanceReportDTOTestsDataProvider.FinanceOperations, period);

        Assert.IsNotNull(result);
        Assert.AreEqual(walletId, result.WalletId);
        Assert.AreEqual(walletName, result.WalletName);
        Assert.AreEqual(totalIncome, result.TotalIncome);
        Assert.AreEqual(totalExpense, result.TotalExpense);
        Assert.AreEqual(FinanceReportDTOTestsDataProvider.FinanceOperations, result.Operations);
        Assert.AreEqual(period, result.Period);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportDTOTestsDataProvider.MethodEqualsResultTrueData), typeof(FinanceReportDTOTestsDataProvider))]
    public void Equals_FinanceReportsAreEqual_True(FinanceReportDTO fr1, FinanceReportDTO fr2)
    {
        Assert.AreEqual(fr1, fr1);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportDTOTestsDataProvider.MethodEqualsResultFalseData), typeof(FinanceReportDTOTestsDataProvider))]
    public void Equals_FinanceReportsAreNotEqual_False(FinanceReportDTO fr1, object fr2)
    {
        Assert.AreNotEqual(fr1, fr2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportDTOTestsDataProvider.MethodEqualsResultTrueData), typeof(FinanceReportDTOTestsDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(FinanceReportDTO fr1, FinanceReportDTO fr2)
    {
        var hash1 = fr1.GetHashCode();
        var hash2 = fr2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
