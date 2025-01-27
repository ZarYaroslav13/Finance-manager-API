using Infrastructure.Models;
using DataLayerTests.Data.Models;

namespace DataLayerTests.Models;

[TestClass]
public class FinanceOperationTypeTest
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationTypeDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationTypeDataProvider))]
    public void Equals_FinanceOperationTypeAreEqual_True(FinanceOperationType fot1, FinanceOperationType fot2)
    {
        Assert.AreEqual(fot1, fot2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationTypeDataProvider.MethodEqualsResultFalseData), typeof(FinanceOperationTypeDataProvider))]
    public void Equals_FinanceOperationTypeAreNotEqual_False(FinanceOperationType fot1, object fot2)
    {
        Assert.AreNotEqual(fot1, fot2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationTypeDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationTypeDataProvider))]
    public void GetHashCode_SameProperties_ReturnsSameHashCode(FinanceOperationType fot1, FinanceOperationType fot2)
    {
        var hashCode1 = fot1.GetHashCode();
        var hashCode2 = fot2.GetHashCode();

        Assert.AreEqual(hashCode1, hashCode2);
    }
}
