using DomainLayer.Models;
using DomainLayerTests.Data.Models;

namespace DomainLayerTests.Models;

[TestClass]
public class FinanceOperationTests
{
    [TestClass]
    public class FinanceOperationTest
    {
        [TestMethod]
        [DynamicData(nameof(FinanceOperationDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationDataProvider))]
        public void Equals_FinanceOperationModelsAreEqual_True(FinanceOperationModel fo1, object fo2)
        {
            Assert.AreEqual(fo1, fo2);
        }

        [TestMethod]
        [DynamicData(nameof(FinanceOperationDataProvider.MethodEqualsResultFalseData), typeof(FinanceOperationDataProvider))]
        public void Equals_FinanceOperationModelsAreEqual_False(FinanceOperationModel fo1, object fo2)
        {
            Assert.AreNotEqual(fo1, fo2);
        }
    }
}
