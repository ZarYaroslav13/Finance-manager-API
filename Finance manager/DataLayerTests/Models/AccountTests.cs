using DataLayer.Models;
using DataLayerTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests.Models;

[TestClass]
public class AccountTests
{
    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.EqualsData), typeof(AccountDataProvider))]
    public void Account_Equals_Bool(Account ac1, object ac2, bool expected)
    {
        bool result = ac1.Equals(ac2);

        Assert.AreEqual(expected, result);
    }
}
