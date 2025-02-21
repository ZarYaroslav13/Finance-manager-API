using FinanceManager.Domain.Models;
using FinanceManager.Domain.Tests.Data.Models;

namespace FinanceManager.Domain.Tests.Models
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

        [TestMethod]
        [DynamicData(nameof(FinanceOperationTypeDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationTypeDataProvider))]
        public void GetHashCode_SameValues_ReturnsSameHashCode(FinanceOperationTypeModel type1, FinanceOperationTypeModel type2)
        {
            var hash1 = type1.GetHashCode();
            var hash2 = type2.GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }
    }
}
