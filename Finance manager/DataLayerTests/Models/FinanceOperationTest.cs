using DataLayer.Models;
using DataLayerTests.TestData;

namespace DataLayerTests.Models;

[TestClass]
public class FinanceOperationTest
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationDataProvider.EqualsData), typeof(FinanceOperationDataProvider))]
    public void FinanceOperation_Equals_Bool(FinanceOperation fo1, object fo2, bool expected)
    {
        bool result = fo1.Equals(fo2);

        Assert.AreEqual(expected, result);
    }
}
