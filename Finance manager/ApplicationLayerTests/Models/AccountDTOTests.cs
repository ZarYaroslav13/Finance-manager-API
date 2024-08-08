using ApplicationLayerTests.Data.Models;
using ApplicationLayer.Models;

namespace ApplicationLayerTests.Models;

[TestClass]
public class AccountDTOTests
{
    [TestMethod]
    [DynamicData(nameof(AccountDTOTestDataProvider.MethodEqualsResultTrueData), typeof(AccountDTOTestDataProvider))]
    public void Equals_AccountDTOsAreEqual_True(AccountDTO ac1, AccountDTO ac2)
    {
        Assert.AreEqual(ac1, ac2);
    }

    [TestMethod]
    [DynamicData(nameof(AccountDTOTestDataProvider.MethodEqualsResultFalseData), typeof(AccountDTOTestDataProvider))]
    public void Equals_AccountDTOsAreNotEqual_False(AccountDTO ac1, object ac2)
    {
        Assert.AreNotEqual(ac1, ac2);
    }
}
