using FinanceManager.Domain.Models;
using FinanceManager.Domain.Tests.Data.Models;

namespace FinanceManager.Domain.Tests.Models;

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


    [TestMethod]
    [DynamicData(nameof(WalletDataProvider.MethodEqualsResultTrueData), typeof(WalletDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(WalletModel w1, WalletModel w2)
    {
        var hash1 = w1.GetHashCode();
        var hash2 = w2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
