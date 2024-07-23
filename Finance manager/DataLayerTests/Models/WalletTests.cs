using DataLayer.Models;
using DataLayerTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests.Models;

[TestClass]
public class WalletTests
{
    [TestMethod]
    [DynamicData(nameof(WalletDataProvider.EqualsData), typeof(WalletDataProvider))]
    public void Wallet_Equals_Bool(Wallet w1, object w2, bool expected)
    {
        bool result = w1.Equals(w2);

        Assert.AreEqual(expected, result);
    }
}
