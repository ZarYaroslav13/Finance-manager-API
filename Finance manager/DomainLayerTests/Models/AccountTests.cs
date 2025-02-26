﻿using DomainLayer.Models;
using DomainLayerTests.Data.Models;

namespace DomainLayerTests.Models;

[TestClass]
public class AccountTests
{
    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.MethodEqualsResultTrueData), typeof(AccountDataProvider))]
    public void Equals_AccountModelsAreEqual_True(AccountModel ac1, AccountModel ac2)
    {
        Assert.AreEqual(ac1, ac2);
    }

    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.MethodEqualsResultFalseData), typeof(AccountDataProvider))]
    public void Equals_AccountModelsAreNotEqual_False(AccountModel ac1, object ac2)
    {
        Assert.AreNotEqual(ac1, ac2);
    }

    [TestMethod]
    [DynamicData(nameof(AccountDataProvider.MethodEqualsResultTrueData), typeof(AccountDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(AccountModel account1, AccountModel account2)
    {
        var hash1 = account1.GetHashCode();
        var hash2 = account2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
