using FinanceManager.ApiService.Models;
using ApplicationLayerTests.Data.Models;

namespace ApplicationLayerTests.Models;

[TestClass]
public class FinanceOperationDTOTests
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationDTOTestDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationDTOTestDataProvider))]
    public void Equals_FinanceOperationDTOsAreEqual_True(FinanceOperationDTO fo1, FinanceOperationDTO fo2)
    {
        Assert.AreEqual(fo1, fo2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationDTOTestDataProvider.MethodEqualsResultFalseData), typeof(FinanceOperationDTOTestDataProvider))]
    public void Equals_FinanceOperationDTOsAreNotEqual_False(FinanceOperationDTO fo1, object fo2)
    {
        Assert.AreNotEqual(fo1, fo2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationDTOTestDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationDTOTestDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(FinanceOperationDTO fo1, FinanceOperationDTO fo2)
    {
        var hash1 = fo1.GetHashCode();
        var hash2 = fo2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
