using DomainLayer.Models;
using DomainLayerTests.Data;

namespace DomainLayerTests.Models;

[TestClass]
public class WalletTests
{
    [TestMethod]
    [DynamicData(nameof(WalletDataProvider.EqualsData), typeof(WalletDataProvider))]
    public void Wallet_Equals_Bool(WalletModel w1, object w2, bool expected)
    {
        bool result = w1.Equals(w2);

        Assert.AreEqual(expected, result);
    }
}
