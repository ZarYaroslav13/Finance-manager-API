using FinanceManager.Domain.Models;
using FinanceManager.Domain.Tests.Data.Models;

namespace FinanceManager.Domain.Tests.Models;

[TestClass]
public class PeriodTests
{
    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultTrueData), typeof(PeriodDataProvider))]
    public void Equals_PeriodModelsAreNotEqual_True(Period p1, Period p2)
    {
        Assert.AreEqual(p1, p2);
    }

    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultFalseData), typeof(PeriodDataProvider))]
    public void Equals_PeriodModelsAreEqual_False(Period p1, object p2)
    {
        Assert.AreNotEqual(p1, p2);
    }

    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultTrueData), typeof(PeriodDataProvider))]
    public void OperatorEquals_PeriodModelsAreEqual_True(Period p1, Period p2)
    {
        Assert.IsTrue(p1 == p2);
    }

    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultFalseData), typeof(PeriodDataProvider))]
    public void OperatorEquals_PeriodModelsAreNotEqual_False(Period p1, Period p2)
    {
        Assert.IsFalse(p1 == p2);
    }

    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultTrueData), typeof(PeriodDataProvider))]
    public void OperatorNotEquals_PeriodModelsAreEqual_False(Period p1, Period p2)
    {
        Assert.IsFalse(p1 != p2);
    }

    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultFalseData), typeof(PeriodDataProvider))]
    public void OperatorNotEquals_PeriodModelsAreNotEqual_True(Period p1, Period p2)
    {
        Assert.IsTrue(p1 != p2);
    }

    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultTrueData), typeof(PeriodDataProvider))]
    public void GetHashCode_PeriodModelsAreEqual_SameHashCode(Period p1, Period p2)
    {
        Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
    }

    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultFalseData), typeof(PeriodDataProvider))]
    public void GetHashCode_PeriodModelsAreNotEqual_DifferentHashCodes(Period p1, Period p2)
    {
        Assert.AreNotEqual(p1.GetHashCode(), p2.GetHashCode());
    }
}
