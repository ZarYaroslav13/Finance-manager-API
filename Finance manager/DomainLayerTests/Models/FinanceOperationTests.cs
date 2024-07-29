using DomainLayer.Models;
using DomainLayerTests.Data;

namespace DomainLayerTests.Models;

[TestClass]
public class FinanceOperationTests
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationDataProvider.EqualsData), typeof(FinanceOperationDataProvider))]
    public void FinanceOperation_Equals_Bool(FinanceOperationModel fot1, object fot2, bool expected)
    {
        bool result = fot1.Equals(fot2);

        Assert.AreEqual(expected, result);
    }
}
