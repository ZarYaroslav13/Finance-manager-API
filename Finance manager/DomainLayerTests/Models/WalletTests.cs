using DomainLayer.Models;
using DomainLayerTests.Data.Models;

namespace DomainLayerTests.Models;

[TestClass]
public class WalletTests
{
    [TestMethod]
    [DynamicData(nameof(WalletDataProvider.MethodEqualsResultTrueData), typeof(WalletDataProvider))]
    public void Equals_WalletModelsAreEqual_True(WalletModel w1, object w2)
    {
        Assert.AreEqual(w1, w2);
    }

    [TestMethod]
    [DynamicData(nameof(WalletDataProvider.MethodEqualsResultFalseData), typeof(WalletDataProvider))]
    public void Equals_WalletModelsAreEqual_False(WalletModel w1, object w2)
    {
        Assert.AreNotEqual(w1, w2);
    }
}
