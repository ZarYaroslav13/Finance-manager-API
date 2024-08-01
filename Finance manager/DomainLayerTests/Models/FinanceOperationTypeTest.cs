using DomainLayer.Models;
using DomainLayerTests.Data.Models;

namespace DomainLayerTests.Models
{
    [TestClass]
    public class FinanceOperationTypeTest
    {
        [TestMethod]
        [DynamicData(nameof(FinanceOperationTypeDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationTypeDataProvider))]
        public void Equals_FinanceOperationTypeModelsAreEqual_True(FinanceOperationTypeModel fot1, FinanceOperationTypeModel fot2)
        {
            Assert.AreEqual(fot1, fot2);
        }

        [TestMethod]
        [DynamicData(nameof(FinanceOperationTypeDataProvider.MethodEqualsResultFalseData), typeof(FinanceOperationTypeDataProvider))]
        public void Equals_FinanceOperationTypeModelsAreNotEqual_False(FinanceOperationTypeModel fot1, object fot2)
        {
            Assert.AreNotEqual(fot1, fot2);
        }
    }
}
