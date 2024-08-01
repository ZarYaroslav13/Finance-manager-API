using DataLayer.Models;
using DataLayerTests.Data.Models;

namespace DataLayerTests.Models;

[TestClass]
public class FinanceOperationTest
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationDataProvider))]
    public void Equals_FinanceOperationAreEqual_True(FinanceOperation fo1, FinanceOperation fo2)
    {
        Assert.AreEqual(fo1, fo2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationDataProvider.MethodEqualsResultFalseData), typeof(FinanceOperationDataProvider))]
    public void Equals_FinanceOperationAreNotEqual_False(FinanceOperation fo1, object fo2)
    {
        Assert.AreNotEqual(fo1, fo2);
    }
}
