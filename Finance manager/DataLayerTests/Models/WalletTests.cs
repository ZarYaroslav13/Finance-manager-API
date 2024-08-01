using DataLayer.Models;
using DataLayerTests.Data.Models;

namespace DataLayerTests.Models;

[TestClass]
public class WalletTests
{
    [TestMethod]
    [DynamicData(nameof(WalletDataProvider.MethodEqualsResultTrueData), typeof(WalletDataProvider))]
    public void Equals_WalletsAreEqual_True(Wallet w1, Wallet w2)
    {
        Assert.AreEqual(w1, w2);
    }

    [TestMethod]
    [DynamicData(nameof(WalletDataProvider.MethodEqualsResultFalseData), typeof(WalletDataProvider))]
    public void Equals_WalletsAreNotEqual_False(Wallet w1, object w2)
    {
        Assert.AreNotEqual(w1, w2);
    }
}
