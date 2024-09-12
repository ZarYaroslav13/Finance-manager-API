using ApplicationLayer.Models;
using ApplicationLayer.Models.Base;
using ApplicationLayerTests.Data.Models;

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

    [TestMethod]
    [DynamicData(nameof(WalletDTOTestDataProvider.MethodEqualsResultTrueData), typeof(WalletDTOTestDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(WalletDTO w1, WalletDTO w2)
    {
        var hash1 = w1.GetHashCode();
        var hash2 = w2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
