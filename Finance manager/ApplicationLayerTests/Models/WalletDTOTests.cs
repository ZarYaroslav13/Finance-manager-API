using ApplicationLayerTests.Data.Models;
using ApplicationLayer.Models;

namespace ApplicationLayerTests.Models;

[TestClass]
public class WalletDTOTests
{
    [TestMethod]
    [DynamicData(nameof(WalletDTOTestDataProvider.MethodEqualsResultTrueData), typeof(WalletDTOTestDataProvider))]
    public void Equals_WalletDTOsAreEqual_True(WalletDTO w1, object w2)
    {
        Assert.AreEqual(w1, w2);
    }

    [TestMethod]
    [DynamicData(nameof(WalletDTOTestDataProvider.MethodEqualsResultFalseData), typeof(WalletDTOTestDataProvider))]
    public void Equals_WalletDTOsAreEqual_False(WalletDTO w1, object w2)
    {
        Assert.AreNotEqual(w1, w2);
    }
}
