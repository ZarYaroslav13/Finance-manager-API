using ApplicationLayer.Models;
using ApplicationLayerTests.Data.Models;

namespace ApplicationLayerTests.Models;

[TestClass]
public class FinanceOperationTypeDTOTests
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationTypeDTOTestDataProvider.MethodEqualsResultTrueData), typeof(FinanceOperationTypeDTOTestDataProvider))]
    public void Equals_FinanceOperationTypeDTOsAreEqual_True(FinanceOperationTypeDTO fot1, FinanceOperationTypeDTO fot2)
    {
        Assert.AreEqual(fot1, fot2);
    }

    [TestMethod]
    [DynamicData(nameof(FinanceOperationTypeDTOTestDataProvider.MethodEqualsResultFalseData), typeof(FinanceOperationTypeDTOTestDataProvider))]
    public void Equals_FinanceOperationTypeDTOsAreNotEqual_False(FinanceOperationTypeDTO fot1, object fot2)
    {
        Assert.AreNotEqual(fot1, fot2);
    }
}
