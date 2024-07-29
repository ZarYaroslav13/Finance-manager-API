using DomainLayer.Models;
using DomainLayerTests.Data;

namespace DomainLayerTests.Models
{
    [TestClass]
    public class FinanceOperationTypeTest
    {
        [TestMethod]
        [DynamicData(nameof(FinanceOperationTypeDataProvider.EqualsData), typeof(FinanceOperationTypeDataProvider))]
        public void FinanceOperationType_Equals_Bool(FinanceOperationTypeModel fot1, object fot2, bool expected)
        {
            bool result = fot1.Equals(fot2);

            Assert.AreEqual(expected, result);
        }
    }
}
