using DataLayer.Models;
using DataLayerTests.TestData;

namespace DataLayerTests.Models;

[TestClass]
public class FinanceOperationTypeTest
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationTypeDataProvider.EqualsData), typeof(FinanceOperationTypeDataProvider))]
    public void FinanceOperationType_Equals_Bool(FinanceOperationType fot1, object fot2, bool expected)
    {
        bool result = fot1.Equals(fot2);

        Assert.AreEqual(expected, result);
    }
}
