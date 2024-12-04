using DataLayer.Models;
using DataLayerTests.Data.Models;

namespace DataLayerTests.Models;

[TestClass]
public class AccountTests
{
    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.MethodEqualsResultTrueData), typeof(AccountDataProvider))]
    public void Equals_AccountsAreEqual_True(Account ac1, Account ac2)
    {
        Assert.AreEqual(ac1, ac2);
    }

    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.MethodEqualsResultFalseData), typeof(AccountDataProvider))]
    public void Equals_AccountsAreNotEqual_False(Account ac1, object ac2)
    {
        Assert.AreNotEqual(ac1, ac2);
    }

    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.MethodEqualsResultTrueData), typeof(AccountDataProvider))]
    public void GetHashCode_SameProperties_ReturnsSameHashCode(Account ac1, Account ac2)
    {
        var hashCode1 = ac1.GetHashCode();
        var hashCode2 = ac2.GetHashCode();

        Assert.AreEqual(hashCode1, hashCode2);
    }
}
