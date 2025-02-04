using Infrastructure.Models;
using InfractructureTests.Data.Models;

namespace InfractructureTests.Models;

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

    [TestMethod]
    [DynamicData(nameof(WalletDataProvider.MethodEqualsResultTrueData), typeof(WalletDataProvider))]
    public void GetHashCode_SameProperties_ReturnsSameHashCode(Wallet w1, Wallet w2)
    {
        var hashCode1 = w1.GetHashCode();
        var hashCode2 = w2.GetHashCode();

        Assert.AreEqual(hashCode1, hashCode2);
    }
}
