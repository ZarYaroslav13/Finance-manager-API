﻿using DataLayer.Models;
using DataLayerTests.Data.Models;

namespace DataLayerTests.Models;

[TestClass]
public class AccountTests
{
    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.MethodEqualsResultTrueData), typeof(AccountDataProvider))]
    public void Equals_AccountsAreEqual_True(Account ac1, object ac2)
    {
        Assert.AreEqual(ac1, ac2);
    }

    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.MethodEqualsResultFalseData), typeof(AccountDataProvider))]
    public void Equals_AccountsAreEqual_False(Account ac1, object ac2)
    {
        Assert.AreNotEqual(ac1, ac2);
    }
}
