using FinanceManager.Domain.Models;
using FinanceManager.Domain.Tests.Data.Models;

namespace FinanceManager.Domain.Tests.Models;

[TestClass]
public class FinanceOperationTests
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationDataProvider))]
    public void Equals_FinanceOperationModelsAreEqual_True(FinanceOperationModel fo1, FinanceOperationModel fo2)
    {
        Assert.AreEqual(fo1, fo2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationDataProvider.MethodEqualsResultFalseData), typeof(FinanceOperationDataProvider))]
    public void Equals_FinanceOperationModelsAreNotEqual_False(FinanceOperationModel fo1, object fo2)
    {
        Assert.AreNotEqual(fo1, fo2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(FinanceOperationModel operation1, FinanceOperationModel operation2)
    {
        var hash1 = operation1.GetHashCode();
        var hash2 = operation2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
