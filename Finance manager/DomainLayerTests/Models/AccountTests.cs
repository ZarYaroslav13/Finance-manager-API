using DomainLayer.Models;
using DomainLayerTests.Data;

namespace DomainLayerTests.Models;

[TestClass]
public class AccountTests
{
    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.EqualsData), typeof(AccountDataProvider))]
    public void Account_Equals_Bool(AccountModel ac1, object ac2, bool expected)
    {
        bool result = ac1.Equals(ac2);

        Assert.AreEqual(expected, result);
    }
}
